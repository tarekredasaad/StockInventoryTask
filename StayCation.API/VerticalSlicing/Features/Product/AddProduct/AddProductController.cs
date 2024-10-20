using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
using StayCation.API.VerticalSlicing.Features.Product.AddProduct.Commands;

namespace StayCation.API.VerticalSlicing.Features.Product.AddProduct
{
    public class AddProductController : BaseController
    {
        public AddProductController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        public async Task<ResultDTO> AddProduct(AddProductDTO addProductDTO)
        {
            var result = await _mediator.Send(new AddProductCommand(addProductDTO));

            return ResultDTO.Success(result.Data);
        }

    }
}
