using MediatR;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Common.Helpers;
using StayCation.API.VerticalSlicing.Data.Repositories;

namespace StayCation.API.VerticalSlicing.Features.Recipe.GetRecipeById.Queries
{
    public record GetAllProductQuery() : IRequest<ResultDTO>;
    public class GetAllProductQueryHandler : BaseRequestHandler<Data.Models.Product, GetAllProductQuery, ResultDTO>
    {
        public GetAllProductQueryHandler(RequestParameters<Data.Models.Product> requestParameters ) : base(requestParameters)
        {
        }

        public override async Task<ResultDTO> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return ResultDTO.Failure("Invalid RecipeID!");
            }

            var recipe =  _repository.GetAll();
            return ResultDTO.Success(recipe.ToList());
        }
    }
}
