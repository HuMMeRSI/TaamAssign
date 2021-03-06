﻿using Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationLogics
{
    public class God<T>
    {
        #region Properties

        private List<IDNA<T>> Population { get; set; }

        public IDNA<T> BestFitness { get; set; }
        public int GenerationCount { get; set; }
        public float AvreageFitness { get; set; }
        public CStatutsGraph StatusGraph { get; set; }
        public PopulationSelector<T> MySelector { get; set; }

        #endregion

        public God(Func<IDNA<T>> PopulationGenerator)
        {
            this.Population = new List<IDNA<T>>();
            this.GenerationCount = 0;
            this.StatusGraph = new CStatutsGraph();
            this.MySelector = new PopulationSelector<T>();

            for (int i = 0; i < GlobalConfiguration.PopulationCount; i++)
            {
                IDNA<T> dna = PopulationGenerator();
                dna.Execute();
                this.Population.Add(dna);
            }

            this.AssessPopulation();
        }

        public void GeneratePopulation(int Generations, IProgress<IDNA<T>> progress)
        {
            MatingPool<T> pool = new MatingPool<T>();
            for (int i = 0; i < Generations; i++)
            {
                if (GlobalConfiguration.ApplyNaturalSelection)
                {
                    this.MySelector.NaturalSelection(Population);
                }

                int nElitilstCount = (int)(GlobalConfiguration.ApplyElitist ? Math.Max(this.Population.Count * .001f, 1) : 0);
                this.Population = pool.GetEvolvedPopulation(this.Population, nElitilstCount);
                this.GenerationCount++;
                this.AssessPopulation();

                progress.Report(this.BestFitness);
            }
        }


        private void AssessPopulation()
        {
            int nChunksCount = this.Population.Count / GlobalConfiguration.Performances.ThreadBulkSize;
            int nChunksRemainder = this.Population.Count % GlobalConfiguration.Performances.ThreadBulkSize;
            int nRemainder = nChunksRemainder > 0 ? 1 : 0;
            Task[] Assesments = new Task[nChunksCount + nRemainder];

            for (int i = 0; i < Assesments.Length - nRemainder; i++)
            {
                int q = i;
                Assesments[i] = Task.Factory.StartNew(() => this.PartialAssesment(q * GlobalConfiguration.Performances.ThreadBulkSize, GlobalConfiguration.Performances.ThreadBulkSize), TaskCreationOptions.None);
            }
            if(nRemainder > 0)
            {
                Assesments[Assesments.Length - 1] = Task.Factory.StartNew(() => this.PartialAssesment((Assesments.Length - 1) * GlobalConfiguration.Performances.ThreadBulkSize, nChunksRemainder), TaskCreationOptions.None);
            }

            Task.WaitAll(Assesments);
            int bestIdx = 0;
            float BestDNAFitness = -2;
            float TotalFintess = 0;

            for (int i = 0; i < this.Population.Count; i++)
            {
                IDNA<T> objDNA = this.Population[i];
                TotalFintess += objDNA.GetFitnesss();

                if (objDNA.GetFitnesss() > BestDNAFitness)
                {
                    BestDNAFitness = objDNA.GetFitnesss();
                    bestIdx = i;
                }
            }

            this.BestFitness = this.Population[bestIdx].Clone();
            this.AvreageFitness = TotalFintess / GlobalConfiguration.PopulationCount;

            this.StatusGraph.AddToHistory(BestDNAFitness);
            this.StatusGraph.Average = this.AvreageFitness;
        }

        private void PartialAssesment(int nStart, int Offset)
        {
            for (int i = nStart; i < nStart + Offset; i++)
            {
                this.Population[i].CalculateFitness();
            }
        }

        public string PrintAll()
        {
            string strAll = "";
            foreach (IDNA<T> obj in this.Population)
            {
                strAll += obj.ToString();
                strAll += Environment.NewLine;
            }

            return strAll;
        }
    }
}
