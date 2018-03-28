using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QFlow.Models.DataModels;
using QFlow.Models.DataModels.Emails;

namespace QFlow.Data.Managers
{
    public class MessageManager
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _contextDb;

        public MessageManager(ApplicationDbContext context, ILogger<MessageManager> logger)
        {
            _contextDb = context;
            _logger = logger;
        }

        /// <summary>
        ///  Get didnt send notifications
        /// </summary>
        public async Task<IEnumerable<Message>> GetMessagesByStatusAsync(int statusId)
        {
            _logger.LogInformation($"Get messages by status.");

            return await _contextDb.RequestStatusMessages
                                .Include(_=>_.Message)
                                    .Where(_ => _.RequestStatusId == statusId)
                                        .Select(_=>_.Message)
                                            .ToListAsync();
        }

    }
}
