using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textAdventure
{
    class Area
    {
        //Parts of a world
        public int StepsOut { get; set; } //how many to get out of the area
        public int StepsTaken { get; set; }

        public List<Item> RandomItems { get; set; }
        public List<Item> HiddenItems { get; set; }
        public List<Item> ForcedItems { get; set; }

        public List<int> RandomBattles { get; set; }
        public List<int> BossBattle { get; set; }

        public List<Effect> AreaEffects { get; set; }
        public bool EffectOnAll { get; set; }

        public string LeavingMessage { get; set; }
        public string EnteringMessage { get; set; }

        public string LeftMessage { get; set; }
        public string RightMessage { get; set; }
    }

    class World
    {
        // Level
        public string Name { get; set; }
        public string Desc { get; set; }

        public List<Effect> WorldEffects { get; set; }
        public bool EffectOnAll { get; set; }

        public Enemy Boss { get; set; }

        public Area[] Areas { get; set; }

        //public bool EnterWorld()
        //{
        //    bool WorldDone = false;

        //    while (!WorldDone)
        //    {
        //        for (int i = 0; i < Areas.Length; i++)
        //            if (Areas[i].StepsTaken != Areas[i].StepsOut)

        //    }
        //}

    }
}
