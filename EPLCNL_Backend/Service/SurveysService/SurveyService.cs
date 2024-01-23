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

namespace Service.SurveysService
{
    public class SurveyService : ISurveyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public SurveyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SurveyResponse>> GetSurveys()
        {

            var list = await _unitOfWork.Repository<Survey>().GetAll()
                                            .ProjectTo<SurveyResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<SurveyResponse> Create(SurveyRequest request)
        {
            try
            {
                var survey = _mapper.Map<SurveyRequest, Survey>(request);
                survey.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Survey>().InsertAsync(survey);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Survey, SurveyResponse>(survey);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<SurveyResponse> Delete(Guid id)
        {
            try
            {
                Survey survey = null;
                survey = _unitOfWork.Repository<Survey>()
                    .Find(p => p.Id == id);
                if (survey == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Survey>().HardDeleteGuid(survey.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Survey, SurveyResponse>(survey);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SurveyResponse> Update(Guid id, SurveyRequest request)
        {
            try
            {
                Survey survey = _unitOfWork.Repository<Survey>()
                            .Find(x => x.Id == id);
                if (survey == null)
                {
                    throw new Exception();
                }
                survey = _mapper.Map(request, survey);

                await _unitOfWork.Repository<Survey>().UpdateDetached(survey);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Survey, SurveyResponse>(survey);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
