using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WinFormsMatchingGame.Properties;

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

namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private CardMatchingGrid cardMatchingGrid = new CardMatchingGrid();

        private DataGridViewButtonCellWithCustomName firstCardInPairAttempt;
        private DataGridViewButtonCellWithCustomName secondCardInPairAttempt;

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

            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            cardMatchingGrid.Columns.Add(new DataGridViewButtonColumnWithCustomName());

            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();
            cardMatchingGrid.Rows.Add();

            cardMatchingGrid.CellClick += DataGridView_CellClick;
            cardMatchingGrid.CellPainting += CardMatchingGrid_CellPainting;
            cardMatchingGrid.SizeChanged += Grid_SizeChanged;

            this.panelCardGrid.Controls.Add(cardMatchingGrid);

            ResizeGridContent();
        }

        private void CardMatchingGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // Set up clip regions below in order to prevent the image in the cell from
            // flashing when the cell is repainted.

            Bitmap bmp = null;
            Rectangle rectBitmap = new Rectangle();
            Rectangle rectCellImage = new Rectangle();

            // Is this cell showing an image?
            var button = (this.cardMatchingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCellWithCustomName);
            if (button.ReadOnly)
            {
                var columnCount = cardMatchingGrid.GridDimensions;
                var index = ((columnCount * e.RowIndex) + e.ColumnIndex);
                bmp = cardMatchingGrid.CardList[index].Image;

                rectBitmap = new Rectangle(0, 0, bmp.Width, bmp.Height);

                double ratio = Math.Max(
                    (double)rectBitmap.Width / (double)e.CellBounds.Width,
                    (double)rectBitmap.Height / (double)e.CellBounds.Height);

                var finalWidth = (int)(rectBitmap.Width / ratio) - 4;
                var finalHeight = (int)(rectBitmap.Height / ratio) - 4;

                rectCellImage = new Rectangle(
                    e.CellBounds.Left + ((e.CellBounds.Width - finalWidth) / 2),
                    e.CellBounds.Top + ((e.CellBounds.Height - finalHeight) / 2),
                    finalWidth,
                    finalHeight);

                e.Graphics.ExcludeClip(new Region(
                    rectCellImage));
            }

            // Now paint everything but the cell image if there is one.
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            if (button.ReadOnly)
            {
                e.Graphics.Clip = new Region();

                // Now paint the image in the cell.
                e.Graphics.DrawImage(bmp,
                    rectCellImage,
                    rectBitmap,
                    GraphicsUnit.Pixel);
            }

            e.Handled = true;
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
                firstCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
                firstCardInPairAttempt.TurnOver(true);
            }
            else 
            {
                // This must be the second card turned over in an attempt to find a pair.
                // Has a pair been found?

                var firstIndex = firstCardInPairAttempt.GetCardIndex();
                var cardNameFirst = this.cardMatchingGrid.CardList[firstIndex].Name;

                var cardNameSecond = this.cardMatchingGrid.CardList[index].Name;

                secondCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);

                if (cardNameFirst == cardNameSecond)
                {
                    var card = this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName;
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
                    secondCardInPairAttempt = (this.cardMatchingGrid.Rows[rowIndex].Cells[columnIndex] as DataGridViewButtonCellWithCustomName);
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
                    var button = (this.cardMatchingGrid.Rows[r].Cells[c] as DataGridViewButtonCellWithCustomName);
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

    public class DataGridViewButtonColumnWithCustomName : DataGridViewButtonColumn
    {
        public DataGridViewButtonColumnWithCustomName()
        {
            this.CellTemplate = new DataGridViewButtonCellWithCustomName();
        }
    }

    public class DataGridViewButtonCellWithCustomName : DataGridViewButtonCell
    {
        protected override AccessibleObject CreateAccessibilityInstance()
        {
            return new DataGridViewButtonCellWithCustomNameAccessibleObject(this);
        }

        protected override void OnClick(DataGridViewCellEventArgs e)
        {
            base.OnClick(e);
        }

        public void TurnOver(bool FaceUp)
        {
            ReadOnly = FaceUp;

            var button = (this.DataGridView.Rows[this.RowIndex].Cells[this.ColumnIndex] as DataGridViewButtonCellWithCustomName);
            this.DataGridView.InvalidateCell(button);
        }

        public int GetCardIndex()
        {
            var columnCount = (this.DataGridView as CardMatchingGrid).GridDimensions;
            return ((columnCount * this.RowIndex) + this.ColumnIndex);
        }

        protected class DataGridViewButtonCellWithCustomNameAccessibleObject :
            DataGridViewButtonCellAccessibleObject
        {
            public DataGridViewButtonCellWithCustomNameAccessibleObject(DataGridViewButtonCellWithCustomName owner) : base(owner)
            {
            }

            public override string Name
            {
                get
                {
                    var cardName = Resources.ResourceManager.GetString("Card") + " " +
                        ((this.Owner as DataGridViewButtonCellWithCustomName).GetCardIndex() + 1);

                    // During app development, include the Value in the name.
                    cardName += ", " + this.Value;

                    return cardName;
                }
            }

            public override string Value 
            {
                get
                {
                    var button = (this.Owner as DataGridViewButtonCellWithCustomName);
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
                    var button = (this.Owner as DataGridViewButtonCellWithCustomName);
                    var index = button.GetCardIndex();

                    // A face down card needs no description.
                    string description = button.ReadOnly ?
                        (this.Owner.DataGridView as CardMatchingGrid).CardList[index].Description :
                        "";

                    return description;
                }
            }

            // Attempting to override the UIA ControlType has apparently has no effect.
            // public override AccessibleRole Role
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
