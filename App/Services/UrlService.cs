﻿using Domin.InterFaceRepository;
using Domin.Model;
using Domin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace App.Services
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public UrlService(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public void DeleteUrl(int id)
        {
            _urlRepository.Delete(id);
            _urlRepository.Save();
        }


        public async Task<List<UrlModel>> GetAllUrlsAsync()
        {
            //return await _urlRepository.GetAllAsync();
            var url = await _urlRepository.GetAllAsync();
            return url.OrderBy(u => u.Priority).ToList();
        }
        public async Task<ResultUrl> CreateUrl(CreateUrlViewModel model, string userId,string userName)
        {
            //var validationResult = await _CreateUrlValidator.ValidateAsync(modle);
          

            if (model == null || (string.IsNullOrEmpty(model.Url) && model.ImgFile == null))
            {
                return ResultUrl.Required; // Error result if both Url and ImgFile are empty
            }
            if (!string.IsNullOrEmpty(model.Url))
            {
                var urlWithoutSpaces = model.Url.Trim().Replace(" ", "");

                var exists = await _urlRepository.UrlExist(urlWithoutSpaces);
                if (exists)
                {
                    return ResultUrl.Duplicate;
                }
            }
            if (!model.Priority.HasValue)
            {
                // پیدا کردن بیشترین مقدار Priority در دیتابیس
                var maxPriority = await _urlRepository.GetMaxPriorityAsync();
                model.Priority = maxPriority + 1; // تنظیم Priority به صورت خودکار
            }
            else
            {
                // اگر Priority مشخص شده باشد، بررسی تکراری بودن آن
                var isPriorityDuplicate = await _urlRepository.IsPriorityDuplicateAsync(model.Priority.Value);
                if (isPriorityDuplicate)
                {
                    return ResultUrl.Duplicate; // اگر Priority تکراری باشد
                }
            }
           

            if (model != null)
            {
                var urlModel = new UrlModel
                {
                    Url = model.Url,
                    time = model.Time,
                    Img = model.ImgFile != null ? model.ImgFile.FileName.Trim().Replace(" ", "") : null,
                    Priority = model.Priority.Value,
                    userId = userId  ,    // ذخیره شناسه کاربر
                    userName = userName
                  
                };

                if (model.ImgFile != null)
                {
                    var imgFileName = model.ImgFile.FileName.Trim().Replace(" ", "");
                    //var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", imgFileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Img", imgFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImgFile.CopyToAsync(stream);
                    }
                }

                await _urlRepository.AddAsync(urlModel);
                await _urlRepository.SaveAsync(urlModel);


   
                return ResultUrl.Success;




            }

            return ResultUrl.eror;
        }



        public async Task UpdatePrioritiesAsync()
        {
            var urls = await _urlRepository.GetAllAsync();
            var orderedUrls = urls.OrderBy(u => u.Priority).ToList();

            int priority = 1;
            foreach (var url in orderedUrls)
            {
                url.Priority = priority++;
                _urlRepository.Update(url);
            }

            await _urlRepository.SaveAsync();
        }

        public async Task UpdatePrioritiesAsync(List<UpdateUrlViewModel> updatedPriorities)
        {
            foreach (var item in updatedPriorities)
            {
                // پیدا کردن هر URL بر اساس ID و به‌روزرسانی اولویت
                var url = await _urlRepository.GetByIdAsync(item.Id);
                if (url != null)
                {
                    url.Priority = item.Priority;  // استفاده از اولویت جدید ارسال شده از سمت کلاینت
                    _urlRepository.Update(url);
                }
            }

            await _urlRepository.SaveAsync();

        }
    }
}

