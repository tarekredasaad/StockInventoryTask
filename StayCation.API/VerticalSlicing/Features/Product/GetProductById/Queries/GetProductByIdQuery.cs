using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Repositories;
using StayCation.API.VerticalSlicing.Features.Recipe.GetRecipeById;

namespace StayCation.API.VerticalSlicing.Features.Product.GetProductById.Queries
{
    public record GetProductByIdQuery(int id) : IRequest<ResultDTO>;
    public class GetProductByIdQueryHandler : BaseRequestHandler<Data.Models.Product, GetProductByIdQuery, ResultDTO>
    {
        public GetProductByIdQueryHandler(RequestParameters<Data.Models.Product> requestParameters ) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultDTO.Failure("Invalid RecipeID!");
            }

            var recipe = await _repository.GetByIDAsync(request.id);
            var mappedRecipe = recipe.MapOne<ProductReturnDTO>();
            return ResultDTO.Success(mappedRecipe);
        }
    }
}
