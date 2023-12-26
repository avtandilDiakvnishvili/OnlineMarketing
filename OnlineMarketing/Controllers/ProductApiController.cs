using BusinessLogic.IRepositories;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketing.ViewModels;

namespace OnlineMarketing.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IProductRepository _product;

        public ProductApiController(IProductRepository product)
        {
            _product = product;
        }

        [HttpGet("get_all_products")]
        public async Task<IActionResult> GetProducts()
        {

            var result = await _product.GetAllProductsAsync();
            return Ok(result);
        }

        [HttpGet("get_product_by_id")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _product.GetProductById(id);
            if (result.Id > 0)
            {
                return Ok(result);
            }
            return NotFound($"პროდუქტი გადაცემული id:{id} _ით ვერ მოიძებნა");
        }

        [HttpGet("search_product")]
        public async Task<IActionResult> SearchProduct(string param)
        {
            var result = await _product.SearchProductsByNameOrCode(param);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound($"პროდუქტი გადაცემული param:'{param}' _ით ვერ მოიძებნა");
        }


        [HttpPost("add_product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductViewModel model)
        {
            var result = await _product.AddProduct(model);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("update_product")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel model)
        {
            var result = await _product.UpdateProduct(model);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("remove_product")]
        public async Task<IActionResult> RemoveProduct([FromBody] IdName model)
        {
            var result = await _product.DeleteProduct(model.Id);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("get_products_for_selection")]
        public async Task<IActionResult> GetProductsForSelection()
        {
            var result = await _product.GetProductsForSelection();
            if (result.Count > 0)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
