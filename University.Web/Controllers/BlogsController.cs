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
    public class BlogsController : Controller
    {
        private readonly CosmosService cosmosService;
        private readonly IConfiguration configuration;

        public BlogsController (IConfiguration configuration)
        {
            this.configuration = configuration;
            this.cosmosService = new CosmosService(configuration["CosmosAccount"]);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var blogs = await cosmosService.GetAll<BlogsDTO>("University", "Blogs");
            return View(blogs);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogsDTO blogs)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(blogs);

                var status = await cosmosService.Insert("University", "Blogs", blogs);
                if (status == (int)HttpStatusCode.Created)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var blogs = await cosmosService.GetById<BlogsDTO>("University", "Blogs", id);
            return View(blogs);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogsDTO blogs)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(blogs);

                var status = await cosmosService.Update("University", "Blogs", blogs);
                if (status == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var blogs = await cosmosService.GetById<BlogsDTO>("University", "Blogs", id);
            return View(blogs);

        }

        [HttpPost]
        public async Task<IActionResult> Delete(BlogsDTO blogs)
        {
            try
            {
                var status = await cosmosService.Delete("University", "Blogs", blogs.Id);

                if (status == (int)HttpStatusCode.NoContent)
                    return RedirectToAction(nameof(Index));


            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(blogs);
        }

        [HttpGet]
        public IActionResult Post(string id)
        {
            ViewData["id"] = id;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Post(string id, PostDTO post)
        {
            try
            {
                ViewData["id"] = id;

                if (!ModelState.IsValid)
                    return View(post);


                var blogs = await cosmosService.GetById<BlogsDTO>("University", "Blogs", id);

                if (blogs.Posts == null)
                    blogs.Posts = new List<PostDTO>();
                blogs.Posts.Add(post);

                var status = await cosmosService.Update("University", "Blogs", blogs);
                if (status == (int)HttpStatusCode.OK)
                    return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(post);
        }

    }
}
