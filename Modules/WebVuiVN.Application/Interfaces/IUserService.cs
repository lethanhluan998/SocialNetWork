using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebVuiVN.Application.ViewModels.System;
using WebVuiVN.Utilities;

namespace WebVuiVN.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVm);

        Task DeleteAsync(string id);
        
        Task<List<AppUserViewModel>> GetAllAsync();

        PagedResult<AppUserViewModel> GetAllPagingAsync(string keyword, int page, int pageSize);

        Task<AppUserViewModel> GetById(string id);
        Task<AppUserViewModel> GetByName(string userName);

        Task UpdateAsync(AppUserViewModel userVm);
    }
}
