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
// - Be sure to do the above as changes are made to the dialog. (And watch for TabIndex being added back.)
// - Assume the common Folder browse dlg is accessible.
// - Be clear about what an sterirsk means.

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

            for (int i = 0; i < 8; i++)
            {
                string settingName = "Card" + (i + 1) + "Path";
                try
                {
                    if (String.IsNullOrWhiteSpace(Settings.Default[settingName].ToString()))
                    {
                        break;
                    }

                    DirectoryInfo di = new DirectoryInfo(Settings.Default[settingName].ToString());
                    dataGridViewPictureData.Rows.Add(di.Name);

                    settingName = "Card" + (i + 1) + "Name";
                    dataGridViewPictureData.Rows[i].Cells[1].Value = Settings.Default[settingName];

                    settingName = "Card" + (i + 1) + "Description";
                    dataGridViewPictureData.Rows[i].Cells[2].Value = Settings.Default[settingName];
                }
                catch
                {
                    break;
                }
            }
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

            // The Settings for the card file paths has already been persisted.
            for (int i = 0; i < 8; i++)
            {
                string settingName = "Card" + (i + 1) + "Path";
                try
                {
                    if (String.IsNullOrWhiteSpace(Settings.Default[settingName].ToString()))
                    {
                        break;
                    }

                    settingName = "Card" + (i + 1) + "Name";
                    Settings.Default[settingName] = dataGridViewPictureData.Rows[i].Cells[1].Value;

                    settingName = "Card" + (i + 1) + "Description";
                    Settings.Default[settingName] = dataGridViewPictureData.Rows[i].Cells[2].Value;
                }
                catch
                {
                    break;
                }
            }

            Settings.Default.Save();
        }

        private void radioButtonPicturesYourPictures_CheckedChanged(object sender, EventArgs e)
        {
            var useYourPictures = (sender as RadioButton).Checked;

            labelYourPicturesInstructions.Enabled = useYourPictures;
            textBoxYourPicturesPath.Enabled = useYourPictures;
            buttonYourPicturesBrowse.Enabled = useYourPictures;

            labelPictureDataGrid.Enabled = useYourPictures;
            dataGridViewPictureData.Enabled = useYourPictures;

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

                    DirectoryInfo di = new DirectoryInfo(textBoxYourPicturesPath.Text);

                    string[] extensions = { ".jpg", ".png", ".bmp" };

                    var files = di.EnumerateFiles("*", SearchOption.TopDirectoryOnly)
                                    .Where(f => extensions.Contains(f.Extension.ToLower()))
                                    .ToArray();
                    for (int i = 0; i < files.Length; i++)
                    {
                        dataGridViewPictureData.Rows.Add(files[i].Name);

                        string settingName = "Card" + (i + 1) + "Path";
                        Settings.Default[settingName] = files[i].FullName;
                    }

                    Settings.Default.Save();
                }
            }
        }
    }
}
