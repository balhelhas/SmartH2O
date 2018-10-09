namespace SmartH2O_SeeApp
{
    partial class SeeApp
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
            this.label2 = new System.Windows.Forms.Label();
            this.getSensorsInfoHourly = new System.Windows.Forms.Button();
            this.getSensorsInfoDaily = new System.Windows.Forms.Button();
            this.getSensorsInfoWeekly = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.firstDate = new System.Windows.Forms.ComboBox();
            this.secondDate = new System.Windows.Forms.ComboBox();
            this.alarmInfoBtn = new System.Windows.Forms.Button();
            this.alarmsTriggerd = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label2.Location = new System.Drawing.Point(542, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Sensor\'s Statistics";
            // 
            // getSensorsInfoHourly
            // 
            this.getSensorsInfoHourly.Location = new System.Drawing.Point(535, 57);
            this.getSensorsInfoHourly.Name = "getSensorsInfoHourly";
            this.getSensorsInfoHourly.Size = new System.Drawing.Size(190, 44);
            this.getSensorsInfoHourly.TabIndex = 3;
            this.getSensorsInfoHourly.Text = "Get Sensors Info By Hour";
            this.getSensorsInfoHourly.UseVisualStyleBackColor = true;
            this.getSensorsInfoHourly.Click += new System.EventHandler(this.getSensorsInfoHourly_Click);
            // 
            // getSensorsInfoDaily
            // 
            this.getSensorsInfoDaily.Location = new System.Drawing.Point(535, 117);
            this.getSensorsInfoDaily.Name = "getSensorsInfoDaily";
            this.getSensorsInfoDaily.Size = new System.Drawing.Size(190, 44);
            this.getSensorsInfoDaily.TabIndex = 4;
            this.getSensorsInfoDaily.Text = "Get Sensors Info By Days";
            this.getSensorsInfoDaily.UseVisualStyleBackColor = true;
            this.getSensorsInfoDaily.Click += new System.EventHandler(this.getSensorsInfoDaily_Click);
            // 
            // getSensorsInfoWeekly
            // 
            this.getSensorsInfoWeekly.Location = new System.Drawing.Point(535, 177);
            this.getSensorsInfoWeekly.Name = "getSensorsInfoWeekly";
            this.getSensorsInfoWeekly.Size = new System.Drawing.Size(190, 44);
            this.getSensorsInfoWeekly.TabIndex = 5;
            this.getSensorsInfoWeekly.Text = "Get Sensors Info By Week";
            this.getSensorsInfoWeekly.UseVisualStyleBackColor = true;
            this.getSensorsInfoWeekly.Click += new System.EventHandler(this.getSensorsInfoWeekly_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Alarm\'s Information";
            // 
            // firstDate
            // 
            this.firstDate.FormattingEnabled = true;
            this.firstDate.Location = new System.Drawing.Point(17, 57);
            this.firstDate.Name = "firstDate";
            this.firstDate.Size = new System.Drawing.Size(172, 24);
            this.firstDate.TabIndex = 8;
            this.firstDate.Text = "Select First Date";
            // 
            // secondDate
            // 
            this.secondDate.FormattingEnabled = true;
            this.secondDate.Location = new System.Drawing.Point(17, 87);
            this.secondDate.Name = "secondDate";
            this.secondDate.Size = new System.Drawing.Size(172, 24);
            this.secondDate.TabIndex = 9;
            this.secondDate.Text = "Select Second Date";
            // 
            // alarmInfoBtn
            // 
            this.alarmInfoBtn.Location = new System.Drawing.Point(307, 57);
            this.alarmInfoBtn.Name = "alarmInfoBtn";
            this.alarmInfoBtn.Size = new System.Drawing.Size(202, 54);
            this.alarmInfoBtn.TabIndex = 10;
            this.alarmInfoBtn.Text = "Get Alarms";
            this.alarmInfoBtn.UseVisualStyleBackColor = true;
            this.alarmInfoBtn.Click += new System.EventHandler(this.getAlarmInfo_Click);
            // 
            // alarmsTriggerd
            // 
            this.alarmsTriggerd.Font = new System.Drawing.Font("Calibri", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alarmsTriggerd.Location = new System.Drawing.Point(17, 117);
            this.alarmsTriggerd.Name = "alarmsTriggerd";
            this.alarmsTriggerd.Size = new System.Drawing.Size(492, 332);
            this.alarmsTriggerd.TabIndex = 11;
            this.alarmsTriggerd.Text = "";
            // 
            // SeeApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 480);
            this.Controls.Add(this.alarmsTriggerd);
            this.Controls.Add(this.alarmInfoBtn);
            this.Controls.Add(this.secondDate);
            this.Controls.Add(this.firstDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.getSensorsInfoWeekly);
            this.Controls.Add(this.getSensorsInfoDaily);
            this.Controls.Add(this.getSensorsInfoHourly);
            this.Controls.Add(this.label2);
            this.Name = "SeeApp";
            this.Text = "Smart_H2O";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button getSensorsInfoHourly;
        private System.Windows.Forms.Button getSensorsInfoDaily;
        private System.Windows.Forms.Button getSensorsInfoWeekly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox firstDate;
        private System.Windows.Forms.ComboBox secondDate;
        private System.Windows.Forms.Button alarmInfoBtn;
        private System.Windows.Forms.RichTextBox alarmsTriggerd;
    }
}

