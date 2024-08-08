using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool arenaWork = true;
            int money = 100;
            Random rand = new Random();

            String[] names = {"Leon", "Obama", "Sigma", "Gigachad", "Spartak", "Tyler"};
            
            List<Warrior> warriors = 
            [
                new Swordsman("Hercules", 700, 100, 200),
                new Dima("dima", 260, 120, 120),
                new Swordsman("Krator", 600, 100, 300),

            ];
            Console.WriteLine("Welcome to the Colleseum");

            while (arenaWork)
            {
                
                Console.Write("List of warriors:");
                Console.WriteLine($"                                                           Your current money status is {money} coins");
                for (int i = 0; i < warriors.Count; i++)
                {
                    Console.WriteLine($"{i+1}. {warriors[i].ToString()}");
                }
                Console.WriteLine();

                Console.WriteLine("Choose the warrior by his index - 1\nCreate your own warrior - 2 ");
                Console.Write("Choose an option: ");
                int UserChoice = Convert.ToInt32(Console.ReadLine());
                int warriorChoice;
                switch (UserChoice)
                {
                    case 1:
                        Console.Write("Choose 1 of the warriors from above: ");
                        int warriorChoiceCase = Convert.ToInt32(Console.ReadLine());
                        while (warriorChoiceCase <= 0 || warriorChoiceCase > warriors.Count)
                        {
                            Console.WriteLine("Youre out of range of warriors, enter again:");
                            warriorChoiceCase = Convert.ToInt32(Console.ReadLine());
                        }
                        warriorChoiceCase -= 1;
                        warriorChoice = warriorChoiceCase;
                        Console.WriteLine($"You have choosen {warriors[warriorChoiceCase].Name}");
                            break;
                    case 2:
                        Console.Write("You're creating a new warrior.\nEnter warrior name:");
                        string warriorName = Console.ReadLine();
                        Console.Write("Enter warrior's health:");
                        int warriorHP = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter warrior's armor:");
                        int warriorARM = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter warrior's damage:");
                        int warriorDMG = Convert.ToInt32(Console.ReadLine());
                        
                        if( warriorHP + warriorARM + warriorDMG > 1000)
                        {
                            System.Console.WriteLine("Your warrior is too powerful, create weaker warrior");
                            goto case 2;

                        }

                        warriors.Add(new Swordsman(warriorName, warriorHP, warriorDMG, warriorARM));
                        warriorChoiceCase = warriors.Count - 1;
                        warriorChoice = warriorChoiceCase;
                        break;

                    default:
                        Console.Write("Wrong choice, redirecting to case 1...");
                        goto case 1;
                }



                int enemy = rand.Next(0, warriors.Count - 1);
                while (enemy == warriorChoice) { enemy = rand.Next(0, warriors.Count - 1); }
                System.Console.Write("How many money do you want to bet on your warrior? ");
                int bet = Convert.ToInt32(Console.ReadLine());
                while (bet <= 0 || bet > money)
                {
                    System.Console.WriteLine("Wrong bet, try again:");
                    bet = Convert.ToInt32(Console.ReadLine());
                }
                money -= bet;
                Console.WriteLine($"Your enemy is {warriors[enemy].Name}");


                Console.WriteLine("The fight begins:");


                while (warriors[warriorChoice].Health > 0 && warriors[enemy].Health > 0)
                {
                    warriors[warriorChoice].ShowHealth();
                    warriors[enemy].ShowHealth();
                    warriors[enemy].TakeDMG(warriors[warriorChoice].Damage);
                    warriors[warriorChoice].TakeDMG(warriors[enemy].Damage);

                    Console.ReadKey(true);
                }
                Warrior winner = new Swordsman();
                if (warriors[warriorChoice].Health > 0) 
                { 
                    winner = warriors[warriorChoice];
                    warriors.RemoveAt(enemy);
                    money += bet*2;
                }
                else 
                {
                     winner = warriors[enemy]; 
                     warriors.RemoveAt(warriorChoice);
                     
                }
                Console.WriteLine($"The fight finished and won {winner.Name}");
                winner.Heal();
                if (money == 0)
                {
                    System.Console.WriteLine("You have no money, Raiden, go fuck yourself");
                    arenaWork = false;
                }
                

                int TotalPoints = 1000;
                string RosterName = names[rand.Next(0, names.Length)];
                int RosterHP = rand.Next(100, TotalPoints);
                TotalPoints -= RosterHP;
                int RosterDMG = rand.Next(1, TotalPoints);
                TotalPoints -= RosterDMG;
                int RosterARM = rand.Next(1, TotalPoints);
                TotalPoints -= RosterARM;
                warriors.Add(new Swordsman(RosterName, RosterHP, RosterDMG, RosterARM));


                Console.ReadKey(true);
                Console.Clear();
            }    
        }
    }

    abstract class Warrior
    {
        protected string _name;
        protected int _health;
        protected int _armor;
        protected int _damage;
        protected int _maxHealth;
        protected int _maxArmor;

        public Warrior(string name="", int health=0, int armor=0, int damage=0)
        {
            _name = name;
            _health = health;
            _armor = armor;
            _damage = damage;
            _maxHealth = health;
            _maxArmor = armor;
            
        }

        public String ToString() 
        {
            return $"Warrior {_name} has {_health} health, {_armor} armor and deals {_damage} damage";
        }

        public void TakeDMG(int damage)
        {
            if (_armor - damage > 0)
            { 
             while (_armor - damage > 0)
             {
                _health -= 0; 
                _armor -= 25; 
             
             }
            }
            else { _health -= damage - _armor; }
        }
        public void Heal()
        {
            _health = _maxHealth; 

        }
        public void ShowHealth()
        {
            Console.WriteLine($"Warrior named {_name} has {_health} health");
        }
        public string Name { get { return _name; } }
        public int Health {  get { return _health; } }
        public int Damage {  get { return _damage; } }
    }
    class Swordsman : Warrior 
    {
        public Swordsman(string name="Grisha", int health=0, int armor=0, int damage=0) : base(name, health, armor, damage) 
        {
            Random multiplier = new Random();
            _health = health * multiplier.Next(1, 2);

        }
    }
    class Dima : Warrior
    {
        public Dima(string name, int health, int armor, int damage) : base(name, health, armor, damage) {}
        public void Taunt() {System.Console.WriteLine($"{_name} said: cry baby");}
    }
   
}