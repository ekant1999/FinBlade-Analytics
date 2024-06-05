using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helper;
using api.Interfaces;
using api.Mappers;
using api.Modles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<StockDto>> GetPortfolioAsync(AppUser appUser)
        {
           var stocks = await _context.Portfolios.Where(a=>a.AppUserId == appUser.Id)
           .Select(b=> b.Stock.TostockDto()).ToListAsync();

            return stocks;
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {   
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfoliosAsync(AppUser appUser, string symbol)
        {
            var portfolioModel = await _context.Portfolios.FirstOrDefaultAsync(a=>a.AppUserId== appUser.Id
                                                                 && a.Stock.Symbol.ToLower() == symbol.ToLower());
             if(portfolioModel == null)
             {
                return null;
             }
             _context.Portfolios.Remove(portfolioModel);
             await _context.SaveChangesAsync();
             return portfolioModel;
        }
    }
}