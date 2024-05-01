using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using static cse3055.Pages.Products.ProductModel;

namespace cse3055.Pages.Products
{
    public class AddModel : PageModel
    {

        public Product product = new Product();

        public string successMessage;
        public string ErrorMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
            ErrorMessage = "";
            Product product = new Product()
            {
                ProductID = string.IsNullOrEmpty(Request.Form["ProductID"]) ? "" : Request.Form["ProductID"],
                Material = Request.Form["Material"],
                VerticalSectionType = Request.Form["VerticalSectionType"],
                VerticalSectionSize = Request.Form["VerticalSectionSize"],
                Alloy = Request.Form["Alloy"],
                SpecificWeightInKG = string.IsNullOrEmpty(Request.Form["SpecificWeightInKG"]) ? 0.0 : double.Parse(Request.Form["SpecificWeightInKG"]),
                LengthInMeters = string.IsNullOrEmpty(Request.Form["LengthInMeters"]) ? 0 : int.Parse(Request.Form["LengthInMeters"]),
                WeightOnePieceInKG = string.IsNullOrEmpty(Request.Form["WeightOnePieceInKG"]) ? 0.0 : double.Parse(Request.Form["WeightOnePieceInKG"]),
                UnitPrice = string.IsNullOrEmpty(Request.Form["UnitPrice"]) ? 0 : int.Parse(Request.Form["UnitPrice"])
            };

            try
            {
                if (string.IsNullOrEmpty(product.ProductID) ||
                    string.IsNullOrEmpty(product.Material) ||
                    string.IsNullOrEmpty(product.VerticalSectionType) ||
                    string.IsNullOrEmpty(product.VerticalSectionSize) ||
                    string.IsNullOrEmpty(product.Alloy) ||
                    product.SpecificWeightInKG <= 0 || // Validate numeric properties
                    product.LengthInMeters <= 0 ||     // appropriately
                    product.WeightOnePieceInKG <= 0 || // based on your
                    product.UnitPrice <= 0)            // requirements
                {
                    ErrorMessage = "You must fill in all fields";
                    return;
                }
                string connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand("ProductEdit", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        // Set the parameters for the stored procedure
                        command.Parameters.AddWithValue("@SelectOption", "Insert");
                        command.Parameters.AddWithValue("@ProductID", product.ProductID);
                        command.Parameters.AddWithValue("@Material", product.Material);
                        command.Parameters.AddWithValue("@VerticalSectionType", product.VerticalSectionType);
                        command.Parameters.AddWithValue("@VerticalSectionSize", product.VerticalSectionSize);
                        command.Parameters.AddWithValue("@Alloy", product.Alloy);
                        command.Parameters.AddWithValue("@SpecificWeightInKG", product.SpecificWeightInKG);
                        command.Parameters.AddWithValue("@LengthInMeters", product.LengthInMeters);
                        command.Parameters.AddWithValue("@WeightOnePieceInKG", product.WeightOnePieceInKG);
                        command.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                    }
                    connection.Close();
                }


            }
            catch (Exception ex)
            {
                // Log or print the exception message for debugging purposes
                Console.WriteLine(ex.Message);
            }


            product.ProductID = "";
            product.Material = "";
            product.VerticalSectionType = "";
            product.VerticalSectionSize = "";
            product.Alloy = "";
            product.SpecificWeightInKG = 0;
            product.LengthInMeters = 0;
            product.WeightOnePieceInKG = 0;
            product.UnitPrice = 0;

            successMessage = "New Employee Added Succesfully";

            Response.Redirect("/Products/Index");

        }
    }
}
