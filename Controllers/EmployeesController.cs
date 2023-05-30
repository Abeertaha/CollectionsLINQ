using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Collections.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private static readonly Dictionary<string, string> employees = new Dictionary<string, string>()
    {
        {"Ali", "Ali"},
        {"Julia", "Julia"},
        {"Adam", "Adam"},
        {"Lily", "Lily"},
    };

    private static readonly List<string> employeeList = new List<string>(employees.Values);

    [HttpGet("{key}")]
    public ActionResult<string> GetDictionaryValue(string key)
    {
        if (employees.ContainsKey(key))
        {
            return employees[key];
        }
        return NotFound();
    }

    [HttpGet]
        [Route("GetEmployees")]
        public ActionResult<List<string>> GetEmployees()
        {
            return Ok(employeeList);
        }

        [HttpPost]
        public IActionResult AddEmployee(string key, string value)
        {
            if (!employees.ContainsKey(key))
            {
                employees.Add(key, value);
                employeeList.Add(value);
                return Ok();
            }
            return Conflict("Already Exists");
        }


        [HttpGet]
        [Route("GetAllEmployeesStartingWith/{prefix}")]
        public ActionResult<List<string>> GetAllEmployeesStartingWith(string prefix)
                {
                    var filteredEmployees = employees.Values.Where(a => a.StartsWith(prefix)).ToList(); //(=>) Lambda expression 
                    return Ok(filteredEmployees);
                }

        [HttpGet]
        [Route("GetEmployeesCount")]
        public ActionResult<int> GetEmployeesCount()
        {
            return Ok(employeeList.Count);
        }

        [HttpGet]
        [Route("IsEmployeeFound/{key}")]
        public IActionResult IsEmployeeFound(string key)
        {
            return Ok(employees.ContainsKey(key));
        }

}