
namespace WinFormsMatchingGame
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
            this.labelScreenReaderNotes = new System.Windows.Forms.Label();
            this.groupBoxScreenReader = new System.Windows.Forms.GroupBox();
            this.labelGoal = new System.Windows.Forms.Label();
            this.groupBoxKeyboardInstructions.SuspendLayout();
            this.groupBoxScreenReader.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(843, 642);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(112, 34);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBoxKeyboardInstructions
            // 
            this.groupBoxKeyboardInstructions.Controls.Add(this.labelKeyboardInstructions);
            this.groupBoxKeyboardInstructions.Location = new System.Drawing.Point(12, 149);
            this.groupBoxKeyboardInstructions.Name = "groupBoxKeyboardInstructions";
            this.groupBoxKeyboardInstructions.Size = new System.Drawing.Size(943, 347);
            this.groupBoxKeyboardInstructions.TabIndex = 1;
            this.groupBoxKeyboardInstructions.TabStop = false;
            this.groupBoxKeyboardInstructions.Text = "Keyboard Instructions";
            // 
            // labelKeyboardInstructions
            // 
            this.labelKeyboardInstructions.AutoSize = true;
            this.labelKeyboardInstructions.Location = new System.Drawing.Point(19, 33);
            this.labelKeyboardInstructions.Name = "labelKeyboardInstructions";
            this.labelKeyboardInstructions.Size = new System.Drawing.Size(884, 300);
            this.labelKeyboardInstructions.TabIndex = 0;
            this.labelKeyboardInstructions.Text = resources.GetString("labelKeyboardInstructions.Text");
            // 
            // labelScreenReaderNotes
            // 
            this.labelScreenReaderNotes.AutoSize = true;
            this.labelScreenReaderNotes.Location = new System.Drawing.Point(19, 37);
            this.labelScreenReaderNotes.Name = "labelScreenReaderNotes";
            this.labelScreenReaderNotes.Size = new System.Drawing.Size(858, 75);
            this.labelScreenReaderNotes.TabIndex = 0;
            this.labelScreenReaderNotes.Text = resources.GetString("labelScreenReaderNotes.Text");
            // 
            // groupBoxScreenReader
            // 
            this.groupBoxScreenReader.Controls.Add(this.labelScreenReaderNotes);
            this.groupBoxScreenReader.Location = new System.Drawing.Point(12, 508);
            this.groupBoxScreenReader.Name = "groupBoxScreenReader";
            this.groupBoxScreenReader.Size = new System.Drawing.Size(943, 125);
            this.groupBoxScreenReader.TabIndex = 2;
            this.groupBoxScreenReader.TabStop = false;
            this.groupBoxScreenReader.Text = "Screen Readers";
            // 
            // labelGoal
            // 
            this.labelGoal.AutoSize = true;
            this.labelGoal.Location = new System.Drawing.Point(13, 13);
            this.labelGoal.Name = "labelGoal";
            this.labelGoal.Size = new System.Drawing.Size(959, 125);
            this.labelGoal.TabIndex = 3;
            this.labelGoal.Text = resources.GetString("labelGoal.Text");
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 684);
            this.Controls.Add(this.labelGoal);
            this.Controls.Add(this.groupBoxScreenReader);
            this.Controls.Add(this.groupBoxKeyboardInstructions);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Matching Game Help";
            this.groupBoxKeyboardInstructions.ResumeLayout(false);
            this.groupBoxKeyboardInstructions.PerformLayout();
            this.groupBoxScreenReader.ResumeLayout(false);
            this.groupBoxScreenReader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBoxKeyboardInstructions;
        private System.Windows.Forms.Label labelKeyboardInstructions;
        private System.Windows.Forms.Label labelScreenReaderNotes;
        private System.Windows.Forms.GroupBox groupBoxScreenReader;
        private System.Windows.Forms.Label labelGoal;
    }
}