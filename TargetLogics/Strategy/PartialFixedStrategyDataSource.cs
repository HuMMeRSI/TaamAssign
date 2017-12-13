﻿using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaamLogics
{
    public class PartialFixedStrategyDataSource : IStrategyDataSource
    {
        public CMap Terrain { get; set; }
        public PartialFixedStrategyDataSource(CMap Terrain)
        {
            this.Terrain = Terrain;
        }

        //public CSimpleBattalion[] GetBatalionData()
        //{
        //    int BC = 60;
        //    CSimpleBattalion[] Battalions = new CSimpleBattalion[BC];
        //    for (int i = 0; i < 14; i++)
        //    {
        //        Battalions[i] = new CSimpleBattalion(EConstraints.Land, 0);
        //        Battalions[i].SetUID(i);

        //        int Reseversions = Shared.Next(3);
        //        for (int j = 0; j < Reseversions; j++)
        //        {
        //            Reservation r = new Reservation();
        //            r.Date = (new DateTime(2018, 1, 1)).AddDays(Shared.Next(365));
        //            r.Importance = (EImportance)(Shared.Next(5) +1);
        //            Battalions[i].Reservations.Add(r);
        //        }
        //    }

        //    for (int i = 14; i < 24; i++)
        //    {
        //        Battalions[i] = new CSimpleBattalion(EConstraints.ArmoredBattalion, 0);
        //        Battalions[i].SetUID(i);

        //        int Reseversions = Shared.Next(3);
        //        for (int j = 0; j < Reseversions; j++)
        //        {
        //            Reservation r = new Reservation();
        //            r.Date = (new DateTime(2018, 1, 1)).AddDays(Shared.Next(365));
        //            r.Importance = (EImportance)(Shared.Next(5) + 1);
        //            Battalions[i].Reservations.Add(r);
        //        }
        //    }

        //    for (int i = 24; i < BC; i++)
        //    {
        //        Battalions[i] = new CSimpleBattalion(EConstraints.All, Shared.Next(80) + 20);
        //        Battalions[i].SetUID(i);

        //        int Reseversions = Shared.Next(3);
        //        for (int j = 0; j < Reseversions; j++)
        //        {
        //            Reservation r = new Reservation();
        //            r.Date = (new DateTime(2018, 1, 1)).AddDays(Shared.Next(365));
        //            r.Importance = (EImportance)(Shared.Next(5) + 1);
        //            Battalions[i].Reservations.Add(r);
        //        }
        //    }

        //    return Battalions;
        //}

        //public CSimpleSector[] GetSectorData()
        //{
        //    CSimpleSector[] Secotrs = new CSimpleSector[30];
        //    var objSectorialBrigade = Enum.GetValues(typeof(ESectorialBrigade));

        //    DateTime FirstWednesday = new DateTime(2018,1,1);
        //    while (FirstWednesday.Date.DayOfWeek != DayOfWeek.Wednesday)
        //    {
        //        FirstWednesday = FirstWednesday.AddDays(1);
        //    }

        //    for (int i = 0; i < 3; i++)
        //    {
        //        Secotrs[i] = new CSimpleSector(EConstraints.ArmoredBattalion, ESectorialBrigade.Brigade1, FirstWednesday);
        //        Secotrs[i].SetUID(i);
        //    }

        //    for (int i = 3; i < 6; i++)
        //    {
        //        Secotrs[i] = new CSimpleSector(EConstraints.Land, ESectorialBrigade.Brigade2, FirstWednesday.AddMonths(1));
        //        Secotrs[i].SetUID(i);
        //    }

        //    for (int i = 6; i < 12; i++)
        //    {
        //        Secotrs[i] = new CSimpleSector(EConstraints.Land, ESectorialBrigade.Brigade3, FirstWednesday);
        //        Secotrs[i].SetUID(i);
        //    }

        //    for (int i = 0; i < 6; i++)
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            int id = 12 + i * 3 + j;
        //            Secotrs[id] = new CSimpleSector(
        //                EConstraints.All,
        //                (ESectorialBrigade)objSectorialBrigade.GetValue(Shared.Next(objSectorialBrigade.Length - 3) + 3),
        //                FirstWednesday);
        //            Secotrs[id].SetUID(id);
        //        }
        //    }

        //    return Secotrs;
        //}

        public CSimpleBattalion[] GetBatalionData()
        {
            int BC = 60;
            CSimpleBattalion[] Battalions = new CSimpleBattalion[BC];

            for (int i = 0; i < BC; i++)
            {
                Battalions[i] = new CSimpleBattalion(EConstraints.All, Shared.Next(80) + 20);
                Battalions[i].SetUID(i);

                int Reseversions = Shared.Next(3);
                for (int j = 0; j < Reseversions; j++)
                {
                    Reservation r = new Reservation();
                    r.Date = (new DateTime(2018, 1, 1)).AddDays(Shared.Next(365));
                    r.Importance = (EImportance)(Shared.Next(5) + 1);
                    Battalions[i].Reservations.Add(r);
                }
            }

            return Battalions;
        }

        public CSimpleSector[] GetSectorData()
        {
            CSimpleSector[] Secotrs = new CSimpleSector[30];
            var objSectorialBrigade = Enum.GetValues(typeof(ESectorialBrigade));

            DateTime FirstWednesday = new DateTime(2018, 1, 1);
            while (FirstWednesday.Date.DayOfWeek != DayOfWeek.Wednesday)
            {
                FirstWednesday = FirstWednesday.AddDays(1);
            }

            for (int i = 0; i < Secotrs.Length; i++)
            {
                    Secotrs[i] = new CSimpleSector(
                        EConstraints.All,
                        (ESectorialBrigade)objSectorialBrigade.GetValue(Shared.Next(objSectorialBrigade.Length - 3) + 3),
                        FirstWednesday);
                    Secotrs[i].SetUID(i);
            }

            return Secotrs;
        }
    }
}
