using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {
    [HttpGet("/stylist")]
    public ActionResult Index()
    {
      List<Stylist> allStylist = Stylist.GetAll();
      return View(allStylist);
    }
    [HttpGet("/stylist/new")]
    public ActionResult CreateFormStylist()
    {
      return View();
    }
    [HttpPost("/stylist")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist(Request.Form["newStylists"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return RedirectToAction("Index");
    }
  }

}
