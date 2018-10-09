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

namespace SmartH2O_SeeApp
{
    public partial class SeeApp : Form
    {
        private WebService_InterfaceClient service = new WebService_InterfaceClient();
        public SeeApp()
        {
            InitializeComponent();

            string[] dates = service.GetAlarmLogDates();
            foreach (string date in dates)
            {
                firstDate.Items.Add(date);
                secondDate.Items.Add(date);
            }
        }

        private void getSensorsInfoHourly_Click(object sender, EventArgs e)
        {
            SensorStatisticsByHour param_infos = new SensorStatisticsByHour();
            param_infos.Show();
        }

        private void getSensorsInfoDaily_Click(object sender, EventArgs e)
        {
            SensorStatisticsByDay param_infos = new SensorStatisticsByDay();
            param_infos.Show();
        }

        private void getSensorsInfoWeekly_Click(object sender, EventArgs e)
        {
            SensorStatisticsByWeek param_infos = new SensorStatisticsByWeek();
            param_infos.Show();
        }

        private void getAlarmInfo_Click(object sender, EventArgs e)
        {
            if(firstDate.SelectedIndex < 0 && secondDate.SelectedIndex < 0)
            {
                alarmsTriggerd.Clear();
                string[] alarms = service.GetDailyAlarms();
                if(alarms != null)
                {
                    foreach (string alarm in alarms)
                    {
                        alarmsTriggerd.AppendText(alarm);
                    }
                }                
            }
            else
            {
                if (firstDate.SelectedIndex != secondDate.SelectedIndex)
                {
                    string[] fDate = firstDate.SelectedItem.ToString().Split('-');
                    string[] sDate = secondDate.SelectedItem.ToString().Split('-');
                    DateTime first = new DateTime(int.Parse(fDate[0]), int.Parse(fDate[1]), int.Parse(fDate[2]), 0, 0, 0);
                    DateTime second = new DateTime(int.Parse(sDate[0]), int.Parse(sDate[1]), int.Parse(sDate[2]), 0, 0, 0);

                    if (DateTime.Compare(first, second) < 0)
                    {
                        alarmsTriggerd.Clear();
                        string[] alarms = service.GetAlarmsByDates(firstDate.SelectedItem.ToString(), secondDate.SelectedItem.ToString());
                        if (alarms != null)
                        {
                            foreach (string alarm in alarms)
                            {
                                alarmsTriggerd.AppendText(alarm);
                            }
                        }                                                
                    }
                    else
                    {
                        MessageBox.Show("You have selected a later first date than the second date!" + System.Environment.NewLine + "Select a earlier first date, please.", "Selection Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("You have selected the same date!" + System.Environment.NewLine + "Select two diferent dates, please.", "Selection Date Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
