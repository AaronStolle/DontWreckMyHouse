using DontWreckMyHouse.Core.Models;
using NUnit.Framework;
using System.IO;

namespace DontWreckMyHouse.DAL.Tests
{
    class GuestTests
    {
        const string SEED_DIRECTORY = "TestData";
        const string SEED_FILE = "DALGuestTest.csv";
        const string TEST_DIRECTORY = "test";
        const string TEST_FILE = "guest.dat";

        string SEED_PATH = Path.Combine(SEED_DIRECTORY, SEED_FILE);
        string TEST_PATH = Path.Combine(TEST_DIRECTORY, TEST_FILE);

        GuestRepository repo;

        [SetUp]
        public void Setup()
        {
            if (!Directory.Exists(TEST_DIRECTORY))
            {
                Directory.CreateDirectory(TEST_DIRECTORY);
            }

            File.Copy(SEED_PATH, TEST_PATH, true);
            repo = new GuestRepository(TEST_PATH, new GuestFormatter());
        }

        [Test]
        public void TestFindByEmail()
        {
            //Arrange
            Guest expected = new Guest()
            {
                Id = 3,
                FirstName = "Tremain",
                LastName = "Carncross",
                Email = "tcarncross2@japanpost.jp",
                Phone = "(313) 2245034",
                State = "MI"
            };

            //Act
            Result<Guest> actual = repo.FindByEmail("tcarncross2@japanpost.jp");

            //Assert
            Assert.IsTrue(actual.Success);
            Assert.AreEqual(expected.ToString(), actual.Data.ToString());
        }
    }
}
