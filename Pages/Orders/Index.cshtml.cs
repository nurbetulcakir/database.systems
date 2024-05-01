using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cse3055.Pages.Orders
{
    public class IndexModel : PageModel
    {
        public List<Order> Orders { get; set; }

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM ORDER_"; // Change to use square brackets to handle reserved keyword
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Orders = new List<Order>();

                            while (reader.Read())
                            {
                                Order order = new Order()
                                {
                                    OrderID = Convert.ToInt32(reader["OrderID"]),
                                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                    OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                    DeliveryAddress = reader["DeliveryAddress"].ToString(),
                                    TotalCost = Convert.ToInt32(reader["TotalCost"]),
                                    Status_ = reader["Status_"].ToString(),
                                };
                                Orders.Add(order);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, for example, log the error or display an error message to the user.
                Console.WriteLine(ex.Message);
            }
        }

        public class Order
        {
            public int OrderID { get; set; }
            public int CustomerID { get; set; }
            public DateTime OrderDate { get; set; }
            public string DeliveryAddress { get; set; }
            public int TotalCost { get; set; }
            public string Status_ { get; set; }
        }
    }
}
