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
        public List<HomeSlider> GetActiveSliders()
        {
            return GetAll(x => x.IsActive);
        }

        public List<HomeSlider> GetByDisplayOrder()
        {
            using (ApplicationDbContext context = new())
            {
                return context.HomeSliders
                    .Where(x => x.IsActive)
                    .OrderBy(x => x.Order)
                    .ToList();
            }
        }
    }

}
