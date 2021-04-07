using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FirstWpfDemoProject
{
    public class DataAccessManager
    {
        /// <summary>
        /// The final conenction string used to connect to database.
        /// </summary>
        private readonly string CONNECTION_STRING;

        /// <summary>
        /// Create DataAccessManager by passing in the name of connection in config.
        /// </summary>
        /// <param name="connectionName">Name of connection in config.</param>
        public DataAccessManager(string connectionName) {
            this.CONNECTION_STRING = ConfigurationHelper.GetConnectionString(connectionName);
        }

        /// <summary>
        /// Get all customers as a List using stored procedure.
        /// </summary>
        /// <returns>List of Customers</returns>
        public List<Customer> GetAllCustomers() {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string sp = "dbo.spCustomer_GetAll";
                return connection.Query<Customer>(sp, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <returns>customer with the same customer id</returns>
        public Customer GetCustomerById(int customerId) {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string sql = @"SELECT * FROM Customer WHERE CustomerId = @CustomerId";
                return connection.Query<Customer>(sql, new { CustomerId = customerId }).ToList().First();//return the first customer with the same customer id
            }
        }


        /// <summary>
        /// Search customers by name.
        /// </summary>
        /// <param name="name">Name of customer that is entered in the search box.</param>
        /// <returns>List of Customers</returns>
        public List<Customer> SearchCustomersByName(string name) {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string sp = "dbo.spCustomer_GetByName";
                return connection.Query<Customer>(sp, new { FirstName = name, LastName = name }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        /// <summary>
        /// Delete the customer with specific customer id
        /// </summary>
        /// <param name="customerId">Customer id of the customer to be deleted</param>
        /// <returns>status of operation</returns>
        public bool DeleteCustomerById(int customerId) {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string sp = "dbo.spCustomer_DeleteById";
                int affectedRowsCount = connection.Execute(sp, new { CustomerId = customerId }, commandType: CommandType.StoredProcedure);
                return affectedRowsCount == 1;
            }
        }

        /// <summary>
        /// Update the details of customer with specific customer id
        /// </summary>
        /// <param name="customer">customer to be updated</param>
        /// <returns>status of operation</returns>
        public bool UpdateCustomerById(Customer customer) {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string sp = "dbo.spCustomer_UpdateById";
                int affectedRowsCount = connection.Execute(sp, new { CustomerId = customer.CustomerId, FirstName = customer.FirstName, LastName = customer.LastName, Email = customer.Email, PhoneNumber = customer.PhoneNumber}, commandType: CommandType.StoredProcedure);
                return affectedRowsCount == 1;
            }
        }

        /// <summary>
        /// Insert a new customer to database.
        /// </summary>
        /// <param name="newCustomer">Customer to be created.</param>
        /// <returns>status of operation</returns>
        public bool CreateOneCustomer(Customer newCustomer) {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                string sp = "dbo.spCustomer_CreateOneCustomer";
                int affectedRowsCount = connection.Execute(sp, new { FirstName = newCustomer.FirstName, LastName = newCustomer.LastName, Email = newCustomer.Email, PhoneNumber = newCustomer.PhoneNumber }, commandType: CommandType.StoredProcedure);
                return affectedRowsCount == 1;
            }
        }

      

    }
}
