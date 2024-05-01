using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using static cse3055.Pages.Customers.CustomerModel;


namespace cse3055.Pages.Customers
{
    public class AddModel : PageModel
    {
        public Customer Customer = new Customer(); 

        public string successMessage;
        public string ErrorMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
            ErrorMessage = "";
            Customer customer = new()
            {   
                CustomerID = string.IsNullOrEmpty(Request.Form["CustomerID"]) ? 0 : int.Parse(Request.Form["CustomerID"]),
                Name = Request.Form["Name"],
                District = Request.Form["District"],
                PhoneNumber = Request.Form["PhoneNumber"],
                FaxNumber = Request.Form["FaxNumber"],
                Website = Request.Form["Website"]

            };

            try
            {
                if (customer.CustomerID == 0 ||
                    string.IsNullOrEmpty(customer.Name) ||
                    string.IsNullOrEmpty(customer.District) ||
                    string.IsNullOrEmpty(customer.PhoneNumber) ||
                    string.IsNullOrEmpty(customer.FaxNumber) ||
                    string.IsNullOrEmpty(customer.Website))
                {
                    ErrorMessage = "You must fill in all fields";
                    return;
                }
                string connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CustomerEdit", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        // Set the parameters for the stored procedure
                        command.Parameters.AddWithValue("@SelectOption", "Insert");
                        command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                        command.Parameters.AddWithValue("@Name_", customer.Name.ToUpper());
                        command.Parameters.AddWithValue("@District", customer.District);
                        command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                        command.Parameters.AddWithValue("@FaxNumber", customer.FaxNumber);
                        command.Parameters.AddWithValue("@Website", customer.Website);

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


            customer.CustomerID = 0;
            customer.Name = "";
            customer.District = "";
            customer.PhoneNumber = "";
            customer.FaxNumber = "";
            customer.Website = "";

            successMessage = "New Employee Added Succesfully";
            
            Response.Redirect("/Customers/Customer");

        }
    }
}
