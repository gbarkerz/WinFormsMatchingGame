using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WinFormsMatchingGame.Properties;

namespace WinFormsMatchingGame
{
    // Todo: Localize all the player-facing strings below.

    // This class supports the Settings feature in the WinFormsMatchingGame app.
    // The DataGridView shown in the Settings window has four columns. The first
    // is a hidden column, used as a convenient place to store the full path for
    // each of the player's own pictures. The other three columns are visible, 
    // and show the file name (without path), and the player-supplied accessible
    // name and optional accessible description for the picture.

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
                    if (!String.IsNullOrWhiteSpace(Settings.Default[settingName].ToString()))
                    {
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

            checkBoxAutoExportDetailsOnSave.Checked = Settings.Default.AutoExportDetailsOnSave;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }

        private void GameSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Always query whether Settings changes are to be lost.
            if (AreSettingsChanged())
            {
                var result = MessageBox.Show(
                    this,
                    Resources.ResourceManager.GetString("SettingsChangeWarning"),
                    Resources.ResourceManager.GetString("SettingsTitle"),
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        // Has anything changed in the Settings window since the window was last loaded?
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
                settingsAreChanged = (Settings.Default.AutoExportDetailsOnSave !=
                                        checkBoxAutoExportDetailsOnSave.Checked);
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
                        var description = GetPictureDescription(i);

                        settingName = "Card" + (i + 1) + "Description";
                        settingsAreChanged = (Settings.Default[settingName].ToString() != description);
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
            // Do now permit invalid settings to be saved.
            if (AreSettingsDataValid())
            {
                // The Settings are vald.
                SaveCurrentSettings();

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
        }

        private bool AreSettingsDataValid()
        {
            // Only verify the the Your Pictures data is valid if that data will be used.
            if (radioButtonPicturesNorthernEngland.Checked)
            {
                return true;
            }

            // First check there are exactly the required number of pictures available.
            bool settingsAreValid = formMatchingGame.IsPicturePathValid(textBoxYourPicturesPath.Text);
            if (settingsAreValid)
            {
                // Now check all the required picture data have been supplied.
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

                        MessageBox.Show(
                            this,
                            Resources.ResourceManager.GetString("PictureNamesWarning"),
                            Resources.ResourceManager.GetString("SettingsTitle"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        // Move keyboard focus to where it seems most helpful to supply the missing data.
                        if ((dataGridViewPictureData.Rows[i].Cells[1] == null) ||
                            (dataGridViewPictureData.Rows[i].Cells[1].Value == null) ||
                            String.IsNullOrWhiteSpace(dataGridViewPictureData.Rows[i].Cells[1].Value.ToString()))
                        {
                            // A file path is missing, so move focus to the picture folder field.
                            textBoxYourPicturesPath.Focus();
                        }
                        else if ((dataGridViewPictureData.Rows[i].Cells[2] == null) ||
                            (dataGridViewPictureData.Rows[i].Cells[2].Value == null) ||
                            String.IsNullOrWhiteSpace(dataGridViewPictureData.Rows[i].Cells[2].Value.ToString()))
                        {
                            // A name is missing, so move focus to a name field in the grid.
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
            // Persist all the data in the Settings window.
            Settings.Default.UseYourPictures = radioButtonPicturesYourPictures.Checked;
            Settings.Default.YourPicturesPath = textBoxYourPicturesPath.Text;

            for (int i = 0; i < cardPairCount; i++)
            {
                string settingName = "Card" + (i + 1) + "Path";
                Settings.Default[settingName] = dataGridViewPictureData.Rows[i].Cells[0].Value;

                settingName = "Card" + (i + 1) + "Name";
                Settings.Default[settingName] = dataGridViewPictureData.Rows[i].Cells[2].Value;

                settingName = "Card" + (i + 1) + "Description";
                var description = GetPictureDescription(i);
                Settings.Default[settingName] = description;
            }

            Settings.Default.AutoExportDetailsOnSave = checkBoxAutoExportDetailsOnSave.Checked;

            Settings.Default.Save();

            if (Settings.Default.UseYourPictures &&
                Settings.Default.AutoExportDetailsOnSave)
            {
                if (dataGridViewPictureData.Rows.Count > 0)
                {
                    FileInfo fileInfo = new FileInfo(dataGridViewPictureData.Rows[0].Cells[0].Value.ToString());
                    string exportFile = fileInfo.DirectoryName + "\\" +
                        Resources.ResourceManager.GetString("ImportExportDefaultFileName") +
                        ".txt";

                    StreamWriter streamWriter = null;
                    if ((streamWriter = new StreamWriter(exportFile)) != null)
                    {
                        ExportDetails(streamWriter);
                        streamWriter.Close();
                    }
                }
            }
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

                    // Load up accessible details if we can find them in the folder.
                    string importFile = folderBrowserDialog.SelectedPath + "\\" +
                        Resources.ResourceManager.GetString("ImportExportDefaultFileName") +
                        ".txt";
                    StreamReader streamReader = null;

                    try
                    {
                        if ((streamReader = new StreamReader(importFile)) != null)
                        {
                            string content = null;
                            while ((content = streamReader.ReadLine()) != null)
                            {
                                SetNameDescription(content);
                            }

                            streamReader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewPictureData.Rows.Count < cardPairCount)
            {
                MessageBox.Show(
                    this,
                    Resources.ResourceManager.GetString("ExportWarning"),
                    Resources.ResourceManager.GetString("SettingsTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var saveDlg = new SaveFileDialog();
            saveDlg.Title = Resources.ResourceManager.GetString("ExportDlgTitle");
            saveDlg.FileName = Resources.ResourceManager.GetString("MatchingPictureDetails");
            saveDlg.DefaultExt = "txt";
            saveDlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveDlg.FilterIndex = 1;
            saveDlg.OverwritePrompt = true;

            FileInfo fileInfo = new FileInfo(dataGridViewPictureData.Rows[0].Cells[0].Value.ToString());
            saveDlg.InitialDirectory = fileInfo.DirectoryName;

            DialogResult result = saveDlg.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Stream stream = null;
                if ((stream = saveDlg.OpenFile()) != null)
                {
                    var streamWriter = new StreamWriter(stream);
                    ExportDetails(streamWriter);
                    streamWriter.Close();
                }
            }
        }

        private void ExportDetails(StreamWriter streamWriter)
        {
            string fullContent = "";

            try
            {
                for (int i = 0; i < cardPairCount; i++)
                {
                    var fileName = dataGridViewPictureData.Rows[i].Cells[1].Value.ToString();
                    var name = dataGridViewPictureData.Rows[i].Cells[2].Value.ToString();

                    string description = GetPictureDescription(i);

                    fullContent += fileName + "\t" + name + "\t" + description + "\r\n";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            streamWriter.Write(fullContent);
        }

        private string GetPictureDescription(int rowIndex)
        {
            string description = "";
            if ((dataGridViewPictureData.Rows[rowIndex].Cells[3] != null) &&
                (dataGridViewPictureData.Rows[rowIndex].Cells[3].Value != null))
            {
                description = dataGridViewPictureData.Rows[rowIndex].Cells[3].Value.ToString();
            }

            return description;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            if (dataGridViewPictureData.Rows.Count < cardPairCount)
            {
                MessageBox.Show(
                    this,
                    Resources.ResourceManager.GetString("ImportWarning"),
                    Resources.ResourceManager.GetString("SettingsTitle"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            var openDlg = new OpenFileDialog();
            openDlg.Title = Resources.ResourceManager.GetString("ImportDlgTitle");
            openDlg.FileName = Resources.ResourceManager.GetString("MatchingPictureDetails");
            openDlg.DefaultExt = "txt";
            openDlg.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openDlg.FilterIndex = 1;

            FileInfo fileInfo = new FileInfo(dataGridViewPictureData.Rows[0].Cells[0].Value.ToString());
            openDlg.InitialDirectory = fileInfo.DirectoryName;

            DialogResult result = openDlg.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                Stream stream = null;
                try
                {
                    if ((stream = openDlg.OpenFile()) != null)
                    {
                        var streamReader = new StreamReader(stream);

                        string content = null;
                        while ((content = streamReader.ReadLine()) != null)
                        {
                            SetNameDescription(content);
                        }

                        streamReader.Close();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        // Note that no feedback is presented to the player if the picture details
        // don't match the current list of loaded pictures.
        private void SetNameDescription(string content)
        {
            var fileNameDelimiter = content.IndexOf('\t');
            string fileName = content.Substring(0, fileNameDelimiter);

            for (int i = 0; i < cardPairCount; i++)
            {
                if (fileName == dataGridViewPictureData.Rows[i].Cells[1].Value.ToString())
                {
                    string details = content.Substring(fileNameDelimiter + 1);

                    var nameDelimiter = details.IndexOf('\t');
                    string name = details.Substring(0, nameDelimiter);
                    string description = details.Substring(nameDelimiter + 1);

                    dataGridViewPictureData.Rows[i].Cells[2].Value = name;
                    dataGridViewPictureData.Rows[i].Cells[3].Value = description;

                    break;
                }
            }
        }
    }
}
