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
    public class SiteSettingsDAL : BaseRepository<SiteSettings, ApplicationDbContext>, ISiteSettingsDAL
    {
        public SiteSettings GetCurrentSettings()
        {
            using (ApplicationDbContext context = new())
            {
                return context.SiteSettings.FirstOrDefault();
            }
        }
    }
}
