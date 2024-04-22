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

namespace Service.AssignmentsService
{
    public class AssignmentService: IAssignmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AssignmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AssignmentResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Assignment>().GetAll()
                                            .ProjectTo<AssignmentResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<AssignmentResponse> Get(Guid id)
        {
            try
            {
                Assignment assignment = null;
                assignment = await _unitOfWork.Repository<Assignment>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Module)
                     .Include(x => x.Topic)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (assignment == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Assignment, AssignmentResponse>(assignment);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AssignmentResponse> Create(AssignmentRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var assignment = _mapper.Map<AssignmentRequest, Assignment>(request);
                assignment.Id = Guid.NewGuid();
                assignment.CreatedDate = localTime;
                assignment.IsActive = true;
                await _unitOfWork.Repository<Assignment>().InsertAsync(assignment);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Assignment, AssignmentResponse>(assignment);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AssignmentResponse> Delete(Guid id)
        {
            try
            {
                Assignment assignment = null;
                assignment = _unitOfWork.Repository<Assignment>()
                    .Find(p => p.Id == id);
                if (assignment == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Assignment>().HardDeleteGuid(assignment.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Assignment, AssignmentResponse>(assignment);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AssignmentResponse> Update(Guid id, AssignmentRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Assignment assignment = _unitOfWork.Repository<Assignment>()
                            .Find(x => x.Id == id);
                if (assignment == null)
                {
                    throw new Exception();
                }
                assignment = _mapper.Map(request, assignment);
                assignment.UpdatedDate= localTime;

                await _unitOfWork.Repository<Assignment>().UpdateDetached(assignment);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Assignment, AssignmentResponse>(assignment);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttemptsByAssignment(Guid id)
        {
            var assignment = await _unitOfWork.Repository<Assignment>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (assignment == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var assignmentAttempts = _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                .Where(t => t.AssignmentId == id)
                .ProjectTo<AssignmentAttemptResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return assignmentAttempts;
        }

       
    }
}
