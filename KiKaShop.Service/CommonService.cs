using KiKaShop.Common;
using KiKaShop.Data.Infrastructure;
using KiKaShop.Data.Repositories;
using KiKaShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiKaShop.Service
{
    public interface ICommmonService
    {
        Footer GetFooter();
    }
    public class CommonService : ICommmonService
    {
        IFooterRepository _footerRepository;
        IUnitOfWork _unitOfWork;
        public CommonService(IFooterRepository footerRepository,IUnitOfWork unitOfWork)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
        


        }
        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooterId);
        }
    }
}
