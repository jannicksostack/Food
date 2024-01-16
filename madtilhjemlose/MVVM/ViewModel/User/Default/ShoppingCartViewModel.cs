using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.ViewModel.User.Default
{
    public partial class ShoppingCartViewModel : ObservableObject
    {
        [ObservableProperty]
        private ShoppingCart cart;

        public event EventHandler OrderPlaced;

        private OrderRepository orderRepository = new();

        public IRelayCommand PlaceOrderCommand { get; set; }

        public ShoppingCartViewModel(ShoppingCart cart)
        {
            Cart = cart;
            PlaceOrderCommand = new RelayCommand(PlaceOrder);
        }

        private void PlaceOrder()
        {
            orderRepository.PlaceOrder(Cart);
            Cart.Clear();
            OrderPlaced?.Invoke(this, EventArgs.Empty);
            PopToMainPage();
        }

        private void PopToMainPage()
        {
            App.Navigation.RemovePage(App.Navigation.NavigationStack.SkipLast(1).Last());
            App.Navigation.PopAsync();
        }
    }
}
