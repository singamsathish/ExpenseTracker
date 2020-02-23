using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Data;

namespace ExpenseTracker.Controllers
{
    [Route("api/ExpenseModels")]
    [ApiController]
    public class ExpenseModelsController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpenseModelsController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: api/ExpenseModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseModel>>> GetExpenseTable()
        {
            return await _context.ExpenseTable.ToListAsync();
        }

        // GET: api/ExpenseModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseModel>> GetExpenseModel(int id)
        {
            var expenseModel = await _context.ExpenseTable.FindAsync(id);

            if (expenseModel == null)
            {
                return NotFound();
            }

            return expenseModel;
        }

        // PUT: api/ExpenseModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenseModel(int id, ExpenseModel expenseModel)
        {
            if (id != expenseModel.id)
            {
                return BadRequest();
            }

            _context.Entry(expenseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ExpenseModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ExpenseModel>> PostExpenseModel(ExpenseModel expenseModel)
        {
            _context.ExpenseTable.Add(expenseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenseModel", new { id = expenseModel.id }, expenseModel);
        }

        // DELETE: api/ExpenseModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ExpenseModel>> DeleteExpenseModel(int id)
        {
            var expenseModel = await _context.ExpenseTable.FindAsync(id);
            if (expenseModel == null)
            {
                return NotFound();
            }

            _context.ExpenseTable.Remove(expenseModel);
            await _context.SaveChangesAsync();

            return expenseModel;
        }

        private bool ExpenseModelExists(int id)
        {
            return _context.ExpenseTable.Any(e => e.id == id);
        }
    }
}
