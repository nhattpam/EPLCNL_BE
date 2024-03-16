using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.CoursesService;
using Service.RefundSurveysService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.RefundSurveysService
{
    public class RefundSurveyService : IRefundSurveyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly ICourseService _courseService;
        public RefundSurveyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RefundSurveyResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<RefundSurvey>().GetAll()
                                            .ProjectTo<RefundSurveyResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<RefundSurveyResponse> Get(Guid id)
        {
            try
            {
                RefundSurvey refundSurvey = null;
                refundSurvey = await _unitOfWork.Repository<RefundSurvey>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.RefundRequest)
                         .ThenInclude(x => x.Enrollment)
                             .ThenInclude(x => x.Transaction)
                                 .ThenInclude(x => x.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (refundSurvey == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<RefundSurvey, RefundSurveyResponse>(refundSurvey);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RefundSurveyResponse> Create(RefundSurveyRequest request)
        {
            try
            {
                var refundSurvey = _mapper.Map<RefundSurveyRequest, RefundSurvey>(request);
                refundSurvey.Id = Guid.NewGuid();

                // Insert the refundSurvey
                await _unitOfWork.Repository<RefundSurvey>().InsertAsync(refundSurvey);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<RefundSurvey, RefundSurveyResponse>(refundSurvey);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<RefundSurveyResponse> Delete(Guid id)
        {
            try
            {
                RefundSurvey refundSurvey = null;
                refundSurvey = _unitOfWork.Repository<RefundSurvey>()
                    .Find(p => p.Id == id);
                if (refundSurvey == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<RefundSurvey>().HardDeleteGuid(refundSurvey.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<RefundSurvey, RefundSurveyResponse>(refundSurvey);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RefundSurveyResponse> Update(Guid id, RefundSurveyRequest request)
        {
            try
            {
                RefundSurvey refundSurvey = _unitOfWork.Repository<RefundSurvey>()
                            .Find(x => x.Id == id);
                if (refundSurvey == null)
                {
                    throw new Exception();
                }
                refundSurvey = _mapper.Map(request, refundSurvey);

                await _unitOfWork.Repository<RefundSurvey>().UpdateDetached(refundSurvey);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<RefundSurvey, RefundSurveyResponse>(refundSurvey);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
