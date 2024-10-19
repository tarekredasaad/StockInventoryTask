using Microsoft.AspNetCore.Mvc;
using StayCation.API.VerticalSlicing.Common;
using StayCation.API.VerticalSlicing.Common.DTOs;
//using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe.Commands;
//using StayCation.API.VerticalSlicing.Features.Recipe.Add_Recipe;
using StayCation.API.VerticalSlicing.Features.Product.AddProduct.Commands;
using StayCation.API.VerticalSlicing.Features.Transaction.AddStock;
using StayCation.API.VerticalSlicing.Transaction.AddStock.Commands;

namespace StayCation.API.VerticalSlicing.Features.Transaction.TransferStock
{
    public class TransferStockController : BaseController
    {
        public TransferStockController(ControllereParameters controllereParameters) : base(controllereParameters)
        {
        }

        [HttpPost]
        public async Task<ResultDTO> TransferProduct(StockDTO addProductDTO)
        {
            var result = await _mediator.Send(new AddStockCommand(addProductDTO));

            return ResultDTO.Success(result);
        }

    }
}
