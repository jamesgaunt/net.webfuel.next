using Riok.Mapperly.Abstractions;

namespace Webfuel.Domain;

internal static class ProjectMapper
{
    public static Project Apply(UpdateProject request, Project existing)
    {
        var updated = existing.Copy();
        Mapper.Apply(request, updated);
        return updated;
    }

    public static Project Apply(UpdateProjectRequest request, Project existing)
    {
        var updated = existing.Copy();
        Mapper.Apply(request, updated);
        return updated;
    }

    public static Project Apply(UpdateProjectResearcher request, Project existing)
    {
        var updated = existing.Copy();
        Mapper.Apply(request, updated);
        return updated;
    }

    public static Project Apply(UpdateProjectSupportSettings request, Project existing)
    {
        var updated = existing.Copy();
        Mapper.Apply(request, updated);
        return updated;
    }

    static ProjectMapperImpl Mapper => new ProjectMapperImpl();
}

[Mapper]
internal partial class ProjectMapperImpl
{
    [MapperIgnoreTarget(nameof(Project.Id))]
    [MapperIgnoreTarget(nameof(Project.StatusId))]
    public partial void Apply(UpdateProject request, Project existing);

    [MapperIgnoreTarget(nameof(Project.Id))]
    [MapperIgnoreTarget(nameof(Project.StatusId))]
    public partial void Apply(UpdateProjectRequest request, Project existing);

    [MapperIgnoreTarget(nameof(Project.Id))]
    [MapperIgnoreTarget(nameof(Project.StatusId))]
    public partial void Apply(UpdateProjectResearcher request, Project existing);

    [MapperIgnoreTarget(nameof(Project.Id))]
    [MapperIgnoreTarget(nameof(Project.StatusId))]
    public partial void Apply(UpdateProjectSupportSettings request, Project existing);
}
