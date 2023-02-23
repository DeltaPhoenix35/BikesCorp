using BikesTest.Interfaces;
using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.ServiceExtentions
{
    public static class BicycleTypeServiceExtensions
    {
        public static Dictionary<int, string> GetIdName(this IBicycleTypeService<BicycleType> btService)
        {
            List<BicycleType> typesList = btService.GetAll();
            Dictionary<int, string> idsNames = new Dictionary<int, string>();
            for(int i = 0; i < typesList.Count; i++)
            {
                idsNames.Add(typesList.ElementAt(i).id, typesList.ElementAt(i).typeName);
            }
            return idsNames;
        }
    }
}
