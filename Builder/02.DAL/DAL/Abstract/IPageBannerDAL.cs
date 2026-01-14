using Core.Abstract;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IPageBannerDAL : IBaseRepository<PageBanner>
    {
        PageBanner GetByPageName(string pageName);
    }
}
