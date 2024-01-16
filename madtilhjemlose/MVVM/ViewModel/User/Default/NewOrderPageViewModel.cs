﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.DataAccess;
using madtilhjemlose.MVVM.Model;
using madtilhjemlose.MVVM.View.User.Default;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Default
{
    internal partial class NewOrderPageViewModel : ObservableValidator
    {
        private OrderRepository orderRepository = new();
        private ProductRepository productRepository = new();
        private ActiveProductRepository activeProductRepository;

        private ShoppingCart cart = new();

        [ObservableProperty]
        private ObservableCollection<StoreItem> items;

        [ObservableProperty]
        private ObservableCollection<StoreItem> searchItems;

        public IRelayCommand SearchCommand { get; set; }
        public IRelayCommand ShowCartCommand { get; set; }

        partial void OnItemsChanged(ObservableCollection<StoreItem> value)
        {
            SearchItems = value;
        }

        public NewOrderPageViewModel()
        {
            activeProductRepository = new(productRepository.Items.ToList());
            GetStoreItems();

            SearchCommand = new RelayCommand<string>(Search);
            ShowCartCommand = new RelayCommand(ShowCart);
        }

        private void GetStoreItems()
        {
            var storeItems = activeProductRepository.Items
                .Select(x => new StoreItem(x, AddToCart))
                .ToList();

            var quantitySold = orderRepository.OrderDetails
                .GroupBy(x => x.ActiveProduct.Id)
                .Select(g => new
                {
                    ActiveProductId = g.Key,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .ToList();

            foreach (StoreItem item in storeItems)
            {
                int index = quantitySold.FindIndex(x => x.ActiveProductId == item.ActiveProduct.Id);
                if (index != -1)
                {
                    item.Quantity -= quantitySold[index].Quantity;
                }
            }

            Items = new(storeItems.Where(x => x.Quantity > 0));
        }

        private void ShowCart()
        {
            Page page = new ShoppingCartPage(cart);
            App.Navigation.PushAsync(page);
        }

        private void Search(string? query)
        {
            if (query == null)
            {
                SearchItems = Items;
            }
            else
            {
                SearchItems = new(Items.Where(x => x.ActiveProduct.Product.Name.ToLower().StartsWith(query.ToLower())));
            }
        }

        private void AddToCart(ActiveProduct activeProduct, int count)
        {
            cart.Add(activeProduct, count);

            StoreItem storeItem = Items.Single(x => x.ActiveProduct == activeProduct);
            storeItem.Quantity -= count;
        }

        private void RemoveFromCart(ShoppingCartItem item, int count)
        {
            cart.Remove(item, count);

            StoreItem storeItem = Items.Single(x => x.ActiveProduct == item.ActiveProduct);
            storeItem.Quantity += count;
        }
    }

    public partial class StoreItem : ObservableObject
    {
        [ObservableProperty]
        private ActiveProduct activeProduct;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddToCartCommand))]
        private string count = "";

        [ObservableProperty]
        private int quantity;

        public IRelayCommand AddToCartCommand { get; set; }

        public StoreItem(ActiveProduct activeProduct, Action<ActiveProduct, int> action)
        {
            ActiveProduct = activeProduct;
            Quantity = ActiveProduct.Quantity;
            AddToCartCommand = new RelayCommand(() => AddTocart(action), CanAddToCart);
        }

        private void AddTocart(Action<ActiveProduct, int> action)
        {
            action(ActiveProduct, Convert.ToInt32(Count));
            Count = "";
        }

        private bool CanAddToCart()
        {

            bool isInt = Int32.TryParse(Count, out int value);
            return isInt && value <= Quantity;
        }
    }
}
