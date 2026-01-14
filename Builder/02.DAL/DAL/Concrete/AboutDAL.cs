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
    public class AboutDAL : BaseRepository<About, ApplicationDbContext>, IAboutDAL
    {
        public About GetCurrent()
        {
            using (ApplicationDbContext context = new())
            {
                return context.Abouts.FirstOrDefault();
            }
        }
    }
}
