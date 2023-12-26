using BusinessLogic.IRepositories;
using BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineMarketing.Helper;

namespace OnlineMarketing.Controllers
{
    [Route("api/distributor")]
    [ApiController]
    public class DistributorApiController : ControllerBase
    {
        private readonly IDistributorRepository _distributor;
        private readonly IWebHostEnvironment _env;

        public DistributorApiController(IDistributorRepository distributor, IWebHostEnvironment env)
        {
            _distributor = distributor;
            _env = env;
        }


        [HttpGet("get_all_distributor")]
        public async Task<IActionResult> GetDistributors()
        {

            var result = await _distributor.GetAllDistributorsAsync();
            if (result.Count > 0)
                return Ok(result);
            return NotFound("დისტრიბუტორები ვერ მოიძებნა");
        }

        [HttpGet("get_all_posible_recomendator")]
        public async Task<IActionResult> GetAllPosibileDistributorsForRecomendationAsync()
        {

            var result = await _distributor.GetAllPosibileDistributorsForRecomendationAsync();
            if (result.Count > 0)
                return Ok(result);
            return NotFound("შესაძლო რეკომენდატორი დისტრიბუტორები ვერ მოიძებნა");
        }

        [HttpGet("get_distributor_by_id")]
        public async Task<IActionResult> GetDistributorById(int id)
        {
            var result = await _distributor.GetDistributorById(id);
            if (result.Id > 0)
            {
                return Ok(result);
            }
            return NotFound($"დისტრიბუტორი გადაცემული id:{id} _ით ვერ მოიძებნა");
        }

        [HttpGet("search_distributor")]
        public async Task<IActionResult> SearchDistributor(string param)
        {
            var result = await _distributor.SearchDistributorsByName(param);
            if (result.Count > 0)
            {
                return Ok(result);
            }
            return NotFound($"დისტრიბუტორი გადაცემული param:'{param}' _ით ვერ მოიძებნა");
        }


        [HttpPost("add_distributor")]
        public async Task<IActionResult> AddDistributor([FromBody] DistributorViewModel model)
        {
            if (model.ImgByte != null)
            {
                model.ImgPath = ImageHelper.SaveDistributorImage(model.ImgByte, _env);
            }
            var result = await _distributor.AddDistributor(model);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("update_distributor")]
        public async Task<IActionResult> UpdateDistributor([FromBody] DistributorViewModel model)
        {
            if (!string.IsNullOrEmpty(model.ImgPath))
            {
                ImageHelper.DeleteDistributorImage(_env, model.ImgPath);
            }
            if (model.ImgByte != null)
            {
                model.ImgPath = ImageHelper.SaveDistributorImage(model.ImgByte, _env);
            }
            var result = await _distributor.UpdateDistributor(model);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("remove_distributor")]
        public async Task<IActionResult> RemoveDistributor([FromBody] IdName model)
        {
            var result = await _distributor.DeleteDistributor(model.Id);
            if (result.Status)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("get_children")]
        public async Task<IActionResult> GetChildren(int id)
        {
            var result = await _distributor.GetChildrens(id);
            if (result.Any())
                return Ok(result);
            return BadRequest("კონკრეტული დისტრიბუტორის რეკომენდირებული დისტრიბუტორები ვერ მოიძებნა");
        }


        [HttpGet("get_distributors_for_selection")]
        public async Task<IActionResult> GetDistributorsForSelection()
        {
            var result = await _distributor.GetDistributorsForSelection();
            if (result.Count > 0)
                return Ok(result);
            return BadRequest("კონკრეტული დისტრიბუტორის რეკომენდირებული დისტრიბუტორები ვერ მოიძებნა");
        }

        
        [HttpPost("calculate_distributor_bonus")]
        public async Task<IActionResult> CalculateDistributorBonus([FromBody] RecuestViewModel model)
        {
            var result = await _distributor.CalculateDistributorBonus(model.StartDate, model.EndDate, new List<int>());
            if (result.Any())
                return Ok(result);
            return BadRequest();
        }


        [HttpPost("get_distributors_bonus")]
        public async Task<IActionResult> GetDistributorBonuses([FromBody] RecuestViewModel model)
        {
            var result = await _distributor.GetDistributorBonuses(model.StartDate, model.EndDate);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

    }
}
