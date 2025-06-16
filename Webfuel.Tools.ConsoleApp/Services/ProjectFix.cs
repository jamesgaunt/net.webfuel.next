using Webfuel.Domain;
using Webfuel.Domain.StaticData;

namespace Webfuel.Tools.ConsoleApp;

public interface IProjectFix
{
    Task FixProjects();
}

[Service(typeof(IProjectFix))]
internal class ProjectFix : IProjectFix
{
    private readonly IProjectRepository _projectRepository;
    private readonly IStaticDataService _staticDataService;
    private readonly IProjectEnrichmentService _projectEnrichmentService;

    public ProjectFix(
        IProjectRepository projectRepository,
        IStaticDataService staticDataService,
        IProjectEnrichmentService projectEnrichmentService)
    {
        _projectRepository = projectRepository;
        _staticDataService = staticDataService;
        _projectEnrichmentService = projectEnrichmentService;
    }

    public async Task FixProjects()
    {
        var projects = await _projectRepository.SelectProject();
        var staticData = await _staticDataService.SelectResearcherProfessionalBackground();

        foreach (var original in projects)
        {
            var updated = original.Copy();

            if (updated.ProfessionalBackgroundIds.Any(p => staticData.Any(q => q.Id == p)))
            {
                Console.WriteLine("Updated is correct on " + original.Number);
            }
            else if (updated.LegacyProfessionalBackgroundIds.Any(p => staticData.Any(q => q.Id == p)))
            {
                updated.ProfessionalBackgroundIds = updated.LegacyProfessionalBackgroundIds;
                Console.WriteLine("Legacy is correct on " + original.Number);
                await _projectRepository.UpdateProject(original: original, updated: updated);
            }
            else
            {
                Console.WriteLine("No idea on " + original.Number);
            }
        }
    }


    List<Guid> MapProfessionalBackground(List<Guid> input)
    {
        var output = new List<Guid>();

        foreach (var item in input)
        {
            if (item == ProfessionalBackgroundEnum.AlliedHealthcareProfessional)
                output.Add(ResearcherProfessionalBackgroundEnum.NurseMidwifeAHP);

            else if (item == ProfessionalBackgroundEnum.Doctor)
                output.Add(ResearcherProfessionalBackgroundEnum.ClinicalAcademic);

            else if (item == ProfessionalBackgroundEnum.Dentist)
                output.Add(ResearcherProfessionalBackgroundEnum.ClinicalAcademic);

            else if (item == ProfessionalBackgroundEnum.Midwife)
                output.Add(ResearcherProfessionalBackgroundEnum.NurseMidwifeAHP);

            else if (item == ProfessionalBackgroundEnum.Psychologist)
                output.Add(ResearcherProfessionalBackgroundEnum.ClinicalAcademic);

            else if (item == ProfessionalBackgroundEnum.Nurse)
                output.Add(ResearcherProfessionalBackgroundEnum.NurseMidwifeAHP);

            else if (item == ProfessionalBackgroundEnum.PublicHealthSpecialist)
                output.Add(ResearcherProfessionalBackgroundEnum.PublicHealthPractitioner);

            else if (item == ProfessionalBackgroundEnum.SocialCareSpecialist)
                output.Add(ResearcherProfessionalBackgroundEnum.SocialCarePractitioner);

            else if (item == ProfessionalBackgroundEnum.OtherHealthcareProfessional)
                output.Add(ResearcherProfessionalBackgroundEnum.OtherNHS);

            else if (item != ProfessionalBackgroundEnum.NotHealthcareProfessional)
                Console.WriteLine($"Unknown professional background: {item}");
        }

        return output;
    }
}
