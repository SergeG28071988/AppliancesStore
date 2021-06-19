using System.Collections.Generic;
using System.Linq;

namespace AppliancesStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Appliance appliance, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Appliance.ApplianceId == appliance.ApplianceId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Appliance = appliance,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Appliance appliance)
        {
            lineCollection.RemoveAll(l => l.Appliance.ApplianceId == appliance.ApplianceId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Appliance.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Appliance Appliance { get; set; }
        public int Quantity { get; set; }
    }
}

