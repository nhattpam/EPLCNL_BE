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

namespace Service.QuestionsService
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<QuestionResponse>> GetQuestions()
        {

            var list = await _unitOfWork.Repository<Question>().GetAll()
                                            .ProjectTo<QuestionResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<QuestionResponse> Create(QuestionRequest request)
        {
            try
            {
                var question = _mapper.Map<QuestionRequest, Question>(request);
                question.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Question>().InsertAsync(question);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Question, QuestionResponse>(question);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<QuestionResponse> Delete(Guid id)
        {
            try
            {
                Question question = null;
                question = _unitOfWork.Repository<Question>()
                    .Find(p => p.Id == id);
                if (question == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Question>().HardDeleteGuid(question.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Question, QuestionResponse>(question);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<QuestionResponse> Update(Guid id, QuestionRequest request)
        {
            try
            {
                Question question = _unitOfWork.Repository<Question>()
                            .Find(x => x.Id == id);
                if (question == null)
                {
                    throw new Exception();
                }
                question = _mapper.Map(request, question);

                await _unitOfWork.Repository<Question>().UpdateDetached(question);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Question, QuestionResponse>(question);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
