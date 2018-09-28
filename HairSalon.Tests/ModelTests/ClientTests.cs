using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();

      MySqlConnection conn = DB.Connection();
      conn.Open();

      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"ALTER TABLE clients AUTO_INCREMENT = 1;";
      cmd.ExecuteNonQuery();
      cmd.CommandText = @"ALTER TABLE stylists AUTO_INCREMENT = 1;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=stylist_client_test;";
    }
    [TestMethod]
    public void GetAll_DbStartsEmpty_0()
    {
      //Arrange
      //Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Equals_ReturnsTrue_Client()
    {
      //Arrange, Act
      Client firstClient = new Client("Victoria", new DateTime(2018, 1, 1, 9, 0, 0), 1);
      Client secondClient = new Client("Vera", new DateTime(2018, 1, 1, 9, 0, 0), 1);
      Client thirdClient = new Client("Kathy", new DateTime(2018, 1, 1, 9, 0, 0), 2);
      //Assert
      Assert.AreEqual(firstClient, secondClient);
      Assert.AreNotEqual(firstClient, thirdClient);
    }
    [TestMethod]
    public void Save_SaveToDatabase_List()
    {
      //Arrange
      Client firstClient = new Client("Ahmed", new DateTime(2018, 10, 1, 9, 0, 0));
      List<Client> expectedResult = new List<Client> {firstClient};
      //Act
      firstClient.Save();
      List<Client> result = Client.GetAll();
      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void GetAll_ReturnClientsCorrectly_List()
    {
      //Arrange
      Client firstClient = new Client("Ahemd Khokar", new DateTime(2018, 10, 0, 9, 0, 0));
      firstClient.Save();
      Client secondClient = new Client("Derek Smith", new DateTime(2018, 10, 0, 9, 0, 30));
      secondClient.Save();
      List <Client> expectedClients = new List <Client> {firstClient, secondClient};

      //Act
      List <Client> clients = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(expectedClients, clients);
    }
    [TestMethod]
    public void Find_FindClientInDatabase_Client()
    {
      //Arrange
      Client firstClient = new Client("Ahmed Khokar", new DateTime(2018, 10, 0, 9, 0, 0));
      firstClient.Save();
      //Act
      Client foundClient = Client.Find(firstClient.Id);

      //Assert
      Assert.AreEqual(firstClient, foundClient);
    }

    [TestMethod]
    public void Update_UpdatesDatabase_String()
    {
      //Arrange
      Client firstClient = new Client("Ahmed Khokar", new DateTime(2018, 10, 0, 9, 0, 0));
      firstClient.Save();
      Client firstStudentCopy = new Client("Derek Smith", new DateTime(2018, 10, 0, 9, 0, 30), firstClient.Id);
      string newName = "Derek Smith";

      //Act
      firstClient.Update(newName);
      firstClientCopy.Name = newName;

      //Assert
      Assert.AreEqual(firstClientCopy, firstClient);
    }
    [TestMethod]
    public void Delete_DeletesClientFromDatabase_Client()
    {
      //Arrange
      Client firstClient = new Client("Ahmed Khokar", new DateTime(2018, 10, 0, 10, 0, 0));
      firstClient.Save();
      Client secondClient = new Client("Kenneth Du", new DateTime(2018, 10, 0, 10, 0, 30));
      secondClient.Save();
      List<Client> expectedClients = new List<Client>{firstClient};
      //Act
      Client.Delete(secondClient.Id);
      List<Client> clients = Client.GetAll();
      //Assert
      CollectionAssert.AreEqual(expectedClients, clients);
    }
  }
}
