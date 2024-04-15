using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.AccountsService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TutorService
{
    public class TutorService : ITutorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IAccountService _accountService;

        public TutorService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<List<TutorResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Tutor>().GetAll()
                                            .ProjectTo<TutorResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }


        public async Task<TutorResponse> Get(Guid id)
        {
            try
            {
                Tutor tutor = null;
                tutor = await _unitOfWork.Repository<Tutor>().GetAll()
                    .Include(x => x.Account)
                        .ThenInclude(x => x.Wallet)
                    .Include(x => x.Center)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (tutor == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Tutor, TutorResponse>(tutor);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<CourseResponse>> GetAllCoursesByTutor(Guid id)
        {
            var tutor = await _unitOfWork.Repository<Tutor>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (tutor == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var courses = _unitOfWork.Repository<Course>().GetAll()
                .Where(t => t.TutorId == id)
                .ProjectTo<CourseResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return courses;
        }


        public async Task<List<PaperWorkResponse>> GetAllPaperWorksByTutor(Guid id)
        {
            var tutor = await _unitOfWork.Repository<Tutor>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (tutor == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var paperWorks = _unitOfWork.Repository<PaperWork>().GetAll()
                .Where(t => t.TutorId == id)
                .ProjectTo<PaperWorkResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return paperWorks;
        }

        public async Task<List<ForumResponse>> GetAllForumsByTutor(Guid id)
        {
            var tutor = await _unitOfWork.Repository<Tutor>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (tutor == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var forums = _unitOfWork.Repository<Forum>().GetAll()
                .Where(t => t.Course.TutorId == id)
                .ProjectTo<ForumResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return forums;
        }

        public async Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttemptsByTutor(Guid tutorId)
        {
            var tutor = await _unitOfWork.Repository<Tutor>().GetAll()
                .Where(x => x.Id == tutorId)
                .FirstOrDefaultAsync();

            if (tutor == null)
            {
                // Handle the case where the tutor with the specified id is not found
                return null;
            }

            var assignmentAttempts = _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                .Where(a => a.Assignment.Module.Course.TutorId == tutorId || a.Assignment.Topic.ClassLesson.ClassModule.Course.TutorId == tutorId)
                .ProjectTo<AssignmentAttemptResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return assignmentAttempts;
        }


        public async Task<List<EnrollmentResponse>> GetAllEnrollmentsByTutor(Guid id)
        {
            var tutor = await _unitOfWork.Repository<Tutor>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (tutor == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var enrollments = _unitOfWork.Repository<Enrollment>().GetAll()
                .Where(t => t.Transaction.Course.TutorId == id)
                .ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return enrollments;
        }
        public async Task<decimal> GetTotalAmountByTutor(Guid id)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            decimal total = 0;
            try
            {
                var enrollments = await GetAllEnrollmentsByTutor(id);
                if(enrollments == null)
                {
                    return 0;
                }
                foreach (var item in enrollments)
                {
                    if (item.RefundStatus == false && item.EnrolledDate.HasValue && item.EnrolledDate.Value.Month == localTime.Month)
                    {
                        total += (decimal)item.Transaction.Course.StockPrice;
                    }
                }
                return total;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<TutorResponse> Create(TutorRequest request)
        {
            try
            {
                var tutor = _mapper.Map<TutorRequest, Tutor>(request);
                tutor.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Tutor>().InsertAsync(tutor);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Tutor, TutorResponse>(tutor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TutorResponse> Delete(Guid id)
        {
            try
            {
                Tutor tutor = null;
                tutor = _unitOfWork.Repository<Tutor>()
                    .Find(p => p.Id == id);
                if (tutor == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Tutor>().HardDeleteGuid(tutor.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Tutor, TutorResponse>(tutor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TutorResponse> Update(Guid id, TutorRequest request)
        {
            try
            {
                Tutor tutor = _unitOfWork.Repository<Tutor>()
                            .Find(x => x.Id == id);
                if (tutor == null)
                {
                    throw new Exception();
                }
                tutor = _mapper.Map(request, tutor);

                await _unitOfWork.Repository<Tutor>().UpdateDetached(tutor);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Tutor, TutorResponse>(tutor);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<LearnerResponse>> GetAllLearnersByTutor(Guid id)
        {
            // Retrieve the enrollment
            var tutor = await _unitOfWork.Repository<Tutor>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (tutor == null)
            {
                // Handle the case where the enrollment with the specified id is not found
                return null;
            }

            // Declare a list to store learners
            var learners = new List<LearnerResponse>();

            // Retrieve learners for the enrollment
            var enrollments = await _unitOfWork.Repository<Enrollment>().GetAll()
                .Include(x => x.Transaction)
                    .ThenInclude(x => x.Course)
                .Where(x => x.Transaction.Course.TutorId == id && x.RefundStatus == false)
                .ToListAsync();

            foreach (var item in enrollments)
            {
                var learner = await _unitOfWork.Repository<Learner>().GetAll()
                    .Where(t => t.Id == item.Transaction.LearnerId)

                    .ProjectTo<LearnerResponse>(_mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync();

                if (learner != null)
                {
                    // Add the learner to the list
                    learners.Add(learner);
                }
            }

            return learners;
        }
    }
}
