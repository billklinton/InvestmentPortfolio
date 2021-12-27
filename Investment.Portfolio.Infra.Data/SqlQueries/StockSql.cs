
namespace Investment.Portfolio.Infra.Data.SqlQueries
{
    public static class StockSql
    {
        public static string Insert = "Insert into Stock (Id, Ticker, CorporateName) values (@id, @Ticker, @CorporateName)";
        public static string GetByTicker = "select * from Stock where lower(Ticker) = lower(@Ticker)";
    }
}
