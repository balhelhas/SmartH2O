using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SmartH2O_SeeApp.SmartH2O_Service;
using System.Xml;

namespace SmartH2O_SeeApp
{
    public partial class SensorStatisticsByHour : Form
    {
        private WebService_InterfaceClient service = new WebService_InterfaceClient();

        public SensorStatisticsByHour()
        {
            InitializeComponent();


            sensorTypeList.Items.Add("PH");
            sensorTypeList.Items.Add("NH3");
            sensorTypeList.Items.Add("CI2");

            string[] dates = service.GetParamLogDates();
            foreach (string date in dates)
            {
                specificDateForLog.Items.Add(date);
            }

            sensorsHourlyChart.Titles.Add("Hourly Information");
            sensorsHourlyChart.Series.Clear();
        }

        private void fillSensorChart(object sender, EventArgs e)
        {
            if (sensorTypeList.SelectedIndex > -1 && specificDateForLog.SelectedIndex > -1)
            {
                string serviceXml = service.GetHourlyInformation(sensorTypeList.SelectedItem.ToString(), specificDateForLog.SelectedItem.ToString());
                if(serviceXml != null)
                {
                    fillChart(serviceXml, sensorsHourlyChart);
                }               
            }
        }

        private void fillChart(string serviceXml, Chart sensorChart)
        {
            sensorChart.Series.Clear();

            Series minValues = sensorChart.Series.Add("Sensor Minimum Value");
            minValues.ChartType = SeriesChartType.Column;
            minValues.Color = System.Drawing.Color.Green;
            minValues.Points.Clear();

            Series averageValues = sensorChart.Series.Add("Sensor Average Value");
            averageValues.ChartType = SeriesChartType.Column;
            averageValues.Color = System.Drawing.Color.Orange;
            averageValues.Points.Clear();

            Series maxValues = sensorChart.Series.Add("Sensor Maximum Value");
            maxValues.ChartType = SeriesChartType.Column;
            maxValues.Color = System.Drawing.Color.Red;
            maxValues.Points.Clear();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(serviceXml);

            XmlNodeList hourlyInfo = doc.SelectNodes("/hourly-info/log-hour");

            foreach (XmlNode log in hourlyInfo)
            {
                string hour = log.Attributes["hour"].Value.ToString();
                string min = log["min"].InnerText.ToString();
                string max = log["max"].InnerText.ToString();
                string average = log["average"].InnerText.ToString();

                minValues.Points.AddXY(hour, float.Parse(min));
                averageValues.Points.AddXY(hour, float.Parse(average));
                maxValues.Points.AddXY(hour, float.Parse(max));
            }

        }
    }
}
