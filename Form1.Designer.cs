namespace MajnLauncher
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.start = new System.Windows.Forms.Button();
            this.login = new System.Windows.Forms.TextBox();
            this.heslo = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.domov = new System.Windows.Forms.Button();
            this.cbVerze = new System.Windows.Forms.ComboBox();
            this.odinstalace = new System.Windows.Forms.Button();
            this.ram = new System.Windows.Forms.NumericUpDown();
            this.snapshoty = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.origo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.ram)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.start.Location = new System.Drawing.Point(17, 155);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(360, 58);
            this.start.TabIndex = 0;
            this.start.Text = "Spustit";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // login
            // 
            this.login.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login.Location = new System.Drawing.Point(104, 8);
            this.login.Name = "login";
            this.login.Size = new System.Drawing.Size(273, 30);
            this.login.TabIndex = 1;
            this.login.TextChanged += new System.EventHandler(this.login_TextChanged);
            // 
            // heslo
            // 
            this.heslo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.heslo.Location = new System.Drawing.Point(104, 44);
            this.heslo.Name = "heslo";
            this.heslo.Size = new System.Drawing.Size(183, 30);
            this.heslo.TabIndex = 2;
            this.heslo.UseSystemPasswordChar = true;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 298);
            this.progressBar1.Maximum = 1000;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(360, 30);
            this.progressBar1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 29);
            this.label2.TabIndex = 8;
            this.label2.Text = "Heslo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 29);
            this.label4.TabIndex = 16;
            this.label4.Text = "Dostupná paměť:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(13, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 29);
            this.label3.TabIndex = 15;
            this.label3.Text = "Verze:";
            // 
            // domov
            // 
            this.domov.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.domov.Location = new System.Drawing.Point(17, 219);
            this.domov.Name = "domov";
            this.domov.Size = new System.Drawing.Size(169, 73);
            this.domov.TabIndex = 14;
            this.domov.Text = "Domovský adresář";
            this.domov.UseVisualStyleBackColor = true;
            this.domov.Click += new System.EventHandler(this.domov_Click);
            // 
            // cbVerze
            // 
            this.cbVerze.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVerze.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbVerze.FormattingEnabled = true;
            this.cbVerze.Location = new System.Drawing.Point(104, 80);
            this.cbVerze.Name = "cbVerze";
            this.cbVerze.Size = new System.Drawing.Size(130, 33);
            this.cbVerze.TabIndex = 13;
            // 
            // odinstalace
            // 
            this.odinstalace.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.odinstalace.ForeColor = System.Drawing.Color.Red;
            this.odinstalace.Location = new System.Drawing.Point(192, 219);
            this.odinstalace.Name = "odinstalace";
            this.odinstalace.Size = new System.Drawing.Size(185, 73);
            this.odinstalace.TabIndex = 12;
            this.odinstalace.Text = "Vymazat Minecraft";
            this.odinstalace.UseVisualStyleBackColor = true;
            this.odinstalace.Click += new System.EventHandler(this.odinstalace_Click);
            // 
            // ram
            // 
            this.ram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ram.Location = new System.Drawing.Point(223, 119);
            this.ram.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.ram.Minimum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.ram.Name = "ram";
            this.ram.Size = new System.Drawing.Size(99, 30);
            this.ram.TabIndex = 11;
            this.ram.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            // 
            // snapshoty
            // 
            this.snapshoty.AutoSize = true;
            this.snapshoty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.snapshoty.Location = new System.Drawing.Point(243, 84);
            this.snapshoty.Name = "snapshoty";
            this.snapshoty.Size = new System.Drawing.Size(137, 29);
            this.snapshoty.TabIndex = 9;
            this.snapshoty.Text = "Snapshoty";
            this.snapshoty.UseVisualStyleBackColor = true;
            this.snapshoty.CheckedChanged += new System.EventHandler(this.snapshoty_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(326, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 29);
            this.label5.TabIndex = 17;
            this.label5.Text = "MB";
            // 
            // origo
            // 
            this.origo.AutoSize = true;
            this.origo.Checked = true;
            this.origo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.origo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.origo.Location = new System.Drawing.Point(293, 49);
            this.origo.Name = "origo";
            this.origo.Size = new System.Drawing.Size(87, 29);
            this.origo.TabIndex = 18;
            this.origo.Text = "Origo";
            this.origo.UseVisualStyleBackColor = true;
            this.origo.CheckedChanged += new System.EventHandler(this.origo_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 340);
            this.Controls.Add(this.origo);
            this.Controls.Add(this.odinstalace);
            this.Controls.Add(this.domov);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ram);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbVerze);
            this.Controls.Add(this.snapshoty);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.heslo);
            this.Controls.Add(this.login);
            this.Controls.Add(this.start);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MajnLauncher";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button start;
        private System.Windows.Forms.TextBox login;
        private System.Windows.Forms.TextBox heslo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button domov;
        private System.Windows.Forms.ComboBox cbVerze;
        private System.Windows.Forms.Button odinstalace;
        private System.Windows.Forms.NumericUpDown ram;
        private System.Windows.Forms.CheckBox snapshoty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox origo;
    }
}

