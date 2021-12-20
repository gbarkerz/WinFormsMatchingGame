using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WinFormsSquaresGame.Controls
{
    class SquaresGrid : DataGridView
    {
        public List<Square> SquareList { get; set; }
        private int moveCount = 0;
        private Bitmap backgroundPicture;

        public SquaresGrid()
        {
            CellClick += DataGridView_CellClick;
            SizeChanged += Grid_SizeChanged;
            CellPainting += Grid_CellPainting;
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
                        "Accessible Matching Game",
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
            // Set up clip regions below in order to prevent the image in the cell 
            // from flashing when the cell is repainted.

            Bitmap bmp = null;
            Rectangle rectBitmap = new Rectangle();
            Rectangle rectCellImage = new Rectangle();

            var finalWidth = 0;
            var finalHeight = 0;

            var square = GetSquareFromRowColumn(e.RowIndex, e.ColumnIndex);
            if (square.TargetIndex == 15)
            {
                return;
            }

            var index = ((GridDimensions * e.RowIndex) + e.ColumnIndex);

            if (backgroundPicture == null)
            {
                bmp = SquareList[index].Image;

                rectBitmap = new Rectangle(0, 0, bmp.Width, bmp.Height);

                double ratio = Math.Max(
                    (double)rectBitmap.Width / (double)e.CellBounds.Width,
                    (double)rectBitmap.Height / (double)e.CellBounds.Height);

                // Bring in the image a few pixels from the border.
                finalWidth = (int)(rectBitmap.Width / ratio) - 6;
                finalHeight = (int)(rectBitmap.Height / ratio) - 6;
            }
            else
            {
                bmp = backgroundPicture;

                int width = backgroundPicture.Width / GridDimensions;
                int height = backgroundPicture.Height / GridDimensions;

                int squareColumnIndex = square.TargetIndex % GridDimensions;
                int squareRowIndex = square.TargetIndex / GridDimensions;

                int left = squareColumnIndex * width;
                int top = squareRowIndex * height;

                rectBitmap = new Rectangle(left, top, width, height);

                finalWidth = e.CellBounds.Width - 6;
                finalHeight = e.CellBounds.Height - 6;
            }

            if (bmp != null)
            {
                rectCellImage = new Rectangle(
                    e.CellBounds.Left + ((e.CellBounds.Width - finalWidth) / 2),
                    e.CellBounds.Top + ((e.CellBounds.Height - finalHeight) / 2),
                    finalWidth,
                    finalHeight);

                e.Graphics.ExcludeClip(new Region(rectCellImage));
            }

            // Now paint everything but the cell image if there is one.
            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            if (bmp != null)
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

        public void SetBackgroundPicture(FileInfo fileInfo)
        {
            backgroundPicture = new Bitmap(fileInfo.FullName);

            this.Refresh();
        }
    }
}
