using Microsoft.AspNetCore.Mvc;
using OnatrixInlamning.Data;
using OnatrixInlamning.Models;
using Serilog.Context;
using System.Net;
using System.Net.Mail;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco.Cms.Web.Website.Controllers;

namespace OnatrixInlamning.Controller
{
    public class FormController : SurfaceController
    {

        private readonly IEFCoreScopeProvider<ApplicationDBcontext> _efCoreScopeProvider;

        private readonly SmtpClient client;
        public FormController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, IEFCoreScopeProvider<ApplicationDBcontext> efCoreScopeProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {

            _efCoreScopeProvider = efCoreScopeProvider;

            client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true,

            };

        }

     

        public async Task<IActionResult> SubmitContactForm(ContactFormModel model)
        {

            if (!ModelState.IsValid)
            {
                TempData["name"] = model.Name;
                TempData["email"] = model.Email;
                TempData["phone"] = model.Phone;
                TempData["option"] = model.Option;

                TempData["errorEmail"] = string.IsNullOrEmpty(model.Email);
                TempData["errorName"] = string.IsNullOrEmpty(model.Name);
                TempData["errorPhone"] = string.IsNullOrEmpty(model.Phone);
                TempData["errorOption"] = string.IsNullOrEmpty(model.Option);


                return CurrentUmbracoPage();

            }




            int result = 0;

            using IEfCoreScope<ApplicationDBcontext> scope = _efCoreScopeProvider.CreateScope();

            await scope.ExecuteWithContextAsync<Task>(async db =>
            {

                db.Set<ContactFormModel>().Add(model);

                result = await db.SaveChangesAsync();
            
            });

            scope.Complete();



            if (result != 0)
            {
                TempData["success"] = "Formuläret skickat";

                MailMessage message = new();
                message.From = new MailAddress("onatrixalenagicic@gmail.com");
                message.Subject = "Meddelandet skickat";
                message.To.Add(new MailAddress(model.Email.ToLower()));
                message.Body = "<html><body>Ditt meddelande till oss har inkommit, vi återkommer inom kort!</body></html>";
                message.IsBodyHtml = true;


                client.Send(message);

                return RedirectToCurrentUmbracoPage();
            }



            TempData["success"] = "Formuläret EJ skickat";

            return CurrentUmbracoPage();
			

        }
    }
}
