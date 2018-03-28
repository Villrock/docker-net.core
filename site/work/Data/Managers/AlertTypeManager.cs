using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QFlow.Models.DataModels.Alerts;

namespace QFlow.Data.Managers
{
    public class AlertTypeManager
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _contextDb;

        public AlertTypeManager(ApplicationDbContext context, ILogger<AlertTypeManager> logger)
        {
            _contextDb = context;
            _logger = logger;
        }

        public async Task<IEnumerable<AlertType>> GetAllAlertTypes()
        {
            _logger.LogInformation("Get all AlertTypes");
            return await _contextDb.AlertTypes.ToListAsync();
        }

        public async Task<AlertType> GetAlertType(int id)
        {
            _logger.LogInformation($"Get AlertType (id={id})");
            return await _contextDb.AlertTypes.FindAsync(id);
        }

        public async Task<IEnumerable<PeriodTime>> GetPeriodTimes()
        {
            _logger.LogInformation("Get all PeriodTimes");
            return await _contextDb.PeriodTimes.ToListAsync();
        }

        public async Task<IEnumerable<AlertTypePeriodTime>> GetAlertTypePeriodTimesByAlertType(int alertTypeId)
        {
            _logger.LogInformation("Get all AlertTypePeriodTime");
            return await _contextDb.AlertTypePeriodTime
                                    .Include(_ => _.PeriodTime)
                                    .Where(_ => _.AlertTypeId == alertTypeId)
                                    .ToListAsync();
        }

        public async Task Update(int id, Func<AlertType, bool> makeAlertType, IEnumerable<int> periodIds)
        {
            if(makeAlertType == null)
            {
                _logger.LogError("Func must be init (makeAlertType)");
                return;
            }

            var alertType = await _contextDb.AlertTypes.FindAsync(id);
            if(alertType == null)
            {
                _logger.LogError($"Alert type not found (id={id})");
                return;
            }
            _logger.LogInformation($"Updating AlertType (id={id})");
            if(makeAlertType(alertType))
            {
                _contextDb.AlertTypes.Update(alertType);
                _logger.LogInformation($"Updated AlertType (id={id})");
            }
            //save periods by alert
            if (periodIds != null)
            {
                var oldAlertsList = await _contextDb.AlertTypePeriodTime
                                                        .Where(_ => _.AlertTypeId == id)
                                                        .ToListAsync();
                if (oldAlertsList.Any())
                {
                    _logger.LogInformation($"Removing all periods by alerttype(id={oldAlertsList.Select(_ => _.Id)})");
                    _contextDb.AlertTypePeriodTime.RemoveRange(oldAlertsList);
                }
                foreach (var periodTimeId in periodIds)
                {
                    await _contextDb.AlertTypePeriodTime.AddAsync(new AlertTypePeriodTime
                    {
                        AlertTypeId = id,
                        PeriodTimeId = periodTimeId
                    });
                }
            }
            await _contextDb.SaveChangesAsync();
            _logger.LogInformation($"Saved all changes)");
        }
    }
}
