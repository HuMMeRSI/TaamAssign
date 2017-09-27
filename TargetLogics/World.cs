﻿using EvolutionaryLogic;
using Library;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetLogics
{
    public class CWorld : DNA<CSimpleArtillary>, IDrawable
    {
        public CSimpleArtillary[] Enemies { get; set; }
        public int DeadCount { get; set; }
        private TargetingStrategy Strategy { get; set; }

        public CWorld(TargetingStrategy Strategy)
            : base(Strategy.GetFriendlyCount())
        {
            this.Strategy = Strategy;
            this.DeadCount = 0;

            for (int i = 0; i < this.Genes.Length; i++)
            {
                this.Genes[i] = Strategy.Friendlies[i].Clone();
            }

            this.Enemies = Strategy.GetEnemyArtillary();
        }

        public override void Execute()
        {
            bool blnEndIndicator = false;
            while (!blnEndIndicator)
            {
                if (this.DeadCount == this.Enemies.Length) break;

                blnEndIndicator = true;
                foreach (CSimpleArtillary Cannon in this.Genes)
                {
                    if (Cannon.ShotsTaken < Cannon.ShotsToFire)
                    {
                        this.DeadCount += Cannon.Shoot(this.Enemies);
                    }

                    blnEndIndicator &= Cannon.ShotsTaken == Cannon.ShotsToFire;
                }
            }
        }

        public override void CalculateFitness()
        {
            int nDeadDiff = this.Enemies.Length - this.DeadCount;

            this.Fitness = this.DeadCount;
        }

        protected override void Mutate()
        {
            foreach (CSimpleArtillary Cannon in this.Genes)
            {
                if (Shared.HitChance(.01))
                {
                    Cannon.Mutate();
                }
            }
        }

        public override IDNA CreateChild()
        {
            CWorld c = new CWorld(this.Strategy);
            c.Genes = this.Genes;
            c.Enemies = this.Enemies;

            return c;
        }

        public override IDNA Crossover(IDNA objPartner)
        {
            CWorld child = new CWorld(this.Strategy);
            CWorld partner = (CWorld)objPartner;

            for (int i = 0; i < partner.Genes.Length; i++)
            {
                child[i] = BaseLogic.Coin() ? this[i].Clone() : partner[i].Clone();
            }

            child.Mutate();
            child.Execute();

            return child;
        }

        public override void ResetForNewGeneration()
        {
            this.DeadCount = 0;
            foreach (CSimpleArtillary Friendly in this.Genes)
            {
                Friendly.ResetGeneome();
            }

            foreach (CSimpleArtillary Enemy in this.Enemies)
            {
                Enemy.ResetGeneome();
            }
        }

        public override string ToString()
        {
            bool s = this.Genes.Select(x => x.Targets.Count).Sum() == this.DeadCount;
            return string.Format("D: {0}, T: {1} - {2}", this.DeadCount, string.Join(", ", this.Genes.Select(x => x.Targets.Count).ToArray()), s);
        }

        #region Render

        public void Draw(Graphics g)
        {
            foreach (CSimpleArtillary MyCannon in this.Enemies)
            {
                MyCannon.Draw(g);
            }

            foreach (CSimpleArtillary MyCannon in this.Genes)
            {
                MyCannon.Draw(g);
            }
        }


        #endregion
    }
}
