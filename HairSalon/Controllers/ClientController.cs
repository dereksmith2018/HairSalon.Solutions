using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
      [HttpGet("/client")]
    public ActionResult Index()
    {
      List <Client> allClients = Client.GetAll();
      return View(allClients);
    }

    [HttpPost("/client/new")]
    public ActionResult Create()
    {
      return View();
    }
    [HttpGet("/client/{clientId}/update")]
      public ActionResult UpdateForm(int clientId)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client newClient = Client.Find(clientId);
        model.Add("client", newClient);
        return View(model);
      }
      [HttpGet("/clients/{clientId}")]
      public ActionResult Details(int clientId)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Client client = Client.Find(clientId);
        List<Stylist> allStylists = Stylist.GetAllStylist();
        List<Stylist> stylist = client.GetStylist();
        model.Add("client", client);
        model.Add("clientStylist", stylist);
        model.Add("allstylists", allStylists);
        return View(model);
      }
      [HttpGet("client/{clientId}/update")]
        public ActionResult UpdateForm (int clientId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Client thisClient = Client.Find(clientId);
            model.Add("client", thisClient);
            return View(model);
        }
        [HttpGet("client/delete")]
        public ActionResult DeleteAll()
        {
            Client.DeleteAll();
            return RedirectToAction("Index");
        }
        [HttpGet("client/{clientId}/delete")]
        public ActionResult DeleteOne(int stylistId, int clientId)
        {
            Client newClient = Client.Find(clientId);
            newClient.Delete();
            return RedirectToAction("Index");
        }
        [HttpPost("/client/{clientId}/update")]
        public ActionResult UpdateClient(int stylistId, int clientId)
        {
            Client newClient = Client.Find(clientId);
            newClient.Edit(Request.Form["client-name"]);
            return RedirectToAction("Index", newClient);
        }
        [HttpPost("/client/{clientId}")]
        public ActionResult AddStylistToClient(int clientId)
        {
            Client client = Client.Find(clientId);
            Stylist stylist = Stylist.Find(int.Parse(Request.Form["stylist-id"]));
            client.AddStylist(stylist);
            return RedirectToAction("Details", new { id = clientId});
        }
}
