// See https://aka.ms/new-console-template for more information

using System.Net.Mime;

namespace VirtualPet
{


    class Program
    {

        public static void Main(string[] args)
        {
            var pet = new VirtualPet();
            
           
            pet.ChangeName();
            Console.WriteLine(@"
To win you need to level your pet to level 5, try balancing their needs");
            while (pet.Level != 5)
            {
             reset:  Console.WriteLine(@$"
Please choose one to do. P: Play, E: Examine, F: Feed");
             var key = Console.ReadKey(true).Key;
             // do something with this.
                switch (key)
                {
                    case ConsoleKey.P:
                        pet.Play();
                        break;
                    case ConsoleKey.E:
                        pet.Examine();
                        goto reset;
                    case ConsoleKey.F:
                        pet.Feed();
                        break;
                    default:
                        Console.WriteLine("Press a valid key!");
                        Thread.Sleep(300); goto reset;
                }
                pet.DecreaseFulfillment();
                
            }
            Console.WriteLine("Congratulations you win.");
        }






    }





}
