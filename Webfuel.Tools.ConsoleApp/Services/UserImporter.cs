using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Domain;
using Webfuel.Domain.StaticData;
using Webfuel.Excel;

namespace Webfuel.Tools.ConsoleApp
{
    public interface IUserImporter
    {
        Task Import();
    }

    [Service(typeof(IUserImporter))]
    internal class UserImporter: IUserImporter
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly ISupportTeamUserRepository _supportTeamUserRepository;
        private readonly IStaticDataService _staticDataService;

        public UserImporter(
            IUserRepository userRepository, 
            IStaticDataService staticDataService, 
            IUserGroupRepository userGroupRepository,
            ISupportTeamUserRepository supportTeamUserRepository)
        {
            _userRepository = userRepository;
            _staticDataService = staticDataService;
            _userGroupRepository = userGroupRepository;
            _supportTeamUserRepository = supportTeamUserRepository;

            UserGroups = _userGroupRepository.SelectUserGroup().GetAwaiter().GetResult();
            SupportTeams = _supportTeamUserRepository.SelectSupportTeamUser().GetAwaiter().GetResult();
            StaticData = _staticDataService.GetStaticData().GetAwaiter().GetResult();
        }

        List<UserGroup> UserGroups;

        List<SupportTeamUser> SupportTeams;

        IStaticDataModel StaticData;

        public async Task Import()
        {
            Console.WriteLine(@"Opening D:\users.xlsx");
            var workbook = ExcelWorkbook.Load(@"D:\users.xlsx");

            Console.WriteLine(@"Opened");

            var ws = workbook.Worksheet(1);
            var row = 2;

            while (!ws.IsRowEmpty(row))
            {
                await ProcessRow(ws, row);
                row++;
            }

        }

        async Task ProcessRow(ExcelWorksheet ws, int row)
        {
            var email = ws.Cell(row, "EMAIL").GetValue<String>()?.Trim()?.ToLower();
            if (String.IsNullOrEmpty(email))
                return;

            var existing = await _userRepository.GetUserByEmail(email);
            if(existing == null)
            {
                existing = await _userRepository.InsertUser(new User { 
                    Email = email,
                    UserGroupId = UserGroup(ws, row).Id,
                });
            }

            existing.UserGroupId = UserGroup(ws, row).Id;
            existing.SiteId = Site(ws, row).Id;
            existing.Title = TEXT(ws, row, "TITLE");
            existing.FirstName = TEXT(ws, row, "FIRST NAME");
            existing.LastName = TEXT(ws, row, "SURNAME");
            existing.UniversityJobTitle = TEXT(ws, row, "UNIVERSITY JOB TITLE");

            if (Decimal.TryParse(TEXT(ws, row, "FTE"), out var fte))
                existing.FullTimeEquivalentForRSS = fte;

            existing.StartDateForRSS = DATE(ws, row, "RSS START DATE");
            existing.EndDateForRSS = DATE(ws, row, "RSS END DATE");

            await _userRepository.UpdateUser(existing);

            if (!String.IsNullOrEmpty(TEXT(ws, row, "TRIAGE")))
                await AddUserToTeam(existing.Id, SupportTeamEnum.TriageTeam);

            if(TEXT(ws, row, "RSS EXPERTS TEAM").Contains("Quant"))
                await AddUserToTeam(existing.Id, SupportTeamEnum.ExpertQuantitativeTeam);

            if (TEXT(ws, row, "RSS EXPERTS TEAM").Contains("Qual"))
                await AddUserToTeam(existing.Id, SupportTeamEnum.ExpertQualitativeTeam);

            if (!String.IsNullOrEmpty(TEXT(ws, row, "RSS CTU TEAM")))
                await AddUserToTeam(existing.Id, SupportTeamEnum.CTUTeam);

            if (!String.IsNullOrEmpty(TEXT(ws, row, "RSS PPIE")))
                await AddUserToTeam(existing.Id, SupportTeamEnum.PPIETeam);
        }

        async Task AddUserToTeam(Guid userId, Guid supportTeamId)
        {
            if (!SupportTeams.Any(p => p.UserId == userId && p.SupportTeamId == supportTeamId)) 
                SupportTeams.Add(await _supportTeamUserRepository.InsertSupportTeamUser(new SupportTeamUser { UserId = userId, SupportTeamId = supportTeamId }));
        }

        UserGroup UserGroup(ExcelWorksheet ws, int row)
        {
            if (TEXT(ws, row, "Access").ToLower() == "admin")
                return UserGroups.First(p => p.Name == "Administrator");
            return UserGroups.First(p => p.Name == "User");
        }

        Site Site(ExcelWorksheet ws, int row)
        {
            var t = TEXT(ws, row, "SITE");
            return StaticData.Site.First(p => p.Name == t);
        }

        string TEXT(ExcelWorksheet ws, int row, string header)
        {
            return ws.Cell(row, header).GetValue<String>()?.Trim() ?? String.Empty;
        }

        DateOnly? DATE(ExcelWorksheet ws, int row, string header)
        {
            if (ws.Cell(row, header).TryGetValue<DateTime>(out var result))
                return new DateOnly(result.Year, result.Month, result.Day);
            return null;
        }
    }
}
