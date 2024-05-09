using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Service.AssignmentAttemptsService;
using Service.AssignmentsService;
using Service.CoursesService;
using Service.ModulesService;
using Service.QuizzesService;
using Service.WalletHistoriesService;
using Service.WalletsService;
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
        private ICourseService _courseService;
        private IModuleService _moduleService;
        private IWalletHistoryService _walletHistoryService;
        private IWalletService _walletService;
        
        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper,
            ICourseService courseService, IModuleService moduleService,
            IWalletService walletService, IWalletHistoryService walletHistoryService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseService = courseService;
            _moduleService = moduleService;
            _walletHistoryService = walletHistoryService;
            _walletService = walletService;
        }

        public async Task<List<EnrollmentResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Enrollment>().GetAll()
                                            .Include(x => x.Transaction)
                                            .ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<EnrollmentResponse> Get(Guid id)
        {
            try
            {
                Enrollment enrollment = null;
                enrollment = await _unitOfWork.Repository<Enrollment>().GetAll()
                     .AsNoTracking()
                    .Include(x => x.Transaction)
                        .ThenInclude(x => x.Course)
                    .Include(x => x.Transaction)
                        .ThenInclude(x => x.Learner)
                            .ThenInclude(x => x.Account)
                                .ThenInclude(x => x.Wallet)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (enrollment == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Enrollment, EnrollmentResponse>(enrollment);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<EnrollmentResponse> Create(EnrollmentRequest request)
        {
            try
            {
                var enrollment = _mapper.Map<EnrollmentRequest, Enrollment>(request);
                enrollment.Id = Guid.NewGuid();
                enrollment.RefundStatus = false;
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

        public async Task<EnrollmentResponse> GetEnrollmentByLearnerAndCourseId(Guid learnerId, Guid courseId)
        {
            try
            {
                Enrollment enrollment = await _unitOfWork.Repository<Enrollment>()
                    .GetAll()
                    .AsNoTracking()
                    .Include(x => x.Transaction)
                    .Where(x => x.Transaction.LearnerId == learnerId && x.Transaction.CourseId == courseId && x.RefundStatus == false)
                    .FirstOrDefaultAsync();

                if (enrollment == null)
                {
                    // Handle case where enrollment is not found
                    throw new Exception("Enrollment not found for the specified learner and course.");
                }

                return _mapper.Map<Enrollment, EnrollmentResponse>(enrollment);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw new Exception(ex.Message);
            }
        }

        public async Task<EnrollmentResponse> DeleteEnrollmentByLearnerAndCourseId(Guid learnerId, Guid courseId)
        {
            try
            {
                Enrollment enrollment = await _unitOfWork.Repository<Enrollment>()
                    .GetAll()
                    .AsNoTracking()
                    .Where(x => x.Transaction.LearnerId == learnerId && x.Transaction.CourseId == courseId)
                    .FirstOrDefaultAsync();

                if (enrollment == null)
                {
                    // Handle case where enrollment is not found
                    throw new Exception("Enrollment not found for the specified learner and course.");
                }

                await _unitOfWork.Repository<Enrollment>().HardDeleteGuid(enrollment.Id);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Enrollment, EnrollmentResponse>(enrollment);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw new Exception(ex.Message);
            }
        }

        public async Task<double?> GetCourseScore(Guid id)
        {
            try
            {
                Enrollment enrollment = null;
                enrollment = await _unitOfWork.Repository<Enrollment>().GetAll()
                     .AsNoTracking()
                    .Include(x => x.Transaction)
                    .ThenInclude(x => x.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (enrollment == null)
                {
                    throw new Exception("Not Found");
                }

                var course = await _courseService.Get(enrollment.Transaction.CourseId ?? new Guid());

                if(course == null)
                {
                    throw new Exception("Not Found");
                }

                var modules = await _courseService.GetAllModulesByCourse(course.Id);
                double? score = 0;
                foreach (var module in modules)
                {
                    var quizzes = await _moduleService.GetAllQuizzesByModule(module.Id);
                    if (quizzes != null)
                    {
                        foreach (var quizz in quizzes)
                        {
                            if (quizz != null && quizz.GradeToPass.HasValue)
                            {
                                score += quizz.GradeToPass.Value;
                            }
                            // Add handling if GradeToPass is null
                        }
                    }

                    var assignments = await _moduleService.GetAllAssignmentsByModule(module.Id);
                    if (assignments != null)
                    {
                        foreach (var assignment in assignments)
                        {
                            if (assignment != null && assignment.GradeToPass.HasValue)
                            {
                                score += assignment.GradeToPass.Value;
                            }
                            // Add handling if GradeToPass is null
                        }
                    }
                }




                return score;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<double?> GetLearningScore(Guid id)
        {
            try
            {
                Enrollment enrollment = null;
                enrollment = await _unitOfWork.Repository<Enrollment>().GetAll()
                     .AsNoTracking()
                    .Include(x => x.Transaction)
                    .ThenInclude(x => x.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (enrollment == null)
                {
                    throw new Exception("Not Found");
                }

                var course = await _courseService.Get(enrollment.Transaction.CourseId ?? Guid.Empty);

                if (course == null)
                {
                    throw new Exception("Not Found");
                }

                double? score = 0;

                var modulesByCourse = await _courseService.GetAllModulesByCourse(course.Id);

                foreach (var module in modulesByCourse)
                {
                    // Get the assignment attempts list asynchronously
                    var assignmentAttempts = await GetAllAssignmentAttempts();
                    var filteredAssignmentAttempts = assignmentAttempts
                       .Where(x => x.LearnerId == enrollment.Transaction?.LearnerId
                       && x.Assignment?.ModuleId == module.Id).ToList();

                    // Get the quiz attempts list asynchronously
                    // Get the quiz attempts list asynchronously
                    var quizAttempts = await GetAllQuizAttempts();
                    var highestGradeAttemptsByQuiz = quizAttempts
                        .Where(x => x.LearnerId == enrollment.Transaction?.LearnerId
                                 && x.Quiz?.ModuleId == module.Id
                                 && x.TotalGrade >= x.Quiz?.GradeToPass)
                        .GroupBy(x => x.QuizId)
                        .Select(group => group.OrderByDescending(x => x.TotalGrade).FirstOrDefault())
                        .ToList();

                    // Calculate total score from the highest attempts of each quiz
                    foreach (var attempt in highestGradeAttemptsByQuiz)
                    {
                        if (attempt != null)
                        {
                            score += attempt.TotalGrade;
                        }
                    }




                    // Find the assignment attempt with the highest TotalGrade
                    var highestGradeAttemptAssignment = filteredAssignmentAttempts.OrderByDescending(x => x.TotalGrade).FirstOrDefault();
                    // Add the total grades of assignment attempts
                    if (highestGradeAttemptAssignment != null && enrollment.Transaction?.LearnerId == highestGradeAttemptAssignment.LearnerId
                        && highestGradeAttemptAssignment.TotalGrade >= highestGradeAttemptAssignment.Assignment?.GradeToPass)
                    {
                        score += highestGradeAttemptAssignment.TotalGrade;
                    }

                }




                return score;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttempts()
        {

            var list = await _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                                            .Include(x => x.Assignment)
                                                .ThenInclude(x => x.Module)
                                                      .ThenInclude(x => x.Course)
                                            .ProjectTo<AssignmentAttemptResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }
        public async Task<List<QuizAttemptResponse>> GetAllQuizAttempts()
        {

            var list = await _unitOfWork.Repository<QuizAttempt>().GetAll()
                 .Include(x => x.Quiz)
                        .ThenInclude(x => x.Module)
                            .ThenInclude(x => x.Course)
                                            .ProjectTo<QuizAttemptResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task CheckAndProcessEnrollmentsAsync()
        {
            // Logic to query enrollments that meet certain criteria (e.g., 60 seconds after enrollment)
            // For each eligible enrollment, update the admin's wallet
            // Example:
            var enrollments = await GetEligibleEnrollmentsAsync();
            foreach (var enrollment in enrollments)
            {
                if (enrollment.RefundStatus == false && Is60SecondsAfterEnrollment(enrollment))
                {
                    await ProcessEnrollmentAsync(enrollment);
                }
            }
        }

        public async Task ProcessEnrollmentAsync(EnrollmentResponse enrollment)
        {
            // Fetch the admin's wallet
            var adminWallet = await _walletService.Get(new Guid("188e9df9-be4b-4531-858e-098ff8c3735c"));
            var updatedAdminWallet = new WalletRequest()
            {
                Balance = adminWallet.Balance += enrollment.Transaction.Amount / 24000,
                AccountId = adminWallet.AccountId,

            };

            // Save the updated wallet
            await _walletService.Update(adminWallet.Id, updatedAdminWallet);

            // Save wallet history
            var adminWalletHistory = new WalletHistoryRequest()
            {
                WalletId = adminWallet.Id,
                Note = $@"{enrollment.Transaction.Amount / 24000}$ from {enrollment.Transaction.Learner.Account.FullName} by transaction {enrollment.TransactionId} at {enrollment.Transaction.TransactionDate}"
            };

            await _walletHistoryService.Create(adminWalletHistory);
        }

        public async Task<EnrollmentResponse[]> GetEligibleEnrollmentsAsync()
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;

            // Query enrollments where RefundStatus is false and 60 seconds have passed since enrollment
            var eligibleEnrollments = await _unitOfWork.Repository<Enrollment>().GetAll()
                .Where(enrollment => enrollment.RefundStatus == false &&
                    enrollment.EnrolledDate.HasValue &&
                    EF.Functions.DateDiffSecond(enrollment.EnrolledDate.Value, localTime) >= 60)
                .ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return eligibleEnrollments;
        }


        public bool Is60SecondsAfterEnrollment(EnrollmentResponse enrollment)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;

            if (enrollment.EnrolledDate.HasValue)
            {
                return localTime >= enrollment.EnrolledDate.Value.AddSeconds(60);
            }
            else
            {
                // Handle null case (e.g., return false or throw an exception)
                return false;
            }
        }

       
    }
}
