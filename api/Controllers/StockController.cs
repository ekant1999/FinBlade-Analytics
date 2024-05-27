using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Modles;
using api.Mappers;
using api.Dtos.Stock;
using System.Linq;
using System.Threading.Tasks;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStocks()
        {
            var stocks = await _context.Stocks
                                       .AsNoTracking()
                                       .ToListAsync();
            var stockDtos = stocks.Select(s => s.TostockDto());
            return Ok(stockDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStock(int id)
        {
            var stock = await _context.Stocks
                                      .AsNoTracking()
                                      .FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.TostockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockModel = stockDto.ToStockFromStockDto();
            if (stockModel == null)
            {
                return BadRequest("Invalid stock data.");
            }

            _context.Stocks.Add(stockModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStock), new { id = stockModel.Id }, stockModel.TostockDto());
        }
    }
}
