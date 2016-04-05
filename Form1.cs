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
        private Database myDatabase = new Database();
        
        public Form1()
        {
            InitializeComponent();
            LoadDB();
            lbxScreen.Visible = false;
        }

        public void LoadDB()
        {
            DisplayDataGridViewCustomer();
            DisplayDataGridViewMovie();
            DisplayDataGridViewRentedMovies();
            
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayDataGridViewRentedMovies()
        {
            dgvRentedMovies.DataSource = null;
            try
            {
                dgvRentedMovies.DataSource = myDatabase.FilldgvRentedMovieswithInfo();
                dgvRentedMovies.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message);
            }
        }

        private void MoviesStillOut()
        {
            dgvRentedMovies.DataSource = null;
            try
            {
                dgvRentedMovies.DataSource = myDatabase.FilldgvRentedMovieswithMoviesOut();
                dgvRentedMovies.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int CustomerID = 0;

            try
            {
                CustomerID = (int) dgvCustomer.Rows[e.RowIndex].Cells[0].Value;
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

            if ((tbxFN.Text != string.Empty) && (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) &&
                (tbxPhone.Text != string.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text,
                        tbxCustID.Text,
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
            foreach (Control ctrl in root.Controls)
            {
                if (ctrl is TextBox)
                {
                    ((TextBox) ctrl).Text = String.Empty;
                 
                }
              
                lbxScreen.Visible = false;
            }
        }

        private void btnUpdateCust_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxFN.Text != string.Empty) && (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) &&
                (tbxPhone.Text != string.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text,
                        tbxCustID.Text,
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
            MessageBox.Show(myDatabase.DeleteCustomer(CustID));
            DisplayDataGridViewCustomer();
            ClearAllTextBoxes(this);

        }

        private void dgvMovies_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int MovieID = 0;
            int Year = 0;
            try
            {
                MovieID = (int) dgvMovies.Rows[e.RowIndex].Cells[0].Value;
                tbxRating.Text = dgvMovies.Rows[e.RowIndex].Cells[1].Value.ToString();
                tbxTitle.Text = dgvMovies.Rows[e.RowIndex].Cells[2].Value.ToString();
                tbxYear.Text = dgvMovies.Rows[e.RowIndex].Cells[3].Value.ToString();
                tbxPlot.Text = dgvMovies.Rows[e.RowIndex].Cells[6].Value.ToString();
                tbxGenre.Text = dgvMovies.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (e.RowIndex >= 0)
                {
                    Year = Convert.ToInt16(tbxYear.Text);
                    if (DateTime.Now.Date.Year -5 > Year )
                        tbxCost.Text = "5";
                    else
                    {
                        tbxCost.Text = "2";
                    }

                            tbxMovieID.Text = MovieID.ToString();
                    tbxScreen.Text = tbxTitle.Text;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddMovie_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxRating.Text != String.Empty) && (tbxTitle.Text != string.Empty) && (tbxYear.Text != string.Empty) &&
                (tbxPlot.Text != string.Empty) && (tbxGenre.Text != string.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateMovie(tbxRating.Text, tbxTitle.Text, tbxYear.Text, tbxPlot.Text,
                        tbxGenre.Text, tbxMovieID.Text, tbxCost.Text, "Add");
                    MessageBox.Show(tbxTitle.Text + " added " + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                DisplayDataGridViewMovie();
                tbxMovieID.Text = "";
                tbxRating.Text = "";
                tbxTitle.Text = "";
                tbxYear.Text = "";
                tbxPlot.Text = "";
                tbxGenre.Text = "";

            }
            else
            {
                MessageBox.Show("Please fill in Rating, Title, Year, Plot and Genre boxes.");
            }
        }

        private void btnDelMovie_Click(object sender, EventArgs e)
        {
            string MovieID = tbxMovieID.Text;
            //string result = null;
            MessageBox.Show(myDatabase.DeleteMovie(MovieID));
            DisplayDataGridViewMovie();
            ClearAllTextBoxes(this);
        }

        private void btnUpdateMovie_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxRating.Text != String.Empty) && (tbxTitle.Text != string.Empty) && (tbxYear.Text != string.Empty) &&
                (tbxPlot.Text != string.Empty) && (tbxGenre.Text != string.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateMovie(tbxRating.Text, tbxTitle.Text, tbxYear.Text, tbxPlot.Text,
                        tbxGenre.Text, tbxMovieID.Text, tbxCost.Text, "Update");
                    MessageBox.Show(tbxTitle.Text + " updated " + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                DisplayDataGridViewMovie();
                tbxMovieID.Text = "";
                tbxRating.Text = "";
                tbxTitle.Text = "";
                tbxYear.Text = "";
                tbxPlot.Text = "";
                tbxGenre.Text = "";

            }
            else
            {
                MessageBox.Show("Are these the changes you wish to make?");
            }
        }

        private void btnMostPopular_Click(object sender, EventArgs e)
        {
          lbxScreen.DataSource =  myDatabase.FillListViewwithMostPopularMovies();
          // lvScreen.Items.Add("Title");
            
            tbxScreen.Visible = false;
            lbxScreen.Visible = true;
        }

        private void btnMostVideos_Click(object sender, EventArgs e)
        {
            lbxScreen.DataSource = myDatabase.RentedMostMovies();
           
            tbxScreen.Visible = false;
            lbxScreen.Visible = true;
        }

        private void btnMoviesOut_Click(object sender, EventArgs e)
        {
            MoviesStillOut();
        }

        private void btnAllMoviesIssued_Click(object sender, EventArgs e)
        {
            DisplayDataGridViewRentedMovies();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            string result = null;

            if ((tbxCustID.Text != string.Empty) && (tbxMovieID.Text != string.Empty) )
            {
                try
                {
                    result = myDatabase.IssueMovie(tbxCustID.Text, tbxMovieID.Text);
                    MessageBox.Show( "Title " + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                 DisplayDataGridViewRentedMovies();
            tbxFN.Text = "";
            tbxLN.Text = "";
            tbxAddress.Text = "";
            tbxPhone.Text = "";
           tbxTitle.Text = "";
           
                
            }
            else
            {
                MessageBox.Show("Please select customer and movie from tables.");
            }
            myDatabase.FilldgvRentedMovieswithInfo();
           

        }

        // private void btnFees_Click(object sender, EventArgs e)
        // {
        //myDatabase.CalculateFees();
        //     //DisplayDataGridViewMovie();
        // }



        //private void btnIssue_Click(object sender, EventArgs e)
        //{
        //  myDatabase.IssueMovie();
        //}
    }      
}

