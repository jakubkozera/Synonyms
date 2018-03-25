using Synonyms.BusinessLogic.Dto;
using Synonyms.BusinessLogic.Interfaces;
using Synonyms.WebApplication.Models;
using System.Linq;
using System.Web.Mvc;

namespace Synonyms.WebApplication.Controllers
{
    public class SynonymController : Controller
    {
        private ISynonymService _synonymService;

        public SynonymController(ISynonymService synonymService)
        {
            _synonymService = synonymService;
        }

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Add(SynonymModel model)
        {
            if (ModelState.IsValid)
            {
                _synonymService.Add(new SynonymDto
                {
                    Term = model.Term,
                    Synonyms = model.Synonyms.Split(',').ToList()
                });

                return Json(new { success = true, message = "Synonym added to database" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { sucess = false, message = "Model is invalid." }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetAll()
        {
            var synonyms = _synonymService.Merge();

            return Json(new { items = synonyms }, JsonRequestBehavior.AllowGet);
        }

    }
}