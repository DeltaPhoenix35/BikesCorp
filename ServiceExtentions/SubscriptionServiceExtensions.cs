using BikesTest.Models;
using BikesTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.ServiceExtentions
{
    public static class SubscriptionServiceExtensions
    {
        public static List<DateTime> GetSubscriptionDates(this ISubscriptionService<Subscription> sService,
                                                          Subscription sub,
                                                          List<DateTime> starts, 
                                                          List<DateTime> ends)
        {
            List<DateTime> subDates = new List<DateTime>();

            foreach(var item in sub.subscriptionPlan.subscriptionDays)
            {
                int i = 0, count = 4;
                while(count > 0)
                {
                    if (item.day != sub.creationDate.AddDays(i).DayOfWeek)
                        i++;
                    else
                    {
                        TimeSpan tsStart = new TimeSpan(item.startTime.Hour,
                                                  item.startTime.Minute,
                                                  item.startTime.Second);

                        TimeSpan tsEnd = new TimeSpan(item.endTime.Hour,
                                                  item.endTime.Minute,
                                                  item.endTime.Second);

                        starts.Add(sub.creationDate.AddDays(i).Date + tsStart);
                        ends.Add(sub.creationDate.AddDays(i).Date + tsEnd);
                        i += 7; count--;
                    }
                }               
            }

            return subDates;
        }
    }
}
