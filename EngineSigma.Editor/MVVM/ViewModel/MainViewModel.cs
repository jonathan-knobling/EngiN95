using EngineSigma.Editor.Core;
using EngineSigma.Editor.MVVM.View;

namespace EngineSigma.Editor.MVVM.ViewModel;

public class MainViewModel : ObservableObject
{

    public RelayCommand HomeViewCommand { get; set; }
    public RelayCommand ProjectsViewCommand { get; set; }
    public RelayCommand FeaturesViewCommand { get; set; }
    public RelayCommand AboutUsViewCommand { get; set; }
    
    private HomeViewModel HomeVm { get; set; }
    private FeaturesViewModel FeaturesVm { get; set; }
    private AboutUsViewModel AboutUsVm { get; set; }
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
        FeaturesVm = new FeaturesViewModel();
        AboutUsVm = new AboutUsViewModel();
        
        CurrentView = HomeVm;

        HomeViewCommand = new RelayCommand(o =>
        {
            CurrentView = HomeVm;
        });
        
        ProjectsViewCommand = new RelayCommand(o =>
        {
            CurrentView = ProjectsVm;
        });

        FeaturesViewCommand = new RelayCommand(o =>
        {
            CurrentView = FeaturesVm;
        });

        AboutUsViewCommand = new RelayCommand(o =>
        {
            CurrentView = AboutUsVm;
        });
    }
}