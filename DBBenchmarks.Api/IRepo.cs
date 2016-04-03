using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachineTests
{
    public interface IRepo<T>
    {
        T Get(object o);
        void Set(T obj);
    }

    public interface ILogger
    {
        void Log(string msg);
    }


    public class UserProps
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }



}


