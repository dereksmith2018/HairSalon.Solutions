using HairSalon.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
  public class HomeController: Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }
  }

}
