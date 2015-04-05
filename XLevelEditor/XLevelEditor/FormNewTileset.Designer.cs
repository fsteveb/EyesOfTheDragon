namespace XLevelEditor
{
    partial class FormNewTileset
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.mtbTileHeight = new System.Windows.Forms.MaskedTextBox();
            this.mtbTileWidth = new System.Windows.Forms.MaskedTextBox();
            this.tbTilesetImage = new System.Windows.Forms.TextBox();
            this.tbTilesetName = new System.Windows.Forms.TextBox();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.lblTileWidth = new System.Windows.Forms.Label();
            this.lblTilesetImageName = new System.Windows.Forms.Label();
            this.lblTilesetName = new System.Windows.Forms.Label();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(184, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(81, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // mtbTileHeight
            // 
            this.mtbTileHeight.Location = new System.Drawing.Point(117, 94);
            this.mtbTileHeight.Mask = "000";
            this.mtbTileHeight.Name = "mtbTileHeight";
            this.mtbTileHeight.Size = new System.Drawing.Size(100, 20);
            this.mtbTileHeight.TabIndex = 3;
            // 
            // mtbTileWidth
            // 
            this.mtbTileWidth.Location = new System.Drawing.Point(117, 63);
            this.mtbTileWidth.Mask = "000";
            this.mtbTileWidth.Name = "mtbTileWidth";
            this.mtbTileWidth.Size = new System.Drawing.Size(100, 20);
            this.mtbTileWidth.TabIndex = 2;
            // 
            // tbTilesetImage
            // 
            this.tbTilesetImage.Location = new System.Drawing.Point(117, 37);
            this.tbTilesetImage.Name = "tbTilesetImage";
            this.tbTilesetImage.Size = new System.Drawing.Size(199, 20);
            this.tbTilesetImage.TabIndex = 1;
            // 
            // tbTilesetName
            // 
            this.tbTilesetName.Location = new System.Drawing.Point(117, 6);
            this.tbTilesetName.Name = "tbTilesetName";
            this.tbTilesetName.Size = new System.Drawing.Size(199, 20);
            this.tbTilesetName.TabIndex = 0;
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Location = new System.Drawing.Point(50, 97);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(61, 13);
            this.lblTileHeight.TabIndex = 13;
            this.lblTileHeight.Text = "Tile Height:";
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Location = new System.Drawing.Point(53, 66);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(58, 13);
            this.lblTileWidth.TabIndex = 12;
            this.lblTileWidth.Text = "Tile Width:";
            // 
            // lblTilesetImageName
            // 
            this.lblTilesetImageName.AutoSize = true;
            this.lblTilesetImageName.Location = new System.Drawing.Point(7, 40);
            this.lblTilesetImageName.Name = "lblTilesetImageName";
            this.lblTilesetImageName.Size = new System.Drawing.Size(104, 13);
            this.lblTilesetImageName.TabIndex = 11;
            this.lblTilesetImageName.Text = "Tileset Image Name:";
            // 
            // lblTilesetName
            // 
            this.lblTilesetName.AutoSize = true;
            this.lblTilesetName.Location = new System.Drawing.Point(39, 9);
            this.lblTilesetName.Name = "lblTilesetName";
            this.lblTilesetName.Size = new System.Drawing.Size(72, 13);
            this.lblTilesetName.TabIndex = 10;
            this.lblTilesetName.Text = "Tileset Name:";
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(323, 37);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(28, 23);
            this.btnSelectImage.TabIndex = 24;
            this.btnSelectImage.Text = "...";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            // 
            // FormNewTileset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 149);
            this.ControlBox = false;
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.mtbTileHeight);
            this.Controls.Add(this.mtbTileWidth);
            this.Controls.Add(this.tbTilesetImage);
            this.Controls.Add(this.tbTilesetName);
            this.Controls.Add(this.lblTileHeight);
            this.Controls.Add(this.lblTileWidth);
            this.Controls.Add(this.lblTilesetImageName);
            this.Controls.Add(this.lblTilesetName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormNewTileset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Tileset";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.MaskedTextBox mtbTileHeight;
        private System.Windows.Forms.MaskedTextBox mtbTileWidth;
        private System.Windows.Forms.TextBox tbTilesetImage;
        private System.Windows.Forms.TextBox tbTilesetName;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.Label lblTileWidth;
        private System.Windows.Forms.Label lblTilesetImageName;
        private System.Windows.Forms.Label lblTilesetName;
        private System.Windows.Forms.Button btnSelectImage;
    }
}