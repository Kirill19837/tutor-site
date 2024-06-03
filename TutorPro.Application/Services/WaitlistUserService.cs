using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TutorPro.Application.Interfaces;
using TutorPro.Application.Models;
using TutorPro.Application.Models.RequestModel;

namespace TutorPro.Application.Services
{
    public class WaitlistUserService : IWaitlistUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<WaitlistUserService> _logger;
        private readonly IMapper _mapper;

        public WaitlistUserService(ApplicationDbContext context, ILogger<WaitlistUserService> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<WaitlistUsers>> GetWaitlistUsers()
        {
            var users = _context.WaitlistUsers.Where(u => u.DeletedDate == null).OrderByDescending(u => u.Id);

            if(users == null)
            {
                _logger.LogError("Users was not found");
                throw new Exception("Users was not found");
            }

            return await users.ToListAsync();               
        }

        public async Task<List<WaitlistUsers>> GetDeletedWaitlistUsers()
        {
            var users = _context.WaitlistUsers.Where(u => u.DeletedDate != null);

            if (users == null)
            {
                _logger.LogError("Users was not found");
                throw new Exception("Users was not found");
            }

            return await users.ToListAsync();
        }

        public async Task AddWaitlistUser(AddWailtListUserModel model)
        {
            if(model == null)
            {
                _logger.LogError("Add waitlistUserModel is null");
                throw new Exception("Add waitlistUserModel is null");
            }

            var dbModel = _mapper.Map<WaitlistUsers>(model);
            dbModel.CreateDate = DateTime.UtcNow;

            try
            {
                await _context.WaitlistUsers.AddAsync(dbModel);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"User {model.Name} - added to db");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user to the database");
                throw;
            }
        }

        public async Task HardRemoveWaitlistUserByIdRange(List<int> ids)
        {
            if(ids == null || !ids.Any())
            {
                _logger.LogError("WaitlistUsers IDs are null or empty");
                throw new Exception("WaitlistUsers IDs are null or empty");
            }

            try
            {
                foreach (int id in ids)
                {
                    var user = await _context.WaitlistUsers.FindAsync(id);

                    if (user != null)
                    {
                        _context.WaitlistUsers.Remove(user);
                    }
                    else
                    {
                        _logger.LogWarning($"WaitlistUser with ID {id} not found in the database.");
                    }
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"WaitlistUsers with IDs {string.Join(",", ids)} - removed from db");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing waitlist users from the database");
                throw;
            }
        }

        public async Task RemoveWaitlistUserByIdRange(List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                _logger.LogError("WaitlistUsers IDs are null or empty");
                throw new Exception("WaitlistUsers IDs are null or empty");
            }

            try
            {
                foreach (int id in ids)
                {
                    var user = await _context.WaitlistUsers.FindAsync(id);

                    if (user != null)
                    {
                        user.DeletedDate = DateTime.UtcNow;
                    }
                    else
                    {
                        _logger.LogWarning($"WaitlistUser with ID {id} not found in the database.");
                    }
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"WaitlistUsers with IDs {string.Join(",", ids)} - removed from db");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing waitlist users from the database");
                throw;
            }
        }

        public async Task RestoreWaitlistUserByIdRange(List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                _logger.LogError("WaitlistUsers IDs are null or empty");
                throw new Exception("WaitlistUsers IDs are null or empty");
            }

            try
            {
                foreach (int id in ids)
                {
                    var user = await _context.WaitlistUsers.FindAsync(id);

                    if (user != null)
                    {
                        user.DeletedDate = null;
                    }
                    else
                    {
                        _logger.LogWarning($"WaitlistUser with ID {id} not found in the database.");
                    }
                }

                await _context.SaveChangesAsync();

                _logger.LogInformation($"WaitlistUsers with IDs {string.Join(",", ids)} - restored");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while restoring waitlist users from the database");
                throw;
            }
        }

        public async Task RemoveWaitlistUserById(int id)
        {
            try
            {
                var user = await _context.WaitlistUsers.FindAsync(id);

                if (user != null)
                {
                    user.DeletedDate = DateTime.UtcNow;
                }
                else
                {
                    _logger.LogWarning($"WaitlistUser with ID {id} not found in the database.");
                }
                

                await _context.SaveChangesAsync();

                _logger.LogInformation($"WaitlistUsers with ID {id} - removed from db");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing waitlist users from the database");
                throw;
            }
        }

        public async Task HardRemoveWaitlistUserById(int id)
        {
            try
            {
                var user = await _context.WaitlistUsers.FindAsync(id);

                if (user != null)
                {
                    _context.WaitlistUsers.Remove(user);
                }
                else
                {
                    _logger.LogWarning($"WaitlistUser with ID {id} not found in the database.");
                }


                await _context.SaveChangesAsync();

                _logger.LogInformation($"WaitlistUsers with ID {id} - removed from db");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while removing waitlist users from the database");
                throw;
            }
        }
    }
}
