using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Hosting;
using System.Xml;
using System.Xml.Schema;

namespace SmartH2O_Service
{
    public class WebService : WebService_Interface
    {
        public string validationMessage { get; set; }
        private bool isXMLValid = true;

        string alarmSchemaFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.alarm_log_schema_filename);
        string sensorSchemaFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.param_log_schema_filename);

        public void WriteToAlarmsLog(string[] data)
        {
            string alarmLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.alarm_log_filename);
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(alarmLogFilename))
            {
                XmlElement root = doc.CreateElement("alarm-logs");
                doc.AppendChild(root);
                AddAlarmLogWithNewDate(doc, root, data, alarmLogFilename);
            }
            else
            {
                doc.Load(alarmLogFilename);
                if (isNewAlarmLogDate(doc, data[3]))
                {
                    XmlNode root = doc.SelectSingleNode("alarm-logs");
                    AddAlarmLogWithNewDate(doc, root, data, alarmLogFilename);
                }
                else
                {
                    ConstructAlarmLog(doc, data);
                    if (ValidateXml(doc, alarmSchemaFilename))
                    {
                        doc.Save(alarmLogFilename);
                    }
                }
            }
        }

        public void WriteToSensorsLog(string[] data)
        {
            string sensorLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.param_log_filename);
            XmlDocument doc = new XmlDocument();

            if (!File.Exists(sensorLogFilename))
            {
                XmlElement root = doc.CreateElement("param-logs");
                doc.AppendChild(root);
                AddSensorLogWhitNewDate(doc, root, data, sensorLogFilename);
            }
            else
            {
                doc.Load(sensorLogFilename);
                if (isNewParamLogDate(doc, data[3]))
                {
                    XmlNode root = doc.SelectSingleNode("param-logs");
                    AddSensorLogWhitNewDate(doc, root, data, sensorLogFilename);
                }
                else
                {
                    ConstructParamLog(doc, data);

                    if (ValidateXml(doc, sensorSchemaFilename))
                    {
                        doc.Save(sensorLogFilename);
                    }
                }
            }
        }

        private void AddAlarmLogWithNewDate(XmlDocument doc, XmlNode root, string[] data, string filename)
        {
            XmlElement alarmDate = doc.CreateElement("alarm-date");
            alarmDate.SetAttribute("date", data[3]);
            root.AppendChild(alarmDate);
            XmlElement alarmSensorType = doc.CreateElement("alarm-sensor-type");
            alarmSensorType.SetAttribute("type", "PH");
            alarmDate.AppendChild(alarmSensorType);
            alarmSensorType = doc.CreateElement("alarm-sensor-type");
            alarmSensorType.SetAttribute("type", "NH3");
            alarmDate.AppendChild(alarmSensorType);
            alarmSensorType = doc.CreateElement("alarm-sensor-type");
            alarmSensorType.SetAttribute("type", "CI2");
            alarmDate.AppendChild(alarmSensorType);

            ConstructAlarmLog(doc, data);

            if (ValidateXml(doc, alarmSchemaFilename))
            {
                doc.Save(filename);
            }
        }

        private void ConstructAlarmLog(XmlDocument doc, string[] data)
        {
            XmlElement alarmLog = doc.CreateElement("alarm-log");

            switch (data[5])
            {
                case "equal-to":
                    XmlElement equalTo = doc.CreateElement("trigger-equal-to");
                    XmlElement triggerValue = doc.CreateElement("trigger-value");
                    triggerValue.InnerText = data[6];
                    XmlElement message = doc.CreateElement("message");
                    message.InnerText = data[8];
                    equalTo.AppendChild(triggerValue);
                    equalTo.AppendChild(message);
                    alarmLog.AppendChild(equalTo);
                    break;
                case "greater-than":
                    XmlElement greaterThan = doc.CreateElement("trigger-greater-than");
                    triggerValue = doc.CreateElement("trigger-value");
                    triggerValue.InnerText = data[6];
                    message = doc.CreateElement("message");
                    message.InnerText = data[8];
                    greaterThan.AppendChild(triggerValue);
                    greaterThan.AppendChild(message);
                    alarmLog.AppendChild(greaterThan);
                    break;
                case "less-than":
                    XmlElement lessThan = doc.CreateElement("trigger-less-than");
                    triggerValue = doc.CreateElement("trigger-value");
                    triggerValue.InnerText = data[6];
                    message = doc.CreateElement("message");
                    message.InnerText = data[8];
                    lessThan.AppendChild(triggerValue);
                    lessThan.AppendChild(message);
                    alarmLog.AppendChild(lessThan);
                    break;
                case "between":
                    XmlElement between = doc.CreateElement("trigger-between");
                    XmlElement triggerMinVal = doc.CreateElement("trigger-min-value");
                    triggerMinVal.InnerText = data[6];
                    XmlElement triggerMaxVal = doc.CreateElement("trigger-max-value");
                    triggerMaxVal.InnerText = data[7];
                    message = doc.CreateElement("message");
                    message.InnerText = data[8];
                    between.AppendChild(triggerMinVal);
                    between.AppendChild(triggerMaxVal);
                    between.AppendChild(message);
                    alarmLog.AppendChild(between);
                    break;
            }

            XmlElement sensorId = doc.CreateElement("sensor-id");
            sensorId.InnerText = data[0];
            XmlElement sensorVal = doc.CreateElement("sensor-value");
            sensorVal.InnerText = data[2];
            XmlElement alarmTime = doc.CreateElement("alarm-time");
            alarmTime.InnerText = data[4];
            alarmLog.AppendChild(sensorId);
            alarmLog.AppendChild(sensorVal);
            alarmLog.AppendChild(alarmTime);

            XmlNodeList nodeToInsert = doc.SelectNodes("/alarm-logs/alarm-date[@date='" + data[3] + "']/alarm-sensor-type[@type='" + data[1] + "']");
            nodeToInsert[0].AppendChild(alarmLog);
        }

        private void AddSensorLogWhitNewDate(XmlDocument doc, XmlNode root, string[] data, string filename)
        {
            XmlElement paramDate = doc.CreateElement("param-date");
            paramDate.SetAttribute("date", data[3]);
            root.AppendChild(paramDate);
            XmlElement paramType = doc.CreateElement("param-type");
            paramType.SetAttribute("type", "PH");
            paramDate.AppendChild(paramType);
            paramType = doc.CreateElement("param-type");
            paramType.SetAttribute("type", "NH3");
            paramDate.AppendChild(paramType);
            paramType = doc.CreateElement("param-type");
            paramType.SetAttribute("type", "CI2");
            paramDate.AppendChild(paramType);

            ConstructParamLog(doc, data);

            if (ValidateXml(doc, sensorSchemaFilename))
            {
                doc.Save(filename);
            }
        }

        private void ConstructParamLog(XmlDocument doc, string[] data)
        {
            XmlElement paramLog = doc.CreateElement("param-log");

            XmlElement id = doc.CreateElement("id");
            id.InnerText = data[0];
            XmlElement value = doc.CreateElement("value");
            value.InnerText = data[2];
            XmlElement time = doc.CreateElement("time");
            time.InnerText = data[4];
            paramLog.AppendChild(id);
            paramLog.AppendChild(value);
            paramLog.AppendChild(time);

            XmlNodeList paramToInsert = doc.SelectNodes("/param-logs/param-date[@date='" + data[3] + "']/param-type[@type='" + data[1] + "']");
            paramToInsert[0].AppendChild(paramLog);
        }

        private bool isNewParamLogDate(XmlDocument doc, string logDate)
        {
            XmlNodeList dates = doc.SelectNodes("/param-logs/param-date");
            foreach (XmlNode date in dates)
            {
                if (date.Attributes["date"].Value == logDate)
                {
                    return false;
                }
            }
            return true;
        }

        private bool isNewAlarmLogDate(XmlDocument doc, string logDate)
        {
            XmlNodeList dates = doc.SelectNodes("/alarm-logs/alarm-date");
            foreach (XmlNode date in dates)
            {
                if (date.Attributes["date"].Value == logDate)
                {
                    return false;
                }
            }
            return true;
        }

        public string GetHourlyInformation(string paramType, string specificDate)
        {
            string paramLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.param_log_filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(paramLogFilename);

            XmlDocument sendingDoc = new XmlDocument();
            XmlElement root = sendingDoc.CreateElement("hourly-info");
            sendingDoc.AppendChild(root);
            XmlElement logHour;
            XmlElement min;
            XmlElement max;
            XmlElement average;

            XmlNodeList checkLog = doc.SelectNodes("/param-logs/param-date[@date='" + specificDate + "']");

            if (checkLog.Count > 0)
            {
                for (int hour = 0; hour < 23; hour++)
                {
                    string searchHour;

                    if (hour >= 0 && hour <= 9)
                    {
                        searchHour = string.Format("0{0}", hour);
                    }
                    else
                    {
                        searchHour = string.Format("{0}", hour);
                    }

                    XmlNodeList hourLog = doc.SelectNodes("/param-logs/param-date[@date='" + specificDate + "']/param-type[@type='" + paramType + "']/param-log[starts-with(time,'" + searchHour + "')]");

                    if (hourLog.Count > 0)
                    {
                        string firstValue = hourLog[0]["value"].InnerText;
                        float minValue = float.Parse(firstValue, CultureInfo.InvariantCulture.NumberFormat);
                        float maxValue = float.Parse(firstValue, CultureInfo.InvariantCulture.NumberFormat);
                        float sumOfValues = 0;
                        float averageValue = 0;

                        foreach (XmlNode log in hourLog)
                        {
                            float logValue = float.Parse(log["value"].InnerText, CultureInfo.InvariantCulture.NumberFormat);

                            if (minValue > logValue)
                            {
                                minValue = logValue;
                            }

                            if (maxValue < logValue)
                            {
                                maxValue = logValue;
                            }

                            sumOfValues += logValue;
                        }

                        averageValue = sumOfValues / hourLog.Count;

                        logHour = sendingDoc.CreateElement("log-hour");
                        logHour.SetAttribute("hour", hour.ToString());

                        min = sendingDoc.CreateElement("min");
                        min.InnerText = minValue.ToString();

                        max = sendingDoc.CreateElement("max");
                        max.InnerText = maxValue.ToString();

                        average = sendingDoc.CreateElement("average");
                        average.InnerText = averageValue.ToString();

                        logHour.AppendChild(min);
                        logHour.AppendChild(max);
                        logHour.AppendChild(average);
                        root.AppendChild(logHour);
                    }
                }
                string xmlOutput = System.Xml.Linq.XElement.Parse(sendingDoc.OuterXml).ToString();

                return xmlOutput;
            }
            return null;
        }

        public string GetDailyInformation (string paramType, string firstDate, string secondDate)
        {
            string paramLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.param_log_filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(paramLogFilename);

            XmlDocument sendingDoc = new XmlDocument();
            XmlElement root = sendingDoc.CreateElement("daily-info");
            sendingDoc.AppendChild(root);
            XmlElement logDay;
            XmlElement min;
            XmlElement max;
            XmlElement average;

            string checkFirstDate = firstDate.Replace(@"-", "");
            string checkSecondDate = secondDate.Replace(@"-", "");
            
            XmlNodeList dayLogs = doc.SelectNodes("/param-logs/param-date[translate(@date,'-','')>='"+checkFirstDate+"' and  translate(@date,'-','')<='"+checkSecondDate+"']");

            if (dayLogs.Count > 0)
            {
                foreach (XmlNode day in dayLogs)
                {        
                    XmlDocument dayLog = new XmlDocument();
                    dayLog.LoadXml(day.OuterXml);

                    XmlNodeList paramLogs = dayLog.SelectNodes("/param-date/param-type[@type='"+paramType+"']/param-log");
               
                    string firstValue = paramLogs[0]["value"].InnerText;
                    float minValue = float.Parse(firstValue, CultureInfo.InvariantCulture.NumberFormat);
                    float maxValue = float.Parse(firstValue, CultureInfo.InvariantCulture.NumberFormat);
                    float sumOfValues = 0;
                    float averageValue = 0;

                    foreach (XmlNode log in paramLogs)
                    {
                        float logValue = float.Parse(log["value"].InnerText, CultureInfo.InvariantCulture.NumberFormat);

                        if (minValue > logValue)
                        {
                            minValue = logValue;
                        }

                        if (maxValue < logValue)
                        {
                            maxValue = logValue;
                        }

                        sumOfValues += logValue;
                    }

                    averageValue = sumOfValues / paramLogs.Count;

                    logDay = sendingDoc.CreateElement("log-day");
                    logDay.SetAttribute("day", day.Attributes["date"].Value);

                    min = sendingDoc.CreateElement("min");
                    min.InnerText = minValue.ToString();

                    max = sendingDoc.CreateElement("max");
                    max.InnerText = maxValue.ToString();

                    average = sendingDoc.CreateElement("average");
                    average.InnerText = averageValue.ToString();

                    logDay.AppendChild(min);
                    logDay.AppendChild(max);
                    logDay.AppendChild(average);
                    root.AppendChild(logDay);
                    
                }
                string xmlOutput = System.Xml.Linq.XElement.Parse(sendingDoc.OuterXml).ToString();

                return xmlOutput;
            }
            return null;
        }

        public string GetWeeklyInformation(string paramType)
        {
            string paramLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.param_log_filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(paramLogFilename);

            XmlDocument sendingDoc = new XmlDocument();
            XmlElement root = sendingDoc.CreateElement("weekly-info");
            sendingDoc.AppendChild(root);
            XmlElement logDay;
            XmlElement min;
            XmlElement max;
            XmlElement average;

            DateTime today = DateTime.Today;
            DateTime week = DateTime.Today.AddDays(-7);

            string checkSecondDate = today.Year.ToString() + today.Month.ToString() + today.Day.ToString();
            string checkFirstDate = week.Year.ToString() + week.Month.ToString() + week.Day.ToString();

            XmlNodeList dayLogs = doc.SelectNodes("/param-logs/param-date[translate(@date,'-','')>='" + checkFirstDate + "' and  translate(@date,'-','')<='" + checkSecondDate + "']");

            if (dayLogs.Count > 0)
            {
                foreach (XmlNode day in dayLogs)
                {
                    XmlDocument dayLog = new XmlDocument();
                    dayLog.LoadXml(day.OuterXml);

                    XmlNodeList paramLogs = dayLog.SelectNodes("/param-date/param-type[@type='" + paramType + "']/param-log");

                    string firstValue = paramLogs[0]["value"].InnerText;
                    float minValue = float.Parse(firstValue, CultureInfo.InvariantCulture.NumberFormat);
                    float maxValue = float.Parse(firstValue, CultureInfo.InvariantCulture.NumberFormat);
                    float sumOfValues = 0;
                    float averageValue = 0;

                    foreach (XmlNode log in paramLogs)
                    {
                        float logValue = float.Parse(log["value"].InnerText, CultureInfo.InvariantCulture.NumberFormat);

                        if (minValue > logValue)
                        {
                            minValue = logValue;
                        }

                        if (maxValue < logValue)
                        {
                            maxValue = logValue;
                        }

                        sumOfValues += logValue;
                    }

                    averageValue = sumOfValues / paramLogs.Count;

                    logDay = sendingDoc.CreateElement("log-day");
                    logDay.SetAttribute("day", day.Attributes["date"].Value);

                    min = sendingDoc.CreateElement("min");
                    min.InnerText = minValue.ToString();

                    max = sendingDoc.CreateElement("max");
                    max.InnerText = maxValue.ToString();

                    average = sendingDoc.CreateElement("average");
                    average.InnerText = averageValue.ToString();

                    logDay.AppendChild(min);
                    logDay.AppendChild(max);
                    logDay.AppendChild(average);
                    root.AppendChild(logDay);

                }
                string xmlOutput = System.Xml.Linq.XElement.Parse(sendingDoc.OuterXml).ToString();

                return xmlOutput;
            }
            return null;
        }

        public string[] GetDailyAlarms()
        {
            string alarmLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.alarm_log_filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(alarmLogFilename);

            DateTime today = DateTime.Today;
            string checkDate = today.Year.ToString() + today.Month.ToString() + today.Day.ToString();
            string messageDate = today.Year.ToString() + "-" + today.Month.ToString() + "-" + today.Day.ToString();

            
            XmlNodeList checkAlarms = doc.SelectNodes("/alarm-logs/alarm-date[translate(@date,'-','')='" + checkDate + "']");

            if (checkAlarms.Count > 0)
            {

                XmlNodeList phAlarmLogs = doc.SelectNodes("/alarm-logs/alarm-date[translate(@date,'-','')='" + checkDate + "']/alarm-sensor-type[@type='PH']/alarm-log");

                string[] phAlarms = new string[phAlarmLogs.Count];

                int arrayCounter = 0;

                if (phAlarmLogs.Count > 0)
                {
                    foreach (XmlNode alarm in phAlarmLogs)
                    {
                        phAlarms[arrayCounter] = "Alarm Triggerd[" + messageDate + " at " + alarm["alarm-time"].InnerText + "] :" + System.Environment.NewLine + alarm.ChildNodes[0]["message"].InnerText + System.Environment.NewLine;
                        arrayCounter++;
                    }

                    arrayCounter = 0;
                }

                XmlNodeList nh3AlarmLogs = doc.SelectNodes("/alarm-logs/alarm-date[translate(@date,'-','')='" + checkDate + "']/alarm-sensor-type[@type='NH3']/alarm-log");

                string[] nh3Alarms = new string[nh3AlarmLogs.Count];

                if (nh3AlarmLogs.Count > 0)
                {
                    foreach (XmlNode alarm in nh3AlarmLogs)
                    {
                        nh3Alarms[arrayCounter] = "Alarm Triggerd[" + messageDate + " at " + alarm["alarm-time"].InnerText + "] :" + System.Environment.NewLine + alarm.ChildNodes[0]["message"].InnerText + System.Environment.NewLine;
                        arrayCounter++;
                    }

                    arrayCounter = 0;
                }

                XmlNodeList ci2AlarmLogs = doc.SelectNodes("/alarm-logs/alarm-date[translate(@date,'-','')='" + checkDate + "']/alarm-sensor-type[@type='CI2']/alarm-log");

                string[] ci2Alarms = new string[ci2AlarmLogs.Count];

                if (ci2AlarmLogs.Count > 0)
                {
                    foreach (XmlNode alarm in ci2AlarmLogs)
                    {
                        ci2Alarms[arrayCounter] = "Alarm Triggerd[" + messageDate + " at " + alarm["alarm-time"].InnerText + "] :" + System.Environment.NewLine + alarm.ChildNodes[0]["message"].InnerText + System.Environment.NewLine;
                        arrayCounter++;
                    }

                    arrayCounter = 0;
                }

                string[] alarmsArray = new string[phAlarmLogs.Count + nh3AlarmLogs.Count + ci2AlarmLogs.Count];

                foreach (string infoph in phAlarms)
                {
                    alarmsArray[arrayCounter] = infoph;
                    arrayCounter++;
                }

                foreach (string infonh3 in nh3Alarms)
                {
                    alarmsArray[arrayCounter] = infonh3;
                    arrayCounter++;
                }

                foreach (string infoci2 in ci2Alarms)
                {
                    alarmsArray[arrayCounter] = infoci2;
                    arrayCounter++;
                }

                return alarmsArray;
            }

            return null;
        }

        public string[] GetAlarmsByDates(string firstDate, string secondDate)
        {
            string alarmLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.alarm_log_filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(alarmLogFilename);

            XmlDocument docAux = new XmlDocument();
            string checkFirstDate = firstDate.Replace(@"-", "");
            string checkSecondDate = secondDate.Replace(@"-", "");

            XmlNodeList daysAlarmsLogs = doc.SelectNodes("/alarm-logs/alarm-date[translate(@date,'-','')>='" + checkFirstDate + "' and translate(@date,'-','')<='" + checkSecondDate + "']");
          
            if (daysAlarmsLogs.Count > 0) {

                XmlNodeList countLogs = doc.SelectNodes("/alarm-logs/alarm-date[translate(@date,'-','')>='" + checkFirstDate + "' and translate(@date,'-','')<='" + checkSecondDate + "']/alarm-sensor-type/alarm-log");

                string[] alarmsArray = new string[countLogs.Count];

                int alarmsArrayCounter = 0;

                foreach (XmlNode day in daysAlarmsLogs)
                {
                    docAux.LoadXml(day.OuterXml);

                    XmlNodeList phAlarmLogs = docAux.SelectNodes("/alarm-date/alarm-sensor-type[@type='PH']/alarm-log");

                    string[] phAlarms = new string[phAlarmLogs.Count];

                    int arrayCounter = 0;

                    if (phAlarmLogs.Count > 0)
                    {
                        foreach (XmlNode alarm in phAlarmLogs)
                        {
                            phAlarms[arrayCounter] = "Alarm Triggerd[" + day.Attributes["date"].Value.ToString() + " at " + alarm["alarm-time"].InnerText + "] :" + System.Environment.NewLine + alarm.ChildNodes[0]["message"].InnerText + System.Environment.NewLine;
                            arrayCounter++;
                        }

                        arrayCounter = 0;
                    }

                    XmlNodeList nh3AlarmLogs = docAux.SelectNodes("/alarm-date/alarm-sensor-type[@type='NH3']/alarm-log");

                    string[] nh3Alarms = new string[nh3AlarmLogs.Count];

                    if (nh3AlarmLogs.Count > 0)
                    {
                        foreach (XmlNode alarm in nh3AlarmLogs)
                        {
                            nh3Alarms[arrayCounter] = "Alarm Triggerd[" + day.Attributes["date"].Value.ToString() + " at " + alarm["alarm-time"].InnerText + "] :" + System.Environment.NewLine + alarm.ChildNodes[0]["message"].InnerText + System.Environment.NewLine;
                            arrayCounter++;
                        }

                        arrayCounter = 0;
                    }

                    XmlNodeList ci2AlarmLogs = docAux.SelectNodes("/alarm-date/alarm-sensor-type[@type='CI2']/alarm-log");

                    string[] ci2Alarms = new string[ci2AlarmLogs.Count];

                    if (ci2AlarmLogs.Count > 0)
                    {
                        foreach (XmlNode alarm in ci2AlarmLogs)
                        {
                            ci2Alarms[arrayCounter] = "Alarm Triggerd[" + day.Attributes["date"].Value.ToString() + " at " + alarm["alarm-time"].InnerText + "] :" + System.Environment.NewLine + alarm.ChildNodes[0]["message"].InnerText + System.Environment.NewLine;
                            arrayCounter++;
                        }

                        arrayCounter = 0;
                    }

                    foreach (string infoph in phAlarms)
                    {
                        alarmsArray[alarmsArrayCounter] = infoph;
                        alarmsArrayCounter++;
                    }

                    foreach (string infonh3 in nh3Alarms)
                    {
                        alarmsArray[alarmsArrayCounter] = infonh3;
                        alarmsArrayCounter++;
                    }

                    foreach (string infoci2 in ci2Alarms)
                    {
                        alarmsArray[alarmsArrayCounter] = infoci2;
                        alarmsArrayCounter++;
                    }

                }
                return alarmsArray;
            }
            return null;
        }

        public string[] GetParamLogDates()
        {
            string paramLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.param_log_filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(paramLogFilename);

            XmlNodeList dates = doc.SelectNodes("//@date");
            string[] arrayDates = new string[dates.Count];
            int countNode = 0;
            foreach (XmlNode date in dates)
            {
                arrayDates[countNode] = date.Value;
                countNode++;
            }
            return arrayDates;
        }

        public string[] GetAlarmLogDates()
        {
            string alarmLogFilename = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", SmartH2O_Service.Properties.Settings.Default.alarm_log_filename);
            XmlDocument doc = new XmlDocument();
            doc.Load(alarmLogFilename);

            XmlNodeList dates = doc.SelectNodes("//@date");
            string[] arrayDates = new string[dates.Count];
            int countNode = 0;
            foreach (XmlNode date in dates)
            {
                arrayDates[countNode] = date.Value;
                countNode++;
            }
            return arrayDates;
        }

        public bool ValidateXml(XmlDocument doc, string schemaFile)
        {
            isXMLValid = true;
            validationMessage = "Document is valid";

            try
            {
                ValidationEventHandler eventH = new ValidationEventHandler(MyEvent);
                //doc.Schemas.Add(null, XsdFilePath);
                doc.Schemas.Add(null, schemaFile);
                doc.Validate(eventH);
            }
            catch (XmlException ex)
            {
                isXMLValid = false;
                validationMessage = string.Format("Document invalid {0}", ex.Message); // can also use ToText()?
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
