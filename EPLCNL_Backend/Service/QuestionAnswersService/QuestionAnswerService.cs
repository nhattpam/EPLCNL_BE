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

namespace Service.QuestionAnswersService
{
    public class QuestionAnswerService : IQuestionAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public QuestionAnswerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<QuestionAnswerResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<QuestionAnswer>().GetAll()
                                            .ProjectTo<QuestionAnswerResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<QuestionAnswerResponse> Create(QuestionAnswerRequest request)
        {
            try
            {
                var questionAnswer = _mapper.Map<QuestionAnswerRequest, QuestionAnswer>(request);
                questionAnswer.Id = Guid.NewGuid();
                await _unitOfWork.Repository<QuestionAnswer>().InsertAsync(questionAnswer);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<QuestionAnswer, QuestionAnswerResponse>(questionAnswer);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<QuestionAnswerResponse> Delete(Guid id)
        {
            try
            {
                QuestionAnswer questionAnswer = null;
                questionAnswer = _unitOfWork.Repository<QuestionAnswer>()
                    .Find(p => p.Id == id);
                if (questionAnswer == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<QuestionAnswer>().HardDeleteGuid(questionAnswer.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<QuestionAnswer, QuestionAnswerResponse>(questionAnswer);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<QuestionAnswerResponse> Update(Guid id, QuestionAnswerRequest request)
        {
            try
            {
                QuestionAnswer questionAnswer = _unitOfWork.Repository<QuestionAnswer>()
                            .Find(x => x.Id == id);
                if (questionAnswer == null)
                {
                    throw new Exception();
                }
                questionAnswer = _mapper.Map(request, questionAnswer);

                await _unitOfWork.Repository<QuestionAnswer>().UpdateDetached(questionAnswer);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<QuestionAnswer, QuestionAnswerResponse>(questionAnswer);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
