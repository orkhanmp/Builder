using Core.Concrete;
using DAL.Abstract;
using DAL.Database;
using Entities.TableModels.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class PageBannerDAL : BaseRepository<PageBanner, ApplicationDbContext>, IPageBannerDAL
    {
        public PageBanner GetByPageName(string pageName)
        {
            return Get(x => x.PageName == pageName && x.IsActive);
        }
    }
}
