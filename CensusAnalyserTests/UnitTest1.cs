using CensusAnalyser;
using CensusAnalyser.DTO;
using CensusAnalyser.POCO;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using static CensusAnalyser.CensusAnalyser;

namespace CensusAnalyserTests
{
    public class Tests
    {

        static string indianStateCensusHeaders = "State,Population,AreaInSqKm,DensityPerSqKm";
        static string indianStateCodeHeaders = "SrNo,State Name,TIN,StateCode";
        static string indianStateCensusFilePath = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\IndiaStateCensusData.csv";
        static string wrongHeaderIndianCensusFilePath = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\WrongIndiaStateCensusData.csv";
        static string delimiterIndianCensusFilePath = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\DelimiterIndiaStateCensusData.csv";
        static string wrongIndianStateCensusFilePath = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\IndiaData.csv";
        static string wrongIndianStateCensusFileType = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\IndiaStateCensusData.txt";
        static string indianStateCodeFilePath = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\IndiaStateCode.csv";
        static string wrongIndianStateCodeFileType = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\IndiaStateCode.txt";
        static string delimiterIndianStateCodeFilePath = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\DelimiterIndiaStateCode.csv";
        static string wrongHeaderStateCodeFilePath = @"C:\Users\HP\source\repos\IndianStateCensusAnalyserDemo\CensusAnalyserTests\CSVFiles\WrongIndiaStateCode.csv";

        CensusAnalyser.CensusAnalyser censusAnalyser;
        Dictionary<string, CensusDTO> totalRecord;
        Dictionary<string, CensusDTO> stateRecord;

        [SetUp]
        public void Setup()
        {
            censusAnalyser = new CensusAnalyser.CensusAnalyser();
            totalRecord = new Dictionary<string, CensusDTO>();
            stateRecord = new Dictionary<string, CensusDTO>();
        }

        /// <summary>
        /// TC 1.1 Given the States Census CSV file, Check to ensure the Number of Record matches
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFile_WhenReaded_ShouldReturnCensusDataCount()
        {
            totalRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCensusFilePath, indianStateCensusHeaders);
            stateRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCodeFilePath, indianStateCodeHeaders);
            Assert.AreEqual(29, totalRecord.Count);
            Assert.AreEqual(37, stateRecord.Count);
        }

        /// <summary>
        /// TC 1.2 Given the State Census CSV File if incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenWrongIndianCensusDataFile_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFilePath, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, stateException.eType);
        }

        /// <summary>
        /// TC 1.3 Given the State Census CSV File when correct but type incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenWrongIndianCensusDataFileType_WhenReaded_ShouldReturnCustomException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFileType, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCodeFileType, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, stateException.eType);
        }

        /// <summary>
        /// TC 1.4 Given the State Census CSV File when correct but delimiter incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFile_WhenNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianCensusFilePath, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianStateCodeFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, stateException.eType);
        }

        /// <summary>
        /// TC 1.5 Given the State Census CSV File when correct but csv header incorrect Returns custom Exception
        /// </summary>
        [Test]
        public void GivenIndianCensusDataFile_WhenHeaderNotProper_ShouldReturnException()
        {
            var censusException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderIndianCensusFilePath, indianStateCensusHeaders));
            var stateException = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderStateCodeFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, censusException.eType);
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, stateException.eType);
        }

        /// <summary>
        /// TC 2.1 Given Indian State Code Data File, Check to ensure the Number of Record matches
        /// </summary>
        [Test]
        public void GivenIndianStateCodeDataFile_WhenReaded_ShouldReturnCensusDataCount()
        {
            stateRecord = censusAnalyser.LoadCensusData(Country.INDIA, indianStateCodeFilePath, indianStateCodeHeaders);
            Assert.AreEqual(37, stateRecord.Count);
        }

        /// <summary>
        /// TC 2.2 Given the Indian State Code Data CSV File if incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenWrongIndianStateCodeDataFile_WhenReaded_ShouldReturnCustomException()
        {
            var expected = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCensusFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.FILE_NOT_FOUND, expected.eType);
        }

        /// <summary>
        /// TC 2.3 Given the Indian State Code CSV File when correct but type incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenWrongIndianStateCodeFileType_WhenReaded_ShouldReturnCustomException()
        {
            var expected = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongIndianStateCodeFileType, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INVALID_FILE_TYPE, expected.eType);
        }

        /// <summary>
        /// TC 2.4 Given the Indian State Code CSV File when correct but delimiter incorrect Returns a custom Exception
        /// </summary>
        [Test]
        public void GivenIndianStateCodeDataFile_WhenNotProper_ShouldReturnException()
        {
            var expected = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, delimiterIndianStateCodeFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_DELIMITER, expected.eType);
        }

        /// <summary>
        /// TC 2.5 Given the Indian State Code CSV File when correct but csv header incorrect Returns custom Exception
        /// </summary>
        [Test]
        public void givenIndianStateCodeDataFile_WhenHeaderNotProper_ShouldReturnException()
        {
            var expected = Assert.Throws<CensusAnalyserException>(() => censusAnalyser.LoadCensusData(Country.INDIA, wrongHeaderStateCodeFilePath, indianStateCodeHeaders));
            Assert.AreEqual(CensusAnalyserException.ExceptionType.INCORRECT_HEADER, expected.eType);
        }
    }
}