using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Treehouse.FitnessFrog.Data;
using Treehouse.FitnessFrog.Models;

namespace Treehouse.FitnessFrog.Controllers
{
    public class EntriesController : Controller
    {
        private EntriesRepository _entriesRepository = null;

        public EntriesController()
        {
            _entriesRepository = new EntriesRepository();
        }

        public ActionResult Index()
        {
            List<Entry> entries = _entriesRepository.GetEntries();

            // Calculate the total activity.
            double totalActivity = entries
                .Where(e => e.Exclude == false)
                .Sum(e => e.Duration);

            // Determine the number of days that have entries.
            int numberOfActiveDays = entries
                .Select(e => e.Date)
                .Distinct()
                .Count();

            ViewBag.TotalActivity = totalActivity;
            ViewBag.AverageDailyActivity = (totalActivity / (double)numberOfActiveDays);

            return View(entries);
        }

        public ActionResult Add()
        {

            var entry = new Entry() {

                Date = DateTime.Today,

            };

            return View(entry);
        }


        [HttpPost]
        public ActionResult Add(Entry entry)
        {

            if (ModelState.IsValid)
            {
                _entriesRepository.AddEntry(entry);
            }

            return View(entry);

        }

        /*
        [HttpPost]
        public ActionResult Add(DateTime? date, 
            int? activityId, 
            double? duration, 
            Entry.IntensityLevel? intensity, 
            bool? exclude, 
            string notes)
        {

            /*
            ViewBag.Date = date;
            ViewBag.ActivityId = activityId;
            ViewBag.Duration = duration;
            ViewBag.Intensity = intensity;
            ViewBag.Exclude = exclude;
            ViewBag.Notes = notes;
            */

        /*
        ViewBag.Date = ModelState["Date"].Value.AttemptedValue;
        ViewBag.ActivityId = ModelState["ActivityId"].Value.AttemptedValue;
        ViewBag.Duration = ModelState["Duration"].Value.AttemptedValue;
        ViewBag.Intensity = ModelState["Intensity"].Value.AttemptedValue;
        ViewBag.Exclude = ModelState["Exclude"].Value.AttemptedValue;
        ViewBag.Notes = ModelState["Notes"].Value.AttemptedValue;


        return View();

    }
*/


        /*
        ------> PRIMEIRA FORMA COM PARAMETROS DENTRO DO MÉTODO E RELACIONANDO O ACTIONNAME ADD
        [ActionName("Add"), HttpPost]
        public ActionResult AddPost()
        {

            string date = Request.Form["Date"];
            Console.WriteLine("OK");

            return View();

        }
        */

        /*
        ------> SEGUNDA E 'MELHOR' FORMA COM PARAMETROS SENDO PASSADOS COMO ATRIBUTOS DO MÉTODO COM TODOS OS CAMPOS
                DO FORM QUE ESTÁ PRESENTE NA ACTION ADD TIRANDO A NECESSIDADE DE ADICIONAR A ACTIONNAME ADD AO MÉTODO
                POIS O NOME DO MÉTODO AGORA É O MESMO QUE ADD E NÃO ADDPOST
        */





        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }
    }
}