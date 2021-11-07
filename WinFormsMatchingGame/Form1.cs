using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private CardMatchingGrid cardMatchingGrid = new CardMatchingGrid();

        private DataGridViewButtonCellWithCustomName firstCardInPairAttempt;
        private DataGridViewButtonCellWithCustomName secondCardInPairAttempt;

        public FormMatchingGame()
        {
            InitializeComponent();

            cardMatchingGrid.CardList = new List<Card>()
            {
                new Card { Name = "A", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card { Name = "A", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card1) },
                new Card { Name = "B", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card { Name = "B", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card2) },
                new Card { Name = "C", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card { Name = "C", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card3) },
                new Card { Name = "D", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card { Name = "D", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card4) },
                new Card { Name = "E", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card { Name = "E", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card5) },
                new Card { Name = "F", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card { Name = "F", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card6) },
                new Card { Name = "G", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card { Name = "G", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card7) },
                new Card { Name = "H", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
                new Card { Name = "H", Image = new Bitmap(WinFormsMatchingGame.Properties.Resources.Card8) },
            };

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
            cardMatchingGrid.SizeChanged += Grid_SizeChanged;

            this.panelCardGrid.Controls.Add(cardMatchingGrid);

            cardMatchingGrid.CellPainting += CardMatchingGrid_CellPainting;

            ResizeGridContent();
        }

        private void CardMatchingGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            var button = (this.cardMatchingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCellWithCustomName);
            if (button.ReadOnly)
            {
                var columnCount = cardMatchingGrid.GridDimensions;
                var index = ((columnCount * e.RowIndex) + e.ColumnIndex);
                var bmp = cardMatchingGrid.CardList[index].Image;

                var w = bmp.Width;
                var h = bmp.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                double ratio = Math.Max(
                    (double)w / (double)e.CellBounds.Width, 
                    (double)h / (double)e.CellBounds.Height);

                var finalWidth  = (int)(w / ratio) - 4;
                var finalHeight = (int)(h / ratio) - 4;

                e.Graphics.DrawImage(bmp,
                    new Rectangle(
                        e.CellBounds.Left + ((e.CellBounds.Width - finalWidth) / 2),
                        e.CellBounds.Top + ((e.CellBounds.Height - finalHeight) /2 ),
                        finalWidth, 
                        finalHeight),
                    new Rectangle(0, 0, bmp.Width, bmp.Height),
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
            var grid = sender as DataGridView;

            // Take no action while the Try Again button is enabled.
            if (buttonTryAgain.Enabled)
            {
                return;
            }

            // Take no action is the click is on a read-only cell.
            if (this.cardMatchingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly)
            {
                return;
            }

            var gridDimensions = cardMatchingGrid.GridDimensions;
            var index = (gridDimensions * e.RowIndex) + e.ColumnIndex;

            // Is this the first card turned over in an attempt to find a pair?
            if (firstCardInPairAttempt == null)
            {
                firstCardInPairAttempt = (this.cardMatchingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCellWithCustomName);
                firstCardInPairAttempt.ReadOnly = true;
            }
            else 
            {
                // This must be the second card turned over in an attempt to find a pair.
                // Has a pair been found?

                var firstIndex = firstCardInPairAttempt.GetCardIndex();
                var cardNameFirst = this.cardMatchingGrid.CardList[firstIndex].Name;

                var cardNameSecond = this.cardMatchingGrid.CardList[index].Name;

                secondCardInPairAttempt = (this.cardMatchingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCellWithCustomName);

                if (cardNameFirst == cardNameSecond)
                {
                    this.cardMatchingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;

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
                            "Congratulations, you won! Would you like another game?",
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
                    secondCardInPairAttempt = (this.cardMatchingGrid.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewButtonCellWithCustomName);
                    secondCardInPairAttempt.ReadOnly = true;

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
                    button.ReadOnly = false;
                }
            }

            buttonTryAgain.Enabled = false;

            this.cardMatchingGrid.Refresh();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTryAgain_Click(object sender, EventArgs e)
        {
            buttonTryAgain.Enabled = false;

            firstCardInPairAttempt.ReadOnly = false;
            secondCardInPairAttempt.ReadOnly = false;

            firstCardInPairAttempt = null;
            secondCardInPairAttempt = null;

            this.cardMatchingGrid.Refresh();
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
                    // Todo: Localize this.
                    //return Resources.ResourceManager.GetString("Card")
                    
                    var cardName = "Card " + 
                        ((this.Owner as DataGridViewButtonCellWithCustomName).GetCardIndex() + 1);

                    // During app development, include the Value in the name.
                    var index = (this.Owner as DataGridViewButtonCellWithCustomName).GetCardIndex();

                    string fullName = cardName + ", " + this.Value;

                    return fullName;
                }
            }

            public override string Value 
            {
                get
                {
                    // The Name will contain all the data of interest to the customer.
                    var button = (this.Owner as DataGridViewButtonCellWithCustomName);
                    var index = button.GetCardIndex();
                    
                    string value = button.ReadOnly ?
                        (this.Owner.DataGridView as CardMatchingGrid).CardList[index].Name : 
                        "Face down";

                    return value;
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
        public Bitmap Image { get; set; }
        public bool Matched { get; set; }
    }
}
