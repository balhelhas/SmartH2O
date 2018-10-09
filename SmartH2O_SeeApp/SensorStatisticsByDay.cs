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
    public partial class SensorStatisticsByDay : Form
    {
        private WebService_InterfaceClient service = new WebService_InterfaceClient();
        public SensorStatisticsByDay()
        {
            InitializeComponent();

            sensorTypeList.Items.Add("PH");
            sensorTypeList.Items.Add("NH3");
            sensorTypeList.Items.Add("CI2");

            string[] dates = service.GetParamLogDates();
            foreach (string date in dates)
            {
                firstDateForLog.Items.Add(date);
                secondDateForLog.Items.Add(date);
            }

            sensorsDailyChart.Titles.Add("Daily Information");
            sensorsDailyChart.Series.Clear();
        }

        private void fillSensorChart(object sender, EventArgs e)
        {
            if (sensorTypeList.SelectedIndex > -1 && firstDateForLog.SelectedIndex > -1 && secondDateForLog.SelectedIndex > -1)
            {
                if (firstDateForLog.SelectedIndex != secondDateForLog.SelectedIndex)
                {
                    string[] fDate = firstDateForLog.SelectedItem.ToString().Split('-');
                    string[] sDate = secondDateForLog.SelectedItem.ToString().Split('-');
                    DateTime first = new DateTime(int.Parse(fDate[0]), int.Parse(fDate[1]), int.Parse(fDate[2]), 0, 0, 0);
                    DateTime second = new DateTime(int.Parse(sDate[0]), int.Parse(sDate[1]), int.Parse(sDate[2]), 0, 0, 0);

                    if (DateTime.Compare(first, second) < 0)
                    {
                        string serviceXml = service.GetDailyInformation(sensorTypeList.SelectedItem.ToString(), firstDateForLog.SelectedItem.ToString(), secondDateForLog.SelectedItem.ToString());
                        if(serviceXml != null)
                        {
                            fillChart(serviceXml, sensorsDailyChart);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You have selected a later first date than the second date!" + System.Environment.NewLine + "Select a earlier first date, please.", "Selection Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You have selected the same date!"+System.Environment.NewLine+"Select two diferent dates, please.", "Selection Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            XmlNodeList hourlyInfo = doc.SelectNodes("/daily-info/log-day");

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
