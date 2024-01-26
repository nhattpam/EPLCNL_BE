using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CloudFoneService
{
    public interface ICloudFoneService
    {
        public Task<CloudFone> Create(CloudFone request);
        Task<List<CloudFone>> GetAll();
    }
    }
