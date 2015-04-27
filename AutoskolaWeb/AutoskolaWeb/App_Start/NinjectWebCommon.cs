[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(AutoskolaWeb.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(AutoskolaWeb.App_Start.NinjectWebCommon), "Stop")]

namespace AutoskolaWeb.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using AutoskolaWeb.DAL;
    using AutoskolaWeb.Model;
    using System.Security.Principal;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new NinjectSettings() { InjectNonPublic = true });
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<QuizManagerDbContext>()
                .ToSelf()
                .InRequestScope();

            kernel.Bind<QuizRepository>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument(typeof(IIdentity), GetCurrentUser);

            kernel.Bind<QuestionRepository>()
                .ToSelf()
                .InRequestScope()
                .WithConstructorArgument(typeof(IIdentity), GetCurrentUser);

            kernel.Bind<AnswerRepository>()
               .ToSelf()
               .InRequestScope()
               .WithConstructorArgument(typeof(IIdentity), GetCurrentUser);

            
        }

        private static object GetCurrentUser(Ninject.Activation.IContext context, Ninject.Planning.Targets.ITarget target)
        {
            if(HttpContext.Current != null)
                return HttpContext.Current.GetOwinContext().Authentication.User.Identity;

            return null;
        } 
    }
}
