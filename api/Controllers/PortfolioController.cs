using System.IO;
using api.Data;
using api.extensions;
using api.Interfaces;
using api.Modles;
using api.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YourNamespace;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDBContext _context;
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly IStockRepository _stockRepository;
        public PortfolioController(ApplicationDBContext dBContext,
                        UserManager<AppUser> userManager, IPortfolioRepository portfolioRepository,
                        IStockRepository stockRepository)
        {
            _context = dBContext;
            _userManager = userManager;
            _portfolioRepo = portfolioRepository;
            _stockRepository= stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPortfolios()
        {
           if(!ModelState.IsValid)
           {
            return BadRequest(ModelState);
           }
           var userName = User.GetUserName();
           var appUser = await _userManager.FindByNameAsync(userName);
           var portfolioStock = await _portfolioRepo.GetPortfolioAsync(appUser);
            return Ok(portfolioStock);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePortfolios(string symbol)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepository.GetStockBySymbolAsync(symbol);
            var userPortfolio = await _portfolioRepo.GetPortfolioAsync(appUser);
            if(stock == null)
            {
                return BadRequest("Stock not found");
            }
            if(userPortfolio.Any(a=>a.Symbol.ToLower() == symbol.ToLower() ))
            {
                return BadRequest("Stock already exists");
            }

            var portfolioModel = new Portfolio();
            portfolioModel.AppUserId= appUser.Id;
            portfolioModel.StockId = stock.Id;

            var portfolioStock = await _portfolioRepo.CreatePortfolioAsync(portfolioModel);
            if(portfolioStock == null)
            {
                 return BadRequest("Portfolio was not created");
            }

            return Ok("Portfolio created successfully");
        }
    
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolios(string symbol)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userName = User.GetUserName();
            var appUser = await _userManager.FindByNameAsync(userName);  
            var portfolio = await _portfolioRepo.DeletePortfoliosAsync(appUser,symbol);

            if(portfolio == null)
            {
                return StatusCode(500, "Portfolio do not exists");
            }
            return Ok("Portfolio was deleted successfully");
        }
    }
}