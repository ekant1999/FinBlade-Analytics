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

namespace api.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDBContext _context;

    public StockRepository(ApplicationDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StockDto>> GetAllStocksAsync()
    {
        var stocks =await  _context.Stocks.Include(c=>c.Comments).AsNoTracking().ToListAsync();
        var stockDtos = stocks.Select(s => s.TostockDto());
        return stockDtos;
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

        _context.Stocks.Update(existingStock);
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

}