using System.Net;
using RandomActs.Models;
using Microsoft.AspNet.Mvc;

namespace RandomActs.Controllers
{
    public class ActController : Controller
    {
		private readonly IRandomActRepository actRepository;

        //public ActController() : this(new RandomActRepository())
        //{
        //}

        public ActController(IRandomActRepository RandomActRepository)
        {
            this.actRepository = RandomActRepository;
        }

        // GET: /Home/
        public ActionResult Index()
        {
            return View(actRepository.AllIncluding(a => a.Actors));
        }

        // GET: /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("RandomActId","Title","Description","Location","Address","City","State","StartTime","EndTime","MaxActors")] RandomAct randomact)
        {
            if (ModelState.IsValid)
            {
                actRepository.InsertOrUpdate(randomact);
                actRepository.Save();
                return RedirectToAction("Index");
            }

            return View(randomact);
        }

        // GET: /Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            RandomAct randomact = actRepository.Find(id.Value);
            if (randomact == null)
            {
                return HttpNotFound();
            }
            return View(randomact);
        }

        // POST: /Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("RandomActId","Title","Description","Location","Address","City","State","StartTime","EndTime","MaxActors")] RandomAct randomact)
        {
            if (ModelState.IsValid)
            {
                actRepository.InsertOrUpdate(randomact);
                actRepository.Save();
                return RedirectToAction("Index");
            }
            return View(randomact);
        }

        // GET: /Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            RandomAct randomact = actRepository.Find(id.Value);
            if (randomact == null)
            {
                return HttpNotFound();
            }
            return View(randomact);
        }

        // POST: /Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            actRepository.Delete(id);
            actRepository.Save();
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            return View("About");
        }

        public ActionResult Contact()
        {
            return View("Contact");
        }


        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
