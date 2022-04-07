using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace DontWreckMyHouse.DAL.Tests
{
    public class ReservationTests
    {
        const string SEED_DIRECTORY = "TestData";
        const string SEED_FILE = "293508da-e367-437a-9178-1ebaa7d83015.csv";
        const string TEST_DIRECTORY = "test";
        const string TEST_FILE = "293508da-e367-437a-9178-1ebaa7d83015.csv";

        string SEED_PATH = Path.Combine(SEED_DIRECTORY, "ReservationTest", SEED_FILE);
        string TEST_PATH = Path.Combine(TEST_DIRECTORY,TEST_FILE);

        ReservationRepository repo;

        [SetUp]
        public void Setup()
        {
            if (!Directory.Exists(TEST_DIRECTORY))
            {
                Directory.CreateDirectory(TEST_DIRECTORY);
            }

            File.Copy(SEED_PATH, TEST_PATH, true);
            repo = new ReservationRepository(TEST_DIRECTORY, new ReservationFormatter());
        }

        [Test]
        public void FindAllReservations_WithHostId_ReturnsAllHostReservations()
        {
            //Arrange
            int expected = 3;

            //Act
            // Use the email to get the host (tested in HostDALTest).
            // Use this returned host's GUID to get the CSV.
            // Test data for this CSV has 3 records.
            Result<List<Reservation>> actual = repo.FindAllByHostId(new System.Guid("293508da-e367-437a-9178-1ebaa7d83015"));

            //Assert
            Assert.AreEqual(expected, actual.Data.Count);
        }
    }
}