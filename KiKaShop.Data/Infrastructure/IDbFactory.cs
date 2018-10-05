using System;

namespace KiKaShop.Data.Infrastructure
{
    //Giao thuc khoi tao cac Object Entity gian tiep
    public interface IDbFactory : IDisposable
    {
        KikaShopDbContext Init();
    }
}