using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAssessmentJane
{
    internal class Database
    {
        private SqlConnection Connection = new SqlConnection();
        private SqlCommand Command = new SqlCommand();
        private SqlDataAdapter da = new SqlDataAdapter();

        public Database()
        {
            string connectionString =  
                @"Data Source= LAPTOP\sqlexpress;Initial Catalog=VBMoviesFullData;Integrated Security=True";
            Connection.ConnectionString = connectionString;
            Command.Connection = Connection;
        }

        public DataTable FilldgvCustomerWithCustomer()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from Customer", Connection))
            {
                Connection.Open();
                da.Fill(dt);
                Connection.Close();
            }
            return dt;
        }

        public DataTable FilldgvMoviesWithMovies()
        {
            DataTable dt = new DataTable();
            using (da = new SqlDataAdapter("select * from Movies", Connection))
            {
                Connection.Open();
                da.Fill(dt);
                Connection.Close();
            }
            return dt;
        
    }
    }
}
