using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadePattern
{
    // To watch movie in HomeTheatre system facade
    // it needs to operate screen, DVD player, Speakers, Projector
    public class Program
    {
        static void Main(string[] args)
        {
            var facade = new HomeTheatreSystemFacade();
            facade.WatchMovie();
            Console.ReadKey();
        }
    }
       
    #region subSystems
    public class TVScreen
    {
        public decimal Brightness { get; set; }
        public decimal Contrast { get; set; }
        public bool IsOn { get; set; }
        public void OperateScreen(decimal brightness, decimal contrast, bool on)
        {
            IsOn = on;
            if (on)
            {
                Brightness = brightness;
                Contrast = contrast;                
                Console.WriteLine("Turn on TV screen. Set brightness {0}, contrast {1}", brightness, contrast);
            }
            else
            {
                Console.WriteLine("Turn off TV screen");
            }    
        }      
    }

    public class Speakers
    {
        public decimal Volume { get; set; }
        public bool IsOn { get; set; }
        public void Operate(decimal volume, bool on)
        {
            IsOn = on;
            if (on)
            {                
                Volume = volume;
                Console.WriteLine("Speaker on, Volume {0}", volume);
            }
            else
            {
                Console.WriteLine("Turn Off Speaker");
            }
        }
    }

    public class DVDPlayer
    {
        public bool ReadingDvd { get; set; }
        public void ReadDvd(TVScreen tv, Speakers speaker)
        {
            if (tv.IsOn && speaker.IsOn)
            {
                ReadingDvd = true;
                Console.Write("DVD reading");
            }
            else
            {
                ReadingDvd = false;
                Console.Write("DVD Player Off");
            }
        }
    }

    public class Projector
    {
        public void ProjectToHomeThareScreen(TVScreen tv, Speakers speaker, DVDPlayer dvd)
        {
            if (tv.IsOn && speaker.IsOn && dvd.ReadingDvd)
            {
                Console.WriteLine("Started Projecting to Screen");
            }
            else
            {
                Console.WriteLine("Cannot project to screen - Maybe TV, speaker or Dvd player not functioning");
            }
        }
    }
    #endregion subSystem 
    
    #region facade
    public class HomeTheatreSystemFacade
    {
        TVScreen tv;
        Speakers speakers;
        DVDPlayer dvd;
        Projector projector;

        public HomeTheatreSystemFacade()
        {
            tv = new TVScreen();
            speakers = new Speakers();
            dvd = new DVDPlayer();
            projector = new Projector();
        }

        public void WatchMovie()
        {
            Console.WriteLine("Start watch movie");
            tv.OperateScreen(75, 80, true);
            speakers.Operate(25, true);
            dvd.ReadDvd(tv, speakers);
            projector.ProjectToHomeThareScreen(tv, speakers, dvd);
            Console.WriteLine("Now watching");
        }
    }
    #endregion facade
}
