namespace SmartH2O_SeeApp
{
    partial class SensorStatisticsByWeek
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.sensorTypeList = new System.Windows.Forms.ComboBox();
            this.fillChartBtn = new System.Windows.Forms.Button();
            this.sensorsWeeklyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.sensorsWeeklyChart)).BeginInit();
            this.SuspendLayout();
            // 
            // sensorTypeList
            // 
            this.sensorTypeList.FormattingEnabled = true;
            this.sensorTypeList.Location = new System.Drawing.Point(12, 12);
            this.sensorTypeList.Name = "sensorTypeList";
            this.sensorTypeList.Size = new System.Drawing.Size(200, 24);
            this.sensorTypeList.TabIndex = 6;
            this.sensorTypeList.Text = "Select sensor type";
            // 
            // fillChartBtn
            // 
            this.fillChartBtn.Location = new System.Drawing.Point(296, 11);
            this.fillChartBtn.Name = "fillChartBtn";
            this.fillChartBtn.Size = new System.Drawing.Size(253, 24);
            this.fillChartBtn.TabIndex = 8;
            this.fillChartBtn.Text = "Get Chart";
            this.fillChartBtn.UseVisualStyleBackColor = true;
            this.fillChartBtn.Click += new System.EventHandler(this.fillSensorChart);
            // 
            // sensorsWeeklyChart
            // 
            chartArea1.Name = "ChartArea1";
            this.sensorsWeeklyChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.sensorsWeeklyChart.Legends.Add(legend1);
            this.sensorsWeeklyChart.Location = new System.Drawing.Point(12, 63);
            this.sensorsWeeklyChart.Name = "sensorsWeeklyChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.sensorsWeeklyChart.Series.Add(series1);
            this.sensorsWeeklyChart.Size = new System.Drawing.Size(601, 365);
            this.sensorsWeeklyChart.TabIndex = 9;
            this.sensorsWeeklyChart.Text = "chart1";
            // 
            // SensorStatisticsByWeek
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 440);
            this.Controls.Add(this.sensorsWeeklyChart);
            this.Controls.Add(this.fillChartBtn);
            this.Controls.Add(this.sensorTypeList);
            this.Name = "SensorStatisticsByWeek";
            this.Text = "Weekly Sensor Statistics";
            ((System.ComponentModel.ISupportInitialize)(this.sensorsWeeklyChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox sensorTypeList;
        private System.Windows.Forms.Button fillChartBtn;
        private System.Windows.Forms.DataVisualization.Charting.Chart sensorsWeeklyChart;
    }
}