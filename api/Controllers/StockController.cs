using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Modles;
using Project_Course.Mappers;

namespace YourNamespace.Controllers
{
    public class StockController : Controller
    {
       private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetStocks()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.TostockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStock(int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.TostockDto());
        }
        [HttpPost]
        public IActionResult AddStock([FromBody] Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return Ok(stock.TostockDto());
        }
    }
}
