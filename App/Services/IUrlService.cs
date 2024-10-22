using Domin.Model;
using Domin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    public interface IUrlService
    {
        Task<List<UrlModel>> GetAllUrlsAsync();
        //Task<UrlModel> GetUrlByIdAsync(int id);
        void DeleteUrl(int id);
        //Task<ResultUrl> CreateUrl(CreateUrlViewModel model);
        Task<ResultUrl> CreateUrl(CreateUrlViewModel model, string userId);
        Task UpdatePrioritiesAsync(List<UpdateUrlViewModel> updatedPriorities); // متد جدید
        Task UpdatePrioritiesAsync(); // متد جدید


    }
}
