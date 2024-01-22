using Abp.AspNetCore.Mvc.Authorization;
using Kaizen.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Kaizen.Web.Controllers
{
    [AbpMvcAuthorize]
    public class AboutController : KaizenControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
