using System;
using System.Net;
using System.Collections.Generic;
using System.Text;

using RestSharp;
using Newtonsoft.Json;

using json_parse.ViewModels;

namespace json_parse.ConnectAPIwithRestSharp
{
    /// <summary>
    /// API Sample: https://github.com/yorklin020/dotnet-core-web-api-with-swagger
    /// </summary>
    public static class RestSharpImplement
    {
        public static string url = "https://localhost:44370";
        public static void RunWithAPI()
        {
            Console.WriteLine("------------RunWithAPI------------");
            string url = "https://localhost:44370/weatherforecast";

            using (WebClient wc = new WebClient())
            {
                var response = wc.DownloadString(new Uri(url));
                var myobjs = JsonConvert.DeserializeObject<List<WeatherForecastViewModel>>(response);

                Write(myobjs);

                //foreach (var obj in myobjs)
                //{
                //    Console.WriteLine(
                //        $"key:{obj.Date}\t" +
                //        $"value:{obj.TemperatureC}\t" +
                //        $"value:{obj.TemperatureF}\t" +
                //        $"value:{obj.Summary}");
                //}
                //Console.ReadKey();
            }
        }

        public static void RunWithRestSharpGetAll()
        {
            Console.WriteLine("------------RunWithGet------------");
            var client = new RestClient(url);
            var request = new RestRequest("api/Sample/GetAllProducts", Method.GET);

            var response = client.Execute(request);
            Console.WriteLine(response.Content);   
        }

        public static void RunWithRestSharpGetDetail()
        {
            Console.WriteLine("-----------RunWithGetId-----------");
            var client = new RestClient(url);
            var request = new RestRequest("api/Sample/GetProductDetail/{id}", Method.GET);

            request.Timeout = 10000;
            request.AddUrlSegment("id", 1);
            var response = client.Execute(request);
            Console.WriteLine(response.Content);

        }

        public static void RunWithRestSharpPostProduct()
        {
            Console.WriteLine("------------RunWithPost-----------");
            ProductDetail dataDetail = new ProductDetail
            {
                ProductNo = "004",
                ProductType = "Test"
            };
            Products data = new Products
            {
                Id = 4,
                Product = dataDetail
            };

            var jsonString = JsonConvert.SerializeObject(data);
            Console.WriteLine("Input Json: " + jsonString);

            var client = new RestClient(url);
            var request = new RestRequest("api/Sample/PostProducts", Method.POST);       
            request.AddJsonBody(jsonString);

            var response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        public class Products
        {
            public int Id;
            public ProductDetail Product;
        }

        public class ProductDetail
        {
            public string ProductNo;
            public string ProductType;
        }

        private static void Write(this List<WeatherForecastViewModel> rootObject)
        {
            foreach (var obj in rootObject)
            {
                Console.WriteLine("date: " + obj.Date);
                Console.WriteLine("temperatureC: " + obj.TemperatureC);
                Console.WriteLine("temperatureF: " + obj.TemperatureF);
                Console.WriteLine("summary: " + obj.Summary);
            }
        }
    }
}
