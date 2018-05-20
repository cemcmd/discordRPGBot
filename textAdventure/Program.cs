// Writen by:
// Kemicl#4392
// Kemicl
// Cemcmd
// Ryan
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace textAdventure
{
    class Program
    {

        static void Main(string[] args) // Make it so that the json data is loaded into player 1 and player 2's inventory for start
        {
            List<Item> RefItems = JsonConvert.DeserializeObject<List<Item>>(
                System.IO.File.ReadAllText(@"..\..\items.json"));

            List<Move> RefMoves = JsonConvert.DeserializeObject<List<Move>>(
                System.IO.File.ReadAllText(@"..\..\moves.json"));

            List<Player> players = new List<Player>();
            List<Enemy> enemies = new List<Enemy>();

            TADebug debug = new TADebug();
            debug.Moves = RefMoves;

            CreatePlayer(ref players, "Trump");
            CreatePlayer(ref players, "Putin");

            for (int p = 0; p < players.Count; p++)
            {
                players[p].Inventory = RefItems;

                for (int i = 0; i < RefItems.Count; i++)
                {
                    players[p].Inventory[i].Ammount = 99;
                }

                players[p].Moves = RefMoves;

                //for (int i = 0; i < RefMoves.Count; i++)
                //{
                //    players[p].MovesQuantity.Add(99);
                //}
            }

            //Console.WriteLine(String.Join("\n", players[0].Moves));

            //ListUsableActions(players[0].Inventory, players[0].Moves, players[0].InventoryQuantity, players[0].MovesQuantity);

            //var f = JsonConvert.SerializeObject(players);

            debug.CreateDebugBattle(players);

        }

        static void CreatePlayer(ref List<Player> players, string name) // Adds a player into the list players. 
        {
            players.Add(new Player());
            int index = players.Count - 1;
            players[index].Name = name;
            players[index].Health = 200;
            players[index].MaxHealth = 1000;
            players[index].IsAlive = true;

            players[index].Init(); // Init process

            Console.WriteLine("Player {0} has been regestered!", name);
        }
    }
}
