using System;
using System.Collections.Generic;
using System.Text;

namespace CensusAnalyser.POCO
{
    public class USCensusDAO
    {
        public string stateId;
        public string stateName;
        public long population;
        public long housingUnits;
        public double totalArea;
        public double waterArea;
        public double landArea;
        public double populationDensity;
        public double housingDensity;

        public USCensusDAO(string stateId, string stateName, string population, string housingUnits, string totalArea, string waterArea, string landArea, string populationDensity, string housingDensity)
        {
            this.stateId = stateId;
            this.stateName = stateName;
            this.population = Convert.ToUInt32(population);
            this.housingUnits = Convert.ToUInt32(housingUnits);
            this.totalArea = Convert.ToDouble(totalArea);
            this.waterArea = Convert.ToDouble(waterArea);
            this.landArea = Convert.ToDouble(landArea);
            this.populationDensity = Convert.ToDouble(populationDensity);
            this.housingDensity = Convert.ToDouble(housingDensity);
        }
    }
}
