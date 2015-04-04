namespace XLevelEditor
{
    partial class FormNewLevel
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
            this.lblLevelName = new System.Windows.Forms.Label();
            this.lblMapName = new System.Windows.Forms.Label();
            this.lblMapWidth = new System.Windows.Forms.Label();
            this.lblMapHeight = new System.Windows.Forms.Label();
            this.tbLevelName = new System.Windows.Forms.TextBox();
            this.tbMapName = new System.Windows.Forms.TextBox();
            this.mtbWidth = new System.Windows.Forms.MaskedTextBox();
            this.mtbHeight = new System.Windows.Forms.MaskedTextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblLevelName
            // 
            this.lblLevelName.AutoSize = true;
            this.lblLevelName.Location = new System.Drawing.Point(11, 28);
            this.lblLevelName.Name = "lblLevelName";
            this.lblLevelName.Size = new System.Drawing.Size(67, 13);
            this.lblLevelName.TabIndex = 0;
            this.lblLevelName.Text = "Level Name:";
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Location = new System.Drawing.Point(16, 59);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(62, 13);
            this.lblMapName.TabIndex = 1;
            this.lblMapName.Text = "Map Name:";
            // 
            // lblMapWidth
            // 
            this.lblMapWidth.AutoSize = true;
            this.lblMapWidth.Location = new System.Drawing.Point(16, 90);
            this.lblMapWidth.Name = "lblMapWidth";
            this.lblMapWidth.Size = new System.Drawing.Size(62, 13);
            this.lblMapWidth.TabIndex = 2;
            this.lblMapWidth.Text = "Map Width:";
            // 
            // lblMapHeight
            // 
            this.lblMapHeight.AutoSize = true;
            this.lblMapHeight.Location = new System.Drawing.Point(13, 121);
            this.lblMapHeight.Name = "lblMapHeight";
            this.lblMapHeight.Size = new System.Drawing.Size(65, 13);
            this.lblMapHeight.TabIndex = 3;
            this.lblMapHeight.Text = "Map Height:";
            // 
            // tbLevelName
            // 
            this.tbLevelName.Location = new System.Drawing.Point(85, 24);
            this.tbLevelName.Name = "tbLevelName";
            this.tbLevelName.Size = new System.Drawing.Size(199, 20);
            this.tbLevelName.TabIndex = 0;
            // 
            // tbMapName
            // 
            this.tbMapName.Location = new System.Drawing.Point(85, 55);
            this.tbMapName.Name = "tbMapName";
            this.tbMapName.Size = new System.Drawing.Size(199, 20);
            this.tbMapName.TabIndex = 1;
            // 
            // mtbWidth
            // 
            this.mtbWidth.Location = new System.Drawing.Point(85, 86);
            this.mtbWidth.Mask = "0000";
            this.mtbWidth.Name = "mtbWidth";
            this.mtbWidth.Size = new System.Drawing.Size(100, 20);
            this.mtbWidth.TabIndex = 2;
            // 
            // mtbHeight
            // 
            this.mtbHeight.Location = new System.Drawing.Point(85, 117);
            this.mtbHeight.Mask = "0000";
            this.mtbHeight.Name = "mtbHeight";
            this.mtbHeight.Size = new System.Drawing.Size(100, 20);
            this.mtbHeight.TabIndex = 3;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(64, 150);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 150);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // FormNewLevel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 185);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.mtbHeight);
            this.Controls.Add(this.mtbWidth);
            this.Controls.Add(this.tbMapName);
            this.Controls.Add(this.tbLevelName);
            this.Controls.Add(this.lblMapHeight);
            this.Controls.Add(this.lblMapWidth);
            this.Controls.Add(this.lblMapName);
            this.Controls.Add(this.lblLevelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormNewLevel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Level";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLevelName;
        private System.Windows.Forms.Label lblMapName;
        private System.Windows.Forms.Label lblMapWidth;
        private System.Windows.Forms.Label lblMapHeight;
        private System.Windows.Forms.TextBox tbLevelName;
        private System.Windows.Forms.TextBox tbMapName;
        private System.Windows.Forms.MaskedTextBox mtbWidth;
        private System.Windows.Forms.MaskedTextBox mtbHeight;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}