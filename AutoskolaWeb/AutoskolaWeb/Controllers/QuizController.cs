using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoskolaWeb.DAL;
using AutoskolaWeb.Model;
using AutoskolaWeb.Models;

namespace AutoskolaWeb.Controllers
{
    [Authorize]
    public class QuizController : ControllerBase
    {
        [Authorize(Roles = "User,Admin")]
        public ActionResult Index()
        {

            var model = QuizRepository.All.ToList();

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public ActionResult IndexAjax(QuizFilterModel filter)
        {
            var quizQuery = QuizRepository.All;

            if (!string.IsNullOrWhiteSpace(filter.QuizName))
                quizQuery = quizQuery.Where(p => p.QuizName.Contains(filter.QuizName));

            if (filter.Score != null)
                quizQuery = quizQuery.Where(p => p.QuizPoints.Equals(filter.Score));


            var model = quizQuery.ToList();

            return PartialView("_IndexTable", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddQuestion(int id)
        {
            FillDropdownValues();
            var model = QuizRepository.Find(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("AddQuestion")]
        public ActionResult AddQuestionPost(Quiz formModel, int Question)
        {
            
            if (ModelState.IsValid)
            {
                var modelDb = QuizRepository.Find(formModel.ID);
                var modelQuestion = QuestionRepository.Find(Question);
                modelDb.Questions.Add(modelQuestion);
                if (this.TryUpdateModel(modelDb))
                {
                    QuizRepository.Update(modelDb);
                    QuizRepository.Save();

                    //return RedirectToAction("Index");
                }

            }
            FillDropdownValues();
            return View(formModel);
        }
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var model = QuizRepository.Find(id);

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            FillDropdownValues();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Quiz model)
        {
            if(ModelState.IsValid)
            {
                QuizRepository.Insert(model);
                QuizRepository.Save();
                return RedirectToAction("Index");
            }

            FillDropdownValues();
            return View(model);
        }

       [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            FillDropdownValues();

            var model = QuizRepository.Find(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [Authorize(Roles = "Admin")]
        public ActionResult EditPost(Quiz formModel)
        {
            if (ModelState.IsValid)
            {
                var modelDb = QuizRepository.Find(formModel.ID);
                if(this.TryUpdateModel(modelDb))
                {
                    QuizRepository.Update(modelDb);
                    QuizRepository.Save();

                    return RedirectToAction("Index");
                }
            }

            FillDropdownValues();
            return View(formModel);
        }

       [Authorize(Roles = "Admin")]
        public JsonResult Delete(int id)
        {
            QuizRepository.Delete(id);
            QuizRepository.Save();

            return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        //Brisanje pitanja iz kviza
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteQuestion(int id, int quizId)
        {
            var modelQuestion = QuestionRepository.Find(id);
            var modelQuiz = QuizRepository.Find(quizId);
            modelQuiz.Questions.Remove(modelQuestion);
            QuizRepository.Update(modelQuiz);
            QuizRepository.Save();

            return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private void FillDropdownValues()
        {
            var possibleQuestions = QuestionRepository.All.ToList();

            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem(); 
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var question in possibleQuestions)
            {
                listItem = new SelectListItem();
                listItem.Text = question.QuestionText;
                listItem.Value = question.ID.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
            ViewBag.PossibleQuestions = selectItems;
        }
    }
}