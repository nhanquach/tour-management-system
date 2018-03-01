using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class TourDTO
    {
        public int MaTour { get; set; }
        public string TenTour { get; set; }
        public string ThongTinTour { get; set; }
        public Nullable<decimal> GiaTour { get; set; }
        public virtual IEnumerable<Tour> tour { get; set; }

        /*private int maTour;
        private string tenTour;
        private string thongtinTour;
        private int giaTour;
        public Int32 MaTour
        {
            get { return maTour; }
            set { maTour = value; }
        }
        public String TenTour
        {
            get { return tenTour; }
            set { tenTour = value; }
        }
        public String ThongtinTour
        {
            get { return thongtinTour; }
            set { thongtinTour = value; }
        }
        public Int32 GiaTour
        {
            get { return giaTour; }
            set { giaTour = value; }
        }
         
        public TourDTO(int matour, string tentour, string thongtintour, int giatour)
        {
            maTour = matour;
            tenTour = tentour;
            thongtinTour = thongtintour;
            giaTour = giatour;
        }
        public TourDTO() { }
         */
    }
}
