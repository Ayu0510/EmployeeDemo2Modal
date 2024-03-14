using AutoMapper;
using EmployeeDemo.Domain.Model;
using EmployeeDemo.web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using System.Drawing;
using EmployeeDemo.web.Views.Shared.Components.SearchBar;

namespace EmployeeDemo.web.Controllers
{
    [Produces("application/json")]
    public class EmployeeController : Controller
    {
        private readonly EmployeesService _employeesService;
        private readonly SkillsService _skillsService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(EmployeesService employeesService,SkillsService skillsService,IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _employeesService = employeesService;
            _skillsService = skillsService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string sortOrder,string SearchText, int pg = 1)
        {
            SPager SearchPager = new SPager() { Action = "Index", Controller = "Employee", SearchText = SearchText };
            ViewBag.SearchPager = SearchPager;
            return View("Index");
        }
        //public async Task<IActionResult> Index(string sortOrder,string SearchText, int pg = 1)
        //{
        //    SPager SearchPager = new SPager() { Action = "Index", Controller = "Employee", SearchText = SearchText };
        //    ViewBag.SearchPager = SearchPager;
        //    const int pageSize = 5;
        //    if (pg < 1)
        //    {
        //        pg = 1;
        //    }

        //    Func<Employee, bool> searchPredicate;

        //    if (!string.IsNullOrEmpty(SearchText))
        //    {
        //        searchPredicate = e => e.FirstName.Contains(SearchText) 
        //        || e.LastName.Contains(SearchText) 
        //        || e.Email.Contains(SearchText,StringComparison.OrdinalIgnoreCase);
        //    }
        //    else
        //    {
        //        searchPredicate = e => true;

        //    }
        //    var employee = await _employeesService.GetAllEmployees();
        //    var FilteredEmployee = employee.Where(searchPredicate).ToList();
        //    var Employeedata = _mapper.Map<List<EmployeeModelView>>(FilteredEmployee);
        //    int recsCount = Employeedata.Count();
        //    var pager = new Pager(recsCount, pg, pageSize);
        //    int recSkip = (pg - 1) * pageSize;
        //    var data = Employeedata.Skip(recSkip).Take(pager.PageSize).ToList();

        //    ViewData["Name"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    ViewData["Email"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
        //    ViewData["Project Manager"] = String.IsNullOrEmpty(sortOrder) ? "Project_Manager" : "";
        //    ViewData["Sr. Developer"] = String.IsNullOrEmpty(sortOrder) ? "Sr._Developer" : "";
        //    ViewData["Jr. Developer"] = String.IsNullOrEmpty(sortOrder) ? "Jr._Developer" : "";
        //    ViewData["Gender"] = String.IsNullOrEmpty(sortOrder) ? "Gender" : "";
        //    var employees = from e in data select e;

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            employees = employees.OrderByDescending(e => e.FirstName);
        //            break;
        //        case "email_desc":
        //            employees = employees.OrderByDescending(e => e.Email);
        //            break;
        //        case "Project_Manager":
        //            employees = employees.OrderByDescending(e => e.Designation == "Project Manager");
        //            break;
        //        case "Sr._Developer":
        //            employees = employees.OrderByDescending(e => e.Designation == "Sr. Developer");
        //            break; 
        //        case "Jr._Developer":
        //            employees = employees.OrderByDescending(e => e.Designation == "Jr. Developer");
        //            break;
        //        case "Gender":
        //            employees = employees.OrderByDescending(e => e.Gender == "Male");
        //            break;
        //        default:
        //            employees = employees.OrderBy(e => e.FirstName);
        //            break;
        //    }

        //    this.ViewBag.Pager = pager;
        //    if(SearchText != null)
        //    {
        //        TempData["success"] = "Employee Searched Successfully";
        //    }
        //    return View(employees.ToList());
        //}
        public async Task<IActionResult> UpSert(int? id)
        {
            if(id == 0 || id == null)
            {
                return View();
            }
            else 
            {
                var EmployeeEditMapper = _mapper.Map<EmployeeModelView>(await _employeesService.GetEmployeeById(id));
                return View(EmployeeEditMapper);
            }
        }

