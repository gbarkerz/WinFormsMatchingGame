using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// Todo:
// - Change the grid acc name to be not pulled from the preceding label.

namespace WinFormsMatchingGame
{
    public partial class GameSettings : Form
    {
        private FormMatchingGame formMatchingGame;

        // For now, there are exactly 8 different pairs of cards in the game.
        private const int cardPairCount = 8;

        public GameSettings(FormMatchingGame formMatchingGame)
        {
            this.formMatchingGame = formMatchingGame;

            InitializeComponent();

            this.FormClosing += GameSettings_FormClosing;

            LoadCurrentSettings();
        }

        private void LoadCurrentSettings()
        { 
            radioButtonPicturesYourPictures.Checked = Settings.Default.UseYourPictures;
            radioButtonPicturesNorthernEngland.Checked = !Settings.Default.UseYourPictures;

            textBoxYourPicturesPath.Text = Settings.Default.YourPicturesPath;
            if (!String.IsNullOrWhiteSpace(textBoxYourPicturesPath.Text))
            {
                dataGridViewPictureData.Rows.Clear();

                for (int i = 0; i < cardPairCount; i++)
                {
                    string settingName = "Card" + (i + 1) + "Path";
                    if (String.IsNullOrWhiteSpace(Settings.Default[settingName].ToString()))
                    {
                        break;
                    }

                    DirectoryInfo di = new DirectoryInfo(Settings.Default[settingName].ToString());
                    dataGridViewPictureData.Rows.Add(di.FullName);
                    dataGridViewPictureData.Rows[i].Cells[1].Value = di.Name;

                    settingName = "Card" + (i + 1) + "Name";
                    dataGridViewPictureData.Rows[i].Cells[2].Value = Settings.Default[settingName];

                    settingName = "Card" + (i + 1) + "Description";
                    dataGridViewPictureData.Rows[i].Cells[3].Value = Settings.Default[settingName];
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void GameSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AreSettingsChanged())
            {
                var result = MessageBox.Show(
                    this,
                    "Do you want to lose the changes made to the settings?",
                    "Matching Game Settings",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private bool AreSettingsChanged()
        {
            bool settingsAreChanged = (Settings.Default.UseYourPictures != 
                                        radioButtonPicturesYourPictures.Checked);
            if (!settingsAreChanged)
            {
                settingsAreChanged = (Settings.Default.YourPicturesPath != 
                                        textBoxYourPicturesPath.Text);
            }

            if (!settingsAreChanged)
            {
                for (int i = 0; i < cardPairCount; i++)
                {
                    string settingName = "Card" + (i + 1) + "Path";
                    settingsAreChanged = (Settings.Default[settingName] !=
                                            dataGridViewPictureData.Rows[i].Cells[0].Value);
                    if (!settingsAreChanged)
                    {
                        settingName = "Card" + (i + 1) + "Name";
                        settingsAreChanged = (Settings.Default[settingName] !=
                                                dataGridViewPictureData.Rows[i].Cells[2].Value);
                    }

                    if (!settingsAreChanged)
                    {
                        settingName = "Card" + (i + 1) + "Description";
                        settingsAreChanged = (Settings.Default[settingName] !=
                                                dataGridViewPictureData.Rows[i].Cells[3].Value);
                    }

                    if (settingsAreChanged)
                    {
                        break;
                    }
                }
            }

            return settingsAreChanged;
        }

        private void buttonSaveClose_Click(object sender, EventArgs e)
        {
            if (AreSettingsDataValid())
            {
                SaveCurrentSettings();

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }

        private bool AreSettingsDataValid()
        {
            // Only verify the Your Pictures data is that data will be used.
            if (radioButtonPicturesNorthernEngland.Checked)
            {
                return true;
            }

            // First check there are exactly the required number of pictures available.
            bool settingsAreValid = formMatchingGame.IsPicturePathValid(textBoxYourPicturesPath.Text);
            if (settingsAreValid)
            {
                // Now check all required data have been supplied.
                for (int i = 0; i < cardPairCount; i++)
                {
                    if ((dataGridViewPictureData.Rows[i].Cells[0] == null) ||
                        (dataGridViewPictureData.Rows[i].Cells[1] == null) ||
                        (dataGridViewPictureData.Rows[i].Cells[2] == null) ||
                        (dataGridViewPictureData.Rows[i].Cells[0].Value == null) ||
                        (dataGridViewPictureData.Rows[i].Cells[1].Value == null) ||
                        (dataGridViewPictureData.Rows[i].Cells[2].Value == null) ||
                        String.IsNullOrWhiteSpace(dataGridViewPictureData.Rows[i].Cells[0].Value.ToString()) ||
                        String.IsNullOrWhiteSpace(dataGridViewPictureData.Rows[i].Cells[1].Value.ToString()) ||
                        String.IsNullOrWhiteSpace(dataGridViewPictureData.Rows[i].Cells[2].Value.ToString()))
                    {
                        settingsAreValid = false;

                        // Todo: Localize.
                        MessageBox.Show(
                            this,
                            "Please provide 8 named pictures in the Your Picture Details table.",
                            "Matching Game Settings",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        if ((dataGridViewPictureData.Rows[i].Cells[1] == null) ||
                            (dataGridViewPictureData.Rows[i].Cells[1].Value == null) ||
                            String.IsNullOrWhiteSpace(dataGridViewPictureData.Rows[i].Cells[1].Value.ToString()))
                        {
                            textBoxYourPicturesPath.Focus();
                        }
                        else if ((dataGridViewPictureData.Rows[i].Cells[2] == null) ||
                            (dataGridViewPictureData.Rows[i].Cells[2].Value == null) ||
                            String.IsNullOrWhiteSpace(dataGridViewPictureData.Rows[i].Cells[2].Value.ToString()))
                        {
                            dataGridViewPictureData.CurrentCell = dataGridViewPictureData.Rows[i].Cells[2];
                            dataGridViewPictureData.Focus();
                        }

                        break;
                    }
                }
            }

            return settingsAreValid;
        }

        private void SaveCurrentSettings()
        {
            Settings.Default.UseYourPictures = radioButtonPicturesYourPictures.Checked;
            Settings.Default.YourPicturesPath = textBoxYourPicturesPath.Text;

            for (int i = 0; i < cardPairCount; i++)
            {
                string settingName = "Card" + (i + 1) + "Path";
                Settings.Default[settingName] = dataGridViewPictureData.Rows[i].Cells[0].Value;

                settingName = "Card" + (i + 1) + "Name";
                Settings.Default[settingName] = dataGridViewPictureData.Rows[i].Cells[2].Value;

                settingName = "Card" + (i + 1) + "Description";
                Settings.Default[settingName] = dataGridViewPictureData.Rows[i].Cells[3].Value;
            }

            Settings.Default.Save();
        }

        // Only enable the Your Pictures controls when appropriate.
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
                // The selected folder must contain exactly the required number of pictures in it.
                if (formMatchingGame.IsPicturePathValid(folderBrowserDialog.SelectedPath))
                {
                    textBoxYourPicturesPath.Text = folderBrowserDialog.SelectedPath;

                    dataGridViewPictureData.Rows.Clear();

                    DirectoryInfo di = new DirectoryInfo(textBoxYourPicturesPath.Text);

                    string[] extensions = { ".jpg", ".png", ".bmp" };

                    var files = di.EnumerateFiles("*", SearchOption.TopDirectoryOnly)
                                    .Where(f => extensions.Contains(f.Extension.ToLower()))
                                    .ToArray();
                    for (int i = 0; i < files.Length; i++)
                    {
                        dataGridViewPictureData.Rows.Add(files[i].FullName);
                        dataGridViewPictureData.Rows[i].Cells[1].Value = files[i].Name;
                    }
                }
            }
        }
    }
}
