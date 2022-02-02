
namespace WinFormsSquaresGame
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxKeyboardInstructions = new System.Windows.Forms.GroupBox();
            this.labelKeyboardInstructions = new System.Windows.Forms.Label();
            this.groupBoxScreenReader = new System.Windows.Forms.GroupBox();
            this.labelScreenReaderNotes = new System.Windows.Forms.Label();
            this.labelGoal = new System.Windows.Forms.Label();
            this.groupBoxSpeech = new System.Windows.Forms.GroupBox();
            this.labelSpeech = new System.Windows.Forms.Label();
            this.groupBoxKeyboardInstructions.SuspendLayout();
            this.groupBoxScreenReader.SuspendLayout();
            this.groupBoxSpeech.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelGoal
            // 
            this.labelGoal.AutoSize = true;
            this.labelGoal.Location = new System.Drawing.Point(13, 7);
            this.labelGoal.Name = "labelGoal";
            this.labelGoal.Size = new System.Drawing.Size(948, 100);
            this.labelGoal.Text = resources.GetString("labelGoal.Text");
            // 
            // groupBoxKeyboardInstructions
            // 
            this.groupBoxKeyboardInstructions.Controls.Add(this.labelKeyboardInstructions);
            this.groupBoxKeyboardInstructions.Location = new System.Drawing.Point(12, 123);
            this.groupBoxKeyboardInstructions.Name = "groupBoxKeyboardInstructions";
            this.groupBoxKeyboardInstructions.Size = new System.Drawing.Size(949, 377);
            this.groupBoxKeyboardInstructions.TabIndex = 0;
            this.groupBoxKeyboardInstructions.TabStop = false;
            this.groupBoxKeyboardInstructions.Text = "Keyboard Instructions";
            // 
            // labelKeyboardInstructions
            // 
            this.labelKeyboardInstructions.AutoSize = true;
            this.labelKeyboardInstructions.Location = new System.Drawing.Point(19, 37);
            this.labelKeyboardInstructions.Name = "labelKeyboardInstructions";
            this.labelKeyboardInstructions.Size = new System.Drawing.Size(887, 325);
            this.labelKeyboardInstructions.TabIndex = 0;
            this.labelKeyboardInstructions.Text = resources.GetString("labelKeyboardInstructions.Text");
            // 
            // groupBoxScreenReader
            // 
            this.groupBoxScreenReader.Controls.Add(this.labelScreenReaderNotes);
            this.groupBoxScreenReader.Location = new System.Drawing.Point(12, 514);
            this.groupBoxScreenReader.Name = "groupBoxScreenReader";
            this.groupBoxScreenReader.Size = new System.Drawing.Size(949, 93);
            this.groupBoxScreenReader.TabStop = false;
            this.groupBoxScreenReader.Text = "Screen Readers";
            // 
            // labelScreenReaderNotes
            // 
            this.labelScreenReaderNotes.AutoSize = true;
            this.labelScreenReaderNotes.Location = new System.Drawing.Point(19, 27);
            this.labelScreenReaderNotes.Name = "labelScreenReaderNotes";
            this.labelScreenReaderNotes.Size = new System.Drawing.Size(905, 50);
            this.labelScreenReaderNotes.Text = resources.GetString("labelScreenReaderNotes.Text");
            // 
            // groupBoxSpeech
            // 
            this.groupBoxSpeech.Controls.Add(this.labelSpeech);
            this.groupBoxSpeech.Location = new System.Drawing.Point(12, 616);
            this.groupBoxSpeech.Name = "groupBoxSpeech";
            this.groupBoxSpeech.Size = new System.Drawing.Size(949, 69);
            this.groupBoxSpeech.TabStop = false;
            this.groupBoxSpeech.Text = "Speech";
            // 
            // labelSpeech
            // 
            this.labelSpeech.AutoSize = true;
            this.labelSpeech.Location = new System.Drawing.Point(19, 27);
            this.labelSpeech.Name = "labelSpeech";
            this.labelSpeech.Size = new System.Drawing.Size(917, 25);
            this.labelSpeech.Text = "To click one of the squares shown in the app using speech input, say \"Click\" foll" +
    "owed by the number of the square.";
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(854, 698);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(112, 34);
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonClose;
            this.ClientSize = new System.Drawing.Size(978, 744);
            this.Controls.Add(this.labelGoal);
            this.Controls.Add(this.groupBoxKeyboardInstructions);
            this.Controls.Add(this.groupBoxScreenReader);
            this.Controls.Add(this.groupBoxSpeech);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Squares Game Help";
            this.groupBoxKeyboardInstructions.ResumeLayout(false);
            this.groupBoxKeyboardInstructions.PerformLayout();
            this.groupBoxScreenReader.ResumeLayout(false);
            this.groupBoxScreenReader.PerformLayout();
            this.groupBoxSpeech.ResumeLayout(false);
            this.groupBoxSpeech.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxKeyboardInstructions;
        private System.Windows.Forms.Label labelKeyboardInstructions;
        private System.Windows.Forms.GroupBox groupBoxScreenReader;
        private System.Windows.Forms.Label labelScreenReaderNotes;
        private System.Windows.Forms.Label labelGoal;
        private System.Windows.Forms.GroupBox groupBoxSpeech;
        private System.Windows.Forms.Label labelSpeech;
    }
}