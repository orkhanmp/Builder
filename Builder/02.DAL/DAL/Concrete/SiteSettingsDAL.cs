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
        private readonly ApplicationDbContext _context;
        public SiteSettingsDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public SiteSettings GetCurrentSettings()
        {            
            return _context.SiteSettings.FirstOrDefault(x => x.Deleted == 0);
        }
    }
}
