using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetOrnek
{
    public class ProductDal
    {
        public DataTable GetAll()
        {
            SqlConnection connection = new SqlConnection("Server=.; initial catalog =ETrade;integrated security=true");
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            SqlCommand command = new SqlCommand("select * from products",connection);
             SqlDataReader reader =command.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);
            reader.Close();
            connection.Close();
            return dataTable;
        }
    }
}
