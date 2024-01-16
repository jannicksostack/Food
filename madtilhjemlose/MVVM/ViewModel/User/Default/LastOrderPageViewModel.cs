using CommunityToolkit.Mvvm.ComponentModel;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using System.Collections.ObjectModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Default
{
    public partial class LastOrderPageViewModel : ObservableValidator
    {
        private OrderRepository orderRepository = new();

        [ObservableProperty]
        private Order? order;

        [ObservableProperty]
        private ObservableCollection<OrderDetails> orderDetails = new();

        public LastOrderPageViewModel(int userId)
        {
            Order = orderRepository.Orders
                .OrderByDescending(x => x.Date)
                .ThenByDescending(x => x.Id)
                .FirstOrDefault(x => x.UserId == userId);

            if (Order is null)
            {
                return;
            }

            OrderDetails = new(orderRepository.OrderDetails
                .Where(x => x.OrderId == Order.Id));
        }
    }
}
