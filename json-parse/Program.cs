using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using json_parse.ConnectAPIwithRestSharp;

// NuGet
// Install-Package Newtonsoft.Json -Version 13.0.1
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace json_parse
{
    class Program
    {
        static void Main(string[] args)
        {
            //RestSharpImplement.RunWithAPI();
            
            //RestSharpImplement.RunWithRestSharpGetAll();
            //RestSharpImplement.RunWithRestSharpGetDetail();
            //RestSharpImplement.RunWithRestSharpPostProduct();
            
            
            RunWithDynamicParse(InputData());
            RunWithDeserializeObject(InputData());
        }

        private static void RunWithDynamicParse(string json)
        {

            Console.WriteLine("-------RunWithDynamicParse--------");
            JObject jo = JObject.Parse(json);
            dynamic dyna = jo as dynamic;
            var aryName = ((JArray)jo["Name"]).Cast<dynamic>().ToArray();
            var aryAddress = ((JArray)jo["Address"]).Cast<dynamic>().ToArray();
            Console.WriteLine("ID:{0}, Gender:{1}, Name:{2}, Address:{3}"
                            , dyna.ID, dyna.Gender, string.Join("/", aryName), string.Join("/", aryAddress));

            Console.WriteLine("-----------Object Data------------");

            foreach (var data in aryName)
            {
                Console.WriteLine("FirstName" + ":" + data.FirstName);
                Console.WriteLine("LastName" + ":" + data.LastName);
                Console.WriteLine("FullName" + ":" + data.FullName);
            }
            foreach (var data in aryAddress)
            {
                Console.WriteLine("Country" + ":" + data.Country);
                Console.WriteLine("City" + ":" + data.City);
            }
        }

        private static void RunWithDeserializeObject(string json)
        {
            dynamic dynObj = JsonConvert.DeserializeObject(json);

            Console.WriteLine("-----RunWithDeserializeObject-----");

            Console.WriteLine("ID" + ":" + dynObj.ID);
            Console.WriteLine("Gender" + ":" + dynObj.Gender);
            Console.WriteLine("Name" + ":" + dynObj.Name);
            Console.WriteLine("Address" + ":" + dynObj.Address);

            Console.WriteLine("-----------Object Data------------");
            foreach (var data in dynObj.Name)
            {
                Console.WriteLine("FirstName" + ":" + data.FirstName);
                Console.WriteLine("LastName" + ":" + data.LastName);
                Console.WriteLine("FullName" + ":" + data.FullName);
            }
            foreach (var data in dynObj.Address)
            {
                Console.WriteLine("Country" + ":" + data.Country);
                Console.WriteLine("City" + ":" + data.City);
            }
        }

        private static string InputData()
        {
            return @"
                    {
                        'ID':'28',
                        'Gender':'Male',
                        'Name':
                        [
                            {
                                'FirstName':'Da-Ming',
                                'LastName':'Wang',
                                'FullName':'Da-Ming Wang'
                            }
                        ],
                        'Address':
                        [
                            {
                                'Country':'Taiwan',
                                'City':'Taoyuan'
                            },
                            {
                                'Country':'Taiwan',
                                'City':'Kaohsiung'
                            }
                        ]
                    }
                    ";
        }
    }
}