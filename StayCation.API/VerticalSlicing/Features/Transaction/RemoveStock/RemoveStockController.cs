using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
//using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe.Commands;
//using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe;
using StayCation.API.VerticalSlicing.Features.Product.AddProduct.Commands;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock;
using StayCation.API.VerticalSlicing.Transaction.AddStock.Commands;
using StayCation.API.VerticalSlicing.Transaction.RemoveStock.Commands;
using StayCation.API.VerticalSlicing.Features.Transaction.RemoveStock.Commands;

namespace StayCation.API.VerticalSlicing.Features.Transaction.RemoveStock
{
    public class RemoveStockController : BaseController
    {
        public RemoveStockController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        public async Task<ResultDTO> RemoveStock(StockDTO addProductDTO)
        {
            var result = await _mediator.Send(new RemoveStockOrchateratorCommand(addProductDTO));

            return ResultDTO.Success(result);
        }

    }
}
