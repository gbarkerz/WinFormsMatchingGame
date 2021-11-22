
namespace WinFormsMatchingGame
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
            this.groupBoxCardPictures = new System.Windows.Forms.GroupBox();
            this.labelCardPicturesInstructions = new System.Windows.Forms.Label();
            this.radioButtonPicturesNorthernEngland = new System.Windows.Forms.RadioButton();
            this.radioButtonPicturesYourPictures = new System.Windows.Forms.RadioButton();
            this.labelYourPicturesInstructions = new System.Windows.Forms.Label();
            this.textBoxYourPicturesPath = new System.Windows.Forms.TextBox();
            this.buttonYourPicturesBrowse = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxCardPictures.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxCardPictures
            // 
            this.groupBoxCardPictures.Controls.Add(this.labelCardPicturesInstructions);
            this.groupBoxCardPictures.Controls.Add(this.radioButtonPicturesNorthernEngland);
            this.groupBoxCardPictures.Controls.Add(this.radioButtonPicturesYourPictures);
            this.groupBoxCardPictures.Controls.Add(this.labelYourPicturesInstructions);
            this.groupBoxCardPictures.Controls.Add(this.textBoxYourPicturesPath);
            this.groupBoxCardPictures.Controls.Add(this.buttonYourPicturesBrowse);
            this.groupBoxCardPictures.Location = new System.Drawing.Point(13, 13);
            this.groupBoxCardPictures.Name = "groupBoxCardPictures";
            this.groupBoxCardPictures.Size = new System.Drawing.Size(819, 346);
            this.groupBoxCardPictures.TabIndex = 0;
            this.groupBoxCardPictures.TabStop = false;
            this.groupBoxCardPictures.Text = "C&ard pictures";
            // 
            // labelCardPicturesInstructions
            // 
            this.labelCardPicturesInstructions.AutoSize = true;
            this.labelCardPicturesInstructions.Location = new System.Drawing.Point(22, 49);
            this.labelCardPicturesInstructions.Name = "labelCardPicturesInstructions";
            this.labelCardPicturesInstructions.Size = new System.Drawing.Size(575, 30);
            this.labelCardPicturesInstructions.TabIndex = 0;
            this.labelCardPicturesInstructions.Text = "Choose the pictures that you\'d like to be shown on the cards";
            // 
            // radioButtonPicturesNorthernEngland
            // 
            this.radioButtonPicturesNorthernEngland.AutoSize = true;
            this.radioButtonPicturesNorthernEngland.Checked = true;
            this.radioButtonPicturesNorthernEngland.Location = new System.Drawing.Point(43, 103);
            this.radioButtonPicturesNorthernEngland.Name = "radioButtonPicturesNorthernEngland";
            this.radioButtonPicturesNorthernEngland.Size = new System.Drawing.Size(203, 34);
            this.radioButtonPicturesNorthernEngland.TabIndex = 1;
            this.radioButtonPicturesNorthernEngland.TabStop = true;
            this.radioButtonPicturesNorthernEngland.Text = "&Northern England";
            this.radioButtonPicturesNorthernEngland.UseVisualStyleBackColor = true;
            // 
            // radioButtonPicturesYourPictures
            // 
            this.radioButtonPicturesYourPictures.AutoSize = true;
            this.radioButtonPicturesYourPictures.Location = new System.Drawing.Point(43, 159);
            this.radioButtonPicturesYourPictures.Name = "radioButtonPicturesYourPictures";
            this.radioButtonPicturesYourPictures.Size = new System.Drawing.Size(158, 34);
            this.radioButtonPicturesYourPictures.TabIndex = 2;
            this.radioButtonPicturesYourPictures.Text = "&Your pictures";
            this.radioButtonPicturesYourPictures.UseVisualStyleBackColor = true;
            this.radioButtonPicturesYourPictures.CheckedChanged += new System.EventHandler(this.radioButtonPicturesYourPictures_CheckedChanged);
            // 
            // labelYourPicturesInstructions
            // 
            this.labelYourPicturesInstructions.AutoSize = true;
            this.labelYourPicturesInstructions.Location = new System.Drawing.Point(69, 201);
            this.labelYourPicturesInstructions.Name = "labelYourPicturesInstructions";
            this.labelYourPicturesInstructions.Size = new System.Drawing.Size(726, 60);
            this.labelYourPicturesInstructions.TabIndex = 3;
            this.labelYourPicturesInstructions.Text = "The pictures shown will be 8 randomly chosen from the folder you pick here. \r\nThe" +
    " picture format needs to be JPG, PNG, or BMP.";
            // 
            // textBoxYourPicturesPath
            // 
            this.textBoxYourPicturesPath.Location = new System.Drawing.Point(69, 282);
            this.textBoxYourPicturesPath.Name = "textBoxYourPicturesPath";
            this.textBoxYourPicturesPath.Size = new System.Drawing.Size(515, 35);
            this.textBoxYourPicturesPath.TabIndex = 4;
            // 
            // buttonYourPicturesBrowse
            // 
            this.buttonYourPicturesBrowse.Location = new System.Drawing.Point(605, 279);
            this.buttonYourPicturesBrowse.Name = "buttonYourPicturesBrowse";
            this.buttonYourPicturesBrowse.Size = new System.Drawing.Size(131, 40);
            this.buttonYourPicturesBrowse.TabIndex = 5;
            this.buttonYourPicturesBrowse.Text = "&Browse";
            this.buttonYourPicturesBrowse.UseVisualStyleBackColor = true;
            this.buttonYourPicturesBrowse.Click += new System.EventHandler(this.buttonYourPicturesBrowse_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(701, 398);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(131, 40);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 450);
            this.Controls.Add(this.groupBoxCardPictures);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GameSettings";
            this.groupBoxCardPictures.ResumeLayout(false);
            this.groupBoxCardPictures.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCardPictures;
        private System.Windows.Forms.TextBox textBoxYourPicturesPath;
        private System.Windows.Forms.Button buttonYourPicturesBrowse;
        private System.Windows.Forms.RadioButton radioButtonPicturesYourPictures;
        private System.Windows.Forms.RadioButton radioButtonPicturesNorthernEngland;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelYourPicturesInstructions;
        private System.Windows.Forms.Label labelCardPicturesInstructions;
    }
}