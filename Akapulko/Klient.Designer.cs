namespace Akapulko
{
    partial class Klient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Klient));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnAutoSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelKlient = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Port:";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(270, 36);
            this.txtIp.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(195, 32);
            this.txtIp.TabIndex = 4;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(270, 81);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(195, 32);
            this.txtPort.TabIndex = 5;
            // 
            // btnConnect
            // 
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Location = new System.Drawing.Point(181, 146);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(144, 78);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Sprawdź połączenie";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Location = new System.Drawing.Point(370, 146);
            this.btnSend.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(139, 78);
            this.btnSend.TabIndex = 7;
            this.btnSend.Text = "Udostępnij ekran";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnAutoSearch
            // 
            this.btnAutoSearch.FlatAppearance.BorderSize = 0;
            this.btnAutoSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoSearch.Location = new System.Drawing.Point(4, 159);
            this.btnAutoSearch.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnAutoSearch.Name = "btnAutoSearch";
            this.btnAutoSearch.Size = new System.Drawing.Size(146, 78);
            this.btnAutoSearch.TabIndex = 8;
            this.btnAutoSearch.Text = "Wyszukaj automatycznie";
            this.btnAutoSearch.UseVisualStyleBackColor = true;
            this.btnAutoSearch.Click += new System.EventHandler(this.btnAutoSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.btnAutoSearch);
            this.panel1.Location = new System.Drawing.Point(-2, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 255);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.labelKlient);
            this.panel2.Location = new System.Drawing.Point(4, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(144, 100);
            this.panel2.TabIndex = 11;
            // 
            // labelKlient
            // 
            this.labelKlient.AutoSize = true;
            this.labelKlient.Font = new System.Drawing.Font("Segoe UI Symbol", 24F, System.Drawing.FontStyle.Bold);
            this.labelKlient.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(45)))));
            this.labelKlient.Location = new System.Drawing.Point(34, 37);
            this.labelKlient.Name = "labelKlient";
            this.labelKlient.Size = new System.Drawing.Size(130, 54);
            this.labelKlient.TabIndex = 10;
            this.labelKlient.Text = "Klient";
            // 
            // Klient
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(44)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(554, 249);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "Klient";
            this.Text = "Klient";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnAutoSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelKlient;
    }
}