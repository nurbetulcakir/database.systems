using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace cse3055.Pages.Products
{
    public class ProductModel : PageModel
    {
        public List<Product> Products { get; set; }

        public ProductModel()
        {
            Products = new List<Product>();
        }

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM PRODUCT";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product()
                                {
                                    ProductID = reader["ProductID"].ToString(),
                                    Material = reader["Material"].ToString(),
                                    VerticalSectionType = reader["VerticalSectionType"].ToString(),
                                    VerticalSectionSize = reader["VerticalSectionSize"].ToString(),
                                    Alloy = reader["Alloy"].ToString(),
                                    SpecificWeightInKG = Convert.ToDouble(reader["SpecificWeightInKG"]),
                                    LengthInMeters = Convert.ToInt32(reader["LengthInMeters"]),
                                    WeightOnePieceInKG = Convert.ToDouble(reader["WeightOnePieceInKG"]),
                                    UnitPrice = Convert.ToInt32(reader["UnitPrice"])
                                };
                                Products.Add(product);
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

        public class Product
        {
            public string ProductID { get; set; }
            public string Material { get; set; }
            public string VerticalSectionType { get; set; }
            public string VerticalSectionSize { get; set; }
            public string Alloy { get; set; }
            public double SpecificWeightInKG { get; set; }
            public int LengthInMeters { get; set; }
            public double WeightOnePieceInKG { get; set; }
            public int UnitPrice { get; set; }

            public Product() { }
        }
    }
}
