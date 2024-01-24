using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.AssignmentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AssignmentAttemptsService
{
    public class AssignmentAttemptService: IAssignmentAttemptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AssignmentAttemptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AssignmentAttemptResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                                            .ProjectTo<AssignmentAttemptResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<AssignmentAttemptResponse> Create(AssignmentAttemptRequest request)
        {
            try
            {
                var assignmentattempt = _mapper.Map<AssignmentAttemptRequest, AssignmentAttempt>(request);
                assignmentattempt.Id = Guid.NewGuid();
                assignmentattempt.AttemptedDate = DateTime.Now;
                await _unitOfWork.Repository<AssignmentAttempt>().InsertAsync(assignmentattempt);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AssignmentAttempt, AssignmentAttemptResponse>(assignmentattempt);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AssignmentAttemptResponse> Delete(Guid id)
        {
            try
            {
                AssignmentAttempt assignmentattempt = null;
                assignmentattempt = _unitOfWork.Repository<AssignmentAttempt>()
                    .Find(p => p.Id == id);
                if (assignmentattempt == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<AssignmentAttempt>().HardDeleteGuid(assignmentattempt.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AssignmentAttempt, AssignmentAttemptResponse>(assignmentattempt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AssignmentAttemptResponse> Update(Guid id, AssignmentAttemptRequest request)
        {
            try
            {
                AssignmentAttempt assignmentattempt = _unitOfWork.Repository<AssignmentAttempt>()
                            .Find(x => x.Id == id);
                if (assignmentattempt == null)
                {
                    throw new Exception();
                }
                assignmentattempt = _mapper.Map(request, assignmentattempt);
                await _unitOfWork.Repository<AssignmentAttempt>().UpdateDetached(assignmentattempt);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AssignmentAttempt, AssignmentAttemptResponse>(assignmentattempt);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
