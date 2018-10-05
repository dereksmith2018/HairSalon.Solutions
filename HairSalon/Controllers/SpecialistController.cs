using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class SpecialistController : Controller
    {
        [HttpGet("/specialist")]
        public ActionResult Index()
        {
            List<Specialist> allspecialists = Specialist.GetAll();
            return View(allspecialists);
        }
        [HttpGet("/specialist/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpGet("/specialist/{specialistId}")]
        public ActionResult Details(int specialistId)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Specialist specialist = Specialist.Find(specialistId);
            List<Stylist> allStylists = Stylist.GetAllStylist();
            List<Stylist> stylist = specialist.GetStylist();
            model.Add("specialist", specialist);
            model.Add("specialistStylist", stylist);
            model.Add("allStylists", allStylists);
            return View(model);
        }
        [HttpPost("/specialist/{specialistId}")]
        public ActionResult AddStylistSpecialist(int specialistId)
        {
            Specialist specialist = Specialist.Find(specialistId);
            Stylist stylist = Stylist.Find(int.Parse(Request.Form["stylist-id"]));
            specialist.AddStylist(stylist);
            return RedirectToAction("Details", new { id = specialistId });
        }
    }
}
