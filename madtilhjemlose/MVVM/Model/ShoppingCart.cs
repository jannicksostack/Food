using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace madtilhjemlose.MVVM.Model
{
    public partial class ShoppingCart : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ShoppingCartItem> items = new();

        [ObservableProperty]
        private decimal total;

        public ShoppingCart()
        {
            Items.CollectionChanged += (_, args) =>
            {
                UpdateTotal();
            };
        }

        private void UpdateTotal()
        {
            Total = Items
                .Select(x => x.Total)
                .Sum();
        }

        public void Add(ActiveProduct activeProduct, int quantity)
        {
            ShoppingCartItem? current = Items.FirstOrDefault(x => activeProduct == x.ActiveProduct);

            if (current is ShoppingCartItem item)
            {
                item.Quantity += quantity;
            } else
            {
                Items.Add(new ShoppingCartItem(activeProduct, quantity));
            }
        }

        public void Remove(ShoppingCartItem item, int? quantity = null)
        {
            if (quantity is null || quantity >= item.Quantity)
            {
                Items.Remove(item);
            } 
            else
            {
                item.Quantity -= (int) quantity;
            }
        }

        internal void Clear()
        {
            Items.Clear();
        }
    }

    public partial class ShoppingCartItem : ObservableObject
    {
        [ObservableProperty]
        private ActiveProduct activeProduct;

        [ObservableProperty]
        private int quantity;

        partial void OnQuantityChanged(int value)
        {
            UpdateTotal();
        }

        [ObservableProperty]
        private decimal total;

        public ShoppingCartItem(ActiveProduct activeProduct, int quantity = 0)
        {
            ActiveProduct = activeProduct;
            Quantity = quantity;
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            Total = ActiveProduct.Price * Quantity;
        }
    }
}
