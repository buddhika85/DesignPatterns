using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPattern
{
    // Ref - http://www.dotnettricks.com/learn/designpatterns/adapter-design-pattern-dotnet    
    class Program
    {
        static void Main(string[] args)
        {
            IAdpater hrAdapter = new HRSystemAdapter();
            ThirdPartyPaySystem thirdPartySystem = new ThirdPartyPaySystem(hrAdapter);
            thirdPartySystem.ShowEmployeeNames();

            Console.ReadKey();
        }
    }

    // Client
    public class ThirdPartyPaySystem
    {
        IAdpater adapter;

        public ThirdPartyPaySystem(IAdpater customAdapter)
        {
            adapter = customAdapter;
        }

        public void ShowEmployeeNames()
        {
            // Cannot work with HR system, as it returns List<Employee> objects not List<string>
            // So, need to Communicates with Adpater which gives List<string>
            List<string> employeeNames = adapter.GetEmployeesNames();
            employeeNames.ForEach(e => Console.WriteLine(e));
        }        
    }

    // Adapter interface
    public interface IAdpater
    {
        List<string> GetEmployeesNames();
    }

    // Adapter
    public class HRSystemAdapter : HRSystem, IAdpater
    {
        public List<string> GetEmployeesNames()
        {
            List<Employee> employees = GetEmployees();
            return 
                (from e in employees select string.Format("{0} {1}", e.FirstName, e.LastName)).ToList();
        }        
    }

    // Source - Adaptee
    public class HRSystem
    {
        public List<Employee> GetEmployees()
        {
            return new List<Employee> {
                new Employee { Id = 1, FirstName = "Buddhika", LastName = "Gunasekera"},
                new Employee { Id = 2, FirstName = "Micheal", LastName = "Jackson"},
                new Employee { Id = 3, FirstName = "John", LastName = "Smith"},
            };
        }
    }

    // Model class
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
