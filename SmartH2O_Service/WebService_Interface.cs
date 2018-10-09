using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartH2O_Service
{
    [ServiceContract]
    public interface WebService_Interface
    {
        [OperationContract]
        void WriteToAlarmsLog(string[] data);
        
        [OperationContract]
        void WriteToSensorsLog(string[] data);

        [OperationContract]
        string GetHourlyInformation(string paramType, string specificDate);

        [OperationContract]
        string GetDailyInformation(string paramType, string firstDate, string secondDate);

        [OperationContract]
        string GetWeeklyInformation(string paramType);

        [OperationContract]
        string[] GetDailyAlarms();

        [OperationContract]
        string[] GetAlarmsByDates(string firstDate, string secondDate);

        [OperationContract]
        string[] GetParamLogDates();

        [OperationContract]
        string[] GetAlarmLogDates();

    }

}
