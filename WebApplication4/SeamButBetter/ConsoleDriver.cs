using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConversationManager.SeamButBetter
{
    public class ConsoleDriver : IDriver
    {
        public void Add<T>(int id, T obj)
        {
            Console.WriteLine("Dodano nową konwersację o id: ", id);
        }

        public void CheckTimeout()
        {
            Console.WriteLine("Wyrzucono przeterminowane konwersacje");
        }

        public void Delete(int id)
        {
            Console.WriteLine("Usunięto konwersację o id: ", id);
        }

        public string Get(int id)
        {
            Console.WriteLine("Pobrano konwersację o id: ", id);
            return "";
        }

        public void Update<T>(int id, T obj)
        {
            Console.WriteLine("Zaktualizowano konwersację o id: ", id);
        }
    }
}
