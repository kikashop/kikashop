using KiKaShop.Data.Infrastructure;
using KiKaShop.Model.Models;
namespace KiKaShop.Data.Repositories
{
    public interface IProductRepository
    {

    }


    public class ProductRepository:RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory): base(dbFactory)
        {

        }
    }
}
