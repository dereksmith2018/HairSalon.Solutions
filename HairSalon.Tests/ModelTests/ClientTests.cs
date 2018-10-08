using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;


namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
   public ClientTest()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=derek_smith_test;";
    }
    public void Dispose()
    {
         Stylist.DeleteAll();
         Client.DeleteAll();
         
    }
    [TestMethod]
        public void GetAll_DBStartsEmpty_Empty()
        {
            //Arrange
            int count = Client.GetAllClient().Count;

            //Assert
            Assert.AreEqual(0, count);
        }
    public void Save_ClientSaveToDatabase_ClientList()
    {
            //Arrange
            Client testClient = new Client("Derek Smith", 0);
            testClient.Save();

            //Act
            List<Client> result = Client.GetAllClient();
            List<Client> testlist = new List<Client> { testClient };

            //Assert
            CollectionAssert.AreEqual(testlist, result);
        }
  }
}
