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
    public class AnswerController : ControllerBase
    {
        [Authorize(Roles = "Admin")]
        public ActionResult Index() 
        {
            var model = AnswerRepository.All.ToList();
            return View(model);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult IndexAjax(AnswerFilterModel filter)
        {
            var answerQuery = AnswerRepository.All;

            if (!string.IsNullOrWhiteSpace(filter.AnswerText))
                answerQuery = answerQuery.Where(p => p.AnswerText.Contains(filter.AnswerText));

            var model = answerQuery.ToList();

            return PartialView("_IndexTable", model);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            var model = AnswerRepository.Find(id);

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
        public ActionResult Create(Answer model)
        {
            if(ModelState.IsValid)
            {
                AnswerRepository.Insert(model);
                AnswerRepository.Save();
            }

            FillDropdownValues();
            return View(model);
        }

       [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            FillDropdownValues();

            var model = AnswerRepository.Find(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Edit")]
        public ActionResult EditPost(Answer formModel)
        {
            if (ModelState.IsValid)
            {
                var modelDb = AnswerRepository.Find(formModel.ID);
                if(this.TryUpdateModel(modelDb))
                {
                    AnswerRepository.Update(modelDb);
                    AnswerRepository.Save();

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
