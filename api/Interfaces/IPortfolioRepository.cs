using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helper;
using api.Modles;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<StockDto>> GetPortfolioAsync(AppUser appUser); 

        Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);

       Task<Portfolio?> DeletePortfoliosAsync(AppUser appUser, string symbol);
    }
}