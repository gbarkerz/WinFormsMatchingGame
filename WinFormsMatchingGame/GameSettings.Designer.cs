
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameSettings));
            this.groupBoxCardPictures = new System.Windows.Forms.GroupBox();
            this.labelCardPicturesInstructions = new System.Windows.Forms.Label();
            this.radioButtonPicturesNorthernEngland = new System.Windows.Forms.RadioButton();
            this.radioButtonPicturesYourPictures = new System.Windows.Forms.RadioButton();
            this.labelYourPicturesInstructions = new System.Windows.Forms.Label();
            this.textBoxYourPicturesPath = new System.Windows.Forms.TextBox();
            this.buttonYourPicturesBrowse = new System.Windows.Forms.Button();
            this.labelPictureDataGrid = new System.Windows.Forms.Label();
            this.dataGridViewPictureData = new System.Windows.Forms.DataGridView();
            this.FileFullPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCardFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonSaveClose = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.checkBoxAutoExportDetailsOnSave = new System.Windows.Forms.CheckBox();
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
            this.groupBoxCardPictures.Controls.Add(this.buttonImport);
            this.groupBoxCardPictures.Controls.Add(this.buttonExport);
            this.groupBoxCardPictures.Location = new System.Drawing.Point(11, 9);
            this.groupBoxCardPictures.Name = "groupBoxCardPictures";
            this.groupBoxCardPictures.Size = new System.Drawing.Size(965, 598);
            this.groupBoxCardPictures.TabIndex = 0;
            this.groupBoxCardPictures.TabStop = false;
            this.groupBoxCardPictures.Text = "C&ard pictures";
            // 
            // labelCardPicturesInstructions
            // 
            this.labelCardPicturesInstructions.AutoSize = true;
            this.labelCardPicturesInstructions.Location = new System.Drawing.Point(19, 29);
            this.labelCardPicturesInstructions.Name = "labelCardPicturesInstructions";
            this.labelCardPicturesInstructions.Size = new System.Drawing.Size(497, 25);
            this.labelCardPicturesInstructions.TabIndex = 0;
            this.labelCardPicturesInstructions.Text = "Choose the pictures that you\'d like to be shown on the cards.";
            // 
            // radioButtonPicturesNorthernEngland
            // 
            this.radioButtonPicturesNorthernEngland.AutoSize = true;
            this.radioButtonPicturesNorthernEngland.Checked = true;
            this.radioButtonPicturesNorthernEngland.Location = new System.Drawing.Point(36, 62);
            this.radioButtonPicturesNorthernEngland.Name = "radioButtonPicturesNorthernEngland";
            this.radioButtonPicturesNorthernEngland.Size = new System.Drawing.Size(177, 29);
            this.radioButtonPicturesNorthernEngland.TabIndex = 1;
            this.radioButtonPicturesNorthernEngland.TabStop = true;
            this.radioButtonPicturesNorthernEngland.Text = "&Northern England";
            this.radioButtonPicturesNorthernEngland.UseVisualStyleBackColor = true;
            // 
            // radioButtonPicturesYourPictures
            // 
            this.radioButtonPicturesYourPictures.AutoSize = true;
            this.radioButtonPicturesYourPictures.Location = new System.Drawing.Point(36, 92);
            this.radioButtonPicturesYourPictures.Name = "radioButtonPicturesYourPictures";
            this.radioButtonPicturesYourPictures.Size = new System.Drawing.Size(139, 29);
            this.radioButtonPicturesYourPictures.TabIndex = 2;
            this.radioButtonPicturesYourPictures.Text = "&Your pictures";
            this.radioButtonPicturesYourPictures.UseVisualStyleBackColor = true;
            this.radioButtonPicturesYourPictures.CheckedChanged += new System.EventHandler(this.radioButtonPicturesYourPictures_CheckedChanged);
            // 
            // labelYourPicturesInstructions
            // 
            this.labelYourPicturesInstructions.AutoSize = true;
            this.labelYourPicturesInstructions.Location = new System.Drawing.Point(57, 123);
            this.labelYourPicturesInstructions.Name = "labelYourPicturesInstructions";
            this.labelYourPicturesInstructions.Size = new System.Drawing.Size(897, 75);
            this.labelYourPicturesInstructions.TabIndex = 3;
            this.labelYourPicturesInstructions.Text = resources.GetString("labelYourPicturesInstructions.Text");
            // 
            // textBoxYourPicturesPath
            // 
            this.textBoxYourPicturesPath.AccessibleName = "Your pictures folder";
            this.textBoxYourPicturesPath.Location = new System.Drawing.Point(57, 214);
            this.textBoxYourPicturesPath.Name = "textBoxYourPicturesPath";
            this.textBoxYourPicturesPath.Size = new System.Drawing.Size(760, 31);
            this.textBoxYourPicturesPath.TabIndex = 4;
            // 
            // buttonYourPicturesBrowse
            // 
            this.buttonYourPicturesBrowse.Location = new System.Drawing.Point(835, 208);
            this.buttonYourPicturesBrowse.Name = "buttonYourPicturesBrowse";
            this.buttonYourPicturesBrowse.Size = new System.Drawing.Size(109, 42);
            this.buttonYourPicturesBrowse.TabIndex = 5;
            this.buttonYourPicturesBrowse.Text = "&Browse";
            this.buttonYourPicturesBrowse.UseVisualStyleBackColor = true;
            this.buttonYourPicturesBrowse.Click += new System.EventHandler(this.buttonYourPicturesBrowse_Click);
            // 
            // labelPictureDataGrid
            // 
            this.labelPictureDataGrid.AutoSize = true;
            this.labelPictureDataGrid.Location = new System.Drawing.Point(57, 258);
            this.labelPictureDataGrid.Name = "labelPictureDataGrid";
            this.labelPictureDataGrid.Size = new System.Drawing.Size(480, 25);
            this.labelPictureDataGrid.TabIndex = 6;
            this.labelPictureDataGrid.Text = "Your Pictures &Details. (Columns marked with * are required.)";
            // 
            // dataGridViewPictureData
            // 
            this.dataGridViewPictureData.AccessibleName = "Your Picture Details";
            this.dataGridViewPictureData.AllowUserToAddRows = false;
            this.dataGridViewPictureData.AllowUserToDeleteRows = false;
            this.dataGridViewPictureData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPictureData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPictureData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileFullPath,
            this.ColumnCardFileName,
            this.ColumnName,
            this.ColumnDescription});
            this.dataGridViewPictureData.Location = new System.Drawing.Point(57, 289);
            this.dataGridViewPictureData.MultiSelect = false;
            this.dataGridViewPictureData.Name = "dataGridViewPictureData";
            this.dataGridViewPictureData.RowHeadersVisible = false;
            this.dataGridViewPictureData.RowHeadersWidth = 72;
            this.dataGridViewPictureData.RowTemplate.Height = 37;
            this.dataGridViewPictureData.ShowCellToolTips = false;
            this.dataGridViewPictureData.Size = new System.Drawing.Size(891, 229);
            this.dataGridViewPictureData.StandardTab = true;
            this.dataGridViewPictureData.TabIndex = 7;
            // 
            // FileFullPath
            // 
            this.FileFullPath.HeaderText = "File Full Path";
            this.FileFullPath.MinimumWidth = 9;
            this.FileFullPath.Name = "FileFullPath";
            this.FileFullPath.ReadOnly = true;
            this.FileFullPath.Visible = false;
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
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(54, 537);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(291, 41);
            this.buttonImport.TabIndex = 8;
            this.buttonImport.Text = "&Import Names and Descriptions";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(364, 537);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(291, 41);
            this.buttonExport.TabIndex = 9;
            this.buttonExport.Text = "&Export Names and Descriptions";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonSaveClose
            // 
            this.buttonSaveClose.Location = new System.Drawing.Point(709, 658);
            this.buttonSaveClose.Name = "buttonSaveClose";
            this.buttonSaveClose.Size = new System.Drawing.Size(166, 42);
            this.buttonSaveClose.TabIndex = 2;
            this.buttonSaveClose.Text = "&Save and Close";
            this.buttonSaveClose.UseVisualStyleBackColor = true;
            this.buttonSaveClose.Click += new System.EventHandler(this.buttonSaveClose_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(881, 658);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(109, 42);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // checkBoxAutoExportDetailsOnSave
            // 
            this.checkBoxAutoExportDetailsOnSave.AutoSize = true;
            this.checkBoxAutoExportDetailsOnSave.Location = new System.Drawing.Point(12, 617);
            this.checkBoxAutoExportDetailsOnSave.Name = "checkBoxAutoExportDetailsOnSave";
            this.checkBoxAutoExportDetailsOnSave.Size = new System.Drawing.Size(701, 29);
            this.checkBoxAutoExportDetailsOnSave.TabIndex = 4;
            this.checkBoxAutoExportDetailsOnSave.Text = "E&xport names and descriptions to the file \"MatchingGamePictureDetails.txt\" on sa" +
    "ve.";
            this.checkBoxAutoExportDetailsOnSave.UseVisualStyleBackColor = true;
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1002, 712);
            this.Controls.Add(this.checkBoxAutoExportDetailsOnSave);
            this.Controls.Add(this.groupBoxCardPictures);
            this.Controls.Add(this.buttonSaveClose);
            this.Controls.Add(this.buttonCancel);
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
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxCardPictures;
        private System.Windows.Forms.TextBox textBoxYourPicturesPath;
        private System.Windows.Forms.Button buttonYourPicturesBrowse;
        private System.Windows.Forms.RadioButton radioButtonPicturesYourPictures;
        private System.Windows.Forms.RadioButton radioButtonPicturesNorthernEngland;
        private System.Windows.Forms.Button buttonSaveClose;
        private System.Windows.Forms.Label labelYourPicturesInstructions;
        private System.Windows.Forms.Label labelCardPicturesInstructions;
        private System.Windows.Forms.DataGridView dataGridViewPictureData;
        private System.Windows.Forms.Label labelPictureDataGrid;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileFullPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCardFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.CheckBox checkBoxAutoExportDetailsOnSave;
    }
}