using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Serialization;
using VirtualPet.Foods;
using VirtualPet.Playing;

namespace VirtualPet
{
    internal class VirtualPet
    {
        public int Level { get; private set; }
        private int _xp;
        private int _levelup;
        private string _name;
        private string _description;
        private string _favouriteFood;
        private string _favouritePlayTime;
        private int _age;
        private int _happiness;
        private int _hunger;
        private int _maxHappiness;
        private int _maxHunger;
        private int _color;
        private List<Food> _foodList;
        private List<Play> _playList;
        public VirtualPet()
        {
            Level = 1;
            _levelup = 100;
            _xp = 0;
            _age = ChangeAge();
            _hunger = 50;
            _happiness = 50;
            _maxHappiness = 100;
            _maxHunger = 100;
            _foodList = new() { new Bread(), new Dessert(), new Fruit(), new Meat()};
            _playList = new() { new LaserPointer(), new Pet(), new PlayFight(), new ThrowBall() };
            _favouriteFood = Favorite();
            _color = Common.Randomizer(1, 14);
            _description = GiveDescription();
        }


        public string Favorite()
        {
            _favouritePlayTime = _playList[Common.Randomizer(0, _playList.Count)].SetFavorite();
            return _foodList[Common.Randomizer(0, _foodList.Count)].SetFavorite();

        }
        public void DecreaseFulfillment()
        {
           _hunger = _hunger >= _maxHunger / 2 ? _hunger -= 20 : _hunger -= 10;
           _happiness = _happiness >= _maxHappiness / 2 ? _happiness -= 20 : _happiness -= 10;
           if (_hunger <= 0 || _happiness <= 0)
           {
               Console.WriteLine(@$"You have failed to take care of your pet, someone better is taking care of it now.
the name {_name} will soon leave its memory, just like you will. GAME OVER.");
                Console.WriteLine("Closing, standby.");
               Thread.Sleep(10000);
                Environment.Exit(1);
                
           }
           CheckifLevelUp();
           DrawStats(false);
        }
        private static int ChangeAge()
        {
            return Common.Randomizer(4, 20);
        }

        public void ChangeName()
        {
            Common.SetStringColor("Please give me a name :)",_color);
            _name = Console.ReadLine();
            Common.SetStringColor(@$"{_name}... Yes I like it! Thank you.", _color);
        }

        public void Feed()
        {
            Console.WriteLine(@$"
You have chosen to feed {_name} try to find its favorite food!
");
     g:{ Console.WriteLine(@$"Here are your options, press the corresponding key.

B: Bread, D: Dessert, F: Fruit, M: Meat, press the ESC/Escape key to cancel.
");}

            var key = Console.ReadKey(true).Key;
            Food choice;
            
            switch (key)
                {
                    case ConsoleKey.B:
                        choice = _foodList[0];
                        break;
                    case ConsoleKey.D:
                        choice = _foodList[1];
                        break;
                    case ConsoleKey.F:
                        choice = _foodList[2];
                        break;
                    case ConsoleKey.M:
                        choice = _foodList[3];
                        break;
                    case ConsoleKey.Escape: return;
                    default:
                        Console.WriteLine("Press a valid key!");
                       Thread.Sleep(300); goto g;
            }

            Console.WriteLine($@"You feed it {choice.Name}
{choice.Description}
");
            Thread.Sleep(500);
            Common.SetStringColor(choice.IsFavorite ? "Wow my favorite :)" : "That's Okay", _color);
            _xp += choice.XpValue;
            _hunger += choice.hungerValue;
            if (_hunger >= _maxHunger)
            {
                _hunger = _maxHunger;
                Console.WriteLine(
                    "You have hit the hunger cap, you should probably do something other than feeding for a while.");
                Thread.Sleep(1500);
            }
        }

        public void Play()
        {
            Console.WriteLine(@$"
You have chosen to play with {_name} try to find its favorite activity!
");
            g: { Console.WriteLine(@$"Here are your options, press the corresponding key.

L: Laser Pointer, P: Pet, F: Play Fight, B: Throw Ball, press the ESC/Escape key to cancel.
"); }

            var key = Console.ReadKey(true).Key;
            Play choice;

            switch (key)
            {
                case ConsoleKey.L:
                    choice = _playList[0];
                    break;
                case ConsoleKey.P:
                    choice = _playList[1];
                    break;
                case ConsoleKey.F:
                    choice = _playList[2];
                    break;
                case ConsoleKey.B:
                    choice = _playList[3];
                    break;
                case ConsoleKey.Escape: return;
                default:
                    Console.WriteLine("Press a valid key!");
                    Thread.Sleep(300); goto g;
            }
            Console.WriteLine($@"You decide to {choice.Name}
{choice.Description}
");
            Thread.Sleep(500);

            Common.SetStringColor(choice.IsFavorite ? "Wow my favorite :)" : "That's Okay", _color);
            _xp += choice.XpValue;
            _happiness+= choice.HappinessValue;
            if (_happiness >= _maxHappiness)
            {
                _happiness = _maxHappiness;
                Console.WriteLine(
                    "You have hit the happiness cap, you should probably do something other than playing for a while.");
                Thread.Sleep(1500);
            }

        }

        private void CheckifLevelUp()
        {
            
            if (_xp >= _levelup)
            {
                
                
                Console.WriteLine($@"{_name} is about to level up!
");
                Thread.Sleep(2000); Console.Clear();

                DrawStats(true);
                Console.WriteLine("Leveling...");
                _xp -= _levelup;
                if (_xp < 0) _xp = 0;
                Level++;
                _levelup = 100 * Level;
                _maxHappiness = 100 + 5 * Level;
                _maxHunger = 100 + 5 * Level;
                Thread.Sleep(3000); 
                Console.Clear();

            }
        }

        public void DrawStats(bool levelup)
        {
            // add level up boolean with different draw.
            Common.SetStringColor(
                levelup ? @$"Name: {_name}
Level: {Level} + 1
Xp: {(_xp - _levelup)}/{_levelup} + 0/{100 * (Level + 1)}
Age: {_age}
Hunger: {_hunger}/{_maxHunger} + 0/{5 * (Level + 1)}
Happiness: {_happiness}/{_maxHappiness}  + 0/{5 * (Level + 1)}"
                : // Ternary split. Could inline it, but can I be bothered?
                @$"Name: {_name}
Level: {Level}
Xp: {_xp}/{_levelup}
Age: {_age}
Hunger: {_hunger}/{_maxHunger}
Happiness: {_happiness}/{_maxHappiness}", _color);
        }

        public void Examine()
        {
            DrawStats(false);
            Console.WriteLine(@"
");
            Common.SetStringColor(_description,_color);

        }

        public string GiveDescription()
        {
            var creatures = new[] { "Feline","Canine","Lizard","God","Insect","Bird" };
            var creature = creatures[Common.Randomizer(0,creatures.Length)];
            var color = Enum.GetName(typeof(ConsoleColor), _color);
            return @$"A {_age} year old {color} {creature}.";
        }
    }
}
