using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using static cse3055.Pages.Depots.IndexModel;


namespace cse3055.Pages.Depots
{
    public class AddModel : PageModel
    {
        public Depot depot = new Depot();

        public string successMessage;
        public string ErrorMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
            ErrorMessage = "";
            Depot depot = new()
            {
                DepotID = string.IsNullOrEmpty(Request.Form["DepotID"]) ? 1 : int.Parse(Request.Form["DepotID"]),
                DepotProductID = Request.Form["DepotProductID"],
                DepotProduct = Request.Form["DepotProduct"]
            };

            try
            {
                if (depot.DepotID == 0 ||
                    string.IsNullOrEmpty(depot.DepotProductID) ||
                    string.IsNullOrEmpty(depot.DepotProduct))
                {
                    ErrorMessage = "You must fill in all fields";
                    return;
                }
                string connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand("DepotEdit", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        // Set the parameters for the stored procedure
                        command.Parameters.AddWithValue("@SelectOption", "Insert");
                        command.Parameters.AddWithValue("@DepotID", depot.DepotID);
                        command.Parameters.AddWithValue("@DepotProductID", depot.DepotProductID);
                        command.Parameters.AddWithValue("@DepotProduct", depot.DepotProduct);

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


            depot.DepotID = 1;
            depot.DepotProductID = "";
            depot.DepotProduct = "";

            successMessage = "New Depot Added Succesfully";

            Response.Redirect("/Depots/Index");

        }
    }
}
