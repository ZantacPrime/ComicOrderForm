using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicOrders.DB.Models {
    public class OrderMonthDisplayModel {
        public DateTime OrderMonth { get; set; }

        public OrderMonthDisplayModel(OrderModel Order) {
            OrderMonth = Order.OrderMonth;
        }

        public OrderMonthDisplayModel(DateTime OrderMonth) {
            this.OrderMonth = OrderMonth;
        }

        public override string ToString() {
            return OrderMonth.ToString("MMMM yyyy");
        }
    }
}
