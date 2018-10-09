using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace SmartH2O_Alarm
{
    public partial class AlarmApp : Form
    {
        MqttClient m_cClient = new MqttClient(Properties.Settings.Default.BrokerAddress);
        string[] m_strTopicsInfo = { "sensors" };

        XmlDocument triggerRulesFile = new XmlDocument();
        public string validationMessage { get; set; }
        private bool isXMLValid = true;

        public AlarmApp()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBoxSensorsReadings.BeginInvoke((MethodInvoker)delegate { textBoxSensorsReadings.Text = "No readings yet"; });
            textBoxTriggerRules.BeginInvoke((MethodInvoker)delegate { textBoxTriggerRules.Text = "No rules found yet"; });

            loadTriggerRules(@"trigger-rules.xml");


            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                MessageBox.Show("Error connecting to message broker...");
                return;
            }

            //Specify events we are interested on
            //New message arrived
            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            //Subscribe to topics
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);
        }

        void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string receivedText = Encoding.UTF8.GetString(e.Message);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(receivedText);

            string sensorParameter = getSensorParameter(doc);

            XmlNodeList parameterTriggers = getParameterTriggers(sensorParameter);

            validateTriggers(doc, parameterTriggers);

            string formattedXML = formatXML(doc);

            textBoxSensorsReadings.BeginInvoke((MethodInvoker)delegate { textBoxSensorsReadings.Text = formattedXML; });
        }

        private string formatXML(XmlDocument doc)
        {
            try
            {
                string formattedXML = System.Xml.Linq.XElement.Parse(doc.OuterXml).ToString();
                return formattedXML;
            }
            catch (XmlException e)
            {
                MessageBox.Show("Error formatting received XML: ", e.Message);
            }
            return "";
        }

        private void loadTriggerRules(string filePath)
        {

            if (ValidateXml())
            {
                try
                {
                    triggerRulesFile.Load(filePath);
                    textBoxTriggerRules.Text = formatXML(triggerRulesFile);
                    textBoxTriggerRules.BeginInvoke((MethodInvoker)delegate { textBoxTriggerRules.Text = formatXML(triggerRulesFile); });
                    XmlNodeList nos = triggerRulesFile.SelectNodes("//message");
                    string message = "";
                    foreach (XmlNode messageNode in nos)
                    {
                        message += messageNode.InnerText + System.Environment.NewLine;
                    }
                    textBoxSensorsReadings.Text = "";
                    textBoxSensorsReadings.Text = message;
                }
                catch (XmlException ex)
                {
                    MessageBox.Show("Error reading trigger-rules.xml: " + ex.Message);
                }
            } else
            {
                MessageBox.Show("The triggers xml file is invalid!");
            }
        }

        private string getSensorParameter(XmlDocument sensorData)
        {
            return sensorData.SelectSingleNode("//type").InnerText;
        }

        private XmlNodeList getParameterTriggers(string parameter)
        {
            string[] conditions = { "equal", "less-than", "greater-than", "between" };
            XmlNodeList triggers = triggerRulesFile.SelectNodes("/trigger-rules/triggers[@type='" + parameter + "']/trigger[@status='enabled']");
            this.BeginInvoke( (MethodInvoker) delegate {
                textBoxActiveTriggers.Text = parameter + ": " + triggers.Count;


            });
            return triggers;
        }

        private void validateTriggers(XmlDocument sensorReading, XmlNodeList parameterTriggers)
        {
            float sensorValue = float.Parse(sensorReading.SelectSingleNode("/sensor/value").InnerText, CultureInfo.InvariantCulture.NumberFormat);
            foreach (XmlNode trigger in parameterTriggers)
            {
                XmlNode condition = trigger.ChildNodes[0];
                textBoxCondition.BeginInvoke((MethodInvoker)delegate { textBoxCondition.Text += condition.Name + " "; });
                if (condition.Name.Equals("equal-to", StringComparison.Ordinal))
                {
                    float triggerValue = float.Parse(trigger.SelectSingleNode("//equal-to/value").InnerText, CultureInfo.InvariantCulture);
                    if (sensorValue == triggerValue)
                    {
                        XmlDocument alarmXml = createAlarmXml(sensorReading, trigger);
                        textBoxAlarmsTriggered.BeginInvoke((MethodInvoker)delegate { textBoxAlarmsTriggered.Text = formatXML(alarmXml); });
                        m_cClient.Publish("alarms", Encoding.UTF8.GetBytes(alarmXml.OuterXml.ToString()));
                    }
                    continue;
                }
                if(condition.Name.Equals("less-than", StringComparison.Ordinal))
                {
                    float triggerValue = float.Parse(trigger.SelectSingleNode("//less-than/value").InnerText, CultureInfo.InvariantCulture);
                    if (sensorValue < triggerValue)
                    {
                        XmlDocument alarmXml = createAlarmXml(sensorReading, trigger);
                        textBoxAlarmsTriggered.BeginInvoke((MethodInvoker)delegate { textBoxAlarmsTriggered.Text = formatXML(alarmXml); });
                        m_cClient.Publish("alarms", Encoding.UTF8.GetBytes(alarmXml.OuterXml.ToString()));
                    }
                    continue;
                }
                if (condition.Name.Equals("greater-than", StringComparison.Ordinal))
                {
                    float triggerValue =  float.Parse(trigger.SelectSingleNode("//greater-than/value").InnerText, CultureInfo.InvariantCulture);
                    
                    if(sensorValue > triggerValue)
                    {
                        XmlDocument alarmXml = createAlarmXml(sensorReading, trigger);
                        textBoxAlarmsTriggered.BeginInvoke((MethodInvoker)delegate { textBoxAlarmsTriggered.Text = formatXML(alarmXml); });
                        m_cClient.Publish("alarms", Encoding.UTF8.GetBytes(alarmXml.OuterXml.ToString()));
                    }
                    continue;
                }
                if(condition.Name.Equals("between", StringComparison.Ordinal))
                {
                    float triggerMinValue = float.Parse(trigger.SelectSingleNode("//between/values/min-value").InnerText, CultureInfo.InvariantCulture);
                    float triggerMaxValue = float.Parse(trigger.SelectSingleNode("//between/values/max-value").InnerText, CultureInfo.InvariantCulture);
                    if (sensorValue > triggerMinValue && sensorValue < triggerMaxValue)
                    {
                        XmlDocument alarmXml = createAlarmXml(sensorReading, trigger);
                        textBoxAlarmsTriggered.BeginInvoke((MethodInvoker)delegate { textBoxAlarmsTriggered.Text = formatXML(alarmXml); });
                        m_cClient.Publish("alarms", Encoding.UTF8.GetBytes(alarmXml.OuterXml.ToString()));
                    }
                    continue;
                }
            }
        }

        private XmlDocument createAlarmXml(XmlDocument sensorReading, XmlNode trigger)
        {
            XmlDocument alarmDoc = new XmlDocument();
            XmlElement alarm = alarmDoc.CreateElement("alarm");

            alarmDoc.AppendChild(alarm);
            XmlNode triggerNode = alarmDoc.ImportNode(trigger, true);
            XmlElement triggerData = (XmlElement) triggerNode;
            alarm.AppendChild(triggerData);

            XmlNode sensorReadingNode = alarmDoc.ImportNode(sensorReading.DocumentElement.SelectSingleNode("//*"), true);
            alarm.AppendChild(sensorReadingNode);

            string xml = alarmDoc.OuterXml;
            return alarmDoc;
        }

        private void buttonPauseTriggers_Click(object sender, EventArgs e)
        {

        }

        private void buttonReloadTriggers_Click(object sender, EventArgs e)
        {
            loadTriggerRules(@"trigger-rules.xml");
            MessageBox.Show("The trigger rules file was reloaded!");
        }

        public bool ValidateXml()
        {
            isXMLValid = true;
            validationMessage = "Document is valid";
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(@"trigger-rules.xml");
                ValidationEventHandler eventH = new ValidationEventHandler(MyEvent);
                doc.Schemas.Add(null, @"trigger-rules.xsd");
                doc.Validate(eventH);
            }
            catch (XmlException ex)
            {
                isXMLValid = false;
                validationMessage = string.Format("Document invalid {0}", ex.Message); // can also use ToText()?

                MessageBox.Show("The triggers xml file is invalid");
            }
            return isXMLValid;
        }

        private void MyEvent(object sender, ValidationEventArgs e)
        {
            isXMLValid = false;
            validationMessage = "Invalid document " + e.Message;
        }
    }
}
