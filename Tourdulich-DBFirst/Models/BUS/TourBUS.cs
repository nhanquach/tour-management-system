using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DAO;
using Models.DTO;

namespace Models.BUS
{
    public class TourBUS
    {
        public int themTour(TourDTO tour)
        {
            TourDAO t = new TourDAO();
            return t.ThemTour(tour);
        }

        public int suaTour(TourDTO tour)
        {
            TourDAO t = new TourDAO();
            return t.SuaTour(tour);
        }
    }
}
