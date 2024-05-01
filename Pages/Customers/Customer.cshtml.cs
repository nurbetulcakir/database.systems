using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cse3055.Pages.Customers
{
    public class CustomerModel : PageModel
    {
        public List<Customer> Customers { get; set; }

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM CUSTOMER";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Customers = new List<Customer>();

                            while (reader.Read())
                            {
                                Customer customer = new Customer()
                                {
                                    CustomerID = Convert.ToInt32(reader["CustomerID"]),
                                    Name = reader["Name_"].ToString(),
                                    District = reader["District"].ToString(),
                                    PhoneNumber = reader["PhoneNumber"].ToString(),
                                    FaxNumber = reader["FaxNumber"].ToString(),
                                    Website = reader["Website"].ToString()
                                };
                                Customers.Add(customer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public class Customer
        {
            public int CustomerID { get; set; }
            public string Name { get; set; }
            public string District { get; set; }
            public string PhoneNumber { get; set; }
            public string FaxNumber { get; set; }
            public string Website { get; set; }

            public Customer() { }
        }
    }
}
