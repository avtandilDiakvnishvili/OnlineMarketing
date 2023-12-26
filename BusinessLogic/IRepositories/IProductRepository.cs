using BusinessLogic.ViewModels;
using DataAccess.Models;
using OnlineMarketing.ViewModels;

namespace BusinessLogic.IRepositories
{
    public interface IProductRepository
    {



        public Task<List<ProductViewModel>> GetAllProductsAsync();

        public Task<ProductViewModel> GetProductById(int id);

        public Task<List<ProductViewModel>> SearchProductsByNameOrCode(string param);

        public Task<ResultViewModel> AddProduct(ProductViewModel product);
        public Task<ResultViewModel> UpdateProduct(ProductViewModel product);

        public Task<ResultViewModel> DeleteProduct(int id);
        public Task<List<IdName>> GetProductsForSelection();

        //public Task<ResultViewModel> RemovAllProduct();

    }
}
