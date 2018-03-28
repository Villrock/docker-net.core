using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QFlow.Data.Managers;
using QFlow.Extensions;
using QFlow.Models.DataModels.Alerts;
using QFlow.Models.DataModels.Users;
using QFlow.Models.ManageViewModels;
using QFlow.Services;

namespace QFlow.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManageController : Controller
    {
        private readonly UserManager _userManager;
        private readonly SettingsService _settings;
        private readonly AlertTypeManager _alertTypeManager;

        public ManageController(UserManager userManager, SettingsService settings, AlertTypeManager alertTypeManager)
        {
            _userManager = userManager;
            _settings = settings;
            _alertTypeManager = alertTypeManager;
        }

        #region Area Clients

        [HttpGet]
        public async Task<IActionResult> Clients(int page = 1)
        {
            var startIndex = (page - 1) * _settings.PageSize;
            var totalCount = await _userManager.GetClientsCountAsync();

            var viewModel = new ManageViewModel()
            {
                Scope = "clients",
                PageInfo = _settings.GetPagingInfo(page, startIndex, totalCount, Request.ContentUrl())
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> ClientList(int page = 1)
        {
            var startIndex = (page - 1) * _settings.PageSize;
            var clients = await _userManager.GetClientsPageAsync(startIndex, _settings.PageSize);

            return Json(
                clients.Select(_ => new
                {
                    _.Id,
                    _.Email,
                    FullName = _.ToString()
                }));
        }

        [HttpGet]
        public async Task<JsonResult> GetClient(int id)
        {
            var user = await _userManager.GetClientAsync(id);
            return Json(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email
            });
        }

        [HttpPost]
        public async Task SaveClient(NewUserViewModel model)
        {
            if(model.Id == 0)
            {
                await _userManager.CreateClientAsync(MakeUser(model), model.Password);
            }
            else
            {
                await _userManager.UpdateClientAsync(model.Id, MakeUser(model));
            }
        }

        [HttpPost]
        public async Task DeleteClient(int id)
        {
            await _userManager.DeleteClientAsync(id);
        }

        #endregion

        #region Area Managers

        [HttpGet]
        public async Task<IActionResult> Managers(int page = 1)
        {
            ViewData["VueId"] = "managers";
            var startIndex = (page - 1) * _settings.PageSize;
            var totalCount = await _userManager.GetManagersCountAsync();

            var viewModel = new ManageViewModel
            {
                Scope = "managers",
                PageInfo = _settings.GetPagingInfo(page, startIndex, totalCount, Request.ContentUrl())
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> ManagerList(int page = 1)
        {
            var startIndex = (page - 1) * _settings.PageSize;
            var managers = await _userManager.GetManagersPageAsync(startIndex, _settings.PageSize);

            return Json(
                managers.Select(_ => new
                {
                    _.Id,
                    _.Email,
                    FullName = _.ToString()
                }));
        }

        [HttpGet]
        public async Task<JsonResult> GetManager(int id)
        {
            var user = await _userManager.GetManagerAsync(id);
            return Json(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email
            });
        }

        [HttpPost]
        public async Task SaveManager(NewUserViewModel model)
        {
            if(model.Id == 0)
            {
                await _userManager.CreateManagerAsync(MakeUser(model), model.Password);
            }
            else
            {
                await _userManager.UpdateManagerAsync(model.Id, MakeUser(model));
            }
        }

        [HttpPost]
        public async Task DeleteManager(int id)
        {
            await _userManager.DeleteManagerAsync(id);
        }

        #endregion

        #region Alerts

        [HttpGet]
        public IActionResult Alerts()
        {
            var viewModel = new ManageViewModel()
            {
                Scope = "alerts"
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> AlertList()
        {
            var alerts = await _alertTypeManager.GetAllAlertTypes();
            var notifyTimes = await _alertTypeManager.GetPeriodTimes();

            var alertList = new List<AlertTypeViewModel>();
            foreach(var alert in alerts)
                alertList.Add(await BuildAlertTypeViewModel(alert));

            return Json(new
            {
                Alerts = alertList,
                NotifyTimes = notifyTimes
            });
        }

        [HttpGet]
        public async Task<JsonResult> GetAlert(int id)
        {
            var alert = await _alertTypeManager.GetAlertType(id);
            return Json(await BuildAlertTypeViewModel(alert));
        }

        [HttpPost]
        public async Task SaveAlert(AlertTypeViewModel model)
        {
            await _alertTypeManager.Update(model.Id, MakeAlertType(model), model.NotifyDays);
        }

        #endregion

        #region Helpers

        private static Func<AlertType, bool> MakeAlertType(AlertTypeViewModel model)
        {
            return alertType =>
            {
                if(model == null)
                {
                    return false;
                }
                alertType.IsSendingEmail = model.IsSendingEmail;
                alertType.Message = model.Message;
                return true;
            };
        }

        private static Func<User, bool> MakeUser(UserViewModel model)
        {
            return user =>
            {
                if(model == null)
                {
                    return false;
                }
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                return true;
            };
        }

        private async Task<AlertTypeViewModel> BuildAlertTypeViewModel(AlertType alert)
        {
            return new AlertTypeViewModel
            {
                Id = alert.Id,
                Name = alert.Name,
                Message = alert.Message,
                IsSendingEmail = alert.IsSendingEmail,
                NotifyDays = from time in await _alertTypeManager.GetAlertTypePeriodTimesByAlertType(alert.Id)
                    select time.PeriodTimeId
            };
        }

        #endregion
    }
}
