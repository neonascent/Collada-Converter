namespace WindowsFormsApplication1
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
            this.button1 = new System.Windows.Forms.Button();
            this.tRCfile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tInputDAE = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tOutputFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tMeshName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tCrysisResourceFolder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(102, 301);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tRCfile
            // 
            this.tRCfile.Location = new System.Drawing.Point(12, 31);
            this.tRCfile.Name = "tRCfile";
            this.tRCfile.Size = new System.Drawing.Size(260, 20);
            this.tRCfile.TabIndex = 1;
            this.tRCfile.Text = "D:\\Crysis Wars\\Bin32\\rc\\rc.exe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "RC file location";
            // 
            // tInputDAE
            // 
            this.tInputDAE.Location = new System.Drawing.Point(12, 84);
            this.tInputDAE.Name = "tInputDAE";
            this.tInputDAE.Size = new System.Drawing.Size(260, 20);
            this.tInputDAE.TabIndex = 3;
            this.tInputDAE.Text = "d:\\temp\\phil.DAE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Input DAE location";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tOutputFolder
            // 
            this.tOutputFolder.Location = new System.Drawing.Point(12, 121);
            this.tOutputFolder.Name = "tOutputFolder";
            this.tOutputFolder.Size = new System.Drawing.Size(260, 20);
            this.tOutputFolder.TabIndex = 5;
            this.tOutputFolder.Text = "d:\\temp\\";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Output Folder";
            // 
            // tMeshName
            // 
            this.tMeshName.Location = new System.Drawing.Point(12, 162);
            this.tMeshName.Name = "tMeshName";
            this.tMeshName.Size = new System.Drawing.Size(260, 20);
            this.tMeshName.TabIndex = 7;
            this.tMeshName.Text = "Phil";
            this.tMeshName.TextChanged += new System.EventHandler(this.tMeshName_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 146);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Mesh Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Crysis Resource Folder";
            // 
            // tCrysisResourceFolder
            // 
            this.tCrysisResourceFolder.Enabled = false;
            this.tCrysisResourceFolder.Location = new System.Drawing.Point(14, 211);
            this.tCrysisResourceFolder.Name = "tCrysisResourceFolder";
            this.tCrysisResourceFolder.Size = new System.Drawing.Size(260, 20);
            this.tCrysisResourceFolder.TabIndex = 9;
            this.tCrysisResourceFolder.Text = "Objects/phil/";
            this.tCrysisResourceFolder.TextChanged += new System.EventHandler(this.tCrysisResourceFolder_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 427);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tCrysisResourceFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tMeshName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tOutputFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tInputDAE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tRCfile);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tRCfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tInputDAE;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tOutputFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tMeshName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tCrysisResourceFolder;
    }
}

