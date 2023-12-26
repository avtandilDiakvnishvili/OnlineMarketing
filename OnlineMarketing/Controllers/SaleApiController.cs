using BusinessLogic.IRepositories;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace OnlineMarketing.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class SaleApiController : ControllerBase
    {
        private readonly ISaleRepository _sale;

        public SaleApiController(ISaleRepository sale)
        {
            _sale = sale;
        }

        [HttpGet("get_all_sales")]
        public async Task<IActionResult> GetSales()
        {

            var result = await _sale.GetAllSalesAsync();
            return Ok(result);
        }

        [HttpGet("get_sale_by_id")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var result = await _sale.GetSaleById(id);
            if (result.Id > 0)
            {
                return Ok(result);
            }
            return NotFound($"გაყიდვა გადაცემული id:{id} _ით ვერ მოიძებნა");
        }

        [HttpGet("search_sale")]
        public async Task<IActionResult> SearchSale(string param)
        {
            var result = await _sale.SearchSalesByNameOrCode(param);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound($"გაყიდვები გადაცემული param:'{param}' _ით ვერ მოიძებნა");
        }


        [HttpPost("add_sale")]
        public async Task<IActionResult> AddSale([FromBody] SaleViewModel model)
        {
            var result = await _sale.AddSale(model);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("update_sale")]
        public async Task<IActionResult> UpdateSale([FromBody] SaleViewModel model)
        {
            var result = await _sale.UpdateSale(model);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("remove_sale")]
        public async Task<IActionResult> RemoveSale([FromBody] IdName model)
        {
            var result = await _sale.DeleteSale(model.Id);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
