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

            //return View(entries);
            //Abaixo como colocar uma action com layout alternativo fora do padrão
            return View("Index", "~/Views/Shared/_LayoutAlternativo.cshtml", entries);

        }

        public ActionResult Add()
        {

            var entry = new Entry() {

                Date = DateTime.Today,
                //ActivityId = 2
            };

            ViewBag.ActivitiesSelectListItems = new SelectList(
                Data.Data.Activities,"Id", "Name");

            return View(entry);

            //return View("Index", "~/Views/Shared/_LayoutAlternativo.cshtml", entry);

            //view com tds os parâmetros possíveis
            //return View("Index", "~/Views/Shared/_StaffLayout.cshtml", someViewModel);

        }


        [HttpPost]
        public ActionResult Add(Entry entry)
        {

            //Se não houverem erros de validação no campo 'duration' então se certifica q ele é maior do que zero
            if (ModelState.IsValidField("Duration") && entry.Duration > 0)
            {
                ModelState.AddModelError("Duration", "The Duration field value must be greater than '0'. ");
            }

            if (ModelState.IsValid)
            {
                _entriesRepository.AddEntry(entry);

                return RedirectToAction("Index");


            }
            else
            {
                ViewBag.Erro = "Não rolou";
            }

            ViewBag.ActivitiesSelectListItems = new SelectList(
                Data.Data.Activities, "Id", "Name");

            //entry.ActivityId = 2;

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