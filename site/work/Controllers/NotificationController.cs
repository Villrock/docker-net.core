using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QFlow.Data.Managers;
using QFlow.Extensions;
using QFlow.Models.NotificationViewModels;
using QFlow.Services;

namespace QFlow.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly AlertNotificationManager _alertNotificationManager;
        private readonly SettingsService _settings;

        public NotificationController(AlertNotificationManager alertNotificationManager, SettingsService settings)
        {
            _alertNotificationManager = alertNotificationManager;
            _settings = settings;
        }

        public async Task<ActionResult> Index(int page = 1)
        {
            var startIndex = (page - 1) * _settings.PageSize;
            var totalCount = await _alertNotificationManager.GetAlertCountAsync(User.Identity.Name);
            var items = await _alertNotificationManager.GetAlertsNotificationPageAsync(User.Identity.Name, startIndex, _settings.PageSize);

            var viewModel = new NotificationViewModel
            {
                Items = from alertNotification in items 
                        select new NotificationItemViewModel
                                {
                                    Date = alertNotification.Date,
                                    Text = alertNotification.Text
                                },
                PageInfo = _settings.GetPagingInfo(page, startIndex, totalCount, Request.ContentUrl())
            };
            return View(viewModel);
        }

        [HttpPost("/[controller]/SetAsReaded")]
        public async Task SetAsReaded()
        {
            await _alertNotificationManager.SetIsReadAlertNotificationAsync(User.Identity.Name);
        }
    }
}