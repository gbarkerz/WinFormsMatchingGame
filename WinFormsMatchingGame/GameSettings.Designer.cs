
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
            this.FileFullPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCardFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonSaveClose = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
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
            this.groupBoxCardPictures.Size = new System.Drawing.Size(858, 610);
            this.groupBoxCardPictures.TabIndex = 0;
            this.groupBoxCardPictures.TabStop = false;
            this.groupBoxCardPictures.Text = "C&ard pictures";
            // 
            // labelCardPicturesInstructions
            // 
            this.labelCardPicturesInstructions.AutoSize = true;
            this.labelCardPicturesInstructions.Location = new System.Drawing.Point(22, 49);
            this.labelCardPicturesInstructions.Name = "labelCardPicturesInstructions";
            this.labelCardPicturesInstructions.Size = new System.Drawing.Size(580, 30);
            this.labelCardPicturesInstructions.TabIndex = 0;
            this.labelCardPicturesInstructions.Text = "Choose the pictures that you\'d like to be shown on the cards.";
            // 
            // radioButtonPicturesNorthernEngland
            // 
            this.radioButtonPicturesNorthernEngland.AutoSize = true;
            this.radioButtonPicturesNorthernEngland.Checked = true;
            this.radioButtonPicturesNorthernEngland.Location = new System.Drawing.Point(43, 103);
            this.radioButtonPicturesNorthernEngland.Name = "radioButtonPicturesNorthernEngland";
            this.radioButtonPicturesNorthernEngland.Size = new System.Drawing.Size(203, 34);
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
            this.radioButtonPicturesYourPictures.Text = "&Your pictures";
            this.radioButtonPicturesYourPictures.UseVisualStyleBackColor = true;
            this.radioButtonPicturesYourPictures.CheckedChanged += new System.EventHandler(this.radioButtonPicturesYourPictures_CheckedChanged);
            // 
            // labelYourPicturesInstructions
            // 
            this.labelYourPicturesInstructions.AutoSize = true;
            this.labelYourPicturesInstructions.Location = new System.Drawing.Point(69, 201);
            this.labelYourPicturesInstructions.Name = "labelYourPicturesInstructions";
            this.labelYourPicturesInstructions.Size = new System.Drawing.Size(787, 60);
            this.labelYourPicturesInstructions.Text = "Please select a &folder that contains exactly 8 pictures. The pictures must be lo" +
    "cated \r\nbeneath your Pictures folder, and the picture format needs to be JPG, PN" +
    "G, or BMP.";
            // 
            // textBoxYourPicturesPath
            // 
            this.textBoxYourPicturesPath.AccessibleName = "Your pictures folder";
            this.textBoxYourPicturesPath.Location = new System.Drawing.Point(69, 282);
            this.textBoxYourPicturesPath.Name = "textBoxYourPicturesPath";
            this.textBoxYourPicturesPath.Size = new System.Drawing.Size(632, 35);
            // 
            // buttonYourPicturesBrowse
            // 
            this.buttonYourPicturesBrowse.Location = new System.Drawing.Point(711, 280);
            this.buttonYourPicturesBrowse.Name = "buttonYourPicturesBrowse";
            this.buttonYourPicturesBrowse.Size = new System.Drawing.Size(131, 40);
            this.buttonYourPicturesBrowse.Text = "&Browse";
            this.buttonYourPicturesBrowse.UseVisualStyleBackColor = true;
            this.buttonYourPicturesBrowse.Click += new System.EventHandler(this.buttonYourPicturesBrowse_Click);
            // 
            // labelPictureDataGrid
            // 
            this.labelPictureDataGrid.AutoSize = true;
            this.labelPictureDataGrid.Location = new System.Drawing.Point(69, 333);
            this.labelPictureDataGrid.Name = "labelPictureDataGrid";
            this.labelPictureDataGrid.Size = new System.Drawing.Size(564, 30);
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
            this.dataGridViewPictureData.Location = new System.Drawing.Point(69, 371);
            this.dataGridViewPictureData.MultiSelect = false;
            this.dataGridViewPictureData.Name = "dataGridViewPictureData";
            this.dataGridViewPictureData.RowHeadersVisible = false;
            this.dataGridViewPictureData.RowHeadersWidth = 72;
            this.dataGridViewPictureData.RowTemplate.Height = 37;
            this.dataGridViewPictureData.ShowCellToolTips = false;
            this.dataGridViewPictureData.Size = new System.Drawing.Size(773, 199);
            this.dataGridViewPictureData.StandardTab = true;
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
            // buttonSaveClose
            // 
            this.buttonSaveClose.Location = new System.Drawing.Point(515, 637);
            this.buttonSaveClose.Name = "buttonSaveClose";
            this.buttonSaveClose.Size = new System.Drawing.Size(199, 40);
            this.buttonSaveClose.Text = "&Save and Close";
            this.buttonSaveClose.UseVisualStyleBackColor = true;
            this.buttonSaveClose.Click += new System.EventHandler(this.buttonSaveClose_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(724, 637);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(131, 40);
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(883, 689);
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
    }
}