using System.Collections.ObjectModel;
using System.Reflection.Emit;
using EngineSigma.Editor.MVVM.Model;

namespace EngineSigma.Editor.MVVM.ViewModel;

public class ProjectsViewModel
{
    private ObservableCollection<ProjectModel> ProjectsList { get; set; }

    public ProjectsViewModel()
    {
        ProjectsList = new ObservableCollection<ProjectModel>();
        for (int i = 0; i < 10; i++)
        {
            ProjectsList.Add(new ProjectModel("tim",10f));
        }
    }
}