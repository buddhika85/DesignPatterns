using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    // Ref - https://www.tutorialspoint.com/design_pattern/observer_pattern.htm
    // http://www.dotnettricks.com/learn/designpatterns/observer-design-pattern-c-sharp
    public class Program
    {
        static void Main(string[] args)
        {
            // initial stock price
            var initialStockPrice = new StockModel
            {
                StockName = "Virtusa Stock Price",
                CurrentStockPrice = 1000,
                LastUpdated = DateTime.Now
            };

            // subject to be observed
            var subject = new StockPrice(initialStockPrice);

            // attach observers
            AbstractObserver londonObserver = new LondonStockPriceView(subject);
            AbstractObserver newYorkObserver = new NewYorkStockPriceView(subject);

            // change stock price
            subject.SetState(900);      Console.WriteLine();      
            subject.SetState(1100);     Console.WriteLine();
            subject.SetState(1200);     Console.WriteLine();

            Console.ReadKey();
        }
    }

    public class StockModel
    {
        public string StockName { get; set; }
        public decimal CurrentStockPrice { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    #region Observee - one which is observed

    // Entity / Value that is observed, Observee
    public class StockPrice
    {
        public StockModel CurrentStockState { get; set; }
        public IList<AbstractObserver> Observers { get; set; }

        public StockPrice(StockModel initalStockState)
        {
            CurrentStockState = initalStockState;
            Observers = new List<AbstractObserver>();
        }

        public void SetState(decimal upatedStockPrice)
        {
            this.CurrentStockState.CurrentStockPrice = upatedStockPrice;
            this.CurrentStockState.LastUpdated = DateTime.Now;
            NotifyAllObersvers();
        }

        public StockModel GetState()
        {
            return this.CurrentStockState;
        }

        public void AttachObserver(AbstractObserver observer)
        {
            this.Observers.Add(observer);
        }

        public void DettachObserver(AbstractObserver observer)
        {
            var toRemove = this.Observers.SingleOrDefault(o => o.Id == observer.Id);
            this.Observers.Remove(toRemove);
        }

        private void NotifyAllObersvers()
        {
            foreach (var observer in Observers)
            {
                observer.Update(this.CurrentStockState);
            }
        }
    }

    #endregion 

    #region Observers

    public abstract class AbstractObserver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StockPrice SubjectObserved { get; set; }

        public StockModel MyStockDisplay { get; set; }

        public abstract void Update(StockModel updatedModel);

        public void Display()
        {
            Console.WriteLine("{0} - {1} @ {2} is {3}", this.GetType().Name, MyStockDisplay.StockName, MyStockDisplay.LastUpdated, MyStockDisplay.CurrentStockPrice);            
        }
    }

    public class LondonStockPriceView : AbstractObserver
    {
        public LondonStockPriceView(StockPrice subjectObserved)
        {
            this.Id = 1;
            this.Name = "London Stock Price View";
            this.SubjectObserved = subjectObserved;
            this.SubjectObserved.AttachObserver(this);
        }
        public override void Update(StockModel updatedModel)
        {
            this.MyStockDisplay = updatedModel;
            Display();
        }
    }

    public class NewYorkStockPriceView : AbstractObserver
    {
        public NewYorkStockPriceView(StockPrice subjectObserved)
        {
            this.Id = 1;
            this.Name = "Newyork Stock Price View";
            this.SubjectObserved = subjectObserved;
            this.SubjectObserved.AttachObserver(this);
        }
        public override void Update(StockModel updatedModel)
        {
            this.MyStockDisplay = updatedModel;
            Display();
        }
    }

    #endregion 
}
