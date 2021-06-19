using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static AppliancesStore.Models.Order;

namespace AppliancesStore.Models.Repository
{
    public class Repository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Appliance> Appliances 
        {
            get { return context.Appliances; }
        }

        // Чтение данных из таблицы Orders
        public IEnumerable<Order> Orders
        {
            get
            {
                return context.Orders
                    .Include(o => o.OrderLines.Select(ol => ol.Appliance));
            }            
        }

        public void SaveAppliance(Appliance appliance)
        {
            if (appliance.ApplianceId == 0)
            {
                appliance = context.Appliances.Add(appliance);
            }
            else
            {
                Appliance dbAppliance = context.Appliances.Find(appliance.ApplianceId);
                if (dbAppliance != null)
                {
                    dbAppliance.Name = appliance.Name;
                    dbAppliance.Description = appliance.Description;
                    dbAppliance.Price = appliance.Price;
                    dbAppliance.Category = appliance.Category;
                }
            }
            context.SaveChanges();
        }

        public void DeleteAppliance (Appliance appliance)
        {
            IEnumerable<Order> orders = context.Orders
                .Include(o => o.OrderLines.Select(ol => ol.Appliance))
                .Where(o => o.OrderLines
                    .Count(ol => ol.Appliance.ApplianceId == appliance.ApplianceId) > 0)
                .ToArray();

            foreach (Order order in orders)
            {
                context.Orders.Remove(order);
            }
            context.Appliances.Remove(appliance);
            context.SaveChanges();
        }

        // Сохранить данные заказа в базе данных
        public void SaveOrder(Order order)
        {
            if (order.OrderId == 0)
            {
                order = context.Orders.Add(order);

                foreach (OrderLine line in order.OrderLines)
                {
                    context.Entry(line.Appliance).State
                        = EntityState.Modified;
                }

            }
            else
            {
                Order dbOrder = context.Orders.Find(order.OrderId);
                if (dbOrder != null)
                {
                    dbOrder.Name = order.Name;
                    dbOrder.Line1 = order.Line1;
                    dbOrder.Line2 = order.Line2;
                    dbOrder.Line3 = order.Line3;
                    dbOrder.City = order.City;
                    dbOrder.GiftWrap = order.GiftWrap;
                    dbOrder.Dispatched = order.Dispatched;
                }
            }
            context.SaveChanges();
        }
    }
}
