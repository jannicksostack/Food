﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class OrdersPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public OrdersPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}
