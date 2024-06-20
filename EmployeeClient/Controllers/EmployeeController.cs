using EmployeeClient.Infrastructure;
using EmployeeClient.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EmployeeClient.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IHttpClientService _httpClientService;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private string endPoint;

        public EmployeeController(IHttpClientService httpClientService, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _configuration = configuration;
            endPoint = _configuration["EndPoint:EmployeeApi"];
        }

        [HttpGet]
        public IActionResult Index()
        {
            ServiceResponse<IEnumerable<EmployeesViewModel>> response = new ServiceResponse<IEnumerable<EmployeesViewModel>>();
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<EmployeesViewModel>>>
                ($"{endPoint}Employee/GetAllEmployees", HttpMethod.Get, HttpContext.Request);

            if (response.Success)
            {
                return View(response.Data);
            }

            return View(new List<EmployeesViewModel>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            viewModel.Departments = GetDepartments();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel viewModel, IFormFile file)//public IActionResult Create(EmployeeViewModel viewModel, IFormFile file)
        {

            if (ModelState.IsValid)
            {

                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(viewModel.EmployeeName), "EmployeeName");
                content.Add(new StringContent(viewModel.Gender), "Gender");
                content.Add(new StringContent(viewModel.DepartmentId.ToString()), "DepartmentId");
                content.Add(new StringContent(viewModel.Salary.ToString()), "Salary");
                content.Add(new StringContent(viewModel.IsPermenant.ToString()), "IsPermenant");

                if (file != null && file.Length > 0)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = file.FileName
                    };
                    content.Add(fileContent);


                }

                HttpClient _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://localhost:7114/api/Employee/AddEmployee");
                var response = _httpClient.PostAsync("https://localhost:7114/api/Employee/AddEmployee", content).Result;


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
            viewModel.Departments = GetDepartments();
            return View(viewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(EmployeeViewModel viewModel)//public IActionResult Create(EmployeeViewModel viewModel, IFormFile file)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        //File Uppload Start
        //        //using (var content = new MultipartFormDataContent())
        //        //{

        //        //    content.Add(new StringContent(viewModel.EmployeeName), "EmployeeName");
        //        //    content.Add(new StringContent(viewModel.Gender), "Gender");
        //        //    content.Add(new StringContent(viewModel.DepartmentId.ToString()), "DepartmentId");
        //        //    content.Add(new StringContent(viewModel.Salary.ToString()), "Salary");
        //        //    content.Add(new StringContent(viewModel.IsPermenant.ToString()), "IsPermenant");

        //        //    if (file != null && file.Length > 0)
        //        //    {
        //        //        var fileContent = new StreamContent(file.OpenReadStream());
        //        //        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //        //        {
        //        //            Name = "file",
        //        //            FileName = file.FileName
        //        //        };
        //        //        content.Add(fileContent);
        //        //    }

        //        //    HttpClient _httpClient = new HttpClient();

        //        //    _httpClient.BaseAddress = new Uri("https://localhost:7114/api/Employee/AddEmployee");
        //        //    var response1 = _httpClient.PostAsync("File", content);

        //        //}

        //        //return RedirectToAction("Index");




        //        //File upload end

        //        string apiUrl = $"{endPoint}Employee/AddEmployee";
        //        viewModel.FileName = "test";
        //        var response = _httpClientService.PostHttpResponseMessage(apiUrl, viewModel, HttpContext.Request);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string successResponse = response.Content.ReadAsStringAsync().Result;
        //            var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(successResponse);
        //            TempData["SuccessMessage"] = serviceResponse.Message;
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            string errorResponse = response.Content.ReadAsStringAsync().Result;
        //            var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse<string>>(errorResponse);
        //            if (errorResponse != null)
        //            {
        //                TempData["ErrorMessage"] = serviceResponse.Message;
        //            }
        //            else
        //            {
        //                TempData["ErrorMessage"] = "Something went wrong. Please try after sometime.";
        //            }
        //        }

        //        return RedirectToAction("Index");
        //    }


        //    viewModel.Departments = GetDepartments();
        //    return View(viewModel);
        //}

        [HttpGet]
        public ActionResult Edit(int id)
        {

            var apiUrl = $"{endPoint}Employee/GetEmployeeById/" + id;
            var response = _httpClientService.GetHttpResponseMessage<UpdateEmployeeViewModel>(apiUrl, HttpContext.Request);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                var serviceResponse = JsonConvert
                    .DeserializeObject<ServiceResponse<UpdateEmployeeViewModel>>(data);
                if (serviceResponse != null && serviceResponse.Success && serviceResponse.Data != null)
                {
                    serviceResponse.Data.Departments = GetDepartments();
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
                    .DeserializeObject<ServiceResponse<UpdateEmployeeViewModel>>(errorData);
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
        public IActionResult Edit(UpdateEmployeeViewModel updateEmployee, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //Image code sttart
                using var content = new MultipartFormDataContent();
                content.Add(new StringContent(updateEmployee.EmployeeId.ToString()), "EmployeeId");
                content.Add(new StringContent(updateEmployee.EmployeeName), "EmployeeName");
                content.Add(new StringContent(updateEmployee.Gender), "Gender");
                content.Add(new StringContent(updateEmployee.DepartmentId.ToString()), "DepartmentId");
                content.Add(new StringContent(updateEmployee.Salary.ToString()), "Salary");
                content.Add(new StringContent(updateEmployee.IsPermenant.ToString()), "IsPermenant");

                if (file != null && file.Length > 0)
                {
                    var fileContent = new StreamContent(file.OpenReadStream());
                    fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "file",
                        FileName = file.FileName
                    };
                    content.Add(fileContent);


                }

                HttpClient _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri("https://localhost:7114/api/Employee/UpdateEmployee");
                var response = _httpClient.PutAsync("https://localhost:7114/api/Employee/UpdateEmployee", content).Result;

                //Image code end


                // var apiUrl = $"{endPoint}Employee/UpdateEmployee";
                //HttpResponseMessage response = _httpClientService.PutHttpResponseMessage(apiUrl, updateEmployee, HttpContext.Request);
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
            updateEmployee.Departments = GetDepartments();

            return View(updateEmployee);
        }


        public IActionResult Delete(int id)
        {
            var apiUrl = $"{endPoint}Employee/DeleteEmployee/" + id;
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

        [HttpGet]
        public IActionResult Index2(int page = 1, int pageSize = 2)
        {
            ViewBag.CurrentPAge = page;
            ServiceResponse<int> response1 = new ServiceResponse<int>();
            var response11 = _httpClientService.GetHttpResponseMessage<string>($"{endPoint}Employee/TotalEmployee", HttpContext.Request);
            string data = response11.Content.ReadAsStringAsync().Result;
            var serviceResponse = JsonConvert
                   .DeserializeObject<ServiceResponse<int>>(data);

            var totalCount = serviceResponse == null ? 0 : Convert.ToInt32(serviceResponse.Data);

            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            ServiceResponse<IEnumerable<EmployeesViewModel>> response = new ServiceResponse<IEnumerable<EmployeesViewModel>>();
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<EmployeesViewModel>>>
                ($"{endPoint}Employee/GetPaginatedEmployee/" + page + "/" + pageSize, HttpMethod.Get, HttpContext.Request);

            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            if (response.Success)
            {
                return View(response.Data);
            }

            return View(new List<EmployeesViewModel>());
        }

        private List<DepartmentViewModel> GetDepartments()
        {
            ServiceResponse<IEnumerable<DepartmentViewModel>> response = new ServiceResponse<IEnumerable<DepartmentViewModel>>();
            response = _httpClientService.ExecuteApiRequest<ServiceResponse<IEnumerable<DepartmentViewModel>>>
                ($"{endPoint}Department/GetAllDepartments", HttpMethod.Get, HttpContext.Request);

            if (response.Success)
            {
                return response.Data.ToList();
            }

            return new List<DepartmentViewModel>();
        }


    }
}
