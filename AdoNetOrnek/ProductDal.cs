using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetOrnek
{
    public class ProductDal
    {
        SqlConnection _connection = new SqlConnection("Server=.; initial catalog =ETrade;integrated security=true");
        public List<Product> GetAll()
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("select * from products", _connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    StockAmount = (int)reader["StockAmount"],
                    UnitPrice = (decimal)reader["UnitPrice"]

                };
                products.Add(product);

            }
            reader.Close();
            _connection.Close();
            return products;
        }

        private void ConnectionControl()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        //public DataTable GetAll2()
        //{
        //    SqlConnection connection = new SqlConnection("Server=.; initial catalog =ETrade;integrated security=true");
        //    if (connection.State == ConnectionState.Closed)
        //    {
        //        connection.Open();
        //    }
        //    SqlCommand command = new SqlCommand("select * from products", connection);
        //    SqlDataReader reader = command.ExecuteReader();
        //    DataTable dataTable = new DataTable();
        //    dataTable.Load(reader);
        //    reader.Close();
        //    connection.Close();
        //    return dataTable;
        //}
        public void Add(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("insert into products  values(@Name,@UnitPrice,@StockAmount)", _connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@StockAmount", product.StockAmount);
            command.ExecuteNonQuery();
            _connection.Close();
        }
        public void Update(Product product)
        {
            ConnectionControl();
            SqlCommand command = new SqlCommand("update products set name=@Name,unitprice=@UnitPrice,stockamount=@StockAmount where Id= @id", _connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@StockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();
            _connection.Close();
        }
    }
}
