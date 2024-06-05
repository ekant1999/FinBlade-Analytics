using System.Collections.Generic;
using System.Threading.Tasks;
using api.Modles;
using api.Interfaces;
using api.Data;
using Microsoft.AspNetCore.Mvc.Routing;
using api.Dtos.Stock;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using api.Mappers;
using api.Helper;

namespace api.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;

    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StockDto>> GetAllStocksAsync(QueryObject queryObject)
    {

        var stocks = _context.Stocks.Include(c=>c.Comments).ThenInclude(a=>a.AppUser).AsQueryable();
        if (!string.IsNullOrWhiteSpace(queryObject.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
        }
        if (!string.IsNullOrWhiteSpace(queryObject.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(queryObject.Symbol));
        }
        if(!string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            if(queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = queryObject.IsSortDescending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);
            }
        }
        var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;

        var stock =await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        return stock.Select(s => s.TostockDto());
    }

    public async Task<Stock?> GetStockByIdAsync(int id)
    {
        return await  _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(s => s.Id == id);
    } 

    public async Task<Stock?> CreateStockAsync(Stock stock)
    {
       await _context.Stocks.AddAsync(stock);
       await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock?> UpdateStockAsync(int id , UpdateStockRequestDto stock)
    {
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        if (existingStock == null)
        {
            return null;
        }

        existingStock.Symbol = stock.Symbol;
        existingStock.CompanyName = stock.CompanyName;
        existingStock.Purchase = stock.Purchase;
        existingStock.LastDiv = stock.LastDiv;
        existingStock.Industry = stock.Industry;
        existingStock.MarketCap = stock.MarketCap;

        await _context.SaveChangesAsync();
        return existingStock;
    }

    public async Task<Stock?> DeleteStockAsync(int id)
    {
        var stockModel =await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

        if (stockModel == null)
        {
            return null;
        }
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<bool> StockExistsAsync(int id)
    {
        return await _context.Stocks.AnyAsync(s => s.Id == id);
    }

    public async Task<Stock?> GetStockBySymbolAsync(string symbol)
    {
        return await  _context.Stocks.FirstOrDefaultAsync(s => s.Symbol.ToLower() == symbol.ToLower());
    } 

}