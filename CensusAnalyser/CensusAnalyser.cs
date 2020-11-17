using CensusAnalyser.DTO;
using CensusAnalyser.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CensusAnalyser
{
    public class CensusAnalyser
    {
        public enum Country
        {
            INDIA, US, BRAZIL
        }

        Dictionary<string, CensusDTO> dataMap;

        public Dictionary<string, CensusDTO> LoadCensusData(Country country, string csvFilePath, string dataHeaders)
        {
            dataMap = new CSVAdapterFactory().LoadCsvData(country, csvFilePath, dataHeaders);
            return dataMap;
        }

        public object GetSortedStateCodeDataInJsonFormat(Country country, string csvFilePath, string dataheaders, string sortField, SortOrder.SortBy sortBy)
        {
            var censusData = LoadCensusData(country, csvFilePath, dataheaders);
            List<CensusDTO> lines = censusData.Values.ToList();
            List<CensusDTO> lists = GetSortedData(sortField, lines);
            if (sortBy.Equals(SortOrder.SortBy.DESC))
                lists.Reverse();
            return JsonConvert.SerializeObject(lists);
        }
        public List<CensusDTO> GetSortedData(string sortfield, List<CensusDTO> lines)
        {
            switch (sortfield)
            {
                case "stateName": return lines.OrderBy(x => x.stateName).ToList();
                case "stateCode": return lines.OrderBy(x => x.stateCode).ToList();
                case "state": return lines.OrderBy(x => x.state).ToList();
                case "area": return lines.OrderBy(x => x.area).ToList();
                case "usArea": return lines.OrderBy(x => x.totalArea).ToList();
                case "populationDensity": return lines.OrderBy(x => x.populationDensity).ToList();
                case "density": return lines.OrderBy(x => x.density).ToList();
                case "population": return lines.OrderBy(x => x.population).ToList();
                default: return lines.OrderBy(x => x.tin).ToList();
            }
        }

        public string GetMostDenseStateBetweenUSAndIndia(CensusDataDAO censusDataDAO, USCensusDAO uSCensusDAO)
        {
            Console.WriteLine(censusDataDAO.density + " " + uSCensusDAO.populationDensity);
            string state = (censusDataDAO.density > uSCensusDAO.populationDensity)
                ? censusDataDAO.state : uSCensusDAO.stateName;
            return state;
        }
    }
}
