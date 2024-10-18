using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Webfuel.Common;

namespace Webfuel.Domain
{
    public class SelectProjectSupportFile : IRequest<List<ProjectSupportFile>>
    {
        public required Guid ProjectId { get; set; }
    }

    internal class SelectProjectSupportFileHandler : IRequestHandler<SelectProjectSupportFile, List<ProjectSupportFile>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFileStorageService _fileStorageService;

        public SelectProjectSupportFileHandler(
            IProjectRepository projectRepository,
            IFileStorageService fileStorageService)
        {
            _projectRepository = projectRepository;
            _fileStorageService = fileStorageService;
        }
  
        public async Task<List<ProjectSupportFile>> Handle(SelectProjectSupportFile request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.RequireProject(request.ProjectId);

            var files = await _fileStorageService.QueryFiles(new QueryFileStorageEntry
            {
                FileStorageGroupId = project.FileStorageGroupId,
                Skip = 0,
                Take = 200
            });

            return files.Items.Select(p => new ProjectSupportFile
            {
                Id = p.Id,
                FileName = p.FileName,
                SizeBytes = p.SizeBytes,
                UploadedAt = p.UploadedAt
            }).ToList();
        }
    }
}
