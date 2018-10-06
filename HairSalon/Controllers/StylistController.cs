using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Models;


namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {
    [HttpGet("/stylist")]
        public ActionResult Index()
        {
            List<Stylist> newStylist = Stylist.GetAllStylist();
            return View(newStylist);
        }
        [HttpGet("/stylist/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpGet("/stylist/{id}")]
        public ActionResult Details(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist newStylist = Stylist.Find(id);
            List<Client> stylistClient = newStylist.GetClient();
            List<Specialist> stylistSpecialist = newStylist.GetSpecialist();
            List<Client> allClient = Client.GetAllClient();
            List<Specialist> allSpecialists = Specialist.GetAll();
            model.Add("stylist", newStylist);
            model.Add("stylistSpecialist", stylistSpecialist);
            model.Add("client", stylistClient);
            model.Add("allClient", allClient);
            model.Add("allSpecialists", allSpecialists);
            return View(model);
        }
        [HttpGet("/stylist/{stylistId}/update")]
        public ActionResult UpdateForm (int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist newStylist = Stylist.Find(stylistId);
            model.Add("stylist", newStylist);
            return View("UpdateForm", model);
        }
        [HttpGet("/stylist/delete")]
        public ActionResult DeleteAll()
        {
            Stylist.DeleteAll();
            return RedirectToAction("Index");
        }
        [HttpGet("/stylist/{stylistId}/delete")]
        public ActionResult DeleteStylist(int stylistId)
        {
            Stylist selectStylist = Stylist.Find(stylistId);
            selectStylist.Delete();
            return RedirectToAction("Index");
        }
        [HttpPost("/stylist/{stylistId}/update")]
        public ActionResult UpdateStylist (int stylistId)
        {
            Stylist newStylist = Stylist.Find(stylistId);
            newStylist.Edit(Request.Form["new-stylist"]);
            return RedirectToAction("Index");
        }
        [HttpPost("/stylist/{stylistId}/specialist/new")]
        public ActionResult AddSpecialist (int stylistId)
        {
            Stylist stylist = Stylist.Find(stylistId);
            Specialist specialist = Specialist.Find(int.Parse(Request.Form["specialist-id"]));
            stylist.AddSpecialist(specialist);
            return RedirectToAction("Details", new {id = stylistId});
        }
        [HttpPost("/stylist/{stylistId}/client/new")]
        public ActionResult AddStylist (int stylistId)
        {
            Stylist stylist = Stylist.Find(stylistId);
            Client client = Client.Find(int.Parse(Request.Form["client-id"]));
            stylist.AddClient(client);
            return RedirectToAction("Details", new {id = stylistId});
        }
        [HttpPost("/stylist")]
        public ActionResult CreateStylist()
        {
            Stylist newStylist = new Stylist(Request.Form["new-stylist"]);
            newStylist.Save();
            List<Stylist> listStylist = Stylist.GetAllStylist();
            return RedirectToAction("Index");
        }
        [HttpPost("/client")]
        public ActionResult CreateClient(int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist newStylist = Stylist.Find(stylistId);
            Client newClient = new Client(Request.Form["new-client"]);
            newClient.Save();
            newStylist.AddClient(newClient);
            List<Client> stylistClient = newStylist.GetClient();
            model.Add("client", stylistClient);
            model.Add("stylist", newStylist);
            return RedirectToAction("Index", new{id = stylistId});
        }
        [HttpPost("/specialist")]
        public ActionResult CreateSpecialist(int stylistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Stylist newStylist = Stylist.Find(stylistId);
            Specialist newSpecialist = new Specialist(Request.Form["new-specialist"]);
            newSpecialist.Save();
            newStylist.AddSpecialist(newSpecialist);
            List<Specialist> stylistSpecialist = newStylist.GetSpecialist();
            model.Add("specialist", stylistSpecialist);
            model.Add("stylist", newStylist);
            return RedirectToAction("Index", new{id = stylistId});
        }
  }

}
