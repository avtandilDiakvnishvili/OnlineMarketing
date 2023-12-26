using BusinessLogic.ViewModels;
using DataAccess.Models;

namespace BusinessLogic.IRepositories
{
    public interface IDistributorRepository
    {
        Task<ResultViewModel> AddDistributor(DistributorViewModel model);
        Task<ResultViewModel> UpdateDistributor(DistributorViewModel model);
        Task<ResultViewModel> DeleteDistributor(int id);
        Task<List<DistributorViewModel>> GetAllDistributorsAsync();
        Task<DistributorViewModel> GetDistributorById(int id);
        Task<List<DistributorViewModel>> SearchDistributorsByName(string param);
        Task<List<IdName>> GetAllPosibileDistributorsForRecomendationAsync();
        Task<List<DistributorViewModel>> GetChildrens(int distributor);

        Task<List<dynamic>> CalculateDistributorBonus(DateTime startDate, DateTime endDate, List<int> distributors);
        Task<List<IdName>> GetDistributorsForSelection();

        Task<object> GetDistributorBonuses(DateTime startDate, DateTime endDate);
    }
}
