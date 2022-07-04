using System.Collections.Generic;

namespace EngineSigma.Editor.MVVM.Model;

public class ProjectModel
{
    private string ProjectTitle { get; set; }

    private float Size { get; set; }

    public ProjectModel(string title, float size)
    {
        ProjectTitle = title;
        Size = size;
    }
}