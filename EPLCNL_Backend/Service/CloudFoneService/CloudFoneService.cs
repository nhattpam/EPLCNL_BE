using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CloudFoneService
{
    public class CloudFoneService : ICloudFoneService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CloudFoneService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<CloudFone> Create(CloudFone request)
        {
            try
            {
                await _unitOfWork.Repository<CloudFone>().InsertAsync(request);
                await _unitOfWork.CommitAsync();

                return request;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CloudFone>> GetAll()
        {
            var list =  _unitOfWork.Repository<CloudFone>().GetAll().ToList();
            return list;
        }
    }
}
