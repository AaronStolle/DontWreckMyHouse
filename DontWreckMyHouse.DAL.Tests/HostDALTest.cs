using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System;
using System.IO;

namespace DontWreckMyHouse.DAL.Tests
{
    public class HostTests
    {
        const string SEED_DIRECTORY = "TestData";
        const string SEED_FILE = "DALHostTest.csv";
        const string TEST_DIRECTORY = "test";
        const string TEST_FILE = "host.dat";

        string SEED_PATH = Path.Combine(SEED_DIRECTORY, SEED_FILE);
        string TEST_PATH = Path.Combine(TEST_DIRECTORY, TEST_FILE);

        HostRepository repo;

        [SetUp]
        public void Setup()
        {
            if (!Directory.Exists(TEST_DIRECTORY))
            {
                Directory.CreateDirectory(TEST_DIRECTORY);
            }

            File.Copy(SEED_PATH, TEST_PATH, true);
            repo = new HostRepository(TEST_PATH, new HostFormatter());
        }

        [Test]
        public void TestFindByEmail()
        {
            //Arrange
            Host expected = new Host()
            {
                Id = Guid.Parse("3edda6bc-ab95-49a8-8962-d50b53f84b15"),
                LastName = "Yearnes",
                Email = "eyearnes0@sfgate.com",
                Phone = "(806) 1783815",
                Address = "3 Nova Trail",
                City = "Amarillo",
                State = "TX",
                PostalCode = 79182,
                StandardRate = 340M,
                WeekendRate = 425M
            };

            //Act
            Result<Host> actual = repo.FindByEmail("eyearnes0@sfgate.com");

            //Assert
            Assert.IsTrue(actual.Data.ToString() == expected.ToString());
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.Email, actual.Data.Email);
        }
    }
}
