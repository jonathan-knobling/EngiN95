using EngineSigma.Editor.Core;

namespace EngineSigma.Editor.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{

    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand ProjectsViewCommand { get; set; }
    private HomeViewModel HomeVm { get; set; }

    private ProjectsViewModel ProjectsVm { get; set; }

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

    public MainViewModel()
    {
        HomeVm = new HomeViewModel();
        ProjectsVm = new ProjectsViewModel();
        
        CurrentView = HomeVm;

        HomeViewCommand = new RelayCommand(o =>
        {
            CurrentView = HomeVm;
        });
        
        ProjectsViewCommand = new RelayCommand(o =>
        {
            CurrentView = ProjectsVm;
        });
    }
}