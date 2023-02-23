using BikesTest.Interfaces;
using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.ServiceExtentions
{
    public static class CouponTypeServiceExtensions
    {
        public static Dictionary<int, int> GetIdName(this ICouponTypeService<CouponType> btService)
        {
            List<CouponType> typesList = btService.GetAll();
            Dictionary<int, int> idsValues = new Dictionary<int, int>();
            for (int i = 0; i < typesList.Count; i++)
            {
                idsValues.Add(typesList.ElementAt(i).id, typesList.ElementAt(i).value);
            }
            return idsValues;
        }
    }
}
