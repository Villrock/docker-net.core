using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QFlow.Models.DataModels;
using QFlow.Models.DataModels.Users;

namespace QFlow.Data.Managers
{
    public class UserManager
    {
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _contextDb;
        private readonly UserManager<ApplicationUser> _userIdentityManager;
        private readonly IServiceProvider _serviceProvider;

        public UserManager(
            ApplicationDbContext context, 
            ILogger<UserManager> logger, 
            UserManager<ApplicationUser> userIdentityManager, 
            IServiceProvider serviceProvider)
        {
            _contextDb = context;
            _logger = logger;
            _userIdentityManager = userIdentityManager;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///  Get Clients for page
        /// </summary>
        public async Task<IEnumerable<User>> GetClientsPageAsync(int startIndex, int pageCount)
        {
            _logger.LogInformation($"Get clients page (startIndex={startIndex}, pageCount={pageCount})");

            return await _contextDb.Clients.Skip(startIndex).Take(pageCount).ToListAsync();
        }

        /// <summary>
        ///  Get Mangers for page
        /// </summary>
        public async Task<IEnumerable<User>> GetManagersPageAsync(int startIndex, int pageCount)
        {
            _logger.LogInformation($"Get managers page (startIndex={startIndex}, pageCount={pageCount})");

            return await _contextDb.Managers.Skip(startIndex).Take(pageCount).ToListAsync();
        }

        /// <summary>
        ///  Get all Mangers
        /// </summary>
        public async Task<IEnumerable<User>> GetManagersPageAsync()
        {
            _logger.LogInformation("Get all managers.");

            return await _contextDb.Managers.ToListAsync();
        }

        /// <summary>
        ///  Create Clients and Identity
        /// </summary>
        public async Task CreateClientAsync(Func<Client, bool> makeClient, string password)
        {
            if(makeClient == null)
            {
                throw new NullReferenceException("MakeClient func can't be NULL.");
            }
            var client = new Client();
            if(makeClient(client))
            {
                _logger.LogInformation($"Create client (email={client.Email})");

                await CreateIdentity(client.FirstName, client.LastName, client.Email, password, Roles.Client);

                await _contextDb.Clients.AddAsync(client);
                await _contextDb.SaveChangesAsync();

                _logger.LogInformation($"Created client (id={client.Id}, email={client.Email})");
            }
        }

        /// <summary>
        ///  Create Manager and Identity
        /// </summary>
        public async Task CreateManagerAsync(Func<Manager, bool> makeManager, string password)
        {
            if(makeManager == null)
            {
                throw new NullReferenceException("MakeManager func can't be NULL.");
            }
            var manager = new Manager();
            if(makeManager(manager))
            {
                _logger.LogInformation($"Create manager (email={manager.Email})");

                await CreateIdentity(manager.FirstName, manager.LastName, manager.Email, password, Roles.Manager);

                await _contextDb.Managers.AddAsync(manager);
                await _contextDb.SaveChangesAsync();

                _logger.LogInformation($"Created manager (id={manager.Id}, email={manager.Email})");
            }
        }

        /// <summary>
        ///  Update Manager and Identity
        /// </summary>
        public async Task UpdateClientAsync(int id, Func<Client, bool> makeClient)
        {
            if(makeClient == null)
            {
                throw new NullReferenceException("MakeManager func can't be NULL.");
            }
            var client = await _contextDb.Clients.FindAsync(id);
            if(client == null)
            {
                throw new NullReferenceException($"Manager not found (id={id})");
            }
            var oldEmail = client.Email;
            if(makeClient(client))
            {
                _logger.LogInformation($"Update manager (id={client.Id}email={client.Email})");

                await UpdateIdentity(oldEmail, client.FirstName, client.LastName, client.Email);

                _contextDb.Clients.Update(client);
                await _contextDb.SaveChangesAsync();

                _logger.LogInformation($"Created manager (id={client.Id}email={client.Email})");
            }
        }

        /// <summary>
        ///  Update Manager and Identity
        /// </summary>
        public async Task UpdateManagerAsync(int id, Func<Manager, bool> makeManager)
        {
            if(makeManager == null)
            {
                throw new NullReferenceException("MakeManager func can't be NULL.");
            }
            var manager = await _contextDb.Managers.FindAsync(id);
            if(manager == null)
            {
                throw new NullReferenceException($"Manager not found (id={id})");
            }
            var oldEmail = manager.Email;
            if(makeManager(manager))
            {
                _logger.LogInformation($"Update manager (id={manager.Id}email={manager.Email})");

                await UpdateIdentity(oldEmail, manager.FirstName, manager.LastName, manager.Email);

                _contextDb.Managers.Update(manager);
                await _contextDb.SaveChangesAsync();

                _logger.LogInformation($"Created manager (id={manager.Id}email={manager.Email})");
            }
        }

        /// <summary>
        ///  Delete Manager and Identity
        /// </summary>
        public async Task DeleteClientAsync(int id)
        {
            var client = await _contextDb.Clients.FindAsync(id);
            if(client == null)
            {
                return;
            }
            _logger.LogInformation($"Deleting client (id={client.Id} email={client.Email})");

            await DeleteIdentity(client.Email);

            _contextDb.Clients.Remove(client);
            await _contextDb.SaveChangesAsync();

            _logger.LogInformation($"Deleted client (id={client.Id} email={client.Email})");
        }

        /// <summary>
        ///  Delete Manager and Identity
        /// </summary>
        public async Task DeleteManagerAsync(int id)
        {
            var manager = await _contextDb.Managers.FindAsync(id);
            if(manager == null)
            {
                return;
            }
            _logger.LogInformation($"Deleting manager (id={manager.Id} email={manager.Email})");

            await DeleteIdentity(manager.Email);

            _contextDb.Managers.Remove(manager);
            await _contextDb.SaveChangesAsync();

            _logger.LogInformation($"Deleted manager (id={manager.Id} email={manager.Email})");
        }

        /// <summary>
        ///  Get Clients for page
        /// </summary>
        public async Task<int> GetClientsCountAsync()
        {
            _logger.LogInformation("Get clients count.");

            return await _contextDb.Clients.CountAsync();
        }

        /// <summary>
        ///  Get Mangers for page
        /// </summary>
        public async Task<int> GetManagersCountAsync()
        {
            _logger.LogInformation("Get managers count");

            return await _contextDb.Managers.CountAsync();
        }

        /// <summary>
        ///  Get client by id
        /// </summary>
        public async Task<User> GetClientAsync(int id)
        {
            _logger.LogInformation($"Get client ({id})");

            return await _contextDb.Clients.FindAsync(id);
        }

        /// <summary>
        ///  Get Manger by id
        /// </summary>
        public async Task<User> GetManagerAsync(int id)
        {
            _logger.LogInformation($"Get manager ({id})");

            return await _contextDb.Managers.FindAsync(id);
        }

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            _logger.LogInformation("Get client by email (email={0})", email);
            return await _contextDb.Clients.FirstOrDefaultAsync(_ => _.Email == email);
        }

