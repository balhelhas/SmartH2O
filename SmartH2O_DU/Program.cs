using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using uPLibrary.Networking.M2Mqtt;

namespace SmartH2O_DU
{
    class Program
    {
        static MqttClient m_cClient;
        static void Main(string[] args)
        {
            m_cClient = new MqttClient("127.0.0.1");
            string[] m_strTopicsInfo = { "sensors" };

            m_cClient.Connect(Guid.NewGuid().ToString());
            if(!m_cClient.IsConnected)
            {
                Console.WriteLine("Error connecting to message broker...");
                return;
            }

            
            //if(m_cClient.IsConnected)
            //{
            //    m_cClient.Unsubscribe(m_strTopicsInfo);
            //    m_cClient.Disconnect();
            //}

            // In miliseconds
            int dataGenerationInterval = 5000;
            SensorNodeDll.SensorNodeDll dll = new SensorNodeDll.SensorNodeDll();
            dll.Initialize(getData, dataGenerationInterval);
            Console.ReadKey();
        }

        private static void getData(string message)
        {
            String sensorReadDate = System.DateTime.Now.Date.ToString("yyyy-MM-dd");
            String timeStr = System.DateTime.Now.ToString("HH:mm:ss");
            //Console.WriteLine("Time: " + sensorReadDate.ToString() + "\nData: " + message);

            string[] data = message.Split(';');
            // Console.WriteLine(data[0] + " " + data[1] + " " + data[2]);
            XmlDocument doc = new XmlDocument();
            XmlElement root = doc.CreateElement("sensor");
            XmlElement id = doc.CreateElement("id");
            id.InnerText = data[0];

            XmlElement type = doc.CreateElement("type");
            type.InnerText = data[1];

            XmlElement value = doc.CreateElement("value");
            value.InnerText = data[2];

            XmlElement date = doc.CreateElement("date");
            date.InnerText = sensorReadDate.ToString();

            XmlElement time = doc.CreateElement("time");
            time.InnerText = timeStr;
            
            doc.AppendChild(root);
            root.AppendChild(id);
            root.AppendChild(type);
            root.AppendChild(value);
            root.AppendChild(date);
            root.AppendChild(time);

            // string xmlOutput = doc.OuterXml;
            // Console.WriteLine(xmlOutput.ToString());
            // Console.WriteLine(xmlOutput);

            try
            {
                string xmlOutput = System.Xml.Linq.XElement.Parse(doc.OuterXml).ToString();
                Console.WriteLine(xmlOutput);
            }
            catch (XmlException xex)
            {
                Console.WriteLine("Error formatting received XML: ", xex);
            }
            string xml = doc.OuterXml;

            //publicar o xml
            //doc.Save(@"testxmlOutput");
            m_cClient.Publish("sensors", Encoding.UTF8.GetBytes(xml));
            

        }


    }
}
