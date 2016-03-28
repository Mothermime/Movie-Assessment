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
       
            
        }


    }

