using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webfuel.Domain;

// To remove once added to the database
public partial class ProjectSubmission
{
    public Guid? FundingStreamId { get; set; }
}
