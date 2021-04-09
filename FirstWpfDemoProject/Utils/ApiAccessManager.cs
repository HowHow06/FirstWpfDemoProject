using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirstWpfDemoProject.Utils
{
    public class ApiAccessManager
    {
        private readonly HttpClient ApiClient;
        private readonly string CustomersApiUriPrefix;

        public ApiAccessManager(string baseAddress, string customersApiPrefix) {
            ApiHelper.InitializeApiClient();//set up the static api client
            this.ApiClient = ApiHelper.ApiClient;
            this.ApiClient.BaseAddress = new Uri(baseAddress);
            this.CustomersApiUriPrefix = customersApiPrefix;
        }

        /// <summary>
        ///  Get all customers or search for customers with similar name
        /// </summary>
        /// <param name="search">customer name to search (optional)</param>
        /// <returns>list of customers</returns>
        public async Task<List<Customer>> GetCustomers(string search = "")
        {
            //using Task as return value since this method is async
            string uri = $"{CustomersApiUriPrefix}?search={search}";
            //use using keyword to dispose the object after operation
            using (HttpResponseMessage response = await ApiClient.GetAsync(uri)){
                if (response.IsSuccessStatusCode){
                    List<Customer> customers = await response.Content.ReadAsAsync<List<Customer>>();
                    return customers;
                }else{
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>customer with specific id</returns>
        public async Task<Customer> GetCustomerById(int id)
        {
            //using Task as return value since this method is async
            string uri = $"{CustomersApiUriPrefix}/{id}";
            //use using keyword to dispose the object after operation
            using (HttpResponseMessage response = await ApiClient.GetAsync(uri)){
                if (response.IsSuccessStatusCode){
                    Customer customer = await response.Content.ReadAsAsync<Customer>();
                    return customer;
                }else{
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="customer">new customer</param>
        /// <returns>status of api call</returns>
        public async Task<bool> CreateCustomer(Customer customer) {
            using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync(
                CustomersApiUriPrefix, customer)) {
                return response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Update customer by id
        /// </summary>
        /// <param name="id">id of customer to be updated</param>
        /// <param name="customer">customer object with details</param>
        /// <returns>status of api call</returns>
        public async Task<bool> UpdateCustomerById(int id, Customer customer) {
            string uri = $"{CustomersApiUriPrefix}/{id}";
            using (HttpResponseMessage response = await ApiClient.PutAsJsonAsync(
                uri,customer)){
                return response.IsSuccessStatusCode;
            }
        }

        /// <summary>
        /// Delete customer by id
        /// </summary>
        /// <param name="id">id of customer to be deleted</param>
        /// <returns>status of api call</returns>
        public async Task<bool> DeleteCustomerById(int id) {
            string uri = $"{CustomersApiUriPrefix}/{id}";
            using (HttpResponseMessage response = await ApiClient.DeleteAsync(uri)){
                return response.IsSuccessStatusCode;
            }
        }

    }
}
