﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webfuel.Reporting;

namespace Webfuel.Domain
{
    public class ProjectReportContext
    {
        private readonly Project _item;
        private readonly IServiceProvider _serviceProvider;

        public ProjectReportContext(Project item, IServiceProvider serviceProvider)
        {
            _item = item;
            _serviceProvider = serviceProvider;
        }
    }
}