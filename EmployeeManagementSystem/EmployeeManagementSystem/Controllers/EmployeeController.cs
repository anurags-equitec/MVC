using Dapper;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Repo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        Repository rp=new Repository();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EmpShow(int pageNumber = 1, int pageSize = 5)
        {
            using (var con = new SqlConnection(rp.connectionString))
            {
                con.Open();

                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);

                using (var multi = con.QueryMultiple("getPaginatedEmployee", parameters, commandType: CommandType.StoredProcedure))
                {
                    var employees = multi.Read<EmployeeModel>().ToList();
                    var totalRecords = multi.Read<int>().FirstOrDefault();

                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                    ViewBag.CurrentPage = pageNumber;

                    return View(employees);
                }
            }
        }
        

        public ActionResult EmpBackup(int pageNumber=1,int pageSize=5)
        {
            using(var con=new SqlConnection(rp.connectionString))
            {
                con.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@PageNumber", pageNumber);
                parameters.Add("@PageSize", pageSize);
                using(var multi = con.QueryMultiple("getDelPaginatedEmployee", parameters, commandType: CommandType.StoredProcedure))
                {
                    var employees = multi.Read<BackupModel>().ToList();
                    var totalRecords = multi.Read<int>().FirstOrDefault();
                    ViewBag.TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
                    ViewBag.CurrentPage = pageNumber;
                    return View(employees);
                }
            }
        }

        public ActionResult EmpAdd()
        {
            using (var con = new SqlConnection(rp.connectionString))
            {
                con.Open();
                string sql = "select* from Departments";
                var dep=con.Query<EmployeeModel>(sql);
                ViewBag.dept = dep;

            }
            return View();
        }
        [HttpPost]
        public ActionResult EmpAdd(EmployeeModel emp)
        {
            
                rp.add(emp);
                return RedirectToAction("EmpShow");
           
        }

        public ActionResult EmpView(int id)
        {
            return View(rp.details(id));
        }

        public ActionResult EmpDelDetail(int id)
        {
            return View(rp.deletedDetail(id));
        }

        public ActionResult EmpDelete(int id)
        {
            rp.delete(id);
            return RedirectToAction("EmpShow");
        }
        public ActionResult EmpRestore(int id)
        {
            rp.restore(id);
            return RedirectToAction("EmpBackup");
        }

        public ActionResult EmpEdit(int id)
        {
            var employee=rp.details(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            using (var con = new SqlConnection(rp.connectionString))
            {
                con.Open();
                string sql = "select* from Departments";
                var dep = con.Query<EmployeeModel>(sql);
                ViewBag.dept = dep;

            }
            return View(employee);
        }

        [HttpPost]
        public JsonResult EmpEdit(EmployeeModel emp)
        {
            try
            {
                rp.update(emp);
                return Json(new { success = true,message="Employee Details edited successfully"});
                
            }
            catch (Exception ex) {
                return Json(new { success = false,message=ex.Message });
            }
        }
    }
}