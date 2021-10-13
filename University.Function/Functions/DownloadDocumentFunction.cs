using University.Function.DTOs;
using University.Function.Services.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace University.Function.Functions
{
    public static class DownloadDocumentFunction
    {
        [FunctionName("DownloadDocumentFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                log.LogInformation("C# HTTP trigger function processed a request.");

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                string filePath = data?.filePath;
                string container = data?.container;

                if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(container))
                    return new OkObjectResult(new ResponseDTO
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Message = "No se cumple con la validación del modelo."
                    });

                var blobService = new BlobService(Environment.GetEnvironmentVariable("StorageAccount"));
                var fileDownload = blobService.GetBytes(container, filePath);

                return new OkObjectResult(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.OK,
                    Message = "Se ha realizo el proceso con exito.",
                    Data = new { fileBase64Str = Convert.ToBase64String(fileDownload) }
                });
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);

                return new OkObjectResult(new ResponseDTO
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }
    }
}
