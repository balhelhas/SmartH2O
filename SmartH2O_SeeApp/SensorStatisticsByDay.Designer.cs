namespace SmartH2O_SeeApp
{
    partial class SensorStatisticsByDay
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
            this.sensorTypeList = new System.Windows.Forms.ComboBox();
            this.firstDateForLog = new System.Windows.Forms.ComboBox();
            this.secondDateForLog = new System.Windows.Forms.ComboBox();
            this.sensorsDailyChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.fillChartBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sensorsDailyChart)).BeginInit();
            this.SuspendLayout();
            // 
            // sensorTypeList
            // 
            this.sensorTypeList.FormattingEnabled = true;
            this.sensorTypeList.Location = new System.Drawing.Point(12, 12);
            this.sensorTypeList.Name = "sensorTypeList";
            this.sensorTypeList.Size = new System.Drawing.Size(182, 24);
            this.sensorTypeList.TabIndex = 0;
            this.sensorTypeList.Text = "Select Sensor Type";
            // 
            // firstDateForLog
            // 
            this.firstDateForLog.FormattingEnabled = true;
            this.firstDateForLog.Location = new System.Drawing.Point(12, 42);
            this.firstDateForLog.Name = "firstDateForLog";
            this.firstDateForLog.Size = new System.Drawing.Size(182, 24);
            this.firstDateForLog.TabIndex = 1;
            this.firstDateForLog.Text = "Select First Date";
            // 
            // secondDateForLog
            // 
            this.secondDateForLog.FormattingEnabled = true;
            this.secondDateForLog.Location = new System.Drawing.Point(12, 72);
            this.secondDateForLog.Name = "secondDateForLog";
            this.secondDateForLog.Size = new System.Drawing.Size(182, 24);
            this.secondDateForLog.TabIndex = 2;
            this.secondDateForLog.Text = "Select Second Date";
            // 
            // sensorsDailyChart
            // 
            chartArea2.Name = "ChartArea1";
            this.sensorsDailyChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.sensorsDailyChart.Legends.Add(legend2);
            this.sensorsDailyChart.Location = new System.Drawing.Point(12, 112);
            this.sensorsDailyChart.Name = "sensorsDailyChart";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.sensorsDailyChart.Series.Add(series2);
            this.sensorsDailyChart.Size = new System.Drawing.Size(614, 358);
            this.sensorsDailyChart.TabIndex = 4;
            this.sensorsDailyChart.Text = "chart1";
            // 
            // fillChartBtn
            // 
            this.fillChartBtn.Location = new System.Drawing.Point(292, 41);
            this.fillChartBtn.Name = "fillChartBtn";
            this.fillChartBtn.Size = new System.Drawing.Size(253, 24);
            this.fillChartBtn.TabIndex = 8;
            this.fillChartBtn.Text = "Get Chart";
            this.fillChartBtn.UseVisualStyleBackColor = true;
            this.fillChartBtn.Click += new System.EventHandler(this.fillSensorChart);
            // 
            // SensorStatisticsByDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 482);
            this.Controls.Add(this.fillChartBtn);
            this.Controls.Add(this.sensorsDailyChart);
            this.Controls.Add(this.secondDateForLog);
            this.Controls.Add(this.firstDateForLog);
            this.Controls.Add(this.sensorTypeList);
            this.Name = "SensorStatisticsByDay";
            this.Text = "Daily Sensor Statistics";
            ((System.ComponentModel.ISupportInitialize)(this.sensorsDailyChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox sensorTypeList;
        private System.Windows.Forms.ComboBox firstDateForLog;
        private System.Windows.Forms.ComboBox secondDateForLog;
        private System.Windows.Forms.DataVisualization.Charting.Chart sensorsDailyChart;
        private System.Windows.Forms.Button fillChartBtn;
    }
}