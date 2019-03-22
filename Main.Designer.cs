namespace Blackbox.Server
{
    partial class Main
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
            this.StartStop = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.XmlText = new System.Windows.Forms.TextBox();
            this.Md5Calc = new System.Windows.Forms.TextBox();
            this.Md5In = new System.Windows.Forms.TextBox();
            this.DesText = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.Refresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.DesTextOut = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.XmlTextOut = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Md5Out = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartStop
            // 
            this.StartStop.Location = new System.Drawing.Point(447, 426);
            this.StartStop.Name = "StartStop";
            this.StartStop.Size = new System.Drawing.Size(75, 23);
            this.StartStop.TabIndex = 1;
            this.StartStop.Text = "Start / Stop";
            this.StartStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.StartStop.UseVisualStyleBackColor = true;
            this.StartStop.Click += new System.EventHandler(this.StartStop_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 42);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(510, 378);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.XmlText);
            this.tabPage1.Controls.Add(this.Md5Calc);
            this.tabPage1.Controls.Add(this.Md5In);
            this.tabPage1.Controls.Add(this.DesText);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(502, 352);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Incoming";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // XmlText
            // 
            this.XmlText.Enabled = false;
            this.XmlText.Location = new System.Drawing.Point(256, 23);
            this.XmlText.Multiline = true;
            this.XmlText.Name = "XmlText";
            this.XmlText.Size = new System.Drawing.Size(240, 270);
            this.XmlText.TabIndex = 0;
            // 
            // Md5Calc
            // 
            this.Md5Calc.Enabled = false;
            this.Md5Calc.Location = new System.Drawing.Point(256, 318);
            this.Md5Calc.Multiline = true;
            this.Md5Calc.Name = "Md5Calc";
            this.Md5Calc.Size = new System.Drawing.Size(240, 28);
            this.Md5Calc.TabIndex = 0;
            // 
            // Md5In
            // 
            this.Md5In.Enabled = false;
            this.Md5In.Location = new System.Drawing.Point(9, 318);
            this.Md5In.Multiline = true;
            this.Md5In.Name = "Md5In";
            this.Md5In.Size = new System.Drawing.Size(237, 28);
            this.Md5In.TabIndex = 0;
            // 
            // DesText
            // 
            this.DesText.Enabled = false;
            this.DesText.Location = new System.Drawing.Point(6, 23);
            this.DesText.Multiline = true;
            this.DesText.Name = "DesText";
            this.DesText.Size = new System.Drawing.Size(240, 270);
            this.DesText.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.Md5Out);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.XmlTextOut);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.DesTextOut);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(502, 352);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Outgoing";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(366, 426);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 23);
            this.Refresh.TabIndex = 1;
            this.Refresh.Text = "Update";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "MD5 IN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "MD5 CALC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "3DES";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "XML";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 7);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "3DES";
            // 
            // DesTextOut
            // 
            this.DesTextOut.Enabled = false;
            this.DesTextOut.Location = new System.Drawing.Point(6, 23);
            this.DesTextOut.Multiline = true;
            this.DesTextOut.Name = "DesTextOut";
            this.DesTextOut.Size = new System.Drawing.Size(240, 270);
            this.DesTextOut.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(218, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "XML";
            // 
            // XmlTextOut
            // 
            this.XmlTextOut.Enabled = false;
            this.XmlTextOut.Location = new System.Drawing.Point(256, 23);
            this.XmlTextOut.Multiline = true;
            this.XmlTextOut.Name = "XmlTextOut";
            this.XmlTextOut.Size = new System.Drawing.Size(240, 270);
            this.XmlTextOut.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "MD5 OUT";
            // 
            // Md5Out
            // 
            this.Md5Out.Enabled = false;
            this.Md5Out.Location = new System.Drawing.Point(9, 318);
            this.Md5Out.Multiline = true;
            this.Md5Out.Name = "Md5Out";
            this.Md5Out.Size = new System.Drawing.Size(237, 28);
            this.Md5Out.TabIndex = 8;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 461);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StartStop);
            this.Name = "Main";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button StartStop;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TextBox XmlText;
        public System.Windows.Forms.TextBox Md5Calc;
        public System.Windows.Forms.TextBox Md5In;
        public System.Windows.Forms.TextBox DesText;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox Md5Out;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox XmlTextOut;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox DesTextOut;
    }
}

