using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FirstWpfDemoProject
{
    /// <summary>
    /// this class is to set up http client (or called api client) 
    /// api access class will then use the same http client
    /// it is static to make sure there is only 1 instance
    /// </summary>
    public class ApiHelper
    {
        public static HttpClient ApiClient;

        public static void InitializeApiClient() {
            if(ApiClient == null)
                ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear(); //clear the accept header
            //make sure the application type in header is json
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/JSON"));
        }

    }
}
