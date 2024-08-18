using Data.PnsContext;
using Domin.InterFaceRepository;
using Domin.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly UrlContext _dbContext;
       public UrlRepository(UrlContext dbContext)
        {
            _dbContext = dbContext;
        }
           
        public async Task<List<UrlModel>> GetAllAsync()
        {
           return await _dbContext.Url.ToListAsync();
        }
        public async Task AddAsync(UrlModel url)
        {
           await _dbContext.AddAsync(url);
        }

        public void Delete(int id)
        {
            var url = _dbContext.Url.Find(id);
            if (url != null)
            {
                _dbContext.Url.Remove(url);
            }
        }
        public async Task SaveAsync(UrlModel url) => await _dbContext.SaveChangesAsync();

        public async Task<bool> UrlExist(string url)
        {
            return await _dbContext.Url.AnyAsync(u => u.Url == url);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public async Task<bool> IsPriorityDuplicateAsync(int priority)
        {
            return await _dbContext.Url.AnyAsync(u => u.Priority == priority);
        }
        public async Task<int> GetMaxPriorityAsync()
        {
            return await _dbContext.Url.MaxAsync(u => (int?)u.Priority) ?? 0;
        }

        public async Task<UrlModel> GetByIdAsync(int id)
        {
            return await _dbContext.Url.FindAsync(id);
        }

        public void Update(UrlModel urlModel)
        {
            _dbContext.Url.Update(urlModel); 
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
