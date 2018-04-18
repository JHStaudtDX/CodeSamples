namespace FindGuid
{
    partial class GetDatGuid
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GetDatGuid));
            this.inputBox = new System.Windows.Forms.TextBox();
            this.guidBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.findGUIDButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.workspaceBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.aIDNameBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(172, 32);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(150, 22);
            this.inputBox.TabIndex = 0;
            // 
            // guidBox
            // 
            this.guidBox.Location = new System.Drawing.Point(110, 145);
            this.guidBox.Name = "guidBox";
            this.guidBox.Size = new System.Drawing.Size(212, 22);
            this.guidBox.TabIndex = 3;
            this.guidBox.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 17);
            this.label1.TabIndex = 1001;
            this.label1.Text = "Enter ArtifactID or Name";
            // 
            // findGUIDButton
            // 
            this.findGUIDButton.Location = new System.Drawing.Point(8, 105);
            this.findGUIDButton.Name = "findGUIDButton";
            this.findGUIDButton.Size = new System.Drawing.Size(314, 23);
            this.findGUIDButton.TabIndex = 2;
            this.findGUIDButton.Text = "Find GUID";
            this.findGUIDButton.UseVisualStyleBackColor = true;
            this.findGUIDButton.Click += new System.EventHandler(this.findGUIDButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 64);
            this.label2.MaximumSize = new System.Drawing.Size(169, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 34);
            this.label2.TabIndex = 10003;
            this.label2.Text = "Enter Workspace ArtifactID or Name";
            // 
            // workspaceBox
            // 
            this.workspaceBox.Location = new System.Drawing.Point(172, 64);
            this.workspaceBox.Name = "workspaceBox";
            this.workspaceBox.Size = new System.Drawing.Size(150, 22);
            this.workspaceBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 1000;
            this.label3.Text = "GUID";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 17);
            this.label4.TabIndex = 10004;
            this.label4.Text = "ArtifactID or Name";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // aIDNameBox
            // 
            this.aIDNameBox.Location = new System.Drawing.Point(172, 178);
            this.aIDNameBox.Name = "aIDNameBox";
            this.aIDNameBox.Size = new System.Drawing.Size(150, 22);
            this.aIDNameBox.TabIndex = 10005;
            // 
            // GetDatGuid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 224);
            this.Controls.Add(this.aIDNameBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.workspaceBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.findGUIDButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.guidBox);
            this.Controls.Add(this.inputBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GetDatGuid";
            this.Text = "Get Dat Guid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.TextBox guidBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button findGUIDButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox workspaceBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox aIDNameBox;
    }
}

