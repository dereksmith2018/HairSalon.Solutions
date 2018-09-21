using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class EmployeeController : Controller
  {
    [HttpGet("/employee")]
    public ActionResult Index()
    {
      List<Employee> allEmployee = Employee.GetAll();
      return View(allEmployee);
    }
  }

}
