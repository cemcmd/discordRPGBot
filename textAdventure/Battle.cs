using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textAdventure
{
    class Battle
    {
        public List<Enemy> Opponents { get; set; }
        public List<Player> ParticipatingPlayers { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }

        public void Init() // Its null other wise
        {
            Opponents = new List<Enemy>();
            ParticipatingPlayers = new List<Player>();
        }

        static void ListUsableActions(Player player)
        {
            List<Move> moves = player.Moves;
            List<Item> items = player.Inventory;
            //List<int> inventoryQuantity = player.InventoryQuantity;
            //List<int> movesQuantity = player.MovesQuantity;

            Console.WriteLine("\n== Usable items ==");

            for (int i = 0; i < items.Count; i++) // I cant use foreach :(
            {
                if (items[i].IsWeapon)
                    Console.WriteLine("x{0} {1}", items[i].Ammount, items[i].Name);
            }

            Console.WriteLine("\n== Moves ==");

            for (int i = 0; i < moves.Count; i++)
            {
                Console.WriteLine("{0}", moves[i].Name);
            }

            Console.WriteLine();

        }

        private void Stats()
        {
            //(ParticipatingPlayers[p].Name + " " + ParticipatingPlayers[p].Health + "\\" + ParticipatingPlayers[p].MaxHealth + " HP")
            //Opponents[e].Name + " " + Opponents[e].Health + "\\" + Opponents[e].MaxHealth + " HP"



            Console.WriteLine("\n== Players ==");
            for (int p = 0; p < ParticipatingPlayers.Count; p++)
            {
                if (ParticipatingPlayers[p].IsAlive)
                    Console.WriteLine(ParticipatingPlayers[p].Name + " " + ParticipatingPlayers[p].Health + "\\" + ParticipatingPlayers[p].MaxHealth + " HP");
                else
                    Console.WriteLine("~~{0}~~",ParticipatingPlayers[p].Name);
            }

            Console.WriteLine("\n== Enemies ==");

            for (int e = 0; e < Opponents.Count; e++)
            {
                if (Opponents[e].IsAlive)
                    Console.WriteLine(Opponents[e].Name + " " + Opponents[e].Health + "\\" + Opponents[e].MaxHealth + " HP");
                else
                    Console.WriteLine("~~{0}~~", Opponents[e].Name);
            }

            Console.WriteLine();
        }

        private void ClearVars()
        {
            Opponents = null;
            ParticipatingPlayers = null;
            Title = null;
            Desc = null;
        }

        private bool UpdateDead() // If health is below or is 0, they are dead, so make it so. Returns true if there was an update.
        {
            bool hasUpdated = false;
            for (int p = 0; p < ParticipatingPlayers.Count; p++)
            {
                if (ParticipatingPlayers[p].Health <= 0 && ParticipatingPlayers[p].IsAlive)
                {
                    ParticipatingPlayers[p].Health = 0;
                    ParticipatingPlayers[p].IsAlive = false;
                    hasUpdated = true;
                    Console.WriteLine("{0} has died", ParticipatingPlayers[p].Name);
                }
            }

            for (int e = 0; e < Opponents.Count; e++)
            {
                if (Opponents[e].Health <= 0 && Opponents[e].IsAlive)
                {
                    Opponents[e].Health = 0;
                    Opponents[e].IsAlive = false;
                    hasUpdated = true;
                    Console.WriteLine("{0} has died", Opponents[e].Name);
                }
            }
            return hasUpdated;
        }

        private string WhoWon() // who won? huh? nullstring means no one
        {
            UpdateDead();
            int players = 0, enemies = 0;

            for (int p = 0; p < ParticipatingPlayers.Count; p++)
                if (!ParticipatingPlayers[p].IsAlive)
                    players++;

            for (int e = 0; e < Opponents.Count; e++)
                if (!Opponents[e].IsAlive)
                    enemies++;

            if (players == ParticipatingPlayers.Count)
                return "enemies";
            else if (enemies == Opponents.Count)
                return "players";
            else
                return "";
        }

        private bool PlayerCommand(string command, Player player) // return true if command was sucessfull
        {
            switch (command)
            {
                case "actions":
                    ListUsableActions(player);
                    return true;
                case "stats":
                    Stats();
                    return true;
                case "help":
                    Console.WriteLine("\n== Current Commands ==\nActions\nStats\n");
                    return true;
            }
            return false; //if no command executed, return false.
        }

        private int[] EnemyPickAction(Enemy enemy, string difficulty) // when loadouts is added, add support to this. UPDATE!!! Send nums [ Player#, index#, move or item ] 0 - move, 1 - item
        {
            Random r = new Random(Guid.NewGuid().GetHashCode()); // the value given is random. So that random can accualy be random -_- // thank you stackoverflow

            int moveOrItem = 0;
            int playerIndex = r.Next(0, ParticipatingPlayers.Count);

            while (!ParticipatingPlayers[playerIndex].IsAlive)
                playerIndex = r.Next(0, ParticipatingPlayers.Count);

            switch (difficulty)
            {
                case "Easy":
                default:
                    if (moveOrItem == 0)
                        return new int[] { playerIndex, r.Next(0, enemy.Moves.Count()), moveOrItem };
                    else
                        return new int[] { playerIndex, r.Next(0, enemy.LoadOut.Count()), moveOrItem };
                case "Medium":
                    break;
                case "Hard":
                    break;
                case "Impossible":
                    break;
            }
            return new int[] { 0 };
        }

        public bool Start()
        {

            //Random r = new Random();

            //ushort turnBase = 0; // Turn manager, +1 and its next persons turn. %

            bool multiEnemy = !(Opponents.Count == 1); // If theres more then 1, its false
            bool multiPlayer = !(ParticipatingPlayers.Count == 1);

            List<Dictionary<string, int>> DictItems = new List<Dictionary<string, int>>();
            List<Dictionary<string, int>> DictMoves = new List<Dictionary<string, int>>();

            for (int p = 0; p < ParticipatingPlayers.Count; p++) // Create dictionaries. So when a command is sent, it will know if its an action. And so we dont need to be looking.
            {
                DictMoves.Add(new Dictionary<string, int>()); // the newest one will be 'p'

                for (int m = 0; m < ParticipatingPlayers[p].Moves.Count; m++) // Loop through moves
                {
                    DictMoves[p].Add(ParticipatingPlayers[p].Moves[m].Name.ToLower().Replace(" ", ""), m); // Remove spaces, because input does it
                }

                DictItems.Add(new Dictionary<string, int>());

                for (int i = 0; i < ParticipatingPlayers[p].Inventory.Count; i++) // Loop through items
                {
                    if (ParticipatingPlayers[p].Inventory[i].IsWeapon) // Do if its a weapon
                        DictItems[p].Add(ParticipatingPlayers[p].Inventory[i].Name.ToLower().Replace(" ", ""), i); // Remove spaces, because input does it
                }
            }

            Console.WriteLine("\n*{0}*", Title);
            Console.WriteLine(Desc);

            for (int i = 0; i < Opponents.Count; i++)
            {
                Console.WriteLine("{0} with {1} HP has apeared", Opponents[i].Name, Opponents[i].Health);
            }

            string usrImp = ""; // given value just in case readline() wasnt ran (though it should)
            string action, target;
            int targetIndex;
            string temp = "";
            double atk;
            bool turnPlayed;
            int[] d; // enemy usage

            while (WhoWon() == "")
            {

                for (int p = 0; p < ParticipatingPlayers.Count; p++)
                {

                    turnPlayed = false;

                    while (!turnPlayed && WhoWon() == "" && ParticipatingPlayers[p].IsAlive) // Player first
                    {
                        Console.Write(ParticipatingPlayers[p].Name + " > ");
                        usrImp = Console.ReadLine().ToLower();

                        if (!PlayerCommand(usrImp, ParticipatingPlayers[p])) //if no command was run, do the other crap
                        {

                            if (usrImp == "skip" || usrImp == "k")
                            {   //skip if skip. This cannot be in the 
                                //playercommand function because it needs to set the bool turnPlayed to ture
                                turnPlayed = true;
                                continue;
                            }

                            string[] aryusr = usrImp.Split(' ');

                            List<string> enemyNames = new List<string>();
                            for (int e = 0; e < Opponents.Count; e++)
                                enemyNames.Add(Opponents[e].Name.ToLower().Replace(" ", ""));

                            action = "";
                            target = "";

                            for (int i = 0; i < aryusr.Length; i++) // takes the split input and determans whats what
                            {
                                temp += aryusr[i];

                                if (DictItems[p].ContainsKey(temp) || DictMoves[p].ContainsKey(temp))
                                {
                                    action = temp;
                                    temp = "";
                                }
                                else if (enemyNames.Contains(temp))
                                {
                                    target = temp;
                                    temp = "";
                                }

                            }

                            temp = ""; // Clear temp so we dont have left overs if command is bad

                            if (action == "" || target == "") //if one didnt get set, they both dont deserve anything nice
                            {
                                action = "";
                                target = "";
                            }

                            targetIndex = enemyNames.IndexOf(target);

                            if (DictItems[p].ContainsKey(action))
                            {
                                atk = (ParticipatingPlayers[p].Inventory[DictItems[p][action]].AttackDamage);

                                Opponents[targetIndex].Health -= atk;
                                Console.WriteLine("Dealt {0} damage on {1}", atk, Opponents[targetIndex].Name);

                                if (ParticipatingPlayers[p].Inventory[DictItems[p][action]].Stackable == false)
                                    ParticipatingPlayers[p].Inventory[DictItems[p][action]].Ammount--;

                                turnPlayed = true;
                            }
                            else if (DictMoves[p].ContainsKey(action))
                            {
                                atk = (ParticipatingPlayers[p].Moves[DictMoves[p][action]].AttackDamage);

                                Opponents[targetIndex].Health -= atk;
                                Console.WriteLine("Dealt {0} damage on {1}", atk, Opponents[targetIndex].Name);
                                turnPlayed = true;
                            }
                            else
                            {
                                Console.WriteLine("Bad command"); // after going through all the checks
                            }
                        }
                    }
                }
                // Enemy second

                for (int e = 0; e < Opponents.Count; e++)
                {
                    if (WhoWon() == "" && Opponents[e].IsAlive)
                    {
                        d = EnemyPickAction(Opponents[e], Opponents[e].Difficulty); // haha no var why did I do var

                        if (d[2] == 0)
                            atk = Opponents[e].Moves[d[1]].AttackDamage;
                        else
                            atk = Opponents[e].LoadOut[d[1]].AttackDamage;

                        ParticipatingPlayers[d[0]].Health -= atk;

                        Console.WriteLine("{0} used {1} and delt {2} damage on {3}", Opponents[e].Name, Opponents[e].Moves[d[1]].Name, atk, ParticipatingPlayers[d[0]].Name);
                    }
                }
            }

            if (WhoWon() == "players")
                return true;
            else
                return false;

        }

    }
}
