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
            // This app assumes the count of cards is square number. (Currently the count is 16.)
            cardMatchingGrid.CardList = new List<Card>()
            {
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "A smiling man with a red coat and arms outstretched, standing in front of 3 large Daleks and a TARDIS. The Daleks seem to be raised above the grass beneath them, and a slightly cloudy sky is in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Daleks in Blackpool",
                    Description = "A smiling man with a red coat and arms outstretched, standing in front of 3 large Daleks and a TARDIS. The Daleks seem to be raised above the grass beneath them, and a slightly cloudy sky is in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A small brown stone lighthouse, with an upper small balcony and a lower bigger balcony. A covered sitting area is at the base of the lighthouse. In front of the lighthouse in the stone ground are anchor and compass symbols, and in the background is a grey cloudy sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A small brown stone lighthouse, with an upper small balcony and a lower bigger balcony. A covered sitting area is at the base of the lighthouse. In front of the lighthouse in the stone ground are anchor and compass symbols, and in the background is a grey cloudy sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A first-person view looking down on two portions of fish and chips in trays and paper. In the background is a concrete path, and two partially shown shoes.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A first-person view looking down on two portions of fish and chips in trays and paper. In the background is a concrete path, and two partially shown shoes.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "A small, ancient looking hexagonal red stone building, with castellated battlements housing a cross. Arched windows on the sides of the building are bricked-up from the inside. The building sits of grass, with a bay and cloudy sky in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "Cockersand Abbey",
                    Description = "A small, ancient looking hexagonal red stone building, with castellated battlements housing a cross. Arched windows on the sides of the building are bricked-up from the inside. The building sits of grass, with a bay and cloudy sky in the background.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card {
                    Name = "Lythan St Annes Windmill",
                    Description = "A white windmill with a black roof and black sails. The windmill has stairs going up to a black door, and black windows. In the background is a street of houses and a clear, blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "Lythan St Annes Windmill",
                    Description = "A white windmill with a black roof and black sails. The windmill has stairs going up to a black door, and black windows. In the background is a street of houses and a clear, blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "The stone ruins of a large ancient abbey. The abbey has no roof or windows, and the sun shines through the arches from behind the abbey. In the foreground is a lawn housing multiple large stone graves, and in the background is a clear blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Bolton Abbey",
                    Description = "The stone ruins of a large ancient abbey. The abbey has no roof or windows, and the sun shines through the arches from behind the abbey. In the foreground is a lawn housing multiple large stone graves, and in the background is a clear blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "A large stone obelisk in a town square, with buildings and trees nearby. The sun shines on the obelisk, with a slightly cloudy, blue sky in the background. At the base of the obelisk are six red wreaths.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Harrogate Obelisk",
                    Description = "A large stone obelisk in a town square, with buildings and trees nearby. The sun shines on the obelisk, with a slightly cloudy, blue sky in the background. At the base of the obelisk are six red wreaths.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "A small square stone building with an opening on the near side. Above the railing are 15 Greek symbols. Railing surrounds the top of the building, and a sundial sits at the centre of the top. In the background is rocky grassland, with rolling hills in the far background along with a slightly cloudy, blue sky.",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
                new Card {
                    Name = "Hampsfell Hospice",
                    Description = "A small square stone building with an opening on the near side. Above the railing are 15 Greek symbols. Railing surrounds the top of the building, and a sundial sits at the centre of the top. In the background is rocky grassland, with rolling hills in the far background along with a slightly cloudy, blue sky.",
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
