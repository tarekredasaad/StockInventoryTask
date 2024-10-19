using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Features.Recipe.GetRecipeById.Queries;

namespace StayCation.API.VerticalSlicing.Features.Recipe.GetRecipeById
{
    public class GetProductByIdController : BaseController
    {
        public GetProductByIdController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }
        [HttpPost]
        public async Task<ResultDTO> GetRecipeById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id));

            return ResultDTO.Success(result.Data);
        }
    }
}
