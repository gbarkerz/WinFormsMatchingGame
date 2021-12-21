using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormsSquaresGame.Controls
{
    public class SquaresGrid : DataGridView
    {
        public List<Square> SquareList { get; set; }
        private int moveCount = 0;

        public SquaresGrid()
        {
            CellClick += DataGridView_CellClick;
            SizeChanged += Grid_SizeChanged;
            CellPainting += Grid_CellPainting;

            LoadSettings();
        }

        private void LoadSettings()
        {
            this.ShowNumbers = Settings1.Default.ShowNumbers;
            this.NumberSizeIndex = Settings1.Default.NumberSizeIndex;
            this.ShowPicture = Settings1.Default.ShowPicture;
            this.BackgroundPictureFullName = Settings1.Default.BackgroundPicture;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Square adjacentSquare = null;
            int emptySquareIndex = -1;

            // Is the empty square adjacent to this square?
            if (e.ColumnIndex > 0)
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex, e.ColumnIndex - 1);
                if (adjacentSquare.Image == null)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex, e.ColumnIndex - 1);
                }
            }

            if ((emptySquareIndex == -1) && (e.RowIndex > 0))
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex - 1, e.ColumnIndex);
                if (adjacentSquare.Image == null)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex - 1, e.ColumnIndex);
                }
            }

            if ((emptySquareIndex == -1) && (e.ColumnIndex < GridDimensions - 1))
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex, e.ColumnIndex + 1);
                if (adjacentSquare.Image == null)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex, e.ColumnIndex + 1);
                }
            }

            if ((emptySquareIndex == -1) && (e.RowIndex < GridDimensions - 1))
            {
                adjacentSquare = GetSquareFromRowColumn(e.RowIndex + 1, e.ColumnIndex);
                if (adjacentSquare.Image == null)
                {
                    emptySquareIndex = GetSquareIndexFromRowColumn(e.RowIndex + 1, e.ColumnIndex);
                }
            }

            // If we found an adjacent empty square, swap the clicked square with the empty square.
            if (emptySquareIndex != -1)
            {
                ++moveCount;

                var emptySquare = this.SquareList[emptySquareIndex];

                var clickedSquareIndex = GetSquareIndexFromRowColumn(e.RowIndex, e.ColumnIndex);
                var clickedSquare = this.SquareList[clickedSquareIndex];

                this.SquareList[emptySquareIndex] = clickedSquare;
                this.SquareList[clickedSquareIndex] = emptySquare;

                // Todo: Only refresh the two affected squares, not the whole grid.
                this.Refresh();

                // Has the game been won?
                if (GameIsWon())
                {
                    // Todo: Localize this.
                    var answer = MessageBox.Show(
                        this,
                        "Congratulations! You won the game in " + moveCount +
                        " goes.\r\n\r\nWould you like another game?",
                        "Accessible Squares Game",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        ResetGrid();
                    }
                }
            }
        }

        private bool GameIsWon()
        {
            bool gameIsWon = true;

            for (int i = 0; i < this.SquareList.Count; i++)
            {
                if (SquareList[i].TargetIndex != i)
                {
                    gameIsWon = false;

                    break;
                }
            }

            return gameIsWon;
        }

        // Reset the grid to an initial game state.
        public void ResetGrid()
        {
            moveCount = 0;

            Shuffle();

            this.Refresh();
        }

        private void Grid_SizeChanged(object sender, EventArgs e)
        {
            ResizeGridContent();
        }

        private void ResizeGridContent()
        {
            // Resize the columns and rows such that the collection of cells always
            // exactly fills the grid.

            for (int i = 0; i < GridDimensions; ++i)
            {
                Columns[i].Width = (this.ClientSize.Width / GridDimensions) - 1;
                Rows[i].Height = (this.ClientSize.Height / GridDimensions) - 1;
            }
        }

        public int GetSquareIndexFromRowColumn(int rowIndex, int columnIndex)
        {
            return (this.GridDimensions * rowIndex) + columnIndex;
        }

        public Square GetSquareFromRowColumn(int rowIndex, int columnIndex)
        {
            var index = (this.GridDimensions * rowIndex) + columnIndex;
            return this.SquareList[index];
        }

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            int pictureOffset = 4;
            int showNumberScale = 4 - this.NumberSizeIndex;

            // If this is the empty square, we don;t need to do any custom painting here.
            var square = GetSquareFromRowColumn(e.RowIndex, e.ColumnIndex);
            if (square.TargetIndex == 15)
            {
                return;
            }

            // Set up a clip region to cover where we expect to draw our own content.
            if (ShowNumbers || (ShowPicture && (backgroundPicture != null)))
            {
                Rectangle rectClip = e.CellBounds;

                if (ShowPicture && (backgroundPicture != null))
                {
                    // We'll be filling most of the cell with a picture.
                    rectClip.Inflate(-pictureOffset, -pictureOffset);
                }
                else if (ShowNumbers)
                {
                    // We'll just be showing a number at the top left corner of the square.
                    rectClip = new Rectangle(
                        e.CellBounds.Location.X + 4, e.CellBounds.Location.Y + 4,
                        Math.Min(e.CellBounds.Width - 8, e.CellBounds.Size.Width / showNumberScale),
                        Math.Min(e.CellBounds.Height - 8, e.CellBounds.Size.Height / showNumberScale));
                }

                e.Graphics.ExcludeClip(new Region(rectClip));
            }

            // Now paint everything but our own content.
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            if (ShowNumbers || (ShowPicture && (backgroundPicture != null)))
            {
                e.Graphics.Clip = new Region();

                // Do we have a background picture to paint?
                if (ShowPicture && (backgroundPicture != null))
                {
                    // Todo: Set another clip region to exclude painting the image where
                    // a square number will lie over it.

                    int width = backgroundPicture.Width / GridDimensions;
                    int height = backgroundPicture.Height / GridDimensions;

                    int squareColumnIndex = square.TargetIndex % GridDimensions;
                    int squareRowIndex = square.TargetIndex / GridDimensions;

                    int left = squareColumnIndex * width;
                    int top = squareRowIndex * height;

                    Rectangle rectBitmap = new Rectangle(left, top, width, height);

                    Rectangle rectCellImage = e.CellBounds;
                    rectCellImage.Inflate(-4, -4);

                    // Now paint the image in the cell.
                    e.Graphics.DrawImage(backgroundPicture,
                        rectCellImage,
                        rectBitmap,
                        GraphicsUnit.Pixel);
                }

                if (ShowNumbers)
                {
                    var rect = new Rectangle(
                        e.CellBounds.Location.X + 4, e.CellBounds.Location.Y + 4,
                        Math.Min(e.CellBounds.Width - 8, e.CellBounds.Size.Width / showNumberScale),
                        Math.Min(e.CellBounds.Height - 8, e.CellBounds.Size.Height / showNumberScale));

                    e.Graphics.FillRectangle(SystemBrushes.Control, rect);

                    int fontHeight = this.Font.FontFamily.GetEmHeight(FontStyle.Regular);
                    int lineSpacing = this.Font.FontFamily.GetLineSpacing(FontStyle.Regular);
                    var targetHeight = (e.CellBounds.Height * fontHeight) / (showNumberScale * lineSpacing);
                    var font = new Font(this.Font.FontFamily, targetHeight, GraphicsUnit.Pixel);

                    StringFormat format = new StringFormat();
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;

                    e.Graphics.DrawString(
                        (square.TargetIndex + 1).ToString(),
                        font,
                        SystemBrushes.ControlText,
                        rect,
                        format);
                }
            }

            e.Handled = true;
        }

        public int GridDimensions
        {
            get
            {
                return (int)Math.Sqrt(SquareList.Count);
            }
        }

        public void Shuffle()
        {
            var shuffler = new Shuffler();

            // Keep shuffling until the arrangement of squares can be solved.
            bool gameCanBeSolved;

            do
            {
                shuffler.Shuffle(this.SquareList);

                gameCanBeSolved = CanGameBeSolved();
            }
            while (!gameCanBeSolved);
        }

        private bool CanGameBeSolved()
        {
            int parity = 0;
            int emptySquareRow = 0;

            for (int i = 0; i < this.SquareList.Count; i++)
            {
                if (this.SquareList[i].TargetIndex == 15)
                {
                    // The empty square row is one-based.
                    emptySquareRow = (i / GridDimensions) + 1;

                    continue;
                }

                for (int j = i + 1; j < this.SquareList.Count; j++)
                {
                    if ((this.SquareList[j].TargetIndex != 15) &&
                        (this.SquareList[i].TargetIndex > this.SquareList[j].TargetIndex))
                    {
                        parity++;
                    }
                }
            }

            // The following applies to an even grid.

            bool gridCanBeSolved = false;

            if (emptySquareRow % 2 == 0)
            {
                gridCanBeSolved = (parity % 2 == 0);
            }
            else
            {
                gridCanBeSolved = (parity % 2 != 0);
            }

            return gridCanBeSolved;
        }

        private bool showNumbers;
        public bool ShowNumbers 
        { 
            get
            {
                return showNumbers;
            }
            set
            {
                showNumbers = value;
                this.Refresh();
            }
        }

        private int numberSizeIndex;
        public int NumberSizeIndex
        {
            get
            {
                return numberSizeIndex;
            }
            set
            {
                numberSizeIndex = value;
                this.Refresh();
            }
        }

        private bool showPicture;
        public bool ShowPicture
        {
            get
            {
                return showPicture;
            }
            set
            {
                showPicture = value;
                this.Refresh();
            }
        }

        private Bitmap backgroundPicture;
        private string backgroundPictureFullName;
        public string BackgroundPictureFullName
        {
            get
            {
                return backgroundPictureFullName;
            }
            set
            {
                try
                {
                    this.backgroundPictureFullName = value;

                    if (!string.IsNullOrWhiteSpace(this.backgroundPictureFullName))
                    {
                        this.backgroundPicture = new Bitmap(this.backgroundPictureFullName);
                        this.Refresh();
                    }
                }
                catch (Exception)
                {
                    this.backgroundPictureFullName = "";
                }
            }
        }
    }
}