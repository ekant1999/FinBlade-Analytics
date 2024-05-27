using api.Modles;
using Project_Course.Api.Dtos.Stock;

namespace Project_Course.Mappers
{
    public static class StockMapper
    {
        public static StockDto TostockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
            };
        }
    }
}