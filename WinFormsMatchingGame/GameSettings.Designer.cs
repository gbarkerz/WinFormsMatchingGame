
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
            this.labelPictureDataGrid = new System.Windows.Forms.Label();
            this.dataGridViewPictureData = new System.Windows.Forms.DataGridView();
            this.ColumnCardFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxCardPictures.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPictureData)).BeginInit();
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
            this.groupBoxCardPictures.Controls.Add(this.labelPictureDataGrid);
            this.groupBoxCardPictures.Controls.Add(this.dataGridViewPictureData);
            this.groupBoxCardPictures.Location = new System.Drawing.Point(13, 13);
            this.groupBoxCardPictures.Name = "groupBoxCardPictures";
            this.groupBoxCardPictures.Size = new System.Drawing.Size(819, 610);
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
            this.labelYourPicturesInstructions.Size = new System.Drawing.Size(512, 60);
            this.labelYourPicturesInstructions.TabIndex = 3;
            this.labelYourPicturesInstructions.Text = "Please select a folder that contains exactly 8 pictures. \r\nThe picture format nee" +
    "ds to be JPG, PNG, or BMP.";
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
            // labelPictureDataGrid
            // 
            this.labelPictureDataGrid.AutoSize = true;
            this.labelPictureDataGrid.Location = new System.Drawing.Point(69, 333);
            this.labelPictureDataGrid.Name = "labelPictureDataGrid";
            this.labelPictureDataGrid.Size = new System.Drawing.Size(670, 30);
            this.labelPictureDataGrid.TabIndex = 6;
            this.labelPictureDataGrid.Text = "&Information about your pictures. (Columns marked with * are required.)";
            // 
            // dataGridViewPictureData
            // 
            this.dataGridViewPictureData.AllowUserToAddRows = false;
            this.dataGridViewPictureData.AllowUserToDeleteRows = false;
            this.dataGridViewPictureData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPictureData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPictureData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCardFileName,
            this.ColumnName,
            this.ColumnDescription});
            this.dataGridViewPictureData.Location = new System.Drawing.Point(69, 371);
            this.dataGridViewPictureData.Name = "dataGridViewPictureData";
            this.dataGridViewPictureData.RowHeadersVisible = false;
            this.dataGridViewPictureData.RowHeadersWidth = 72;
            this.dataGridViewPictureData.RowTemplate.Height = 37;
            this.dataGridViewPictureData.Size = new System.Drawing.Size(667, 199);
            this.dataGridViewPictureData.StandardTab = true;
            this.dataGridViewPictureData.TabIndex = 7;
            // 
            // ColumnCardFileName
            // 
            this.ColumnCardFileName.HeaderText = "File*";
            this.ColumnCardFileName.MinimumWidth = 9;
            this.ColumnCardFileName.Name = "ColumnCardFileName";
            this.ColumnCardFileName.ReadOnly = true;
            // 
            // ColumnName
            // 
            this.ColumnName.HeaderText = "Name*";
            this.ColumnName.MinimumWidth = 9;
            this.ColumnName.Name = "ColumnName";
            // 
            // ColumnDescription
            // 
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.MinimumWidth = 9;
            this.ColumnDescription.Name = "ColumnDescription";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(701, 637);
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
            this.ClientSize = new System.Drawing.Size(844, 689);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPictureData)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridViewPictureData;
        private System.Windows.Forms.Label labelPictureDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCardFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
    }
}