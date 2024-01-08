using Synechron.EventsPortal.Dal;
using Synechron.EventsPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Synechron.EventsPortal.Controllers
{
    public class EventsController : Controller
    {
        private readonly EventsDal _eventsDal;
        public EventsController(EventsDal dal)
        {
            _eventsDal = dal;
        }
        // GET: Employees
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Welcome To Synechron Events List!";
            ViewBag.PageSubTitle = "Core Development Team Of Bharat!";
            return View(_eventsDal.GetAllEvents());
        }
        public ActionResult Details(int id)
        {
            ViewBag.PageTitle = "Welcome To Synechron Events List!";
            ViewBag.PageSubTitle = "Core Development Team Of Bharat!";
            return View(_eventsDal.GetEventDetails(id));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Event events)
        {
            if (ModelState.IsValid)
            {
                events.Logo= "~/Images/logo.svg";
                int result = _eventsDal.InsertEvent(events);    
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            return View();
        }
    }
}