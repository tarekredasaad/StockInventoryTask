using Hangfire;
using MediatR;
using Serilog;
using StayCation.API.VerticalSlicing.Features.Product.CheckLowStock.Queries;
using StayCation.API.VerticalSlicing.Features.Transaction.GetTransactionsYear.Queries;

namespace StayCation.API.VerticalSlicing.Features.Product.LogLowStock.LogStockOrchastratorCommand
{
    public class LogLowStockService
    {
        IServiceProvider serviceProvider;
        public LogLowStockService( 
            IServiceProvider serviceProvider)
        {
           
            //RecurringJob.AddOrUpdate<LogLowStockService>(
            //"low-stock-job",
            //service => service.LogLowStock(),
            //Cron.Daily);
            this.serviceProvider = serviceProvider;
        }

        public async Task LogLowStock()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var LowStocks = await mediator.Send(new CheckLowStockQuery());
                var lowStocksResult = LowStocks;
                if (LowStocks.Any())
                {
                    foreach (var lowStock in LowStocks)
                    {
                        Log.Information($" you have to restock this product which has Id  {lowStock.Id.ToString()}");
                    }
                }
            }
        }

        public async Task ArchivesTransactions()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var transactions = await mediator.Send(new GetTransactionsYearlyQuery());
            }
        }

    }
}
