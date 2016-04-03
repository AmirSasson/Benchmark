using Elasticsearch.Net;
using Microsoft.Owin.Hosting;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineTests
{

    public class SomeDocument
    {
        public int SomeInt { get; set; }
        public string Name { get; set; }
        public long Id { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {

            string baseAddress = "http://localhost:3000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //var response = client.GetAsync(baseAddress + "api/values").Result;

                // Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine();
            }

           




            //var node = new Uri("http://iug-elkdev-01:9200");

            //var settings = new ConnectionSettings(node)
            //.DefaultIndex("amirtest")
            //.RequestTimeout(TimeSpan.FromSeconds(5))
            ////.Proxy(new Uri("http://iug-elkdev-01:8080"), "LoginManagerUser", "C0st4Br4va")
            //;
            //var elasticClient = new ElasticClient(settings);
            //var health = elasticClient.ClusterHealth(new ClusterHealthRequest(Indices.Parse("amirtest")));


            //if (health.IsValid)
            //{
            //    SomeDocument doc = new SomeDocument() { SomeInt = 1, Name = "Amir" + DateTime.Now.Ticks, Id = DateTime.Now.Ticks };
            //    var resp = elasticClient.Index<SomeDocument>(doc);
            //    var dr = elasticClient.Get<SomeDocument>(new DocumentPath<SomeDocument>(new Id(new { Id = doc.Id })));
            //    Console.WriteLine(dr.Source.Name);
            //}









            Console.ReadKey();
        }




        static bool StateA()
        {
            Console.WriteLine("IN StateA !!");
            Console.WriteLine("StateA !!");
            Task.Delay(1500).Wait();
            return true;
        }

        static bool StateException()
        {
            Console.WriteLine("IN StateException !!");
            throw new Exception("StateException");
            //Console.WriteLine("StateA !!");
            //Task.Delay(500).Wait();
            //return true;
        }

        async static Task<string> StateB(string a)
        {
            Console.WriteLine("IN StateB !!");
            await Task.Delay(TimeSpan.FromSeconds(1));

            //throw new Exception("StateB Exception");
            Console.WriteLine("StateB !!");
            return "success B";
        }


        async static Task<string> StateC(string a)
        {
            Console.WriteLine("IN StateC !!");
            await Task.Delay(TimeSpan.FromSeconds(1));

            throw new Exception("StateC Exception");
            Console.WriteLine("StateC !!");
            return "success C";
        }
    }
}
