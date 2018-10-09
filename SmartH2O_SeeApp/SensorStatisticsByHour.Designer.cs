namespace SmartH2O_SeeApp
{
    partial class SensorStatisticsByHour
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.sensorsHourlyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.sensorTypeList = new System.Windows.Forms.ComboBox();
            this.specificDateForLog = new System.Windows.Forms.ComboBox();
            this.fillChartBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sensorsHourlyChart)).BeginInit();
            this.SuspendLayout();
            // 
            // sensorsHourlyChart
            // 
            chartArea2.Name = "ChartArea1";
            this.sensorsHourlyChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.sensorsHourlyChart.Legends.Add(legend2);
            this.sensorsHourlyChart.Location = new System.Drawing.Point(12, 90);
            this.sensorsHourlyChart.Name = "sensorsHourlyChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.sensorsHourlyChart.Series.Add(series2);
            this.sensorsHourlyChart.Size = new System.Drawing.Size(601, 365);
            this.sensorsHourlyChart.TabIndex = 3;
            this.sensorsHourlyChart.Text = "chart1";
            // 
            // sensorTypeList
            // 
            this.sensorTypeList.FormattingEnabled = true;
            this.sensorTypeList.Location = new System.Drawing.Point(12, 13);
            this.sensorTypeList.Name = "sensorTypeList";
            this.sensorTypeList.Size = new System.Drawing.Size(200, 24);
            this.sensorTypeList.TabIndex = 5;
            this.sensorTypeList.Text = "Select sensor type";
            // 
            // specificDateForLog
            // 
            this.specificDateForLog.FormattingEnabled = true;
            this.specificDateForLog.Location = new System.Drawing.Point(12, 44);
            this.specificDateForLog.Name = "specificDateForLog";
            this.specificDateForLog.Size = new System.Drawing.Size(200, 24);
            this.specificDateForLog.TabIndex = 6;
            this.specificDateForLog.Text = "Select date log";
            // 
            // fillChartBtn
            // 
            this.fillChartBtn.Location = new System.Drawing.Point(290, 29);
            this.fillChartBtn.Name = "fillChartBtn";
            this.fillChartBtn.Size = new System.Drawing.Size(253, 24);
            this.fillChartBtn.TabIndex = 7;
            this.fillChartBtn.Text = "Get Chart";
            this.fillChartBtn.UseVisualStyleBackColor = true;
            this.fillChartBtn.Click += new System.EventHandler(this.fillSensorChart);
            // 
            // SensorStatisticsByHour
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 467);
            this.Controls.Add(this.fillChartBtn);
            this.Controls.Add(this.specificDateForLog);
            this.Controls.Add(this.sensorTypeList);
            this.Controls.Add(this.sensorsHourlyChart);
            this.Name = "SensorStatisticsByHour";
            this.Text = "Hourly Sensor Statistics";
            ((System.ComponentModel.ISupportInitialize)(this.sensorsHourlyChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataVisualization.Charting.Chart sensorsHourlyChart;
        private System.Windows.Forms.ComboBox sensorTypeList;
        private System.Windows.Forms.ComboBox specificDateForLog;
        private System.Windows.Forms.Button fillChartBtn;
    }
}