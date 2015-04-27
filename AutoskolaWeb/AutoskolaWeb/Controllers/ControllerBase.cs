using Microsoft.Owin.Security;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoskolaWeb.DAL;

namespace AutoskolaWeb.Controllers
{
    [RequireHttps]
    public class ControllerBase : Controller
    {
        [Inject]
        protected QuizRepository QuizRepository { get; set; }

        [Inject]
        protected  QuestionRepository QuestionRepository { get; set; }

        [Inject]
        protected AnswerRepository AnswerRepository { get; set; }

        protected IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}