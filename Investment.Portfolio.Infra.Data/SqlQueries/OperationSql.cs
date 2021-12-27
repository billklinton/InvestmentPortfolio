
namespace Investment.Portfolio.Infra.Data.SqlQueries
{
    public static class OperationSql
    {
        public static string Insert = @"insert into Operation(
											Id,
											StockId,
											Price,
											Quantity,
											OperationDate,
											TotalAmount,
											OperationType)
										values(@Id,
											   @StockId,
											   @Price,
											   @Quantity,
											   @OperationDate,
											   @TotalAmount,
											   @OperationType)";

		public static string GetOperationsByStockId = @"select * from Operation where StockId = @StockId";
	}
}
