
namespace WinFormsSquaresGame
{
    partial class GameSettings
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.checkBoxShowPicture = new System.Windows.Forms.CheckBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelPicture = new System.Windows.Forms.Label();
            this.checkBoxShowNumbers = new System.Windows.Forms.CheckBox();
            this.groupBoxSquareContents = new System.Windows.Forms.GroupBox();
            this.groupBoxSquareContents.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(401, 282);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(112, 34);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // checkBoxShowPicture
            // 
            this.checkBoxShowPicture.AutoSize = true;
            this.checkBoxShowPicture.Location = new System.Drawing.Point(24, 88);
            this.checkBoxShowPicture.Name = "checkBoxShowPicture";
            this.checkBoxShowPicture.Size = new System.Drawing.Size(141, 29);
            this.checkBoxShowPicture.TabIndex = 1;
            this.checkBoxShowPicture.Text = "&Show picture";
            this.checkBoxShowPicture.UseVisualStyleBackColor = true;
            this.checkBoxShowPicture.CheckedChanged += new System.EventHandler(this.checkBoxShowPicture_CheckedChanged);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(40, 123);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(170, 34);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "&Browse for picture";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelPicture
            // 
            this.labelPicture.AutoSize = true;
            this.labelPicture.Location = new System.Drawing.Point(233, 129);
            this.labelPicture.Name = "labelPicture";
            this.labelPicture.Size = new System.Drawing.Size(0, 25);
            this.labelPicture.TabIndex = 3;
            // 
            // checkBoxShowNumbers
            // 
            this.checkBoxShowNumbers.AutoSize = true;
            this.checkBoxShowNumbers.Location = new System.Drawing.Point(24, 53);
            this.checkBoxShowNumbers.Name = "checkBoxShowNumbers";
            this.checkBoxShowNumbers.Size = new System.Drawing.Size(249, 29);
            this.checkBoxShowNumbers.TabIndex = 0;
            this.checkBoxShowNumbers.Text = "Show &numbers on squares";
            this.checkBoxShowNumbers.UseVisualStyleBackColor = true;
            this.checkBoxShowNumbers.CheckedChanged += new System.EventHandler(this.checkBoxShowNumbers_CheckedChanged);
            // 
            // groupBoxSquareContents
            // 
            this.groupBoxSquareContents.Controls.Add(this.checkBoxShowNumbers);
            this.groupBoxSquareContents.Controls.Add(this.checkBoxShowPicture);
            this.groupBoxSquareContents.Controls.Add(this.buttonBrowse);
            this.groupBoxSquareContents.Controls.Add(this.labelPicture);
            this.groupBoxSquareContents.Location = new System.Drawing.Point(13, 13);
            this.groupBoxSquareContents.Name = "groupBoxSquareContents";
            this.groupBoxSquareContents.Size = new System.Drawing.Size(500, 242);
            this.groupBoxSquareContents.TabIndex = 0;
            this.groupBoxSquareContents.TabStop = false;
            this.groupBoxSquareContents.Text = "Square &Content";
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 328);
            this.Controls.Add(this.groupBoxSquareContents);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Game Settings";
            this.groupBoxSquareContents.ResumeLayout(false);
            this.groupBoxSquareContents.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox checkBoxShowPicture;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelPicture;
        private System.Windows.Forms.CheckBox checkBoxShowNumbers;
        private System.Windows.Forms.GroupBox groupBoxSquareContents;
    }
}