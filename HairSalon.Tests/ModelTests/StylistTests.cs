using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;


namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
      Specialist.DeleteAll();
    }
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=derek_smith_test;";
    }
    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Stylist.GetAllStylist().Count;
      //Assert
      Assert.AreEqual(0, result);
    }

    // [TestMethod]
    // public void Equals_returnsTrue_Stylist()
    // {
    //   //Arrange, Act
    //   Stylist firstStylist = new Stylist("Ahmed", 1);
    //   Stylist secondStylist = new Stylist("Panata", 1);

    //   //Assert
    //   Assert.AreEqual(firstStylist, secondStylist);
    // }

    [TestMethod]
    public void Save_SaveToDatabase_List()
    {
      //Arrange
      Stylist firstStylist = new Stylist("Ahmed", 1);
      List<Stylist> expectedResult = new List<Stylist> {firstStylist};
      //Act
      firstStylist.Save();
      List<Stylist> result = Stylist.GetAllStylist();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }

  //   [TestMethod]
  //   public void AddClient_SaveToDatabase_List()
  //   {
  //     //Arrange
  //     Stylist firstStylist = new Stylist("Ahmed", "barber");
  //     firstStylist.Save();
  //     Client firstClient = new Client("Derek Smith", new DateTime(2018, 10, 30, 9, 0, 0));
  //     firstClient.Save();
  //     List <Client> expectedClient = new List<Client> {firstClient};
  //     //Act
  //     firstStylist.AddClient(firstClient.Id);
  //     List <Client> clients = firstStylist.GetAllClients();
  //     //Assert
  //     CollectionAssert.AreEqual(expectedClient, clients);
  //   }
  //   [TestMethod]
  //   public void GetAll_ReturnStylistCorrectly_List()
  //   {
  //     //Arrange
  //     Stylist firstStylist = new Stylist("Ahmed", "barber");
  //     firstStylist.Save();
  //     Stylist secondStylist = new Stylist("Derek Smith", "owner");
  //     secondStylist.Save();
  //     List<Stylist> expectedStylists = new List<Stylist> {firstStylist, secondStylist};
  //     //Act
  //     List<Stylist> stylists = Stylist.GetAll();
  //     //Assert
  //     CollectionAssert.AreEqual(expectedStylists, stylists);
  //   }
  //   [TestMethod]
  //   public void GetAllStylists_ReturnClientsCorrectly_List()
  //   {
  //     //Arrange
  //     Stylist firstStylist = new Stylist("Ahemd Khokar", "barber");
  //     firstStylist.Save();
  //     Client firstClient = new Client("Panada Teacher", new DateTime(2020, 1, 1, 9, 0, 0));
  //     firstClient.Save();
  //     Client secondClient = new Client("Kenneth Du, ", new DateTime (2018, 10, 2, 9, 0, 0));
  //     secondClient.Save();
  //     List<Client> expectedClients = new List<Client> {firstClient, secondClient};
  //     firstStylist.AddClient(firstClient.Id);
  //     firstStylist.AddClient(secondClient.Id);
  //     //Act
  //     List<Client> clients = firstStylist.GetAllClients();
  //     //Assert
  //     CollectionAssert.AreEqual(expectedClients, clients);
  //   }

  //   [TestMethod]
  //   public void Find_FindStylistInDatabase_Course()
  //   {
  //     //Arrange
  //     Stylist firstStylist = new Stylist ("Ahmed", "barber");
  //     firstStylist.Save();
  //     //Act
  //     Stylist foundStylist = Stylist.Find(firstStylist.Id);
  //     //Assert
  //     Assert.AreEqual(firstStylist, foundStylist);
  //   }
  //   [TestMethod]
  //   public void Update_UpdatesDatabase_String()
  //   {
  //     //Arrange
  //     Stylist firstStylist = new Stylist("Ahmed", "barber");
  //     firstStylist.Save();
  //     Stylist firstStylistCopy = new Stylist("Ahmed", "barber", firstStylist.Id);
  //     string newName = "New Barber";
  //     //Act
  //     firstStylist.Update(newName);
  //     firstStylistCopy.Name = newName;
  //     //Assert
  //     Assert.AreEqual(firstStylistCopy, firstStylist);
  //   }
  //   [TestMethod]
  //   public void Delete_DeleteStylistFromDatabase_Stylist()
  //   {
  //     //Arrange
  //     Stylist firstStylist = new Stylist("Ahmed", "barber");
  //     firstStylist.Save();
  //     Stylist secondStylist = new Stylist("Derek Smith", "Owner");
  //     secondStylist.Save();
  //     List<Stylist> expectedStylists = new List<Stylist>{firstStylist};
  //     //Act
  //     Stylist.Delete(secondStylist.Id);
  //     List<Stylist> stylists = Stylist.GetAll();
  //     //Assert
  //     CollectionAssert.AreEqual(expectedStylists, stylists);
  //   }
   }
}
