using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new DocumentFacotry();
            IDocument notepad = factory.GetDocument("notepad");
            IDocument word = factory.GetDocument("word");

            notepad.Read();
            word.Read();

            Console.ReadKey();
        }
    }

    interface IDocument
    {
        void Read();
    }

    class NotepadDocument : IDocument
    {        
        public void Read()
        {
            Console.WriteLine("Read Notepad");
        }
    }

    class WordDocument : IDocument
    {
        public void Read()
        {
            Console.WriteLine("Read Word document");
        }
    }

    // factory class
    class DocumentFacotry
    {
        // factory method - A subtype of IDocument will be created, and the specific sub type to created is decided by this method, and it uses passed param value to make that decision
        public IDocument GetDocument(string documentType)
        {
            switch(documentType)
            {
                case "notepad":
                    {
                        return new NotepadDocument();
                    }
                case "word":
                    {
                        return new WordDocument();
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
