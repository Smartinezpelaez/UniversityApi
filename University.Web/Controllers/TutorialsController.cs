using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class TutorialsController : Controller
    {
        private readonly ApiService apiService = new ApiService();
        private readonly CosmosService cosmosService;
        private readonly IConfiguration configuration;

        public TutorialsController(IConfiguration configuration)
        {

            this.configuration = configuration;
            this.cosmosService = new CosmosService(configuration["CosmosAccount"]);
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var tutorials = await cosmosService.GetAll<TutorialDTO>("University", "Tutorials");
            return View(tutorials);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(TutorialDTO tutorial)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(tutorial);

                var status = await cosmosService.Insert("University", "Tutorials", tutorial);
                if (status == (int)HttpStatusCode.Created)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception  ex) 
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(tutorial);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var tutorial = await cosmosService.GetById<TutorialDTO>("University", "Tutorials", id);
            return View(tutorial);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(TutorialDTO tutorial)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(tutorial);

                var status = await cosmosService.Update("University", "Tutorials", tutorial);
                if (status == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(tutorial);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var tutorial = await cosmosService.GetById<TutorialDTO>("University", "Tutorials", id);
            return View(tutorial);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(TutorialDTO tutorial)
        {
            try
            {
                var status = await cosmosService.Delete("University", "Tutorials", tutorial.Id);

                if (status == (int)HttpStatusCode.NoContent)
                    return RedirectToAction(nameof(Index));

                
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(tutorial);
        }

        [HttpGet]
        public IActionResult Reviews(string id )
        {
            ViewData["id"] = id;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Reviews(string id, ReviewDTO review)
        {
            try
            {
                ViewData["id"] = id;

                if (!ModelState.IsValid)
                    return View(review);


                var tutorial = await cosmosService.GetById<TutorialDTO>("University", "Tutorials", id);
                    
                    if (tutorial.Reviews == null)
                    tutorial.Reviews = new List<ReviewDTO>();
                tutorial.Reviews.Add(review);

                var status = await cosmosService.Update("University", "Tutorials", tutorial);
                if (status == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(review);
        }

        [HttpGet]
        public async Task<IActionResult> Documents(string id)
        {
            var tutorial = await cosmosService.GetById<TutorialDTO>("University", "Tutorials", id);
            if (tutorial.Documents == null)
                tutorial.Documents = new List<DocumentDTO>();
            return View(tutorial);
        }

        [HttpPost]
        public async Task<IActionResult> Documents(string id, IFormFile file)
        {
            var tutorial = await cosmosService.GetById<TutorialDTO>("University", "Tutorials", id);

            try
            {
                var fileId = Guid.NewGuid().ToString();
                var filePath = $"{id}/{fileId}{Path.GetExtension(file.FileName)}";
                var fileBase64Str = string.Empty;

                #region Save Document
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var fileBytes = ms.ToArray();
                    fileBase64Str = Convert.ToBase64String(fileBytes);
                }

                var request = new { fileBase64Str, filePath };
                var responseDTO = await apiService.RequestAPI<string>("https://university-function-smp.azurewebsites.net/",
                                    "api/SaveDocumentFunction?code=/GIRo5Y0prCPC/craVyXBIcV5WHjweqNKtWDfg3iH9O7vS3wfJpuOA==",
                                    request,
                                    ApiService.Method.Post);
                #endregion

                if (responseDTO.Code == (int)HttpStatusCode.OK)
                {
                    #region Update Tutorial
                    if (tutorial.Documents == null)
                        tutorial.Documents = new List<DocumentDTO>();

                    tutorial.Documents.Add(new DocumentDTO
                    {
                        Id = fileId,
                        FilePath = filePath,
                        Name = file.FileName
                    });

                    await cosmosService.Update("University", "Tutorials", tutorial);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(tutorial);
        }

        [HttpGet]
        public async Task<IActionResult> Download(string fileId, string tutorialId)
        {
            var tutorial = await cosmosService.GetById<TutorialDTO>("University", "Tutorials", tutorialId);
            var file = tutorial.Documents.FirstOrDefault(x => x.Id.Equals(fileId));

            var request = new { container = "documents", filePath = file.FilePath };
            var responseDTO = await apiService.RequestAPI<Dictionary<string, string>>("https://university-function-smp.azurewebsites.net/",
                                    "api/DownloadDocumentFunction?code=ebZAOKuArVZ9a/EsXzjBS2c8DhpoSHwVKLvaz7slsYiaayb43AGRbg==",
                                    request,
                                    ApiService.Method.Post);

            var data = (Dictionary<string, string>)responseDTO.Data;
            var fileDownload = Convert.FromBase64String(data["fileBase64Str"]);
            return File(fileDownload, MediaTypeNames.Application.Octet, file.Name);
        }
    }
}
