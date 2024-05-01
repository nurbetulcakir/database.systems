using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cse3055.Pages.Depots
{
    public class IndexModel : PageModel
    {
        public List<Depot> Depots { get; set; }

        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM DEPOT"; // Ensure these columns match the Depot class
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Depots = new List<Depot>();

                            while (reader.Read())
                            {
                                Depot depot = new Depot()
                                {
                                    DepotID = Convert.ToInt32(reader["DepotID"]),
                                    DepotProductID = reader["DepotProductID"].ToString(),
                                    DepotProduct = reader["DepotProduct"].ToString(),
                                };
                                Depots.Add(depot);
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

        public class Depot
        {
            public int DepotID { get; set; }
            public string DepotProductID { get; set; }
            public string DepotProduct { get; set; }
        }
    }
}
