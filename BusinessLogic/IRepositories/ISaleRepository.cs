using BusinessLogic.ViewModels;

namespace BusinessLogic.IRepositories
{
    public interface ISaleRepository
    {

        //TODO add methods for sale CRUD
        public Task<List<SaleViewModel>> GetAllSalesAsync();

        public Task<SaleViewModel> GetSaleById(int id);

        public Task<List<SaleViewModel>> SearchSalesByNameOrCode(string param);

        public Task<ResultViewModel> AddSale(SaleViewModel Sale);
        public Task<ResultViewModel> UpdateSale(SaleViewModel Sale);

        public Task<ResultViewModel> DeleteSale(int id);

    }
}
