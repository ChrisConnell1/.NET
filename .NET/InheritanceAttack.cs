using System;
using System.Collections.Generic;

namespace Inheritance06
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new Player() { Name = "Bob", Strength = 20 };
            var warrior = new Warrior() { Name = "Baltek", Strength = 100, Bonus = 10 };
            var wizard = new Wizard() { Name = "Pentagorn", Strength = 50, Energy = 50 };

            var players = new List<Player>
            {
                player,
                warrior,
                wizard
            };

            DoBattle(players);

            Console.ReadLine();
        }

        static void DoBattle(List<Player> players)
        {

            foreach (var player in players)
            {
                player.Attack();
                Console.WriteLine("");
            }
        }

        class Player
        {
            public string Name { get; set; }
            public int Strength { get; set; }
 

            public virtual void Attack()
            {
                var amount = GenerateRandomNumber(Strength);
                Console.Write($"{Name} attacked for {amount} damage.");
            }

            protected int GenerateRandomNumber(int amount)
            {
                Random random = new Random();
                return random.Next(1, amount + 1);
            }

        }

        class Warrior : Player
        {
            public int Bonus { get; set; }

            public override void Attack()
            {
                var amount = GenerateRandomNumber(Strength);
                
                Console.Write($"{Name} charged for {amount + Bonus} damage (includes {Bonus} bonus damage)");
            }
        }

        class Wizard : Player
        {
            public int Energy { get; set; }

            public override void Attack()
            {
                base.Attack();

                var amount = GenerateRandomNumber(10);
                Energy -= amount;

                Console.WriteLine($"\n Wizard {Name} drained {amount} Engery");
            }
        }
    }
}
