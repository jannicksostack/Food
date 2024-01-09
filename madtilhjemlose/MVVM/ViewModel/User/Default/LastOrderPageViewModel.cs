﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace madtilhjemlose.MVVM.ViewModel.User.Default
{
    internal class LastOrderPageViewModel : ObservableValidator
    {
        private INavigation navigation;

        public LastOrderPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
        }
    }
}
