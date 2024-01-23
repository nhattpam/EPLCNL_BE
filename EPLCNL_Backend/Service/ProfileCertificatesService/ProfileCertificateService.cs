using AutoMapper;
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

namespace Service.ProfileCertificatesService
{
    public class ProfileCertificateService : IProfileCertificateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ProfileCertificateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProfileCertificateResponse>> GetProfileCertificates()
        {

            var list = await _unitOfWork.Repository<ProfileCertificate>().GetAll()
                                            .ProjectTo<ProfileCertificateResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ProfileCertificateResponse> Create(ProfileCertificateRequest request)
        {
            try
            {
                var profileCertificate = _mapper.Map<ProfileCertificateRequest, ProfileCertificate>(request);
                profileCertificate.Id = Guid.NewGuid();
                await _unitOfWork.Repository<ProfileCertificate>().InsertAsync(profileCertificate);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ProfileCertificate, ProfileCertificateResponse>(profileCertificate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ProfileCertificateResponse> Delete(Guid id)
        {
            try
            {
                ProfileCertificate profileCertificate = null;
                profileCertificate = _unitOfWork.Repository<ProfileCertificate>()
                    .Find(p => p.Id == id);
                if (profileCertificate == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ProfileCertificate>().HardDeleteGuid(profileCertificate.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ProfileCertificate, ProfileCertificateResponse>(profileCertificate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ProfileCertificateResponse> Update(Guid id, ProfileCertificateRequest request)
        {
            try
            {
                ProfileCertificate profileCertificate = _unitOfWork.Repository<ProfileCertificate>()
                            .Find(x => x.Id == id);
                if (profileCertificate == null)
                {
                    throw new Exception();
                }
                profileCertificate = _mapper.Map(request, profileCertificate);

                await _unitOfWork.Repository<ProfileCertificate>().UpdateDetached(profileCertificate);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ProfileCertificate, ProfileCertificateResponse>(profileCertificate);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
