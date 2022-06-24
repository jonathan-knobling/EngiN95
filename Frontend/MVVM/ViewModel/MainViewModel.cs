using Frontend.Core;

namespace Frontend.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{
    public HomeViewModel HomeVM { get; set; }

    private object _currentView;

    public object CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value; 
            OnPropertyChanged();
        }
    }
    
    public MainViewModel(object currentView)
    {
        _currentView = currentView;
        HomeVM = new HomeViewModel();
        CurrentView = HomeVM;
    }
}