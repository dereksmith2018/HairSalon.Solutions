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
    [HttpGet("/employee/new")]
    public ActionResult CreateFormEmployee()
    {
      return View();
    }
    [HttpPost("/employees")]
    public ActionResult Create()
    {
      Employee newEmployee = new Employee(Request.Form["newEmployees"]);
      newEmployee.Save();
      List<Employee> allEmployees = Employee.GetAll();
      return RedirectToAction("Index");
    }
  }

}
