using MediatR;
using Serilog;
using StayCation.API.VerticalSlicing.Features.Transaction.GetTransactionsYear.Queries;

namespace StayCation.API.VerticalSlicing.Features.Transaction
{
    public class BackgroundTransactionService
    {
        IServiceProvider serviceProvider;
        public BackgroundTransactionService(
            IServiceProvider serviceProvider)
        {

           
            this.serviceProvider = serviceProvider;
        }

        public async Task ArchivesTransactions()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                var transactions = await mediator.Send(new GetTransactionsYearlyQuery());
                if (transactions.Any())
                {
                    foreach (var transaction in transactions)
                    {
                        Log.Information($" store this transaction   {transaction.Id.ToString()}");
                    }
                }
            }
        }
    }
}
