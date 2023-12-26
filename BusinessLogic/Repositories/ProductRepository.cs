using AutoMapper;
using BusinessLogic.IRepositories;
using BusinessLogic.ViewModels;
using DataAccess.DbContexts;
using DataAccess.Enums;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using OnlineMarketing.ViewModels;


namespace BusinessLogic.Repositories
{
    /// <summary>
    /// პროდუქციის რეგისტრაციის სამართავი კლასი
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ResultViewModel> AddProduct(ProductViewModel model)
        {
            try
            {
                Product product = _mapper.Map<Product>(model);
                await _dbContext.Products.AddAsync(product);
                var result = await _dbContext.SaveChangesAsync();
                return new ResultViewModel()
                {
                    Id = result,
                    Message = "პროდუქტი წარმატებით დაემატა",
                    Status = true
                };
            }
            catch (Exception ex)
            {

                return new ResultViewModel()
                {
                    Id = -1,
                    Message = "პროდუქტი ვერ დაემატა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }

        public async Task<ResultViewModel> UpdateProduct(ProductViewModel model)
        {
            try
            {
                Product product = _mapper.Map<Product>(model);
                _dbContext.Products.Update(product);
                var result = await _dbContext.SaveChangesAsync();
                return new ResultViewModel()
                {
                    Id = result,
                    Message = "პროდუქტი წარმატებით  განახლდა",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel()
                {
                    Id = -1,
                    Message = "პროდუქტი ვერ განახლდა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }


        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            return await _dbContext.Products.Select(f => _mapper.Map<ProductViewModel>(f)).ToListAsync();
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {


            var product = await _dbContext.Products.FirstOrDefaultAsync(f => f.Id == id);
            if (product != null)
            {
                return _mapper.Map<ProductViewModel>(product);
            }

            return new ProductViewModel() { Id = -1 };

        }

        public async Task<ResultViewModel> DeleteProduct(int id)
        {
            try
            {
                var result = await _dbContext.Products.Where(f => f.Id == id).ExecuteDeleteAsync();
                return new ResultViewModel()
                {
                    Id = result,
                    Message = "პროდუქტი წარმატებით წაიშალა",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Id = -1,
                    Message = "პროდუქტი ვერ წაიშალა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }

        public async Task<List<ProductViewModel>> SearchProductsByNameOrCode(string param)
        {
            return await _dbContext.Products.Where(f => f.Name.StartsWith(param) || f.Code.StartsWith(param)).Select(f => _mapper.Map<ProductViewModel>(f)).Take(20).ToListAsync();
        }

        public async Task<List<IdName>> GetProductsForSelection()
        {
            return await _dbContext.Products.Select(f => new IdName { Id = f.Id, Name = f.Name }).ToListAsync();
        }




        //public Task<ResultViewModel> RemovAllProduct()
        //{
        //    throw new NotImplementedException();
        ////}


    }
}
