using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textAdventure
{
    class Player
    {
        public string Name { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; set; } // For modding tool, just set this to the set health

        public List<Item> Inventory { get; set; }
        //public List<int> InventoryQuantity { get; set; }

        public List<Item> LoadOut { get; set; }
        //public List<Item> AppliedItems { get; set; }

        public List<Effect> Effects { get; set; }

        public Item Backpack { get; set; }

        public List<Move> Moves { get; set; }
        //public List<int> MovesQuantity { get; set; } // replace with points, moves use up points. Just to simplify (for me)
        public int Energy { get; set; } // used up by moves. Can recharge over time

        public int SpecialPoints { get; set; }

        public bool IsAlive { get; set; }

        public void Init() // Make variables usable
        {
            Inventory = new List<Item>();
            Moves = new List<Move>();
            //InventoryQuantity = new List<int>();
            //MovesQuantity = new List<int>();
        }
    }

    class Enemy
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public double Health { get; set; }
        public double MaxHealth { get; set; } // For modding tool, just set this to the set health
        public bool IsAlive { get; set; }
        public string Difficulty { get; set; }
        public int ItemUsageChance { get; set; }
        public List<Move> Moves { get; set; }
        public List<Item> LoadOut { get; set; }
        public int Energy { get; set; }
    }

    class Item
    {
        public string Name { get; set; }
        public string Desc { get; set; }

        public bool IsWeapon { get; set; }

        public bool Stackable { get; set; }

        public bool BeingUsed { get; set; } // not for mod to access

        public int Size { get; set; }

        public int Ammount { get; set; }

        public double AttackDamage { get; set; }
        //public int Consumption { get; set; } // Depricated
        public Effect ItemEffect { get; set; }
    }

    class Move
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public double AttackDamage { get; set; }
        // public int Consumption { get; set; } Depricated
        
    }

    class Effect // LUA API!
    {
        // effect. What happens to the player during serten points
        // probly make it so it runs after round for everyone is done, so right before players turn
    }
}
