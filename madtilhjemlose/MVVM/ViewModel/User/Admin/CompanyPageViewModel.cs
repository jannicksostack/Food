using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using madtilhjemlose.MVVM.View.User.Admin;
using System.Windows.Input;

namespace madtilhjemlose.MVVM.ViewModel.User.Admin;

internal class CompanyPageViewModel : ObservableValidator
{

    private INavigation navigation;

    public ICommand SearchCommand { get; set; } 

    public CompanyPageViewModel(INavigation navigation)
    { 
        this.navigation = navigation;
        SearchCommand = new RelayCommand(Search);

    }
   private void Search()
    {

    }
}