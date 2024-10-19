using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Features.Recipe.GetRecipeById.Queries;

namespace StayCation.API.VerticalSlicing.Features.Product.GetAllProduct
{
    public class GetAllProductController : BaseController
    {
        public GetAllProductController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }
        [HttpPost]
        public async Task<ResultDTO> GetAllProduct()
        {
            var result = await _mediator.Send(new GetAllProductQuery());

            return ResultDTO.Success(result.Data);
        }
    }
}
