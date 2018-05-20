using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textAdventure
{
    class TADebug
    {
        public List<Move> Moves { get; set; }
        public List<Item> Items { get; set; }

        public List<Enemy> CreateDebugEnemyList(int amount) //Debug function
        {
            List<Enemy> list = new List<Enemy>();
            Random r = new Random();

            for (int i = 0; i < amount; i++)
            {
                list.Add(new Enemy());
                list[i].Name = "Slime " + i;
                list[i].Health = r.Next(50,150);
                list[i].MaxHealth = list[i].Health;
                list[i].IsAlive = true;
                list[i].ItemUsageChance = 2;
                list[i].Moves = Moves;
                list[i].Difficulty = "Easy";
            }

            return list;
        }

        public void CreateDebugBattle(List<Player> players)
        {
            Battle battle = new Battle();

            battle.Title = "Battle Title";
            battle.Desc = "Debug battle created";

            battle.Opponents = CreateDebugEnemyList(1); // Create the list from function

            //List<Player> lstPlayer = new List<Player>(); // Put player into a list in order to set it
            //lstPlayer.Add(player);
            battle.ParticipatingPlayers = players;
            //battle.ParticipatingPlayers = lstPlayer;    // there we go

            if (battle.Start())
                Console.WriteLine("You win! :("); // aw darn!
            else
                Console.WriteLine("You lose! :)"); // :)
        }
    }
}
