using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult indexView = controller.Index();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void CreateForm_ReturnsCorrectView_True()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult createView = controller.CreateForm();

            //Assert
            Assert.IsInstanceOfType(createView, typeof(ViewResult));

        }
        [TestMethod]
        public void Details_ReturnsCorrectView_True()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult detailsView = controller.Details(0);

            //Assert
            Assert.IsInstanceOfType(detailsView, typeof(ViewResult));

        }
    }
        
    
}
