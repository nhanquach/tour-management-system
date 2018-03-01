using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DTO;
using Models.DAO;

namespace Models.BUS
{
    public class DiaDiemBUS
    {
        public int search_madd(string namedd)
        {
            DiaDiemDAO dd = new DiaDiemDAO();
            return dd.Search_madd(namedd);
        }
    }
}
