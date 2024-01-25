using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain
{
    public class DashboardModel
    {
        public List<DashboardSupportTeam> SupportTeams { get; } = new List<DashboardSupportTeam>();
    }

    public class DashboardSupportTeam
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public int OpenProjects { get; set; } 
    }
}
