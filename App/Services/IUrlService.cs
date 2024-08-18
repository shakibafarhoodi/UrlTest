using Domin.Model;
using Persent_App.Views.Url;
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
        Task<ResultUrl> CreateUrl(CreateUrlViewModel model);
        Task UpdatePrioritiesAsync(List<UpdateUrlViewModel> updatedPriorities); // متد جدید
        Task UpdatePrioritiesAsync(); // متد جدید


    }
}
