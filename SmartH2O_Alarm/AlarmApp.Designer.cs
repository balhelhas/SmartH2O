namespace SmartH2O_Alarm
{
    partial class AlarmApp
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
            this.textBoxSensorsReadings = new System.Windows.Forms.TextBox();
            this.buttonReloadTriggers = new System.Windows.Forms.Button();
            this.buttonPauseTriggers = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTriggerRules = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAlarmsTriggered = new System.Windows.Forms.TextBox();
            this.textBoxActiveTriggers = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxCondition = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxSensorsReadings
            // 
            this.textBoxSensorsReadings.Location = new System.Drawing.Point(12, 100);
            this.textBoxSensorsReadings.Multiline = true;
            this.textBoxSensorsReadings.Name = "textBoxSensorsReadings";
            this.textBoxSensorsReadings.ReadOnly = true;
            this.textBoxSensorsReadings.Size = new System.Drawing.Size(374, 138);
            this.textBoxSensorsReadings.TabIndex = 0;
            // 
            // buttonReloadTriggers
            // 
            this.buttonReloadTriggers.Location = new System.Drawing.Point(26, 29);
            this.buttonReloadTriggers.Name = "buttonReloadTriggers";
            this.buttonReloadTriggers.Size = new System.Drawing.Size(135, 23);
            this.buttonReloadTriggers.TabIndex = 1;
            this.buttonReloadTriggers.Text = "Reload Triggers";
            this.buttonReloadTriggers.UseVisualStyleBackColor = true;
            this.buttonReloadTriggers.Click += new System.EventHandler(this.buttonReloadTriggers_Click);
            // 
            // buttonPauseTriggers
            // 
            this.buttonPauseTriggers.Location = new System.Drawing.Point(167, 29);
            this.buttonPauseTriggers.Name = "buttonPauseTriggers";
            this.buttonPauseTriggers.Size = new System.Drawing.Size(138, 23);
            this.buttonPauseTriggers.TabIndex = 2;
            this.buttonPauseTriggers.Text = "Pause Triggers";
            this.buttonPauseTriggers.UseVisualStyleBackColor = true;
            this.buttonPauseTriggers.Click += new System.EventHandler(this.buttonPauseTriggers_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Latest data received:";
            // 
            // textBoxTriggerRules
            // 
            this.textBoxTriggerRules.Location = new System.Drawing.Point(392, 100);
            this.textBoxTriggerRules.Multiline = true;
            this.textBoxTriggerRules.Name = "textBoxTriggerRules";
            this.textBoxTriggerRules.ReadOnly = true;
            this.textBoxTriggerRules.Size = new System.Drawing.Size(584, 497);
            this.textBoxTriggerRules.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(389, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Trigger rules:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Trigger status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(119, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Enabled";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Alarms triggered:";
            // 
            // textBoxAlarmsTriggered
            // 
            this.textBoxAlarmsTriggered.Location = new System.Drawing.Point(16, 294);
            this.textBoxAlarmsTriggered.Multiline = true;
            this.textBoxAlarmsTriggered.Name = "textBoxAlarmsTriggered";
            this.textBoxAlarmsTriggered.Size = new System.Drawing.Size(370, 303);
            this.textBoxAlarmsTriggered.TabIndex = 9;
            // 
            // textBoxActiveTriggers
            // 
            this.textBoxActiveTriggers.Location = new System.Drawing.Point(120, 249);
            this.textBoxActiveTriggers.Name = "textBoxActiveTriggers";
            this.textBoxActiveTriggers.Size = new System.Drawing.Size(100, 22);
            this.textBoxActiveTriggers.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Active triggers:";
            // 
            // textBoxCondition
            // 
            this.textBoxCondition.Location = new System.Drawing.Point(392, 12);
            this.textBoxCondition.Multiline = true;
            this.textBoxCondition.Name = "textBoxCondition";
            this.textBoxCondition.Size = new System.Drawing.Size(584, 48);
            this.textBoxCondition.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 609);
            this.Controls.Add(this.textBoxCondition);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxActiveTriggers);
            this.Controls.Add(this.textBoxAlarmsTriggered);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTriggerRules);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonPauseTriggers);
            this.Controls.Add(this.buttonReloadTriggers);
            this.Controls.Add(this.textBoxSensorsReadings);
            this.Name = "Form1";
            this.Text = "SmartH20 Alarm";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSensorsReadings;
        private System.Windows.Forms.Button buttonReloadTriggers;
        private System.Windows.Forms.Button buttonPauseTriggers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTriggerRules;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAlarmsTriggered;
        private System.Windows.Forms.TextBox textBoxActiveTriggers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxCondition;
    }
}

