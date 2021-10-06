using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using University.BL.DTOs;
using University.BL.Services.Implements;

namespace University.Web.Controllers
{
    public class TutorialsController : Controller
    {
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
    }
}
