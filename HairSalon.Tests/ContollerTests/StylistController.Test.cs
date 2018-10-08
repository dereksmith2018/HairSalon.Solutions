using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            //Arrange
            StylistController controller = new StylistController();

            //Act
            ActionResult indexView = controller.Index();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }
        [TestMethod]
        public void CreateForm_ReturnsCorrectView_True()
        {
            //Arrange
            StylistController controller = new StylistController();

            //Act
            ActionResult createView = controller.CreateForm();

            //Assert
            Assert.IsInstanceOfType(createView, typeof(ViewResult));

        }
        // [TestMethod]
        // public void Details_ReturnsCorrectView_True()
        // {
        //     //Arrange
        //     StylistController controller = new StylistController();

        //     //Act
        //     ActionResult detailsView = controller.Details(0);

        //     //Assert
        //     Assert.IsInstanceOfType(detailsView, typeof(ViewResult));

        // }
        [TestMethod]
        public void UpdateForm_ReturnsCorrectView_True()
        {
            //Arrange
            StylistController controller = new StylistController();

            //Act
            ActionResult updateFormView = controller.UpdateForm(0);

            //Assert
            Assert.IsInstanceOfType(updateFormView, typeof(ViewResult));

        }
    }
}
