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

namespace Service.LearnersService
{
    public class LearnerService : ILearnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public LearnerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LearnerResponse>> GetLearners()
        {

            var list = await _unitOfWork.Repository<Learner>().GetAll()
                                            .ProjectTo<LearnerResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<LearnerResponse> Create(LearnerRequest request)
        {
            try
            {
                var learner = _mapper.Map<LearnerRequest, Learner>(request);
                learner.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Learner>().InsertAsync(learner);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LearnerResponse> Delete(Guid id)
        {
            try
            {
                Learner learner = null;
                learner = _unitOfWork.Repository<Learner>()
                    .Find(p => p.Id == id);
                if (learner == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Learner>().HardDeleteGuid(learner.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Learner, LearnerResponse>(learner);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LearnerResponse> Update(Guid id, LearnerRequest request)
        {
            try
            {
                Learner learner = _unitOfWork.Repository<Learner>()
                            .Find(x => x.Id == id);
                if (learner == null)
                {
                    throw new Exception();
                }
                learner = _mapper.Map(request, learner);

                await _unitOfWork.Repository<Learner>().UpdateDetached(learner);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
