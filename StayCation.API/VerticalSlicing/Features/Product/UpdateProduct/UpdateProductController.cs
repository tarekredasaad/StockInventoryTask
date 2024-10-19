using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe.Commands;
using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe;
using StayCation.API.VerticalSlicing.Features.Product.AddProduct.Commands;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct;
using StayCation.API.VerticalSlicing.Features.Product.UpdateProduct.Commands;

namespace StayCation.API.VerticalSlicing.Features.Product.AddProduct
{
    public class UpdateProductController : BaseController
    {
        public UpdateProductController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        public async Task<ResultDTO> UpdateProduct(UpdateProductDTO addProductDTO)
        {
            var result = await _mediator.Send(new UpdateProductCommand(addProductDTO));

            return ResultDTO.Success(result.Data);
        }

    }
}
