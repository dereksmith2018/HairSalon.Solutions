using System;
using HairSalon.Models;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace HairSalon.Tests
{
    [TestClass]
    public class SpecialistTest : IDisposable
    {
        public SpecialistTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=derek_smith_test;";
        }
        public void Dispose()
        {
            Stylist.DeleteAll();
            Client.DeleteAll();
            Specialist.DeleteAll();
        }
    
    

        [TestMethod]
        public void GetAll_DBStartsEmpty_Empty()//passed
        {
            //Arrange
            int count = Specialist.GetAll().Count;

            //Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Equals_TrueForSameName_Specialist()//passed
        {
            //Arrange
            Specialist entryOne = new Specialist("hello", 0);
            Specialist entryTwo = new Specialist("hello", 0);

            //Assert
            Assert.AreEqual(entryOne, entryTwo);
        }

        [TestMethod]
        public void Save_SpecialistToDatabase_List()
        {
            //Arrange
            Specialist testSpecialist = new Specialist("hello", 1);
            testSpecialist.Save();

            //Act
            List<Specialist> result = Specialist.GetAll();
            List<Specialist> testlist = new List<Specialist> { testSpecialist };

            //Assert
            CollectionAssert.AreEqual(testlist, result);
        }

        [TestMethod]
        public void Save_AssignsIdToObject_SpecialistId()
        {
            //Arrange
            Specialist testSpecialist = new Specialist("hello", 0);
            testSpecialist.Save();

            //Act
            Specialist saveSpecialist = Specialist.GetAll()[0];

            int result = saveSpecialist.GetSpecialistId();
            int testId = testSpecialist.GetSpecialistId();

            //Assert 
            Assert.AreEqual(testId, result);
        }
        // [TestMethod]
        // public void Find_FindsSpecialistInDatabase_Specialist()
        // {
        //     //Arrange
        //     Specialist testSpecialist = new Specialist("hello", 0);
        //     testSpecialist.Save();

        //     //Act
        //     Specialist result = Specialist.Find(testSpecialist.GetSpecialistId());

        //     //Assert

        //     Assert.AreEqual(testSpecialist, result);

        // }
        }
        }
                
    
