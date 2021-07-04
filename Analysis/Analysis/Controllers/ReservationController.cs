using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Analysis.Infrastructure;
using Analysis.Models;
using Analysis.Models.Repositories;
using Analysis.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Analysis.Models.Pages;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Analysis.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private IClientsRepository clientsRepository;
        private IResultsRepository resultsRepository;
        private IClientAnalysisRepository clientAnalysisRepository;
        private IAnalysisFeaturesRepository analysisFeaturesRepository;
        private IAnalysisTypeRepository analysisTypeRepository;
        public ReservationController(IClientsRepository clientsRepo,IResultsRepository results, IClientAnalysisRepository clientRepository ,IAnalysisTypeRepository typeRepository, IAnalysisFeaturesRepository featuresRepository)
        {
            clientsRepository = clientsRepo;
            resultsRepository = results;
            clientAnalysisRepository = clientRepository;
            analysisFeaturesRepository = featuresRepository;
            analysisTypeRepository = typeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [ImportModelSatate]
        public IActionResult Reserve()
        {
            IEnumerable<AnalysisType> AllAnalysis = analysisTypeRepository.AllAnalysis;
            ClientAnalysisVM clientAnalysisVM = new ClientAnalysisVM();
            clientAnalysisVM.AllAnalysis = AllAnalysis;
            return View(clientAnalysisVM);
        }
        [HttpPost]
        [ExportModelState]
        public IActionResult Reserve(ClientAnalysisVM clientAnalysisVM)
        {
           
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int clientCode = random.Next(1000, int.MaxValue);
                clientAnalysisVM.Client.ClientCode = clientCode;
                TempData["ClientCode"] = clientCode;

                long ClientId = clientAnalysisRepository.AddClient(clientAnalysisVM.Client);
                if (ClientId > 0)
                {
                    List<ClientAnalysis> ClientAnalysisList = new List<ClientAnalysis>();
                    foreach (var Id in clientAnalysisVM.ClientAnalysis.AnalysisIds)
                    {
                        ClientAnalysis clientAnalysis = new ClientAnalysis
                        {
                            AnalysisTypeId = Id,
                            ClientId = ClientId,
                            
                        };
                       // ClientAnalysisList.Add(clientAnalysis);
                        clientAnalysisRepository.AddClientAnalysis(clientAnalysis);
                    }

                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Reserve));
        }
        public IActionResult ClientsList()
        {
            
            return View(clientAnalysisRepository.ClientAnalysis);
        }
        [HttpGet]
        public IActionResult WriteResult(long? AnalysisTypeId, string ClientName, long? ClientAnalysisId)
        {
            if (!AnalysisTypeId.HasValue)
            {
                return RedirectToAction(nameof(ClientsList));

            }
            else
            {
                

                IEnumerable<AnalysisFeatures> analysisFeatures = analysisFeaturesRepository.GetAnalysisFeatures(AnalysisTypeId ?? 0);

                string AnalysisTypeVM = analysisTypeRepository.GetAnalysis(AnalysisTypeId ?? 0).Name;
                WriteResultsVM writeResultsVM = new WriteResultsVM
                {
                    ClientName = ClientName,
                    ClientAnalysisId = ClientAnalysisId ?? 0,
                    AnalysisName = AnalysisTypeVM,
                    AnalysisFeatures = analysisFeatures
                };
                return View(writeResultsVM);
            }
        }
        [HttpPost]
        public IActionResult SaveResult(long ClientAnalysisId, List<AnalysisKeyValue> analysisKeyValue)
        {
            resultsRepository.AddResults(ClientAnalysisId, analysisKeyValue);
            return RedirectToAction(nameof(ClientsList));
        }
       public IActionResult AllClients(QueryOptions options)
        {
            return View(clientsRepository.GetClients(options));
        }
        
        [HttpPost]
        public IActionResult DeleteClient(Client client)
        {
            clientsRepository.DeleteClient(client);
            return RedirectToAction(nameof(AllClients));
        }
        public IActionResult EditClient(long id)
        {
            return View(clientsRepository.GetClient(id));
        }
        [HttpPost]
        public IActionResult EditClient(Client client)
        {
            if (ModelState.IsValid)
            {
                clientsRepository.UpdateClient(client);
                return RedirectToAction(nameof(AllClients));
            }
            return View(client);
        }
    }
}
