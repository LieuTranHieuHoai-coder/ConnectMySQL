using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ConnectMySQL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Dapper;

namespace ConnectMySQL.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var db = new Business.ProblemBusiness();
                var list = await db.GetList();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetId(int id)
        {
            try
            {
                var db = new Business.ProblemBusiness();
                var v = await db.GetId(id);
                return Ok(v);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Problem problem)
        {
            try
            {
                var db = new Business.ProblemBusiness();
                var result = await db.Insert(problem);
                if (result > 0)
                {
                    return Ok();
                }
                return NotFound("Duplicated data.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Update([FromBody] Problem problem)
        {
            try
            {
                var db = new Business.ProblemBusiness();
                var result = db.Update(problem);
                if (result > 0)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var db = new Business.ProblemBusiness();
                if (db.Delete(id) > 0)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
