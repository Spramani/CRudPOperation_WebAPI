using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MvcApiDemo.Models;
using System.Net.Http.Headers;

namespace MvcApiDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private HttpClient employeeApi = new HttpClient();

        public EmployeeController() {
            employeeApi.BaseAddress = new Uri("http://localhost:9010");
        }

        // GET: Employee
        public ActionResult Index()
        {
            MediaTypeWithQualityHeaderValue contenType = new MediaTypeWithQualityHeaderValue("aplpication/Json");
            employeeApi.DefaultRequestHeaders.Accept.Add(contenType);
            HttpResponseMessage apiresponse = employeeApi.GetAsync("Api/Employee").Result;
            string respondsdata = apiresponse.Content.ReadAsStringAsync().Result;
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(respondsdata);

            return View(employeeList.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage apiresponse = employeeApi.GetAsync("/Api/Employee/" + id).Result;
            string userdata = apiresponse.Content.ReadAsStringAsync().Result;
            Employee data = JsonConvert.DeserializeObject<Employee>(userdata);

            return View(data);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee newuser)
        {
            try
            {
                // TODO: Add insert logic here

                string newuserdata = JsonConvert.SerializeObject(newuser);
                var userdata = new StringContent(newuserdata, System.Text.Encoding.UTF8, "application/Json");
                HttpResponseMessage apiresponse = employeeApi.PostAsync("/Api/Employee", userdata).Result;
                ViewBag.message = apiresponse.Content.ReadAsStringAsync().Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage apiresponse = employeeApi.GetAsync("/Api/Employee/" + id).Result;
            string userdata = apiresponse.Content.ReadAsStringAsync().Result;
            Employee usertoedit = JsonConvert.DeserializeObject<Employee>(userdata);

            return View(usertoedit);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee useredit)
        {
            try
            {
                // TODO: Add update logic here
                string newuserdata = JsonConvert.SerializeObject(useredit);
                var userdata = new StringContent(newuserdata, System.Text.Encoding.UTF8, "application/Json");
                HttpResponseMessage apiresponse = employeeApi.PutAsync("/Api/Employee/" + useredit.id, userdata).Result;
                ViewBag.message = apiresponse.Content.ReadAsStringAsync().Result;
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage apiresponse = employeeApi.GetAsync("/Api/Employee/" + id).Result;
            string userdata = apiresponse.Content.ReadAsStringAsync().Result;
            Employee userToDelete = JsonConvert.DeserializeObject<Employee>(userdata);

            return View(userToDelete);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                HttpResponseMessage apiresponse = employeeApi.DeleteAsync("/Api/Employee/" + id).Result;
                ViewBag.message = apiresponse.Content.ReadAsStringAsync().Result;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}