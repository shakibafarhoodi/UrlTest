using Domin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.InterFaceRepository
{
    public interface IUrlRepository
    {
        Task<List<UrlModel>> GetAllAsync();
        //Task<UrlModel> GetByIdAsync(int id);
        Task SaveAsync(UrlModel url);
        void Save();
        Task AddAsync(UrlModel url);
        void Delete(int id);
        Task<bool> UrlExist(string url);
        Task<bool> IsPriorityDuplicateAsync(int priority);
        Task<int> GetMaxPriorityAsync();
        Task<UrlModel> GetByIdAsync(int id); // متد جدید برای دریافت بر اساس Id
        void Update(UrlModel urlModel);
        Task SaveAsync();


    }
}
