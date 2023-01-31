using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Interfaces.Query;
using Sprout.Exam.Business.Interfaces.Command;
using System.Threading;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IGetEmployeesQuery getEmployeesQuery;
        private readonly IGetEmployeeByIdQuery getEmployeeByIdQuery;
        private readonly IUpdateEmployeeCommand updateEmployeeCommand;
        private readonly IAddEmployeeCommand addEmployeeCommand;
        private readonly IDeleteEmployeeCommand deleteEmployeeCommand;
        private readonly ICalculateSalaryCommand calculateSalaryCommand;

        public EmployeesController(IGetEmployeesQuery getEmployeesQuery,
            IGetEmployeeByIdQuery getEmployeeByIdQuery,
            IUpdateEmployeeCommand updateEmployeeCommand,
            IAddEmployeeCommand addEmployeeCommand,
            IDeleteEmployeeCommand deleteEmployeeCommand,
            ICalculateSalaryCommand calculateSalaryCommand)
        {
            this.getEmployeesQuery = getEmployeesQuery;
            this.getEmployeeByIdQuery = getEmployeeByIdQuery;
            this.updateEmployeeCommand = updateEmployeeCommand;
            this.addEmployeeCommand = addEmployeeCommand;
            this.deleteEmployeeCommand = deleteEmployeeCommand;
            this.calculateSalaryCommand = calculateSalaryCommand;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await getEmployeesQuery.ExecuteAsync(string.Empty, CancellationToken.None);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await getEmployeeByIdQuery.ExecuteAsync(id, CancellationToken.None);
            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditEmployeeDto input)
        {
            input.Id = id;
            var item = await updateEmployeeCommand.ExecuteAsync(input, CancellationToken.None);

            return Ok(item);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var id = await addEmployeeCommand.ExecuteAsync(input, CancellationToken.None);
            return Created($"/api/employees/{id}", id);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await deleteEmployeeCommand.ExecuteAsync(id, CancellationToken.None);
            return Ok(id);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id">The id of the employee.</param>
        /// <param name="data">Contains the absent days and worked days.</param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, CalculateSalaryDto data)
        {
            var result = await calculateSalaryCommand.ExecuteAsync(new SalaryCalculatorParameter(id, data.AbsentDays, data.WorkedDays), CancellationToken.None);
            return Ok(result);
        }
    }
}
