﻿
namespace WinFormsMatchingGame
{
    partial class FormMatchingGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelCardGrid = new System.Windows.Forms.Panel();
            this.buttonTryAgain = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonRestartGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelCardGrid
            // 
            this.panelCardGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCardGrid.Location = new System.Drawing.Point(13, 13);
            this.panelCardGrid.Name = "panelCardGrid";
            this.panelCardGrid.Size = new System.Drawing.Size(551, 452);
            this.panelCardGrid.TabIndex = 0;
            // 
            // buttonTryAgain
            // 
            this.buttonTryAgain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonTryAgain.Enabled = false;
            this.buttonTryAgain.Location = new System.Drawing.Point(13, 484);
            this.buttonTryAgain.Name = "buttonTryAgain";
            this.buttonTryAgain.Size = new System.Drawing.Size(131, 40);
            this.buttonTryAgain.TabIndex = 1;
            this.buttonTryAgain.Text = "&Try Again";
            this.buttonTryAgain.UseVisualStyleBackColor = true;
            this.buttonTryAgain.Click += new System.EventHandler(this.buttonTryAgain_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(432, 484);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(131, 40);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "&Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonRestartGame
            // 
            this.buttonRestartGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestartGame.Location = new System.Drawing.Point(295, 484);
            this.buttonRestartGame.Name = "buttonRestartGame";
            this.buttonRestartGame.Size = new System.Drawing.Size(131, 40);
            this.buttonRestartGame.TabIndex = 3;
            this.buttonRestartGame.Text = "&Restart";
            this.buttonRestartGame.UseVisualStyleBackColor = true;
            this.buttonRestartGame.Click += new System.EventHandler(this.buttonRestartGame_Click);
            // 
            // FormMatchingGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 536);
            this.Controls.Add(this.buttonRestartGame);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonTryAgain);
            this.Controls.Add(this.panelCardGrid);
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "FormMatchingGame";
            this.Text = "Accessible Matching Game";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCardGrid;
        private System.Windows.Forms.Button buttonTryAgain;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonRestartGame;
    }
}

