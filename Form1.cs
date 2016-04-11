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
{//'Forms' is a class.  The only methods etc that should be here are those that apply to the form and it's components. 
    //All others should be in a different class e.g. database class (It's the only other one I have at the moment!)
    public partial class Form1 : Form
    { //Instantiate database class - create a new 'instance' of it from the 'blueprint'
        private Database myDatabase = new Database();
        
        public Form1()
        {//Set everything in place - preparation
            InitializeComponent();
            LoadDB();
            lbxScreen.Visible = false;
        }
        //Display the data in the tables
        public void LoadDB()
        {//call up all the methods that have been written below
            DisplayDataGridViewCustomer();
            DisplayDataGridViewMovie();
            DisplayDataGridViewRentedMovies();
            
        }
        //private vs public:  If there is no need for anyone else to have access to the method then it should be private.  Anything that applies only to the class in which it is located can be a private.  Anything that is going to be accessed by other classes needs to be public.  Technically, the above method could be private
        
        private void DisplayDataGridViewCustomer()
        {//load the customer table 
            //start off empty
            dgvCustomer.DataSource = null;
            try
            {
                dgvCustomer.DataSource = myDatabase.FilldgvCustomerWithCustomer();
                dgvCustomer.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (Exception ex)
            {// wonderful way to show where to find any errors!
                MessageBox.Show(ex.Message);
            }
        }

        private void DisplayDataGridViewMovie()
        {//load the Movie table
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
        {//load  rented movies table
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
        {//load the view that has been created and fill the data grid view with a new set of information
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
        {//specify the layout of the dgv and what it will contain
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
            // if there's nothing in the specified text boxes
            if ((tbxFN.Text != string.Empty) && (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) &&
                (tbxPhone.Text != string.Empty))
            {
                try
                {//call up the method from the class
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text,
                        tbxCustID.Text,
                        "Add");
                    MessageBox.Show(tbxFN.Text + " " + tbxLN.Text + " added " + result);
                }
                catch (Exception a)
                {

                    MessageBox.Show((a.Message));
                }
                //where the output/results are to be displayed
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
        {//Gets rid of all the information sitting in text boxes
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
        {//again, if the boxes 
            string result = null;

            if ((tbxFN.Text != string.Empty) && (tbxLN.Text != string.Empty) && (tbxAddress.Text != string.Empty) &&
                (tbxPhone.Text != string.Empty))
            {
                try
                {
                    result = myDatabase.InsertorUpdateCustomer(tbxFN.Text, tbxLN.Text, tbxAddress.Text, tbxPhone.Text,
                        tbxCustID.Text,
                        "Update");
                    MessageBox.Show(tbxFN.Text + " "+ tbxLN.Text + " updated" + result);
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
        { //check before deleting  - for some reason two strings are needed to make this work.
                DialogResult dialogueResult = MessageBox.Show("Are you sure you want to delete this Customer's details?","", MessageBoxButtons.OKCancel);

            if (dialogueResult == DialogResult.OK)
            {//then go ahead and delete
                string CustID = tbxCustID.Text;
            //string result = null;
            MessageBox.Show(myDatabase.DeleteCustomer(CustID));
            DisplayDataGridViewCustomer();
            ClearAllTextBoxes(this);
            }
            //otherwise, if it is 'cancel' it doesn't delete
            

        }

        private void dgvMovies_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {//display the movie information of the movie clicked on in the text boxes
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
                    //Year = Convert.ToInt16(tbxYear.Text);
                    //if (DateTime.Now.Date.Year -5 > Year )
                    //    tbxCost.Text = "5";
                    //else
                    //{
                    //    tbxCost.Text = "2";
                    //}
                    
                            tbxMovieID.Text = MovieID.ToString();
                    tbxScreen.Text = tbxTitle.Text;
                    tbxCost.Text = Cost(dgvMovies.Rows[e.RowIndex].Cells[3].Value.ToString());
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string Cost(string MovieYear)
        {//Work out the cost of the movies based on the year they were released
            int Yearnow = (Convert.ToInt32(DateTime.Now.Year));
            int YearsOld = Yearnow - Convert.ToInt32(MovieYear);
            if (YearsOld <= 5)
            
            {
                return "5";

            }
            else
            {
                return "2";
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
            DialogResult dialogueResult = MessageBox.Show("Are you sure you want to delete this Movie?", "",
                MessageBoxButtons.OKCancel);

            if (dialogueResult == DialogResult.OK)
            {
                string MovieID = tbxMovieID.Text;
                //string result = null;
                MessageBox.Show(myDatabase.DeleteMovie(MovieID));
                DisplayDataGridViewMovie();
                ClearAllTextBoxes(this);
            }
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
                    tbxScreen.Text = tbxTitle.Text;
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
         
            
            tbxScreen.Visible = false;
            lbxScreen.Visible = true;
        }

        private void btnMostVideos_Click(object sender, EventArgs e)
        { //information comes from the RentedMostMOvies view
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
                
                tbxScreen.Text = ("$" + tbxCost.Text + " to pay.");
            }
            else
            {
                MessageBox.Show("Please select customer and movie from tables.");
            }
            myDatabase.FilldgvRentedMovieswithInfo();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string RMID = tbxRMID.Text;
           
            MessageBox.Show(myDatabase.ReturnMovie(RMID));
            DisplayDataGridViewMovie();
            //ClearAllTextBoxes(this);
        }

        private void dgvRentedMovies_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RMID = 0;
            //int CustIDFK = 0;
            //int MovieIDFK = 0;


            try
            {
                RMID = (int)dgvRentedMovies.Rows[e.RowIndex].Cells[0].Value;
                
                if (e.RowIndex >= 0)
                {
                    tbxRMID.Text = RMID.ToString();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }      
}

