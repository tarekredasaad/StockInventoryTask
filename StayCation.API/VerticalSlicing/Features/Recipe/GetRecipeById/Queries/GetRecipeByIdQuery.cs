using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Repositories;

namespace StayCation.API.VerticalSlicing.Features.Recipe.GetRecipeById.Queries
{
    public record GetProductByIdQuery(int id) : IRequest<ResultDTO>;
    public class GetRecipeByIdQueryHandler : BaseRequestHandler<Data.Models.Recipe, GetProductByIdQuery, ResultDTO>
    {
        public GetRecipeByIdQueryHandler(RequestParameters<Data.Models.Recipe> requestParameters ) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultDTO.Failure("Invalid RecipeID!");
            }

            var recipe = await _repository.GetByIDAsync(request.id);
            var mappedRecipe = recipe.MapOne<RecipeReturnDTO>();
            return ResultDTO.Success(mappedRecipe);
        }
    }
}
