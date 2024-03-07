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

        public async Task<IActionResult> Index(string SearchText)
        {
            SPager SearchPager = new SPager() { Action = "Index", Controller = "Employee" , SearchText = SearchText};
            ViewBag.SearchPager = SearchPager;
            if (!string.IsNullOrEmpty(SearchText))
            {
                var employee = await _employeesService.GetEmployeeByName(SearchText);
                var Employeedata = _mapper.Map<List<EmployeeModelView>>(employee);
                return View(Employeedata);
            }
            else
            {
                var employee = await _employeesService.GetAllEmployees();
                var Employeedata = _mapper.Map<List<EmployeeModelView>>(employee);
                return View("Index", Employeedata);
            }
        }

        //public async Task<IActionResult> Index(string Search)
        //{
        //    Func<Employee, bool> searchPredicate;

        //    if (!string.IsNullOrEmpty(Search))
        //    {
        //        searchPredicate = e => e.FirstName.Contains(Search);
        //    }
        //    else
        //    {
        //        searchPredicate = e => true;

        //    }
        //    var employees = await _employeesService.GetAllEmployees();
        //    var filteredEmployees = employees.Where(searchPredicate).ToList();

        //    var Employeedata = _mapper.Map<List<EmployeeModelView>>(filteredEmployees);
        //    return View("Index", Employeedata);
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

        public async Task<IActionResult> _UpSert(int? id)
        {
            if (id == 0 || id == null)
            {
                EmployeeModelView emp = new EmployeeModelView();
                return PartialView();
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
                    return RedirectToAction("Index","Employee");
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
                    return RedirectToAction("Index", "Employee");
                }
            }
            else
            {
                    return View();
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
            return RedirectToAction("Index","Employee");
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employee = await _employeesService.GetAllEmployees();
            var Employeedata = _mapper.Map<List<EmployeeModelView>>(employee);
            return Json(new { data = Employeedata});
        }
        #endregion
    }
}
