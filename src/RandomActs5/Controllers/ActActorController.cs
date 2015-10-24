using System.Linq;
using System.Net;
using Microsoft.AspNet.Mvc;
using RandomActs.Models;
using Microsoft.AspNet.Mvc.Rendering;

namespace RandomActs.Controllers
{
    public class ActActorController : Controller
    {
		private readonly IRandomActActorRepository actActorRepository;
        private readonly IRandomActRepository actRepository;
        private readonly IRandomActorRepository actorRepository;

        //public ActActorController() : this(new RandomActActorRepository(), 
        //                                    new RandomActRepository(),
        //                                    new RandomActorRepository())
        //{
        //}

        public ActActorController(IRandomActActorRepository ActActorRepository,
            IRandomActRepository ActRepository,
            IRandomActorRepository ActorRepository)
        {
            this.actActorRepository = ActActorRepository;
            this.actRepository = ActRepository;
            this.actorRepository = ActorRepository;
        }

        public ActionResult AttendeeList(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            RandomAct randomAct = actRepository.Find(id.Value);
            if (randomAct == null)
            {
                return HttpNotFound();
            }

            ViewBag.RandomActId = id.Value;
            ViewBag.RandomActTitle = randomAct.Title;
            ViewBag.MaxActors = randomAct.MaxActors;
            if (randomAct.Actors.Count < randomAct.MaxActors)
            {
                ViewBag.Registered = (randomAct.Actors == null ? 0 : randomAct.Actors.Count);
                ViewBag.WaitListed = 0;
                ViewBag.MaxedOut = false;
            }
            else
            {
                ViewBag.Registered = randomAct.MaxActors;
                ViewBag.WaitListed = randomAct.Actors.Count - randomAct.MaxActors;
                ViewBag.MaxedOut = true;
            }
            var randomactactors = actActorRepository.FilterByActId(id.Value);
            return View(randomactactors);
        }

        // GET: /ActActor/Register
        public ActionResult Register(int? id)
        {
            return GoRegister(id, false);
        }

        // GET: /ActActor/WaitList
        public ActionResult WaitList(int? id)
        {
            return GoRegister(id, true);
        }

        private ActionResult GoRegister(int? id, bool waitList)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            RandomAct randomAct = actRepository.Find(id.Value);
            if (randomAct == null)
            {
                return HttpNotFound();
            }

            RandomActActor newRandomActActor = new RandomActActor();
            newRandomActActor.RandomActId = id.Value;
            newRandomActActor.WaitList = waitList;
            ViewBag.RandomActTitle = randomAct.Title;
            ViewBag.RandomActorId = new SelectList(actorRepository.All, "RandomActorId", "FullName");

            return View("Register", newRandomActActor);
        }

        // POST: /ActActor/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind("RandomActActorId","RandomActId","RandomActorId","WhatICanBring","Message","WaitList")] RandomActActor randomactactor)
        {
            if (actActorRepository.IsActorRegisteredAlready(randomactactor.RandomActId, randomactactor.RandomActorId))
                ModelState.AddModelError(string.Empty, "This actor already registered for event.");
            else if (ModelState.IsValid)
            {
                actActorRepository.InsertOrUpdate(randomactactor);
                actActorRepository.Save();
                return RedirectToAction("AttendeeList", new { id = randomactactor.RandomActId });
            }

            ViewBag.RandomActTitle = actRepository.Find(randomactactor.RandomActId).Title;
            ViewBag.RandomActorId = new SelectList(actorRepository.All, "RandomActorId", "FullName", randomactactor.RandomActorId);
            return View("Register", randomactactor);
        }

        // GET: /ActActor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            RandomActActor randomactactor = actActorRepository.Find(id.Value);
            if (randomactactor == null)
            {
                return HttpNotFound();
            }
            ViewBag.RandomActorId = new SelectList(actorRepository.All, "RandomActorId", "FullName", randomactactor.RandomActorId);
            return View(randomactactor);
        }

        // POST: /ActActor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("RandomActActorId","RandomActId","RandomActorId","WhatICanBring","Message","WaitList")] RandomActActor randomactactor)
        {
            if (ModelState.IsValid)
            {
                actActorRepository.InsertOrUpdate(randomactactor);
                actActorRepository.Save();
                return RedirectToAction("AttendeeList", new { id = randomactactor.RandomActId });
            }
            ViewBag.RandomActorId = new SelectList(actorRepository.All, "RandomActorId", "FullName", randomactactor.RandomActorId);
            return View(randomactactor);
        }

        // GET: /ActActor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            RandomActActor randomactactor = actActorRepository.Find(id.Value);
            if (randomactactor == null)
            {
                return HttpNotFound();
            }
            return View(randomactactor);
        }

        // POST: /ActActor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int randomActId = actActorRepository.Find(id).RandomActId;
            actActorRepository.Delete(id);
            actActorRepository.Save();
            return RedirectToAction("AttendeeList", new { id = randomActId });
        }
    }
}
