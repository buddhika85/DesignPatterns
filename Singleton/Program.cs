using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    // Ref - http://csharpindepth.com/Articles/General/Singleton.aspx
    class Program
    {
        static void Main(string[] args)
        {
            var singleton1 = SingletonThreadSafe.GetSingleton();
            var singleton2 = SingletonThreadSafe.GetSingleton();
            CheckEquality(singleton1, singleton2);

            var singleton3 = SingletonLazyThreadSafe.GetSingleton().Value;
            var singleton4 = SingletonLazyThreadSafe.GetSingleton().Value;            
            CheckEquality(singleton3, singleton4);

            var singleton5 = new SingletonLazyThreadSafe();         
            CheckEquality(singleton4, singleton5);                  // Should not print anything as it vioaltes singleton

            Console.ReadKey();
        }

        private static void CheckEquality(SingletonThreadSafe singleton1, SingletonThreadSafe singleton2)
        {
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
                Console.WriteLine("ReferenceEquals\n");
            }
        }

        private static void CheckEquality(SingletonLazyThreadSafe singleton1, SingletonLazyThreadSafe singleton2)
        {
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
        }
    }

    

    public sealed class SingletonThreadSafe                 // sealed avoids inheritance
    {
        private readonly static object singletonLock = new object();
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

    public sealed class SingletonLazyThreadSafe
    {
        private static Lazy<SingletonLazyThreadSafe> singleton;
        private readonly static object singletonLock = new object();
        public SingletonLazyThreadSafe()                            // public contrtuctor works with Lazy keyword for singletons - wrong 
        {
        }

        public static Lazy<SingletonLazyThreadSafe> GetSingleton()
        {
            lock(singletonLock)
            {
                if (singleton == null)
                {
                    singleton = new Lazy<SingletonLazyThreadSafe>();
                }
                return singleton;
            }
        }
    }
}
