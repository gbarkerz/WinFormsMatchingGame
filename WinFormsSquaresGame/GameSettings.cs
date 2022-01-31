using System;
using System.IO;
using System.Windows.Forms;
using WinFormsSquaresGame.Controls;

namespace WinFormsSquaresGame
{
    public partial class GameSettings : Form
    {
        private SquaresGrid squaresGrid;

        public GameSettings(SquaresGrid squaresGrid)
        {
            this.squaresGrid = squaresGrid;

            InitializeComponent();

            this.FormClosing += GameSettings_FormClosing;

            LoadSettings();
        }

        private void GameSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Todo: Support a way to cancel changes made at the Settings window.
            Settings1.Default.Save();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadSettings()
        {
            checkBoxShowNumbers.Checked = this.squaresGrid.ShowNumbers;
            comboBoxNumberSize.SelectedIndex = this.squaresGrid.NumberSizeIndex;
            checkBoxShowPicture.Checked = this.squaresGrid.ShowPicture;

            if (string.IsNullOrWhiteSpace(Settings1.Default.BackgroundPicture))
            {
                labelPicture.Text = "<No picture selected>";
            }
            else
            {
                labelPicture.Text = this.squaresGrid.BackgroundPictureFullName;
            }

            checkBoxClickSquareOnEnterPress.Checked = this.squaresGrid.ClickSquareOnEnterPress;
        }

        private void checkBoxShowNumbers_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxShowNumbers = (sender as CheckBox);

            this.squaresGrid.ShowNumbers = checkBoxShowNumbers.Checked;
            Settings1.Default.ShowNumbers = checkBoxShowNumbers.Checked;
        }

        private void comboBoxNumberSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBoxNumberSize = (sender as ComboBox);

            this.squaresGrid.NumberSizeIndex = comboBoxNumberSize.SelectedIndex;
            Settings1.Default.NumberSizeIndex = comboBoxNumberSize.SelectedIndex;
        }

        private void checkBoxShowPicture_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxShowPicture = (sender as CheckBox);

            this.squaresGrid.ShowPicture = checkBoxShowPicture.Checked;
            Settings1.Default.ShowPicture = checkBoxShowPicture.Checked;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select background picture";
            openFileDialog.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
            openFileDialog.Filter = "All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                labelPicture.Text = openFileDialog.FileName;
                this.squaresGrid.BackgroundPictureFullName = openFileDialog.FileName;

                Settings1.Default.BackgroundPicture = openFileDialog.FileName;
            }
        }

        private void checkBoxClickSquareOnEnterPress_CheckedChanged(object sender, EventArgs e)
        {
            var checkBoxClickSquareOnEnterPress = (sender as CheckBox);

            this.squaresGrid.ClickSquareOnEnterPress = checkBoxClickSquareOnEnterPress.Checked;
            Settings1.Default.ClickSquareOnEnterPress = checkBoxClickSquareOnEnterPress.Checked;
        }
    }
}
