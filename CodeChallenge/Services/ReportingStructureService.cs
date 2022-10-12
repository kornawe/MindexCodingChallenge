using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IReportingStructureRepository _reportingStructureRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IReportingStructureRepository reportingStructureRepository)
        {
            _reportingStructureRepository = reportingStructureRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public ReportingStructure GetByEmployeeId(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                var reportingStructure = _reportingStructureRepository.GetByEmployeeId(id);
                return reportingStructure;
            }
            return null;
        }
    }
}
