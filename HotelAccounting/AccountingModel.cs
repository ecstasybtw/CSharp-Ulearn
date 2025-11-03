using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public double Price
        {
            get => price;
            set
            {
                if (value < 0) throw new ArgumentException();
                price = value;
                UpdateTotal();
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        public int NightsCount
        {
            get => nightsCount;
            set
            {
                if (value <= 0) throw new ArgumentException();
                nightsCount = value;
                UpdateTotal();
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        public double Discount
        {
            get => discount;
            set
            {
                if (value > 100) throw new ArgumentException();
                discount = value;
                UpdateTotal();
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        public double Total
        {
            get => total;
            set
            {
                if (value < 0) throw new ArgumentException();
                total = value;
                UpdateDiscountFromTotal();
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }

        private void UpdateTotal()
        {
            total = price * nightsCount * (1 - discount / 100);
        }

        private void UpdateDiscountFromTotal()
        {
            if (price > 0 && nightsCount > 0)
                discount = 100 - (100 * total / (price * nightsCount));
        }
    }


}
