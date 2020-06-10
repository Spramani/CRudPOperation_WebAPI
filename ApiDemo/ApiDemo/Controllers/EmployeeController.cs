using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public static List<Employee> employees = new List<Employee>();
        // GET: api/Employee
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return employees.ToList();
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(employees.SingleOrDefault(e => e.id == id));
        }

        // POST: api/Employee
        [HttpPost]
        public void Post([FromBody] Employee newemployee)
        {
            if (newemployee == null)
            {
                newemployee  = new Employee();
                newemployee.id = 1;
                newemployee.Name = "shubh";
                newemployee.address = "surat";
            }
            employees.Add(newemployee);
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Employee updateemployee)
        {
            Employee employeetoupdate = employees.SingleOrDefault(e => e.id == id);
            employeetoupdate.Name = updateemployee.Name;
            employeetoupdate.address = updateemployee.address;

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Employee employeetodelete = employees.SingleOrDefault(e => e.id == id);
            employees.Remove(employeetodelete);   
        }
    }
}
