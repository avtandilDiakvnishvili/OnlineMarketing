using AutoMapper;
using BusinessLogic.ViewModels;
using DataAccess.DbContexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using OnlineMarketing.ViewModels;

namespace BusinessLogic.IRepositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public SaleRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }




        public async Task<ResultViewModel> AddSale(SaleViewModel model)
        {
            try
            {
                Sale Sale = _mapper.Map<Sale>(model);
                await _dbContext.Sales.AddAsync(Sale);
                var result = await _dbContext.SaveChangesAsync();
                return new ResultViewModel()
                {
                    Id = result,
                    Message = "გაყიდვა წარმატებით დაემატა",
                    Status = true
                };
            }
            catch (Exception ex)
            {

                return new ResultViewModel()
                {
                    Id = -1,
                    Message = "გაყიდვა ვერ დაემატა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }

        public async Task<ResultViewModel> DeleteSale(int id)
        {
            try
            {
                var result = await _dbContext.Sales.Where(f => f.Id == id).ExecuteDeleteAsync();
                return new ResultViewModel()
                {
                    Id = result,
                    Message = "გაყიდვა წარმატებით წაიშალა",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel
                {
                    Id = -1,
                    Message = "გაყიდვა ვერ წაიშალა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }

        public async Task<List<SaleViewModel>> GetAllSalesAsync()
        {
            return await _dbContext.Sales.Include(f => f.Distributor)
                    .Select(f => _mapper.Map<SaleViewModel>(f)).ToListAsync();
        }

        public async Task<SaleViewModel> GetSaleById(int id)
        {
            var product = await _dbContext.Sales.Include(f => f.ProductList).ThenInclude(s => s.Product).FirstOrDefaultAsync(f => f.Id == id);
            if (product != null)
            {
                return _mapper.Map<SaleViewModel>(product);
            }

            return new SaleViewModel() { Id = -1 };
        }

        public Task<List<SaleViewModel>> SearchSalesByNameOrCode(string param)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultViewModel> UpdateSale(SaleViewModel model)
        {
            try
            {
                Sale sale = _mapper.Map<Sale>(model);
                var oldProductList = await _dbContext.SalesProducts.Where(f => f.SaleId == sale.Id).ToListAsync();
                if (oldProductList.Count > 0)
                {
                    _dbContext.SalesProducts.RemoveRange(oldProductList);
                }
                _dbContext.Sales.Update(sale);
                var result = await _dbContext.SaveChangesAsync();
                return new ResultViewModel()
                {
                    Id = result,
                    Message = "გაყიდვა წარმატებით  განახლდა",
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new ResultViewModel()
                {
                    Id = -1,
                    Message = "გაყიდვა ვერ განახლდა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }
    }
}
