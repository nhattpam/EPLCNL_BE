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

namespace Service.CertificateCoursesService
{
    public class CertificateCourseService : ICertificateCourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CertificateCourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CertificateCourseResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<CertificateCourse>().GetAll()
                                            .ProjectTo<CertificateCourseResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<CertificateCourseResponse> Create(CertificateCourseRequest request)
        {
            try
            {
                var certificateCourse = _mapper.Map<CertificateCourseRequest, CertificateCourse>(request);
                await _unitOfWork.Repository<CertificateCourse>().InsertAsync(certificateCourse);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<CertificateCourse, CertificateCourseResponse>(certificateCourse);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CertificateCourseResponse> Delete(Guid id)
        {
            try
            {
                CertificateCourse certificateCourse = null;
                certificateCourse = _unitOfWork.Repository<CertificateCourse>()
                    .Find(p => p.Id == id);
                if (certificateCourse == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<CertificateCourse>().HardDeleteGuid(certificateCourse.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CertificateCourse, CertificateCourseResponse>(certificateCourse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CertificateCourseResponse> Update(Guid id, CertificateCourseRequest request)
        {
            try
            {
                CertificateCourse certificateCourse = _unitOfWork.Repository<CertificateCourse>()
                            .Find(x => x.Id == id);
                if (certificateCourse == null)
                {
                    throw new Exception();
                }
                certificateCourse = _mapper.Map(request, certificateCourse);

                await _unitOfWork.Repository<CertificateCourse>().UpdateDetached(certificateCourse);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<CertificateCourse, CertificateCourseResponse>(certificateCourse);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
