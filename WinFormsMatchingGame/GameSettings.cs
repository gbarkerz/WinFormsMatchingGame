using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// Barker notes: Steps to creating this dialog.
// - Add instructions and requirements as labels in the dialog.
// - Add keyboard shortcuts.
// - After creating UI, before adding code-behind:
//  -- Remove TabIndex.
//  -- Reorder the Add() calls in Group and in Dialog, verify the order with AIWin.
//  -- Run AIWin.
// - Assume the common Folder browse dlg is accessible.


namespace WinFormsMatchingGame
{
    public partial class GameSettings : Form
    {
        private FormMatchingGame formMatchingGame;

        public GameSettings(FormMatchingGame formMatchingGame)
        {
            this.formMatchingGame = formMatchingGame;

            InitializeComponent();

            this.FormClosing += GameSettings_FormClosing;

            radioButtonPicturesYourPictures.Checked = Settings.Default.YourPictures;
            radioButtonPicturesNorthernEngland.Checked = !Settings.Default.YourPictures;

            textBoxYourPicturesPath.Text = Settings.Default.YourPicturesPath;
        }

        private void GameSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (radioButtonPicturesYourPictures.Checked)
            {
                if (!formMatchingGame.IsPicturePathValid(textBoxYourPicturesPath.Text))
                {
                    e.Cancel = true;
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            SaveCurrentSettings();

            this.Close();
        }

        private void SaveCurrentSettings()
        {
            Settings.Default.YourPictures = radioButtonPicturesYourPictures.Checked;
            Settings.Default.YourPicturesPath = textBoxYourPicturesPath.Text;

            Settings.Default.Save();
        }

        private void radioButtonPicturesYourPictures_CheckedChanged(object sender, EventArgs e)
        {
            var useYourPictures = (sender as RadioButton).Checked;

            labelYourPicturesInstructions.Enabled = useYourPictures;
            textBoxYourPicturesPath.Enabled = useYourPictures;
            buttonYourPicturesBrowse.Enabled = useYourPictures;
        }

        private void buttonYourPicturesBrowse_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyPictures;

            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (formMatchingGame.IsPicturePathValid(folderBrowserDialog.SelectedPath))
                {
                    textBoxYourPicturesPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}
