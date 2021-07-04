using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Analysis.Models.Repositories;
using Analysis.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Analysis.Infrastructure;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Analysis.Controllers
{
    [Authorize]
    
    public class AdminController : Controller
    {
        private IAnalysisFeaturesRepository analysisFeaturesRepository;
        private IAnalysisTypeRepository analysisTypeRepository;
        public AdminController(IAnalysisTypeRepository typeRepository, IAnalysisFeaturesRepository featuresRepository)
        {
            analysisFeaturesRepository = featuresRepository;
            analysisTypeRepository = typeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllAnalysis()
        {
            return View(analysisTypeRepository.AllAnalysis);
        }
        public IActionResult AddAnalysis()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddAnalysis(AnalysisType analysisType)
        {
            if (ModelState.IsValid)
            {
                long typeId = analysisTypeRepository.AddAnalysis(analysisType);
                //TempData["typeId"] = typeId;
                return RedirectToAction(nameof(AllAnalysis));
            }
            return View();
        }
        public IActionResult EditAnalysis(long Id)
        {
            AnalysisType analysisType = analysisTypeRepository.GetAnalysis(Id);
            if(analysisType != null)
            {
                return View(analysisType);
            }
            return RedirectToAction(nameof(AllAnalysis));
        }
        [HttpPost]
        public IActionResult EditAnalysis(AnalysisType analysisType)
        {
            if (ModelState.IsValid)
            {
                analysisTypeRepository.UpdateAnalysis(analysisType);
                return RedirectToAction(nameof(AllAnalysis));
            }
            return View();
        }
        [HttpPost]
        public IActionResult DeleteAnalysis(AnalysisType analysisType)
        {
            analysisTypeRepository.DeleteAnalysis(analysisType);
            return RedirectToAction(nameof(AllAnalysis));
        }
       [ImportModelSatate]
        public IActionResult Features(long id, long EditId = 0)
        {
            
            AnalysisType analysisType = analysisTypeRepository.GetAnalysis(id);
            if(id == 0 || id.GetType() != typeof(long) || analysisType == null)
            {
                return RedirectToAction(nameof(AllAnalysis));
            }
            if(EditId > 0)
            {
                ViewBag.EditId = EditId;
            }
            ViewBag.AnalysisTypeId = id;
            return View(analysisFeaturesRepository.GetAnalysisFeatures(id));
        }
        [HttpPost]
        [ExportModelState]
        public IActionResult AddFeature(AnalysisFeatures analysisFeature)
        {
            if (string.IsNullOrEmpty(analysisFeature.Name))
            {
                ModelState.AddModelError(nameof(analysisFeature.Name), "*Please enter name");
            }
            if (string.IsNullOrEmpty(analysisFeature.NormalRange))
            {
                ModelState.AddModelError(nameof(analysisFeature.NormalRange), "*Please enter Normal Range");
            }
            if (string.IsNullOrEmpty(analysisFeature.MeasruingUnit))
            {
                ModelState.AddModelError(nameof(analysisFeature.MeasruingUnit), "*Please Measuring Unit");
            }
            if (ModelState.IsValid)
            {
                analysisFeaturesRepository.AddFeature(analysisFeature);
                return RedirectToAction(nameof(Features), new { id = analysisFeature.AnalysisTypeId });

            }
         
           return RedirectToAction(nameof(Features), new { id = analysisFeature.AnalysisTypeId });
                
            
        }
        [HttpGet]
        public IActionResult EditFeature(long id, long AFId)
        {
            ViewBag.AnalysisTypeId = id;
            ViewBag.EditId = AFId;
            return View("Features", analysisFeaturesRepository.GetAnalysisFeatures(id));
            //return View("", new Result { });
            
        }
        [ExportModelState]
        [HttpPost]
        public IActionResult UpdateFeature(AnalysisFeatures analysisFeature)
        {
            ValidateAnalysisFeature(analysisFeature);
            if (ModelState.IsValid)
            {
                analysisFeaturesRepository.UpdateFeature(analysisFeature);
                return RedirectToAction(nameof(Features), new { id = analysisFeature.AnalysisTypeId });
            }
            //TempData["EditId"] = analysisFeature.Id;
            return RedirectToAction(nameof(Features), new { id = analysisFeature.AnalysisTypeId,EditId= analysisFeature.Id});

        }
        [HttpPost]
        public IActionResult DeleteFeature(AnalysisFeatures feature)
        {
            analysisFeaturesRepository.DeleteFeature(feature);
            return RedirectToAction(nameof(Features), new { id = feature.AnalysisTypeId });
        }




        public void ValidateAnalysisFeature(AnalysisFeatures analysisFeature)
        {
            if (analysisFeature.Id == 0)
            {
                ModelState.AddModelError(nameof(analysisFeature.Id), "There is something wrong with id");
            }
            if (analysisFeature.AnalysisTypeId == 0)
            {
                ModelState.AddModelError(nameof(analysisFeature.AnalysisTypeId), "There is something wrong with AnalysisTypeId");
            }
            if (string.IsNullOrEmpty(analysisFeature.Name))
            {
                ModelState.AddModelError(nameof(analysisFeature.Name), "*Please enter name");
            }
            if (string.IsNullOrEmpty(analysisFeature.NormalRange))
            {
                ModelState.AddModelError(nameof(analysisFeature.NormalRange), "*Please enter Normal Range");
            }
            if (string.IsNullOrEmpty(analysisFeature.MeasruingUnit))
            {
                ModelState.AddModelError(nameof(analysisFeature.MeasruingUnit), "*Please Measuring Unit");
            }
        }
    }
}
