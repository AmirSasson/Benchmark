using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineTests
{
    class ElasticRepo<T> : IRepo<T> where T : class
    {
        ElasticClient _elasticClient;//= new ElasticClient(settings);
        ILogger _log;
        public ElasticRepo(ILogger log)
        {
            _log = log;
            var node = new Uri("http://iug-elkdev-01:9200");

            var settings = new ConnectionSettings(node)
            .DefaultIndex("amirtest")
            .RequestTimeout(TimeSpan.FromSeconds(5))
            //.Proxy(new Uri("http://iug-elkdev-01:8080"), "LoginManagerUser", "C0st4Br4va")
            ;
            _elasticClient = new ElasticClient(settings);
            var health = _elasticClient.ClusterHealth(new ClusterHealthRequest(Indices.Parse("amirtest")));


            if (!health.IsValid)
            {
                throw new Exception($"ElasticRepo Failed to start! {health}");
                //SomeDocument doc = new SomeDocument() { SomeInt = 1, Name = "Amir" + DateTime.Now.Ticks, Id = DateTime.Now.Ticks };

                //Console.WriteLine(dr.Source.Name);
            }
        }

        public T Get(object o)
        {
            return _elasticClient.Get<T>(new DocumentPath<T>(new Id(o))).Source;
        }

        public void Set(T obj)
        {
            var resp = _elasticClient.Index<T>(obj);
            _log.Log($"Saving object {resp.IsValid}");
        }
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
