using AutoMapper;
using BusinessLogic.IRepositories;
using BusinessLogic.ViewModels;
using DataAccess.DbContexts;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using OnlineMarketing.ViewModels;
using System.Data.Common;

namespace BusinessLogic.Repositories
{
    public class DistributorRepository : IDistributorRepository
    {

        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public DistributorRepository(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        ///  დისტრიბუტორის რეგისტრაციის მეთოდი, რომელიც არგუმენტად იღებს DistributorViewModel კლასს, AutoMapper ის გამოყენებით გარდაქმნის Distributo კლასად.
        /// რეგისტრაციის პროცესში მოწმდება შემდეგი პირობები:
        /// 1. ჰყავს თუ არა რეკომენდატორი დისტრიბუტორს
        /// 2. აჭარბებს თუ არა დაწესებულ ლიმიტებს ( რეკომენდატორის მიერ გაწეული მაქსიმალურ რეკომენდაციებს, იერარქიის დონეს)
        /// 
        /// აბრუნებს ResultViewModel კლასს, შესაბამისი სტატუსით და შეტყობინებით. თუ რაიმე გაუთვალისწინებლი შეცდომა დაფიქსირდა გააჩნია Error და InnerMessage
        /// ველები რომლებშიც შესაბამისად გამოვა Exception ს შეტყობინება.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ResultViewModel</returns>
        public async Task<ResultViewModel> AddDistributor(DistributorViewModel model)
        {
            try
            {
                Distributor distributor = _mapper.Map<Distributor>(model);
                if (distributor.Recommender > 0)
                {
                    var recomender = await _dbContext.Distributors.Where(f => f.Id == distributor.Recommender).FirstOrDefaultAsync();
                    if (recomender != null)
                    {
                        if (recomender.RecommendedCount < 3)
                        {
                            distributor.Level = recomender.Level + 1;
                            if (distributor.Level > 4)
                            {
                                return new ResultViewModel()
                                {
                                    Id = -1,
                                    Message = $"დისტრიბუტორი ვერ დაემატა,რადგან აჭარბებს დაშვებული იერარქიის მაქსიმაულ დონეს",
                                    Status = false
                                };
                            }

                            recomender.RecommendedCount++;
                            _dbContext.Distributors.Update(recomender);
                        }
                        else
                        {
                            return new ResultViewModel()
                            {
                                Id = -1,
                                Message = $"დისტრიბუტორი ვერ დაემატა,რეკომენდატორს,{recomender.FirstName} {recomender.LastName}, აღარ შეუძლია რეკომენდაციის გაწევა",
                                Status = false
                            };
                        }
                    }
                    else
                    {
                        return new ResultViewModel()
                        {
                            Id = -1,
                            Message = $"დისტრიბუტორი ვერ დაემატა,რეკომენდატორი,id ით {distributor.Recommender},ვერ მოიძებნა",
                            Status = false
                        };
                    }

                }

                await _dbContext.Distributors.AddAsync(distributor);
                var result = await _dbContext.SaveChangesAsync();


                return new ResultViewModel()
                {
                    Id = distributor.Id,
                    Message = "დისტრიბუტორი წარმატებით დაემატა",
                    Status = true
                };
            }
            catch (Exception ex)
            {

                return new ResultViewModel()
                {
                    Id = -1,
                    Message = "დისტრიბუტორი ვერ დაემატა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResultViewModel> DeleteDistributor(int id)
        {
            using var dbTransaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var result = -1;
                var distributor = await _dbContext.Distributors.Where(f => f.Id == id).FirstOrDefaultAsync();
                if (distributor != null)
                {
                    if (distributor.Recommender > 0)
                    {
                        var recommender = await _dbContext.Distributors.Where(f => f.Id == distributor.Recommender).FirstOrDefaultAsync();
                        if (recommender != null)
                        {
                            recommender.RecommendedCount--;
                            _dbContext.Distributors.Update(recommender);
                        }
                    }

                    var updatedDistributors = await _dbContext.Distributors
                        .Where(f => f.Recommender == distributor.Id).ToListAsync();
                    updatedDistributors.ForEach(f => { f.Level = 0; f.Recommender = 0; });

                    _dbContext.Distributors.UpdateRange(updatedDistributors);


                    _dbContext.Distributors.Remove(distributor);

                    result = await _dbContext.SaveChangesAsync();

                    dbTransaction.Commit();
                    return new ResultViewModel()
                    {
                        Id = result,
                        Message = "დისტრიბუტორი წარმატებით წაიშალა",
                        Status = true
                    };

                }
                return new ResultViewModel()
                {
                    Id = -1,
                    Message = "დისტრიბუტორი ვერ მოიძებნა",
                    Status = false
                };

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();

                return new ResultViewModel
                {
                    Id = -1,
                    Message = "დისტრიბუტორი ვერ წაიშალა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }

        public async Task<List<DistributorViewModel>> GetAllDistributorsAsync()
        {
            return await _dbContext.Distributors
                .Include(f => f.PersonalContacts)
                .Include(f => f.PersonalDocuments)
                .Include(f => f.DistributorAddress)
                .Select(distributor => _mapper.Map<DistributorViewModel>(distributor)).ToListAsync();
        }

        public async Task<DistributorViewModel> GetDistributorById(int id)
        {
            var distributor = await _dbContext.Distributors.Include(f => f.PersonalContacts)
                .Include(f => f.PersonalDocuments)
                .Include(f => f.DistributorAddress)
                .FirstOrDefaultAsync(f => f.Id == id);
            if (distributor != null)
            {
                return _mapper.Map<DistributorViewModel>(distributor);
            }

            return new DistributorViewModel();
        }


        public async Task<List<IdName>> GetAllPosibileDistributorsForRecomendationAsync()
        {
            return await _dbContext.Distributors.Where(f => f.RecommendedCount < 3)
                .Select(distributor => new IdName
                {
                    Id = distributor.Id,
                    Name = $"{distributor.FirstName} {distributor.LastName} ({distributor.RecommendedCount})"
                }).ToListAsync();
        }

        public async Task<List<DistributorViewModel>> SearchDistributorsByName(string param)
        {
            return await _dbContext.Distributors
                .Where(f => f.FirstName.StartsWith(param) || f.LastName.StartsWith(param))
                .Select(f => _mapper.Map<DistributorViewModel>(f)).Take(20).ToListAsync();

        }

        public async Task<ResultViewModel> UpdateDistributor(DistributorViewModel model)
        {
            try
            {
                Distributor distributor = _mapper.Map<Distributor>(model);

                Distributor? distributorFromDb = await _dbContext.Distributors.AsNoTracking().Where(f => f.Id == model.Id).FirstOrDefaultAsync();

                if (distributorFromDb != null && distributorFromDb.Recommender > 0 && distributorFromDb.Recommender != distributor.Recommender)
                {
                    var oldRcomender = await _dbContext.Distributors.Where(f => f.Id == distributorFromDb.Recommender).FirstOrDefaultAsync();

                    if (oldRcomender != null && oldRcomender.RecommendedCount > 0)
                    {
                        oldRcomender.RecommendedCount--;
                        _dbContext.Distributors.Update(oldRcomender);

                    }
                }

                if (distributor.Recommender > 0 && distributor.Recommender != distributorFromDb?.Recommender)
                {
                    Distributor? recomender = await _dbContext.Distributors.Where(f => f.Id == distributor.Recommender).FirstOrDefaultAsync();

                    if (recomender != null)
                    {
                        if (recomender.RecommendedCount < 3)
                        {
                            distributor.Level = recomender.Level + 1;
                            if (distributor.Level > 5)
                            {
                                return new ResultViewModel()
                                {
                                    Id = -1,
                                    Message = $"დისტრიბუტორი ვერ დაემატა,რადგან აჭარბებს დაშვებული იერარქიის მაქსიმაულ დონეს",
                                    Status = false
                                };
                            }
                            recomender.RecommendedCount++;
                            _dbContext.Distributors.Update(recomender);
                        }
                        else
                        {
                            return new ResultViewModel()
                            {
                                Id = -1,
                                Message = $"დისტრიბუტორი ვერ განახლდა,რეკომენდატორს,{recomender.FirstName} {recomender.LastName}, აღარ შეუძლია რეკომენდაციის გაწევა",
                                Status = false
                            };
                        }
                    }
                    else
                    {
                        return new ResultViewModel()
                        {
                            Id = -1,
                            Message = $"დისტრიბუტორი ვერ განახლდა,რეკომენდატორი,id ით {distributor.Recommender},ვერ მოიძებნა",
                            Status = false
                        };
                    }

                }

                var contacts = await _dbContext.PersonalContacts.Where(f => f.DistributorId == distributor.Id).ToListAsync();
                var documents = await _dbContext.PersonalDocuments.Where(f => f.DistributorId == distributor.Id).ToListAsync();
                var address = await _dbContext.DistributorAddresses.Where(f => f.DistributorId == distributor.Id).ToListAsync();
                //clear exists contacts, documents,addresses
                if (contacts.Count > 0)
                {
                    _dbContext.PersonalContacts.RemoveRange(contacts);

                }
                if (documents.Count > 0)
                {
                    _dbContext.PersonalDocuments.RemoveRange(documents);
                }
                if (address.Count > 0)
                {
                    _dbContext.DistributorAddresses.RemoveRange(address);
                }

                _dbContext.Distributors.Update(distributor);
                var result = await _dbContext.SaveChangesAsync();


                return new ResultViewModel()
                {
                    Id = distributor.Id,
                    Message = "დისტრიბუტორი წარმატებით განახლდა",
                    Status = true
                };
            }
            catch (Exception ex)
            {

                return new ResultViewModel()
                {
                    Id = -1,
                    Message = "დისტრიბუტორი ვერ განახლდა",
                    Status = false,
                    Error = ex.Message,
                    InnerMessage = ex.InnerException?.Message
                };
            }
        }


        public async Task<List<DistributorViewModel>> GetChildrens(int distributor)
        {
            List<DistributorViewModel> childrens = new();
            //List<Distributor> all = await _dbContext.Distributors.ToListAsync();

            //GetChildrenHierarchy(all, distributor, new());

            return childrens;
        }


        private static int PrintParentHierarchy(List<Distributor> distributors, Distributor distributor, int level)
        {
            if (distributor == null)
                return level;

            string indentation = new string(' ', level * 2);
            Console.WriteLine($"{indentation}{distributor.FirstName} {distributor.Id} {level}");


            Distributor recommender = distributors.FirstOrDefault(d => d.Id == distributor.Recommender);


            int maxChildLevel = PrintParentHierarchy(distributors, recommender, level + 1);
            return Math.Max(level, maxChildLevel);
        }


        private void GetChildrenHierarchy(List<Distributor> distributors, int distributoId, List<(int, int)> childrenHierarchy)
        {
            if (distributoId == 0)
            {
                return;
            }

            var children = distributors.Where(d => d.Recommender == distributoId).ToList();

            foreach (var child in children)
            {
                childrenHierarchy.Add((child.Id, child.Level));

                GetChildrenHierarchy(distributors, child.Id, childrenHierarchy);
            }
        }

        public async Task<List<dynamic>> CalculateDistributorBonus(DateTime startDate, DateTime endDate, List<int> distributors)
        {
            List<dynamic> resultList = new();
            using var dbTransaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                List<Distributor> dbDistributors = await _dbContext.Distributors.ToListAsync();
                distributors = distributors.Count > 0
                    ? dbDistributors.Where(f => distributors.Contains(f.Id)).Select(f => f.Id).ToList()
                    : dbDistributors.Select(f => f.Id).ToList();


                var salesByDistributors = await _dbContext.Sales
                                                    .Where(f => f.TDate.Date >= startDate.Date && f.TDate.Date <= endDate.Date && f.DistributorId.HasValue)
                                                    .ToListAsync();

                var distributorsBonus = await _dbContext.DistributorBonus.Include(f => f.UsedSales)
                                                    .Where(f => distributors.Contains(f.DistributorId))
                                                    .GroupBy(f => f.DistributorId)
                                                    .ToDictionaryAsync(f => f.Key, f => f.SelectMany(ff => ff.UsedSales).ToList());



                foreach (var distributor in distributors)
                {
                    var childrens = new List<(int id, int level)>();
                    distributorsBonus.TryGetValue(distributor, out var usedSales);
                    var usedSaleIds = usedSales?.Select(s => s.SalesId).ToList();

                    GetChildrenHierarchy(dbDistributors, distributor, childrens);

                    var minLevel = childrens.Any() ? childrens?.Min(f => f.level) : 0;

                    var forBonusCount = childrens?.Where(f => f.level <= (minLevel + 1)).ToList();

                    if (forBonusCount?.Count > 0)
                    {


                        var sumDistributorsSale = salesByDistributors
                                .Where(f => f.DistributorId == distributor)
                                .Where(f => usedSaleIds == null || !usedSaleIds.Contains(f.Id))
                                .ToList();

                        var firstLevelChildrens = forBonusCount?.Where(f => f.level == minLevel).Select(f => f.id).ToList();
                        var secondLevelChildrens = forBonusCount?.Where(f => f.level == (minLevel + 1)).Select(f => f.id).ToList();

                        var sumDistributorFirstLevelSales = salesByDistributors
                            .Where(f => firstLevelChildrens != null && firstLevelChildrens.Contains(f.DistributorId ?? 0))
                            .Where(f => usedSaleIds == null || !usedSaleIds.Contains(f.Id))

                            .ToList();

                        var sumDistributorSecondLevelSales = salesByDistributors
                            .Where(f => secondLevelChildrens != null && secondLevelChildrens.Contains(f.DistributorId ?? 0))
                            .Where(f => usedSaleIds == null || !usedSaleIds.Contains(f.Id))

                            .ToList();



                        var bonusDistributorMainSale = (sumDistributorsSale.Sum(f => f.TotalPrice) * 10) / 100;
                        var bonusFirstLevelChildrensSale = (sumDistributorFirstLevelSales.Sum(s => s.TotalPrice) * 5) / 100;
                        var bonusSecondLevelChildrensSale = (sumDistributorSecondLevelSales.Sum(s => s.TotalPrice) * 1) / 100;

                        var finalBonus = bonusDistributorMainSale + bonusFirstLevelChildrensSale + bonusSecondLevelChildrensSale;

                        var salesForBonus = sumDistributorsSale
                            .Union(sumDistributorSecondLevelSales)
                            .Union(sumDistributorFirstLevelSales).Select(f => new UsedSales()
                            {
                                SalesId = f.Id
                            }).ToList();
                        if (salesForBonus.Count > 0)
                            SaveUsedSales(startDate, endDate, distributor, finalBonus, salesForBonus);

                        resultList.Add(new { Id = distributor, Bonus = finalBonus });

                    }
                    else
                    {
                        var sumDistributorsSale = salesByDistributors
                               .Where(f => f.DistributorId == distributor)
                               .Where(f => usedSaleIds == null || !usedSaleIds.Contains(f.Id))
                               .ToList();
                        var bonusDistributorMainSale = (sumDistributorsSale.Sum(f => f.TotalPrice) * 10) / 100;
                        var salesForBonus = sumDistributorsSale
                        .Select(f => new UsedSales()
                        {
                            SalesId = f.Id
                        }).ToList();
                        if (salesForBonus.Count > 0)
                            SaveUsedSales(startDate, endDate, distributor, bonusDistributorMainSale, salesForBonus);

                        resultList.Add(new { Id = distributor, Bonus = bonusDistributorMainSale });

                    }
                }

                await _dbContext.SaveChangesAsync();
                dbTransaction.Commit();

            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                return new() { new { Id = -1, Message = "მოხდა გაუთვალისწინებელი შეცდომა", Error = ex.Message } };
            }


            return resultList;
        }

        private void SaveUsedSales(DateTime startDate, DateTime endDate, int distributor, decimal finalBonus, List<UsedSales> salesForBonus)
        {
            var usedSales = new DistributorBonus
            {
                DistributorId = distributor,
                StartDate = startDate,
                EndDate = endDate,
                Bonus = finalBonus,
                UsedSales = salesForBonus

            };
            _dbContext.DistributorBonus.Add(usedSales);

        }

        public async Task<List<IdName>> GetDistributorsForSelection()
        {
            return await _dbContext.Distributors.Select(f => new IdName
            {
                Id = f.Id,
                Name = $"{f.FirstName} {f.LastName}"
            }).ToListAsync();
        }

        public async Task<object> GetDistributorBonuses(DateTime startDate, DateTime endDate)
        {

            var distributors = await _dbContext.Distributors.ToDictionaryAsync(f => f.Id, f => $"{f.FirstName} {f.LastName}");
            return await _dbContext.DistributorBonus
                .Where(b => b.StartDate >= startDate && b.StartDate <= endDate)
                .Select(s => new
                {
                    bonus = s.Bonus,
                    name = distributors.ContainsKey(s.DistributorId) ? distributors[s.DistributorId] : string.Empty,
                    start_date = s.StartDate,
                    end_date = s.EndDate
                }).ToListAsync();
        }
    }
}
