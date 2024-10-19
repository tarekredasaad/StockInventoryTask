using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
//using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe.Commands;
//using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe;
using StayCation.API.VerticalSlicing.Features.Product.AddProduct.Commands;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock;
using StayCation.API.VerticalSlicing.Transaction.AddStock.Commands;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock.Commands;

namespace StayCation.API.VerticalSlicing.Features.Transaction.AddStock
{
    public class AddStockController : BaseController
    {
        public AddStockController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        public async Task<ResultDTO> AddStock(StockDTO addProductDTO)
        {
            var result = await _mediator.Send(new AddStockOrchateratorCommand(addProductDTO));

            return ResultDTO.Success(result);
        }

    }
}
