using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace cse3055.Pages.Customers
{
    public class UpdateModel : PageModel
    {
        public string successMessage = "";
        public string ErrorMessage = "";
        public void OnGet()
        {

        }
        public void OnPost()
        {
            {


                try
                {

                    string CustomerID = Request.Query["CustomerID"];
                    string Name = Request.Form["Name"];
                    string District = Request.Form["District"];
                    string PhoneNumber = Request.Form["PhoneNumber"];
                    string FaxNumber = Request.Form["FaxNumber"];
                    string Website = Request.Form["Website"];



                    string connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        connection.Open();
                        using (SqlCommand command = new SqlCommand("CustomerEdit", connection))
                        {

                            command.CommandType = CommandType.StoredProcedure;
                            // Add parameters to the stored procedure
                            command.Parameters.AddWithValue("@SelectOption", "Update");
                            command.Parameters.AddWithValue("@CustomerID", CustomerID);
                            command.Parameters.AddWithValue("@Name", Name);
                            command.Parameters.AddWithValue("@District", District);
                            command.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                            command.Parameters.AddWithValue("@FaxNumber", FaxNumber);
                            command.Parameters.AddWithValue("@Website", Website);

                            // Execute the stored procedure
                            command.ExecuteNonQuery();

                        }
                        connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }

                successMessage = "Customer Updated Succesfully";
            }
        }
    }
}
