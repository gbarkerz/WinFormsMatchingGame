using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMatchingGame.Properties;

// DataGridViewButtonCell doesn't seem to support an image on the button.
// (Painting the image in CellPainting doesn't seem a very clean way to go.)
// So use DataGridViewImageCell, and explicitly check for a Space key press.

// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_Name
// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_HelpText
// https://docs.microsoft.com/en-us/accessibility-tools-docs/items/WinForms/DataItem_ValueValue
// https://www.linkedin.com/pulse/common-approaches-enhancing-programmatic-your-win32-winforms-barker/
// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.queryaccessibilityhelp?redirectedfrom=MSDN&view=windowsdesktop-5.0

// Don't bother setting the DataGridViewImageCellWithCustomName's Description property.
// That doesn't get exposed as UIA clients would expect. (Rather it gets exposed in a
// way to support clients of a legacy Windows accessibility API.)

namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private CardMatchingGrid cardMatchingGrid = new CardMatchingGrid();

        private DataGridViewImageCellWithCustomName firstCardInPairAttempt;
        private DataGridViewImageCellWithCustomName secondCardInPairAttempt;

        private int tryAgainCount = 0;

        private Shuffler _shuffler;

        public FormMatchingGame()
        {
            InitializeComponent();

            //cardMatchingGrid.FaceDownCellImage = new Bitmap(WinFormsMatchingGame.Properties.Resources.BlankCell);
            cardMatchingGrid.FaceDownCellImage = null;

            cardMatchingGrid.CardList = new List<Card>()
            {
                new Card { 
                    Name = "Daleks in Blackpool", 
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card { 
                    Name = "Daleks in Blackpool",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card { 
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card { 
                    Name = "Lower Lighthouse in Fleetwood",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card { 
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card { 
                    Name = "Fish and Chips in Cleveleys",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card { 
                    Name = "Cockersand Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card { 
                    Name = "Cockersand Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card { 
                    Name = "North Pier in Blackpool",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card { 
                    Name = "North Pier in Blackpool",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card { 
                    Name = "Bolton Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card { 
                    Name = "Bolton Abbey",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card { 
                    Name = "Harrogate Obelisk",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card { 
                    Name = "Harrogate Obelisk",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card { 
                    Name = "Hampsfell Hospice",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
                new Card { 
                    Name = "Hampsfell Hospice",
                    Description = "A",
                    Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
            };

            _shuffler = new Shuffler();
            _shuffler.Shuffle(cardMatchingGrid.CardList);

            cardMatchingGrid.AccessibilityObject.Name = "Cards for matching";

            cardMatchingGrid.RowHeadersVisible = false;
            cardMatchingGrid.ColumnHeadersVisible = false;
            cardMatchingGrid.Dock = DockStyle.Fill;
            cardMatchingGrid.SelectionMode = DataGridViewSelectionMode.CellSelect;
            cardMatchingGrid.AllowUserToAddRows = false;
            cardMatchingGrid.ShowCellToolTips = false;

            cardMatchingGrid.Columns.Add(new DataGridViewImageColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewImageColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewImageColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewImageColumnWithCustomName());

            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();

            cardMatchingGrid.CellClick += DataGridView_CellClick;
            cardMatchingGrid.SizeChanged += Grid_SizeChanged;

            cardMatchingGrid.KeyPress += CardMatchingGrid_KeyPress;

            this.panelCardGrid.Controls.Add(cardMatchingGrid);

            ResizeGridContent();
        }

        private void CardMatchingGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Has a Space been pressed on a cell?
            if (e.KeyChar == 0x20)
            {
                // Consider this a click on the cell.
                if (this.cardMatchingGrid.SelectedCells.Count > 0)
                {
                    var cell = this.cardMatchingGrid.SelectedCells[0];
                    ClickCell(cell.RowIndex, cell.ColumnIndex);
                }
            }
        }
        private void Grid_SizeChanged(object sender, EventArgs e)
        {
            ResizeGridContent();
        }

        private void ResizeGridContent()
        {
            var gridDimensions = cardMatchingGrid.GridDimensions;

            for (int i = 0; i < gridDimensions; ++i)
            {
                cardMatchingGrid.Columns[i].Width = (this.panelCardGrid.ClientSize.Width / gridDimensions) - 1;
                cardMatchingGrid.Rows[i].Height = (this.panelCardGrid.ClientSize.Height / gridDimensions) - 1;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ClickCell(e.RowIndex, e.ColumnIndex);
        }

        private void ClickCell(int rowIndex, int columnIndex)
        { 
            // Take no action while the Try Again button is enabled.
            if (buttonTryAgain.Enabled)
            {
                return;
            }

            // Take no action is the click is on a read-only cell.
            if (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex].ReadOnly)
            {
                return;
            }

            var gridDimensions = cardMatchingGrid.GridDimensions;
            var index = (gridDimensions * rowIndex) + columnIndex;

            // Is this the first card turned over in an attempt to find a pair?
            if (firstCardInPairAttempt == null)
            {
                firstCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewImageCellWithCustomName);
                firstCardInPairAttempt.TurnOver(true);
            }
            else 
            {
                // This must be the second card turned over in an attempt to find a pair.
                // Has a pair been found?

                var firstIndex = firstCardInPairAttempt.GetCardIndex();
                var cardNameFirst = this.cardMatchingGrid.CardList[firstIndex].Name;

                var cardNameSecond = this.cardMatchingGrid.CardList[index].Name;

                secondCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewImageCellWithCustomName);

                if (cardNameFirst == cardNameSecond)
                {
                    var card = this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewImageCellWithCustomName;
                    card .TurnOver(true);

                    this.cardMatchingGrid.CardList[index].Matched = true;

                    this.cardMatchingGrid.CardList[index].Matched = true;
                    this.cardMatchingGrid.CardList[firstIndex].Matched = true;

                    firstCardInPairAttempt = null;
                    secondCardInPairAttempt = null;

                    // Has the game been won?
                    if (GameIsWon())
                    {
                        var answer = MessageBox.Show(
                            this,
                            "Congratulations! You won the game in " +
                            (tryAgainCount + 
                                ((cardMatchingGrid.RowCount * cardMatchingGrid.ColumnCount) / 2)) +
                            " goes.\r\n\r\nWould you like another game?",
                            "Accessible Matching Game",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);
                        if (answer == DialogResult.Yes)
                        {
                            RestartGame();
                        }
                    }

                }
                else
                {
                    secondCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewImageCellWithCustomName);
                    secondCardInPairAttempt.TurnOver(true);

                    buttonTryAgain.Enabled = true;
                }
            }
        }

        private bool GameIsWon()
        {
            for (int i = 0; i < this.cardMatchingGrid.CardList.Count; i++)
            {
                if (!this.cardMatchingGrid.CardList[i].Matched)
                {
                    return false;
                }
            }

            return true;
        }

        private void RestartGame()
        {
            tryAgainCount = 0;

            _shuffler.Shuffle(cardMatchingGrid.CardList);

            // Todo: Get focus working back on the grid.
            //this.cardMatchingGrid.Focus();

            for (int i = 0; i < this.cardMatchingGrid.CardList.Count; i++)
            {
                this.cardMatchingGrid.CardList[i].Matched = false;
            }

            firstCardInPairAttempt = null;
            secondCardInPairAttempt = null;

            var gridDimensions = cardMatchingGrid.GridDimensions;

            for (int r = 0; r < gridDimensions; r++)
            {
                for (int c = 0; c < gridDimensions; c++)
                {
                    var button = (this.cardMatchingGrid.Rows[r].Cells[c] as DataGridViewImageCellWithCustomName);
                    button.TurnOver(false);
                }
            }

            buttonTryAgain.Enabled = false;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTryAgain_Click(object sender, EventArgs e)
        {
            ++tryAgainCount;

            buttonTryAgain.Enabled = false;

            firstCardInPairAttempt.TurnOver(false);
            secondCardInPairAttempt.TurnOver(false);

            firstCardInPairAttempt = null;
            secondCardInPairAttempt = null;
        }

        private void buttonRestartGame_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
    }

    public class CardMatchingGrid : DataGridView
    {
        public List<Card> CardList { get; set; }
        public int CardCountInPlay { get; set; }
        public Bitmap FaceDownCellImage { get; set; }

        public int GridDimensions
        {
            get
            {
                return (int)Math.Sqrt(CardList.Count);
            }
        }
    }

    public class DataGridViewImageColumnWithCustomName : DataGridViewImageColumn
    {
        public DataGridViewImageColumnWithCustomName()
        {
            this.CellTemplate = new DataGridViewImageCellWithCustomName();
        }
    }

    public class DataGridViewImageCellWithCustomName : DataGridViewImageCell
    {
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new DataGridViewImageCellWithCustomNameAccessibleObject(this);
        }

        protected override void OnClick(DataGridViewCellEventArgs e)
        {
            base.OnClick(e);
        }

        public void TurnOver(bool FaceUp)
        {
            ReadOnly = FaceUp;

            var index = GetCardIndex();

            var grid = (this.DataGridView as CardMatchingGrid);

            if (ReadOnly)
            {
                this.ImageLayout = DataGridViewImageCellLayout.Zoom;
                Value = grid.CardList[index].Image;

            }
            else
            {
                this.ImageLayout = DataGridViewImageCellLayout.NotSet;
                Value = grid.FaceDownCellImage;
            }
        }

        public int GetCardIndex()
        {
            var columnCount = (this.DataGridView as CardMatchingGrid).GridDimensions;
            return ((columnCount * this.RowIndex) + this.ColumnIndex);
        }

        protected class DataGridViewImageCellWithCustomNameAccessibleObject :
            DataGridViewImageCellAccessibleObject
        {
            public DataGridViewImageCellWithCustomNameAccessibleObject(DataGridViewImageCellWithCustomName owner) : base(owner)
            {
            }

            public override string Name
            {
                get
                {
                    var cardName = Resources.ResourceManager.GetString("Card") + " " +
                        ((this.Owner as DataGridViewImageCellWithCustomName).GetCardIndex() + 1);

                    // During app development, include the Value in the name.
                    // cardName += ", " + this.Value;

                    return cardName;
                }
            }

            public override string Value 
            {
                get
                {
                    var button = (this.Owner as DataGridViewImageCellWithCustomName);
                    var index = button.GetCardIndex();
                    
                    string value = button.ReadOnly ?
                        (this.Owner.DataGridView as CardMatchingGrid).CardList[index].Name :
                        Resources.ResourceManager.GetString("FaceDown");

                    return value;
                }
            }

            // Don't override the Description property here. Doing so impacts how data
            // gets exposed through a legacy Windows accessibility API, but not how 
            // Windows UI Automation clients expect it to be exposed. So override the 
            // Help property instead, as that maps to the UIA HelpText property.
            public override string Help
            {
                get
                {
                    var button = (this.Owner as DataGridViewImageCellWithCustomName);
                    var index = button.GetCardIndex();

                    // A face down card needs no description.
                    string description = button.ReadOnly ?
                        (this.Owner.DataGridView as CardMatchingGrid).CardList[index].Description :
                        "";

                    return description;
                }
            }

            // This didn't impact the UIA ControlType, which was still a Button.
            //public override AccessibleRole Role
            //{
            //    get
            //    {
            //        return AccessibleRole.Cell;
            //    }
            //}
        }
    }

    public class Card
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Bitmap Image { get; set; }
        public bool Matched { get; set; }
    }

    public class Shuffler
    {
        private readonly Random _random;

        public Shuffler()
        {
            this._random = new Random();
        }

        public void Shuffle<T>(IList<T> array)
        {
            for (int i = array.Count; i > 1;)
            {
                int j = this._random.Next(i);

                --i;

                T temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
    }
}
