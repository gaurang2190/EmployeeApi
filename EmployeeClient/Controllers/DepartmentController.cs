using EmployeeClient.Infrastructure;
using EmployeeClient.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EmployeeClient.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {

        private readonly IHttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        private string endPoint;

        public DepartmentController(IHttpClientService httpClientService, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _configuration = configuration;
            endPoint = _configuration["EndPoint:EmployeeApi"];
        }

        public IActionResult Index()
        {
            try
            {
                ServiceResponse<IEnumerable<DepartmentViewModel>> response = new ServiceResponse<IEnumerable<DepartmentViewModel>>();
                response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<DepartmentViewModel>>>
                    ($"{endPoint}Department/GetAllDepartments", HttpMethod.Get, HttpContext.Request);

                if (response.Success)
                {
                    return View(response.Data);
                }

                return View(new List<DepartmentViewModel>());
            }
            catch (Exception ex)
            {

                return View(new List<DepartmentViewModel>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(AddDepartmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

                string apiUrl = $"{endPoint}Department/Create";
                var response = _httpClientService.PostHttpResponseMessage(apiUrl, viewModel, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorResponse);
                    if (errorResponse != null)
                    {
                        TempData["ErrorMessage"] = serviceResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                    }
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var apiUrl = $"{endPoint}Department/GetDepartmentById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<UpdateDepartmentViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<UpdateDepartmentViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    return View(serviceResponse.Data);
                }
                else
                {
                    TempData["ErrorMessage"] = serviceResponse?.Message;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                string errorData = response.Content.ReadAsStringAsync().Result;
                var errorResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<UpdateDepartmentViewModel>>(errorData);
                if (errorResponse != null)
                {
                    TempData["ErrorMessage"] = errorResponse.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                }

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(UpdateDepartmentViewModel updateDepartment)
        {
            if (ModelState.IsValid)
            {

                var apiUrl = $"{endPoint}Department/UpdateDepartment";
                HttpResponseMessage response = _httpClientService.PutHttpResponseMessage(apiUrl, updateDepartment, HttpContext.Request);
                if (response.IsSuccessStatusCode)
                {
                    string successResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
                    TempData["SuccessMessage"] = serviceResponse.Message;
                    return RedirectToAction("Index");
                }
                else
                {
                    string errorResponse = response.Content.ReadAsStringAsync().Result;
                    var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorResponse);
                    if (errorResponse != null)
                    {
                        TempData["ErrorMessage"] = serviceResponse.Message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
                    }
                }
            }

            return View(updateDepartment);
        }

        public IActionResult Delete(int id)
        {
            var apiUrl = $"{endPoint}Department/DeleteDepartment/" + id;
            var response = _httpClientService.ExecuteApiRequest<ServiceResponse<string>>
                ($"{apiUrl}", HttpMethod.Delete, HttpContext.Request);

            if (response.Success)
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = response.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
