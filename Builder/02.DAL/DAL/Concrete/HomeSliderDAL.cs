using Core.Abstract;
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
    public class HomeSliderDAL : BaseRepository<HomeSlider, ApplicationDbContext>, IHomeSliderDAL
    {
        private readonly ApplicationDbContext _context;
        public HomeSliderDAL(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<HomeSlider> GetActiveSliders()
        {
            
            return GetAll(x => x.IsActive && x.Deleted == 0);
        }

        public List<HomeSlider> GetByDisplayOrder()
        {
            
            return _context.HomeSliders
                .Where(x => x.IsActive && x.Deleted == 0)
                .OrderBy(x => x.Order)
                .ToList();
        }
    }
}
