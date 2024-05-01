using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using static cse3055.Pages.Orders.IndexModel;

namespace cse3055.Pages.Orders
{
    public class AddModel : PageModel
    {

        public Order order = new Order();

        public string successMessage;
        public string ErrorMessage = "";

        public void OnGet()
        {

        }
        public void OnPost()
        {
            ErrorMessage = "";
            Order order = new Order()
            {
                OrderID = string.IsNullOrEmpty(Request.Form["OrderID"]) ? 0 : int.Parse(Request.Form["OrderID"]),
                CustomerID = string.IsNullOrEmpty(Request.Form["CustomerID"]) ? 0 : int.Parse(Request.Form["CustomerID"]),
                OrderDate = string.IsNullOrEmpty(Request.Form["OrderDate"]) ? DateTime.MinValue : DateTime.Parse(Request.Form["OrderDate"]),
                DeliveryAddress = Request.Form["DeliveryAddress"],
                TotalCost = string.IsNullOrEmpty(Request.Form["TotalCost"]) ? 0 : int.Parse(Request.Form["TotalCost"]),
                Status_ = Request.Form["Status_"]
            };


            try
            {
                if (order.OrderID <= 0 ||
                    order.CustomerID <= 0 ||
                    order.OrderDate == DateTime.MinValue || // Validate Date property
                    string.IsNullOrEmpty(order.DeliveryAddress) ||
                    order.TotalCost <= 0 || // Validate numeric properties
                    string.IsNullOrEmpty(order.Status_)) // Validate string property
                {
                    ErrorMessage = "You must fill in all fields";
                    return;
                }
                string connectionString = "Data Source=MUZLAC;Initial Catalog=Database_Project;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    using (SqlCommand command = new SqlCommand("OrderEdit", connection))
                    {

                        command.CommandType = CommandType.StoredProcedure;
                        // Set the parameters for the stored procedure
                        command.Parameters.AddWithValue("@SelectOption", "Insert");
                        command.Parameters.AddWithValue("@OrderID", order.OrderID);
                        command.Parameters.AddWithValue("@CustomerID", order.CustomerID);
                        command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        command.Parameters.AddWithValue("@DeliveryAddress", order.DeliveryAddress);
                        command.Parameters.AddWithValue("@TotalCost", order.TotalCost);
                        command.Parameters.AddWithValue("@Status_", order.Status_);

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


            order.OrderID = 0;
            order.CustomerID = 0;
            order.OrderDate = DateTime.MinValue;
            order.DeliveryAddress = "";
            order.TotalCost = 0;
            order.Status_ = "";


            successMessage = "New Employee Added Succesfully";

            Response.Redirect("/Orders/Index");

        }
    }
}
