using AutoskolaWeb.DAL;
using AutoskolaWeb.Model;
using AutoskolaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AutoskolaWeb.Controllers
{
    [Authorize]
    public class QuestionController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = QuestionRepository.All.ToList(); 

            return View(model);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult IndexAjax(QuestionFilterModels filter)
        {
            var questionQuery = QuestionRepository.All;

            if (!string.IsNullOrWhiteSpace(filter.QuestionText))
                questionQuery = questionQuery.Where(p => p.QuestionText.Contains(filter.QuestionText));

            var model = questionQuery.ToList();

            return PartialView("_IndexTable", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            var model = QuestionRepository.Find(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddAnswer(int id)
        {
            FillDropdownValues();
            var model = QuestionRepository.Find(id);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("AddAnswer")]
        public ActionResult AddAnswerPost(Question formModel, int Answer)
        {
            if (ModelState.IsValid)
            {
                var modelDb = QuestionRepository.Find(formModel.ID);
                var modelAnswer = AnswerRepository.Find(Answer);
                modelDb.Answers.Add(modelAnswer);
                if (this.TryUpdateModel(modelDb))
                {
                    QuestionRepository.Update(modelDb);
                    QuestionRepository.Save();

                    //return RedirectToAction("Index");
                }

            }
            FillDropdownValues();
            return View(formModel);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            FillDropdownValues();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Question model)
        {
            if(ModelState.IsValid)
            {
                QuestionRepository.Insert(model);
                QuestionRepository.Save();
                //return RedirectToAction("Index");
            }

            FillDropdownValues();
            return View(model);
        }

       [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            FillDropdownValues();

            var model = QuestionRepository.Find(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public ActionResult EditPost(Question formModel)
        {
            if (ModelState.IsValid)
            {
                var modelDb = QuestionRepository.Find(formModel.ID);
                
                if(this.TryUpdateModel(modelDb))
                {
                    QuestionRepository.Update(modelDb);
                    QuestionRepository.Save();

                    return RedirectToAction("Index");
                }
            }

            FillDropdownValues();
            return View(formModel);
        }

       [Authorize(Roles = "Admin")]
        public JsonResult Delete(int id)
        {
            QuestionRepository.Delete(id);
            QuestionRepository.Save();

            return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        //Brisanje odgovora iz kviza
        [Authorize(Roles = "Admin")]
        public JsonResult DeleteAnswer(int id, int questionId)
        {
            var modelAnswer = AnswerRepository.Find(id);
            var modelQuestion = QuestionRepository.Find(questionId);
            modelQuestion.Answers.Remove(modelAnswer);
            QuestionRepository.Update(modelQuestion);
            QuestionRepository.Save();

            return new JsonResult() { Data = "OK", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        private void FillDropdownValues()
        {
            var possibleAnswers = AnswerRepository.All.ToList();

            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem(); 
            listItem.Text = "- odaberite -";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var answer in possibleAnswers)
            {
                listItem = new SelectListItem();
                listItem.Text = answer.AnswerText;
                listItem.Value = answer.ID.ToString();
                listItem.Selected = false;
                selectItems.Add(listItem);
            }
            ViewBag.PossibleAnswers = selectItems;
        }
    }
    }
