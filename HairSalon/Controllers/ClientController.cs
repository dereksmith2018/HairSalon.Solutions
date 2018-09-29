using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
      [HttpGet("/clients/directory")]
    public ActionResult Index()
    {
      List <Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpPost("/Clients/new")]
    public ActionResult Create()
    {
      DateTime newDate = Convert.ToDateTime(Request.Form["newAppointmentDate"]);
      Client newClient = new Client(Request.Form["newClientName"], newDate);
      newClient.Save();
      return RedirectToAction("Index");
    }
    [HttpGet("/clients/{id}")]
    public ActionResult Details(int id)
    {
      Client foundClient = Client.Find(id);
      List<Stylist> enterStylist = foundClient.GetStylist();
      List<Stylist> allCourses = Stylist.GetAll();
      Dictionary<string, object> model = new Dictionary<string, object> {};
      model.Add("client", foundClient);
      model.Add("stylist", allStylist);

      return View(model);
    }

    [HttpPost("/clients/{id}/add")]
    public ActionResult NewClient(int id)
    {
      Client enterClient = Client.Find(id);
      Stylist enterStylist = Stylist.Find(int.Parse(Request.Form["stylistId"]));
      enterStylist.AddClient(enterClient);
      return RedirectToAction("Details", new {id = id});
    }

    [HttpGet("/clients/{id}/delete")]
    public ActionResult DeleteClient(int id)
    {
      Client foundClient = Client.Find(id);
      foundClient.Delete();
      return RedirectToAction("Index");
    }
    }
}
