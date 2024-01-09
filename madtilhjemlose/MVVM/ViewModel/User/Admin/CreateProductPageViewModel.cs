﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin
{
    internal class CreateProductPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public CreateProductPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}