using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContexts;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Distributor> Distributors { get; set; }

    public virtual DbSet<PersonalDocument> PersonalDocuments { get; set; }

    public virtual DbSet<PersonalContact> PersonalContacts { get; set; }
    public virtual DbSet<Sale> Sales { get; set; }
    public virtual DbSet<SalesProduct> SalesProducts { get; set; }

    public virtual DbSet<DistributorAddress> DistributorAddresses { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<DistributorBonus> DistributorBonus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OnlineMarketingDB;Integrated Security=True;TrustServerCertificate=True;");
    }

}