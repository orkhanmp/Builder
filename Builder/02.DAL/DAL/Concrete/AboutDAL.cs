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
        private readonly ApplicationDbContext _context;
        public AboutDAL(ApplicationDbContext _context): base(_context)
        {
            this._context = _context;
        }
        public About GetCurrent()
        {

            return _context.Abouts.FirstOrDefault(x => x.Deleted == 0);

        }
    }
}
