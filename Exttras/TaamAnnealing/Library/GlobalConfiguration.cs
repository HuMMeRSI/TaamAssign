﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class GlobalConfiguration
    {
        #region Genetic configuration

        public static int PopulationCount = 500;
        public static float MutationChance = .7f;
        public static float ParentChance = 0;
        public static bool ApplyElitist = true;
        public static bool ApplyNaturalSelection = true;
        public static bool PartialGenomCrossover = false;
        public static bool SwitchMutation = false;
        
        private static Dictionary<string, Action<object>> Delegates = new Dictionary<string, Action<object>>();

        static GlobalConfiguration()
        {
            Delegates["PopulationCount"] = (value) => PopulationCount = Convert.ToInt32(value);
            Delegates["MutationChance"] = (value) => MutationChance = (float)Convert.ToDouble(value);
            Delegates["ParentChance"] = (value) => ParentChance = (float)Convert.ToDouble(value);
            Delegates["ApplyElitist"] = (value) => ApplyElitist = Convert.ToBoolean(value);
            Delegates["ApplyNaturalSelection"] = (value) => ApplyNaturalSelection = Convert.ToBoolean(value);
            Delegates["PartialGenomCrossover"] = (value) => PartialGenomCrossover = Convert.ToBoolean(value);
            Delegates["SwitchMutation"] = (value) => SwitchMutation = Convert.ToBoolean(value);
        }

        public static Action<object> GetDelegate(string Name)
        {
            return Delegates[Name];
        }

        #endregion

        public class Assignemnt
        {
            public static int BattalionCount = 60;
            public static int SectorCount = 30;
            static Assignemnt()
            {

                Delegates["BattalionCount"] = (value) => BattalionCount = Convert.ToInt32(value);
                Delegates["SectorCount"] = (value) => SectorCount = Convert.ToInt32(value);
            }
        }

        public class Performances
        {
            public static int ThreadBulkSize = 50;
            public static bool FixedStrategy = false;
            
            static Performances()
            {
                Delegates["ThreadBulkSize"] = (value) => ThreadBulkSize = Convert.ToInt32(value);
                Delegates["FixedStrategy"] = (value) => FixedStrategy = Convert.ToBoolean(value);
            }
        }
    }
}
