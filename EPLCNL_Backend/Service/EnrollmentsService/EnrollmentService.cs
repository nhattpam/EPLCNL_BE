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

namespace Service.EnrollmentsService
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<EnrollmentResponse>> GetEnrollments()
        {

            var list = await _unitOfWork.Repository<Enrollment>().GetAll()
                                            .ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<EnrollmentResponse> Create(EnrollmentRequest request)
        {
            try
            {
                var enrollment = _mapper.Map<EnrollmentRequest, Enrollment>(request);
                enrollment.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Enrollment>().InsertAsync(enrollment);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Enrollment, EnrollmentResponse>(enrollment);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EnrollmentResponse> Delete(Guid id)
        {
            try
            {
                Enrollment enrollment = null;
                enrollment = _unitOfWork.Repository<Enrollment>()
                    .Find(p => p.Id == id);
                if (enrollment == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Enrollment>().HardDeleteGuid(enrollment.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Enrollment, EnrollmentResponse>(enrollment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EnrollmentResponse> Update(Guid id, EnrollmentRequest request)
        {
            try
            {
                Enrollment enrollment = _unitOfWork.Repository<Enrollment>()
                            .Find(x => x.Id == id);
                if (enrollment == null)
                {
                    throw new Exception();
                }
                enrollment = _mapper.Map(request, enrollment);

                await _unitOfWork.Repository<Enrollment>().UpdateDetached(enrollment);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Enrollment, EnrollmentResponse>(enrollment);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
