using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoviesAssessmentJane
{
    public partial class Form1 : Form
    {
        Database myDatabase = new Database();
      public Form1()
        {
            InitializeComponent();
            LoadDB();
        }
        public void LoadDB()
        { 
        DisplayDataGridViewCustomer();
            DisplayDataGridViewMovie();
        }

        private void DisplayDataGridViewCustomer()
        {
            dgvCustomer.DataSource = null;
            try
            {
                dgvCustomer.DataSource = myDatabase.FilldgvCustomerWithCustomer();
                dgvCustomer.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayDataGridViewMovie()
        {
            dgvMovies.DataSource = null;
            try
            {
                dgvMovies.DataSource = myDatabase.FilldgvMoviesWithMovies();
                dgvMovies.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CustomerID = 0;

            try
            {
                CustomerID = (int)dgvCustomer.Rows[e.RowIndex].Cells[0].Value;
                tbxFN.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbxLN.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbxAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbxPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (e.RowIndex >= 0)
                {
                    tbxCustID.Text = CustomerID.ToString();
                }
            }

            catch (Exception ex)
           {
            MessageBox.Show(ex.Message);
           }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxFN.Text != string.Empty)&& (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) && (tbxPhone.Text != string.Empty ))
            {
                try
                {
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text, tbxCustID.Text,
                        "Add");
                    MessageBox.Show(tbxFN.Text + "added " + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                DisplayDataGridViewCustomer();
                tbxFN.Text = "";
                tbxLN.Text = "";
                tbxAddress.Text = "";
                tbxPhone.Text = "";
            }
            else
            {
                MessageBox.Show("Please enter first name, last name, address and phone.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllTextBoxes(this);
        }

        public void ClearAllTextBoxes(Control root)
        {
            foreach ( Control ctrl in root.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox) ctrl).Text = String.Empty;
                }
            }
        }

        private void btnUpdateCust_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxFN.Text != string.Empty) && (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) && (tbxPhone.Text != string.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text, tbxCustID.Text,
                        "Update");
                    MessageBox.Show(tbxFN.Text + " updated" + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                DisplayDataGridViewCustomer();
                tbxFN.Text = "";
                tbxLN.Text = "";
                tbxAddress.Text = "";
                tbxPhone.Text = "";
            }
            else
            {
                MessageBox.Show("Are these the changes you wish to make?");
            }
        }

        private void btnDeleteCust_Click(object sender, EventArgs e)
        {
            string CustID = tbxCustID.Text;
            //string result = null;
            MessageBox.Show(  myDatabase.DeleteCustomer(CustID));
            DisplayDataGridViewCustomer();
            ClearAllTextBoxes(this);

        }

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxTitle.Text != string.Empty) && (tbxYear.Text != string.Empty) && (tbxPlot.Text != string.Empty) && (tbxGenre.Text != string.Empty)&& (tbxRating.Text != String.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateMovie(tbxTitle.Text, tbxYear.Text, tbxPlot.Text, tbxGenre.Text, tbxRating.Text, tbxMovieID.Text,
                        "Add");
                    MessageBox.Show(tbxTitle.Text + " added" + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                DisplayDataGridViewMovie();
                tbxTitle.Text = "";
                tbxYear.Text = "";
                tbxPlot.Text = "";
                tbxGenre.Text = "";
                tbxRating.Text = "";
                tbxMovieID.Text = "";
            }
            else
            {
                MessageBox.Show("Are these the changes you wish to make?");
            }
        }
        private void btnDelMovie_Click(object sender, EventArgs e)
        {
            string MovieID = tbxMovieID.Text;
            //string result = null;
            MessageBox.Show(myDatabase.DeleteCustomer(MovieID));
            DisplayDataGridViewCustomer();
            ClearAllTextBoxes(this);
        }

    }


    }

