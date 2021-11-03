using System;
using System.Windows.Forms;

namespace WinFormsMatchingGame
{
    public partial class FormMatchingGame : Form
    {
        private DataGridView dataGridView = new DataGridView();

        public FormMatchingGame()
        {
            InitializeComponent();

            dataGridView.AccessibilityObject.Name = "Content for matching";

            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnHeadersVisible = false;
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.ShowCellToolTips = false;

            dataGridView.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            dataGridView.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            dataGridView.Columns.Add(new DataGridViewButtonColumnWithCustomName());
            dataGridView.Columns.Add(new DataGridViewButtonColumnWithCustomName());

            dataGridView.Rows.Add();
            dataGridView.Rows.Add();
            dataGridView.Rows.Add();
            dataGridView.Rows.Add();

            dataGridView.CellClick += DataGridView_CellClick;
            dataGridView.SizeChanged += Grid_SizeChanged;

            this.Controls.Add(dataGridView);

            ResizeGridContent();
        }

        private void Grid_SizeChanged(object sender, EventArgs e)
        {
            ResizeGridContent();
        }

        private void ResizeGridContent()
        {
            for (int i = 0; i < 4; ++i)
            {
                dataGridView.Columns[i].Width = (this.ClientSize.Width / 4) - 1;
                dataGridView.Rows[i].Height = (this.ClientSize.Height / 4) - 1;
            }
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
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
                    // Add the custom accessible name, and follow that with the row information.
                    //return Resources.ResourceManager.GetString("ImageColumnAccessibleName") + base.Name;
                    return "Unknown";
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
}
