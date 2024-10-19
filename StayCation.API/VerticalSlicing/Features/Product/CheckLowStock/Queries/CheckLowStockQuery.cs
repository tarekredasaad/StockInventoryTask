using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Repositories;

namespace StayCation.API.VerticalSlicing.Features.Product.CheckLowStock.Queries
{
    public record CheckLowStockQuery() : IRequest<List<Data.Models.Product>>;
    public class CheckLowStockQueryHandler : BaseRequestHandler<Data.Models.Product, CheckLowStockQuery, List<Data.Models.Product>>
    {
        public CheckLowStockQueryHandler(RequestParameters<Data.Models.Product> requestParameters ) : base(requestParameters)
        {
        }

        public override async Task<List<Data.Models.Product>> Handle(CheckLowStockQuery request, CancellationToken cancellationToken)
        {
            

            var Stock =  _repository.GetAll();
            var result = Stock.Where(s => s.LowStockThreshold > s.Quantity).ToList();
            //var mappedRecipe = recipe.MapOne<RecipeReturnDTO>();
            return (result);
        }
    }
}
