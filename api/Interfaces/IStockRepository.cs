using System.Collections.Generic;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Helper;
using api.Modles;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockDto>> GetAllStocksAsync(QueryObject queryObject);
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock?> CreateStockAsync(Stock stock);
        Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stock);
        Task<Stock?> DeleteStockAsync(int id);

        Task<bool> StockExistsAsync(int id);
    }
}
