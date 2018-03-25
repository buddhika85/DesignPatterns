using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var singleton1 = SingletonThreadSafe.GetSingleton();
            var singleton2 = SingletonThreadSafe.GetSingleton();

            if (singleton1 == singleton2)
            {
                Console.WriteLine("==");
            }
            if (singleton1.Equals(singleton2))
            {
                Console.WriteLine("equals");
            }
            if (object.ReferenceEquals(singleton1, singleton2))
            {
                Console.WriteLine("ReferenceEquals");
            }

            Console.ReadKey();
        }
    }

    public sealed class SingletonThreadSafe                 // sealed avoids inheritance
    {
        private static object singletonLock = new object();
        private static SingletonThreadSafe singleton;

        private SingletonThreadSafe()
        {
        }

        public static SingletonThreadSafe GetSingleton()
        {
            lock(singletonLock)                             // a thred needs to aquire the lock to execute below code
            {
                if (singleton == null)
                {
                    singleton = new SingletonThreadSafe();
                }
                return singleton;
            }
        }
    }    
}
