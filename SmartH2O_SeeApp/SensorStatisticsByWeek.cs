using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmartH2O_SeeApp.SmartH2O_Service;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace SmartH2O_SeeApp
{
    public partial class SensorStatisticsByWeek : Form
    {
        private WebService_InterfaceClient service = new WebService_InterfaceClient();
        public SensorStatisticsByWeek()
        {
            InitializeComponent();

            sensorTypeList.Items.Add("PH");
            sensorTypeList.Items.Add("NH3");
            sensorTypeList.Items.Add("CI2");


            sensorsWeeklyChart.Titles.Add("Daily Information");
            sensorsWeeklyChart.Series.Clear();
        }

        private void fillSensorChart(object sender, EventArgs e)
        {
            if(sensorTypeList.SelectedIndex > -1)
            {
                string serviceXml = service.GetWeeklyInformation(sensorTypeList.SelectedItem.ToString());
                if (serviceXml != null)
                {
                    fillChart(serviceXml, sensorsWeeklyChart);
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

            XmlNodeList hourlyInfo = doc.SelectNodes("/weekly-info/log-day");

            foreach (XmlNode log in hourlyInfo)
            {
                string day = log.Attributes["day"].Value.ToString();
                string min = log["min"].InnerText.ToString();
                string max = log["max"].InnerText.ToString();
                string average = log["average"].InnerText.ToString();

                minValues.Points.AddXY(day, float.Parse(min));
                averageValues.Points.AddXY(day, float.Parse(average));
                maxValues.Points.AddXY(day, float.Parse(max));
            }
        }
    }
}
