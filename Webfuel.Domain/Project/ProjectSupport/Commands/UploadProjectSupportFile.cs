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
    public class UploadProjectSupportFile : IRequest<ProjectSupportFile>
    {
        public required Guid ProjectId { get; set; }

        public required IFormFile FormFile { get; set; }
    }

    internal class UploadProjectSupportFileHandler : IRequestHandler<UploadProjectSupportFile, ProjectSupportFile>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFileStorageService _fileStorageService;

        public UploadProjectSupportFileHandler(
            IProjectRepository projectRepository,
            IFileStorageService fileStorageService)
        {
            _projectRepository = projectRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<ProjectSupportFile> Handle(UploadProjectSupportFile request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.RequireProject(request.ProjectId);

            var result = await _fileStorageService.UploadFile(new UploadFileStorageEntry
            {
                FileStorageGroupId = project.FileStorageGroupId,
                FormFile = request.FormFile,
            });

            return new ProjectSupportFile
            {
                Id = result.Id,
                FileName = result.FileName,
                SizeBytes = result.SizeBytes,
                UploadedAt = result.UploadedAt
            };
        }
    }
}
