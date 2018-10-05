namespace KiKaShop.Data.Infrastructure
{
    //Khởi tạo các đối tượng DbContext 
    public class DbFactory : Disposable, IDbFactory
    {
        private KikaShopDbContext dbContext;

        public KikaShopDbContext Init()
        {
            return dbContext ?? (dbContext = new KikaShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}