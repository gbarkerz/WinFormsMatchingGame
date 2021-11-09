using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMatchingGame.Controls;
using WinFormsMatchingGame.Properties;

// TODO:
// Add image descriptions.
// Check exe runs in isolation.
// Code cleanup.
// Create ReadMe file, with prerequisites of .NET 5.

// DataGridViewButtonCell doesn't seem to support an image on the button.
// (Painting the image in CellPainting doesn't seem a very clean way to go.)
// So use DataGridViewButtonCell, and explicitly check for a Space key press.
// Then again, DataGridViewImageCell doesn't support a click, and didn't seem 
// to support being clicked via Windows Speech Recognition, so stick with a
// DataGridViewButtonCell.

// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_Name
// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_HelpText
// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_ValueValue
// https://www.linkedin.com/pulse/common-approaches-enhancing-programmatic-your-win32-winforms-barker/
// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.queryaccessibilityhelp?redirectedfrom=MSDN&view=windowsdesktop-5.0

// Don't bother setting the DataGridViewButtonCellWithCustomName's Description property.
// That doesn't get exposed as UIA clients would expect. (Rather it gets exposed in a
// way to support clients of a legacy Windows accessibility API.)

// Leave the default grid behavior of an Enter press moving to the cell below the focused cell.

namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private CardMatchingGrid cardMatchingGrid;

        public FormMatchingGame()
        {
            InitializeComponent();

            CreateCardMatchingGrid();
        }

        private void CreateCardMatchingGrid()
        {
            cardMatchingGrid = new CardMatchingGrid();

            cardMatchingGrid.AccessibilityObject.Name = Resources.ResourceManager.GetString("CardsForMatching");

            cardMatchingGrid.RowHeadersVisible = false;
            cardMatchingGrid.ColumnHeadersVisible = false;
            cardMatchingGrid.Dock = DockStyle.Fill;
            cardMatchingGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            cardMatchingGrid.AllowUserToAddRows = false;
            cardMatchingGrid.ShowCellToolTips = false;
            cardMatchingGrid.StandardTab = true;
            cardMatchingGrid.AllowUserToResizeRows = false;
            cardMatchingGrid.AllowUserToResizeColumns = false;

            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());

            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();

            SetupCardList();

            this.panelCardGrid.Controls.Add(cardMatchingGrid);
        }

        private void SetupCardList()
        {
            // This app assumes the count of cards is square number. It's currently 16.
            cardMatchingGrid.CardList = new List<Card>()
            {
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "This is a description of Daleks in Blackpool",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "This is a description of Daleks in Blackpool",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "This is a description of Lower Lighthouse in Fleetwood",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "This is a description of Lower Lighthouse in Fleetwood",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "This is a description of Fish and Chips in Cleveleys",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "This is a description of Fish and Chips in Cleveleys",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "This is a description of Cockersand Abbey",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "This is a description of Cockersand Abbey",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "Lythan St Annes Windmill",
                    Description = "This is a descrption of Lythan St Annes Windmill",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "Lythan St Annes Windmill",
                    Description = "This is a descrption of Lythan St Annes Windmill",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "This is a description of Bolton Abbey",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "This is a description of Bolton Abbey",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "This is a description of Harrogate Obelisk",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "This is a description of Harrogate Obelisk",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "This is a description of Hampsfell Hospice",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "This is a description of Hampsfell Hospice",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
            };

            cardMatchingGrid.Shuffle();
        }

        private void buttonTryAgain_Click(object sender, EventArgs e)
        {
            TryAgain();
        }

        private void buttonRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void RestartGame()
        {
            cardMatchingGrid.ResetGrid();

            cardMatchingGrid.Focus();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TryAgain()
        {
            cardMatchingGrid.TryAgain();

            // In the interests of game efficiency, move focus back into the grid now.
            cardMatchingGrid.Focus();
        }
    }
}
