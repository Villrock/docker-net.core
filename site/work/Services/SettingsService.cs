using Microsoft.Extensions.Configuration;
using QFlow.Models;

namespace QFlow.Services
{
    public class SettingsService
    {
        private readonly IConfiguration _configuration;
        public SettingsService( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public string ConnectionString => _configuration.GetConnectionString("DefaultConnection");

        public int PageSize
        {
            get
            {
                var settingsSection = _configuration.GetSection("GlobalSettings");
                return settingsSection?.GetValue<int>("PageSize") ?? 10;
            }
        }

        public string SendGridKey
        {
            get
            {
                var settingsSection = _configuration.GetSection("EmailSettings");
                return settingsSection?.GetValue<string>("SendGridKey");
            }
        }

        public string SendGridUser
        {
            get
            {
                var settingsSection = _configuration.GetSection("EmailSettings");
                return settingsSection?.GetValue<string>("SendGridUser");
            }
        }

        public string EmailFrom
        {
            get
            {
                var settingsSection = _configuration.GetSection("EmailSettings");
                return settingsSection?.GetValue("EmailFrom", "no-reply@qflow.com");
            }
        }

        public string SuperUser
        {
            get
            {
                var settingsSection = _configuration.GetSection("GlobalSettings");
                return settingsSection?.GetValue("SuperUser", "");
            }
        }

        public PagingInfo GetPagingInfo(int page, int startIndex, int totalCount, string contentUrl)
        {
            return new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = totalCount,
                StartIndex = startIndex,
                ContentUrl = contentUrl
            };
        }
    }
}
