using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using SmartH2O_DLog.SmartH2O_Service;

namespace SmartH2O_DLog
{
    class Program

    {
        static WebService_InterfaceClient service = new WebService_InterfaceClient();
        static MqttClient m_cClient = new MqttClient(SmartH2O_DLog.Properties.Settings.Default.broker_ip);
        private static void Main(string[] args)
        {
            string[] m_strTopicsInfo = { "sensors", "alarms" };

            m_cClient.Connect(Guid.NewGuid().ToString());
            if (!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            // New message arrived
            m_cClient.MqttMsgPublishReceived += client_MqttMsgPublishedReceived;

            //Subscribe to topics
            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };//QoS
            m_cClient.Subscribe(m_strTopicsInfo, qosLevels);

        }

        private static void client_MqttMsgPublishedReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string[] dataToAppend;

            if (e.Topic == "sensors")
            {
                String timeStr = System.DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine("New Sensor Entry " + timeStr);
                String publishedSensor = Encoding.UTF8.GetString(e.Message);
                dataToAppend = ExtractSensorFieldsFromXmlStr(publishedSensor);
                service.WriteToSensorsLog(dataToAppend);
            }
            
            if(e.Topic == "alarms")
            {
                String timeStr = System.DateTime.Now.ToString("HH:mm:ss");
                Console.WriteLine("New Alarm Entry " + timeStr);
                String publishedAlarm = Encoding.UTF8.GetString(e.Message);
                dataToAppend = ExtractAlarmFieldsFromXmlStr(publishedAlarm);
                service.WriteToAlarmsLog(dataToAppend);
            }

        }



        private static string[] ExtractAlarmFieldsFromXmlStr(string xmlStr)
        {
            string[] returnArray = new string[9];

            XmlDocument alarm = new XmlDocument();
            alarm.LoadXml(xmlStr);

            XmlNode alarmSensorId = alarm.SelectSingleNode("/alarm/sensor/id");
            returnArray[0] = alarmSensorId.InnerText.Trim();

            XmlNode alarmSensorType = alarm.SelectSingleNode("/alarm/sensor/type");
            returnArray[1] = alarmSensorType.InnerText.Trim();

            XmlNode alarmSensorVal = alarm.SelectSingleNode("/alarm/sensor/value");
            returnArray[2] = alarmSensorVal.InnerText.Trim();

            XmlNode alarmDate = alarm.SelectSingleNode("/alarm/sensor/date");
            returnArray[3] = alarmDate.InnerText.Trim();

            XmlNode alarmTime = alarm.SelectSingleNode("/alarm/sensor/time");
            returnArray[4] = alarmTime.InnerText.Trim();

            XmlNode alarmTrigger = alarm.SelectSingleNode("/alarm/trigger");
            returnArray[5] = alarmTrigger.FirstChild.Name.ToString().Trim();

            if (returnArray[5] == "between")
            {
                XmlNode alarmTriggerMinValue = alarm.SelectSingleNode("/alarm/trigger/between/values/min-value");
                returnArray[6] = alarmTriggerMinValue.InnerText.Trim();

                XmlNode alarmTriggerMaxValue = alarm.SelectSingleNode("/alarm/trigger/between/values/max-value");
                returnArray[7] = alarmTriggerMaxValue.InnerText.Trim();

                XmlNode alarmMessage = alarm.SelectSingleNode("/alarm/trigger/message");
                returnArray[8] = alarmMessage.InnerText.Trim();
            }
            else
            {
                XmlNode alarmTriggerValue = alarm.SelectSingleNode("/alarm/trigger/" + returnArray[5] +"/value");
                returnArray[6] = alarmTriggerValue.InnerText.Trim();

                XmlNode alarmMessage = alarm.SelectSingleNode("/alarm/trigger/message");
                returnArray[8] = alarmMessage.InnerText.Trim();

            }

            return returnArray;
        }

        private static string[] ExtractSensorFieldsFromXmlStr(string xmlStr)
        {
            string[] returnArray = new string[5];

            XmlDocument sensor = new XmlDocument();
            sensor.LoadXml(xmlStr);

            XmlNode sensor_id = sensor.SelectSingleNode("/sensor/id");
            returnArray[0] = sensor_id.InnerText.Trim();

            XmlNode sensor_type = sensor.SelectSingleNode("/sensor/type");
            returnArray[1] = sensor_type.InnerText.Trim();

            XmlNode sensor_value = sensor.SelectSingleNode("/sensor/value");
            returnArray[2] = sensor_value.InnerText.Trim();

            XmlNode sensor_date = sensor.SelectSingleNode("/sensor/date");
            returnArray[3] = sensor_date.InnerText.Trim();

            XmlNode sensor_time = sensor.SelectSingleNode("/sensor/time");
            returnArray[4] = sensor_time.InnerText.Trim();

            return returnArray;
        }
    }
}