        public async Task<IActionResult> UpSertPartial(int? id)
        {
            if (id == 0 || id == null)
            {
                var emp = new EmployeeModelView();
                return PartialView(emp);
            }
            else
            {
                var EmployeeEditMapper = _mapper.Map<EmployeeModelView>(await _employeesService.GetEmployeeById(id));
                return PartialView(EmployeeEditMapper);
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpSert(int? id,EmployeeModelView employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.ImageFile != null)
                {
                    if (employee.Image != null)
                    {
                        string path1 = Path.Combine(_webHostEnvironment.WebRootPath + "/Image/", employee.Image);
                        if (System.IO.File.Exists(path1))
                        {
                            System.IO.File.Delete(path1);
                        }
                    }
                    string folder = "/Image/";
                    string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                    string datetime = DateTime.Now.ToString("dd_MM_yy_hh_mm_ss_ff");
                    string extention = Path.GetExtension(employee.ImageFile.FileName);
                    employee.Image = fileName = "img_" + datetime + "_" + fileName + extention;
                    string path = Path.Combine(_webHostEnvironment.WebRootPath + folder, fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    await employee.ImageFile.CopyToAsync(fileStream);
                }

                List<Skill> skillList = new List<Skill>();
                if (!string.IsNullOrEmpty(employee.SkillNames)) 
                {
                  var skills =  employee.SkillNames.Split(",");
                    if (skills.Length > 0)
                    {
                        
                        foreach(var item in skills)
                        {
                            Skill skill = new Skill();
                            skill.SkillName = item;
                            skillList.Add(skill);
                        }
                    }
                }
                if(id == 0 || id == null)
                {
                    var EmployeeAddMapper = _mapper.Map<Employee>(employee);
                    EmployeeAddMapper.Skills = skillList;
                    var EmployeeAdd = _employeesService.AddEmployee(EmployeeAddMapper);
                    TempData["success"] = "Employee Inserted Successfully";
                }
                else
                {
                    var employeeSkillCheck = _mapper.Map<EmployeeModelView>(await _employeesService.GetEmployeeById(id));
                    if (!string.IsNullOrEmpty(employeeSkillCheck.SkillNames))
                    {
                        var SkillDelete = _mapper.Map<skillsModelView>(await _skillsService.DeleteSkills(id));
                    }
                    var EmployeeUpdateMap = _mapper.Map<Employee>(employee);
                    EmployeeUpdateMap.Skills = skillList;
                    var EmployeeUpdate = _employeesService.UpdateEmployee(EmployeeUpdateMap);
                    TempData["success"] = "Employee Updated Successfully";
                }
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                    
                    return View();
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> UpSertPartial(int? id,EmployeeModelView employee)
        {
            if (ModelState.IsValid)
            {
                if (employee.ImageFile != null)
                {
                    if (employee.Image != null)
                    {
                        string path1 = Path.Combine(_webHostEnvironment.WebRootPath + "/Image/", employee.Image);
                        if (System.IO.File.Exists(path1))
                        {
                            System.IO.File.Delete(path1);
                        }
                    }
                    string folder = "/Image/";
                    string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                    string datetime = DateTime.Now.ToString("dd_MM_yy_hh_mm_ss_ff");
                    string extention = Path.GetExtension(employee.ImageFile.FileName);
                    employee.Image = fileName = "img_" + datetime + "_" + fileName + extention;
                    string path = Path.Combine(_webHostEnvironment.WebRootPath + folder, fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    await employee.ImageFile.CopyToAsync(fileStream);
                }

                List<Skill> skillList = new List<Skill>();
                if (!string.IsNullOrEmpty(employee.SkillNames)) 
                {
                  var skills =  employee.SkillNames.Split(",");
                    if (skills.Length > 0)
                    {
                        
                        foreach(var item in skills)
                        {
                            Skill skill = new Skill();
                            skill.SkillName = item;
                            skillList.Add(skill);
                        }
                    }
                }
                if(id == 0 || id == null)
                {
                    var EmployeeAddMapper = _mapper.Map<Employee>(employee);
                    EmployeeAddMapper.Skills = skillList;
                    var EmployeeAdd = _employeesService.AddEmployee(EmployeeAddMapper);
                    TempData["success"] = "Employee Inserted Successfully";
                }
                else
                {
                    var employeeSkillCheck = _mapper.Map<EmployeeModelView>(await _employeesService.GetEmployeeById(id));
                    if (!string.IsNullOrEmpty(employeeSkillCheck.SkillNames))
                    {
                        var SkillDelete = _mapper.Map<skillsModelView>(await _skillsService.DeleteSkills(id));
                    }
                    var EmployeeUpdateMap = _mapper.Map<Employee>(employee);
                    EmployeeUpdateMap.Skills = skillList;
                    var EmployeeUpdate = _employeesService.UpdateEmployee(EmployeeUpdateMap);
                    TempData["success"] = "Employee Updated Successfully";
                }

                return RedirectToAction("Index");
            }
            else
            {
                    
                    return View("Index");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var SkillDelete = _mapper.Map<skillsModelView>(await _skillsService.DeleteSkills(id));
            var EmployeeDelete =_mapper.Map<EmployeeModelView>( await _employeesService.DeleteEmployee(id));
            if(EmployeeDelete.Image != null) 
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath + "/Image/",EmployeeDelete.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            TempData["success"] = "Employee Deleted Successfully";
            return Json(id);
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee(string? sortOrder, string SearchText)
        {


            Func<Employee, bool> searchPredicate;

            if (!string.IsNullOrEmpty(SearchText))
            {
                searchPredicate = e => e.FirstName.Contains(SearchText)
                || e.LastName.Contains(SearchText)
                || e.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                searchPredicate = e => true;

            }

            var employee = await _employeesService.GetAllEmployees();
            var FilteredEmployee = employee.Where(searchPredicate).ToList();
            var Employeedata = _mapper.Map<List<EmployeeModelView>>(FilteredEmployee);

            var employees = from e in Employeedata select e;

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.FirstName);
                    break;
                case "email_desc":
                    employees = employees.OrderByDescending(e => e.Email);
                    break;
                case "Project_Manager_desc":
                    employees = employees.OrderByDescending(e => e.Designation == "Project Manager");
                    break;
                case "Sr_Developer_desc":
                    employees = employees.OrderByDescending(e => e.Designation == "Sr. Developer");
                    break;
                case "Jr_Developer_desc":
                    employees = employees.OrderByDescending(e => e.Designation == "Jr. Developer");
                    break;
                case "Gender_desc":
                    employees = employees.OrderByDescending(e => e.Gender == "Male");
                    break;
                case "Genderfemale_desc":
                    employees = employees.OrderByDescending(e => e.Gender == "FeMale");
                    break;
                default:
                    employees = employees.OrderBy(e => e.FirstName);
                    break;
            }

            return Json(new { data = employees });
        }


        public async Task<IActionResult> IndexApi(string sortOrder, string SearchText, int pg = 1)
        {
            SPager SearchPager = new SPager() { Action = "Index", Controller = "Employee", SearchText = SearchText };
            ViewBag.SearchPager = SearchPager;
            const int pageSize = 5;
            if (pg < 1)
            {
                pg = 1;
            }
            
            Func<Employee, bool> searchPredicate;

            if (!string.IsNullOrEmpty(SearchText))
            {
                searchPredicate = e => e.FirstName.Contains(SearchText)
                || e.LastName.Contains(SearchText)
                || e.Email.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                searchPredicate = e => true;

            }
            var employee = await _employeesService.GetAllEmployees();
            var FilteredEmployee = employee.Where(searchPredicate).ToList();
            var Employeedata = _mapper.Map<List<EmployeeModelView>>(FilteredEmployee);
            int recsCount = Employeedata.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = Employeedata.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewData["Name"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["Email"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";
            ViewData["Project Manager"] = String.IsNullOrEmpty(sortOrder) ? "Project_Manager" : "";
            ViewData["Sr. Developer"] = String.IsNullOrEmpty(sortOrder) ? "Sr._Developer" : "";
            ViewData["Jr. Developer"] = String.IsNullOrEmpty(sortOrder) ? "Jr._Developer" : "";
            ViewData["Gender"] = String.IsNullOrEmpty(sortOrder) ? "Gender" : "";
            var employees = from e in data select e;

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(e => e.FirstName);
                    break;
                case "email_desc":
                    employees = employees.OrderByDescending(e => e.Email);
                    break;
                case "Project_Manager":
                    employees = employees.OrderByDescending(e => e.Designation == "Project Manager");
                    break;
                case "Sr._Developer":
                    employees = employees.OrderByDescending(e => e.Designation == "Sr. Developer");
                    break;
                case "Jr._Developer":
                    employees = employees.OrderByDescending(e => e.Designation == "Jr. Developer");
                    break;
                case "Gender":
                    employees = employees.OrderByDescending(e => e.Gender == "Male");
                    break;
                default:
                    employees = employees.OrderBy(e => e.FirstName);
                    break;
            }

            this.ViewBag.Pager = pager;
            if (SearchText != null)
            {
                TempData["success"] = "Employee Searched Successfully";
            }
            return Json(new { data = employees });
        }

        #endregion
    }
}