        public async Task<Manager> GetManagerByEmailAsync(string email)
        {
            _logger.LogInformation("Get client by email (email={0})", email);
            return await _contextDb.Managers.FirstOrDefaultAsync(_ => _.Email == email);
        }

        public async Task<IEnumerable<Manager>> GetAllManagers()
        {
            _logger.LogInformation("Get all managers.");
            return await _contextDb.Managers.ToListAsync();
        }

        #region Implementation routines

        private async Task CreateIdentity(string firstName, string lastName, string email, string password, Roles role)
        {
            var clientUser = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email
            };

            var result = await _userIdentityManager.CreateAsync(clientUser, password);
            if(result.Succeeded)
            {
                await _userIdentityManager.AddToRoleAsync(clientUser, role.ToString());
            }
        }

        private async Task UpdateIdentity(string oldEmail, string firstName, string lastName, string newEmail)
        {
            var user = await _userIdentityManager.FindByEmailAsync(oldEmail);
            if(user == null)
            {
                return;
            }

            user.FirstName = firstName;
            user.LastName = lastName;
            user.Email = user.UserName = newEmail;

            await _userIdentityManager.UpdateAsync(user);

            var isChangedEmail = oldEmail != newEmail;
            if (isChangedEmail)
            {
                var alertNotificationManager = _serviceProvider.GetRequiredService<AlertNotificationManager>();
                await alertNotificationManager.UpdateEmail(oldEmail, newEmail);
            }
        }

        private async Task DeleteIdentity(string email)
        {
            var user = await _userIdentityManager.FindByEmailAsync(email);
            if(user == null)
            {
                return;
            }
            await _userIdentityManager.DeleteAsync(user);
        }

        #endregion
    }
}
