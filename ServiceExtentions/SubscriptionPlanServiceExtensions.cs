using BikesTest.Interfaces;
using BikesTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.ServiceExtentions
{
    public static class SubscriptionPlanServiceExtensions
    {
        public static Dictionary<int, string> GetIdName(this ISubscriptionPlanService<SubscriptionPlan> spService)
        {
            List<SubscriptionPlan> subPlansList = spService.GetAll();
            Dictionary<int, string> idsNames = new Dictionary<int, string>();
            for (int i = 0; i < subPlansList.Count; i++)
            {
                idsNames.Add(subPlansList.ElementAt(i).id, subPlansList.ElementAt(i).name);
            }
            return idsNames;
        }
    }
}
