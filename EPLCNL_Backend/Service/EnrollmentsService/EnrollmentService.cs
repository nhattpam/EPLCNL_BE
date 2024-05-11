using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Service.AssignmentAttemptsService;
using Service.AssignmentsService;
using Service.CentersService;
using Service.CoursesService;
using Service.ModulesService;
using Service.QuizzesService;
using Service.TutorService;
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
        private ITutorService _tutorService;
        private ICenterService _centerService;
        private readonly object _lockObject = new object();

        public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper,
            ICourseService courseService, IModuleService moduleService,
            IWalletService walletService, IWalletHistoryService walletHistoryService,
            ITutorService tutorService, ICenterService centerService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseService = courseService;
            _moduleService = moduleService;
            _walletHistoryService = walletHistoryService;
            _walletService = walletService;
            _tutorService = tutorService;
            _centerService = centerService;
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
            IEnumerable<EnrollmentResponse> enrollments = null;
            try
            {
                enrollments = await GetEligibleEnrollmentsAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Console.WriteLine($"An error occurred while querying enrollments: {ex.Message}");
                return; // Exit the method if an error occurs
            }

            List<EnrollmentResponse> enrollmentsToProcess = new List<EnrollmentResponse>();

            foreach (var enrollment in enrollments)
            {
                // If not an online class, process after 60 seconds
                if (enrollment.Transaction.Course.IsOnlineClass == false)
                {
                    if (enrollment.RefundStatus == false && Is60SecondsAfterEnrollment(enrollment))
                    {
                        enrollmentsToProcess.Add(enrollment);
                    }
                }
                // If an online class, process after the end of start date of each class module
                else
                {
                    if (enrollment.RefundStatus == false && await IsAfterAllClassModuleStartDatesAsync(enrollment))
                    {
                        enrollmentsToProcess.Add(enrollment);
                    }
                }
            }

            // Only one thread should execute this part at a time
            lock (_lockObject)
            {
                try
                {
                    foreach (var enrollment in enrollmentsToProcess)
                    {
                        // Process each enrollment synchronously
                        ProcessEnrollmentAsync(enrollment).Wait();
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions here
                    Console.WriteLine($"An error occurred while processing enrollments: {ex.Message}");
                }
            }
        }


        //Tranfer money
        public async Task ProcessEnrollmentAsync(EnrollmentResponse enrollment)
        {
            // Fetch the admin's wallet
            var adminWallet = await _walletService.Get(new Guid("188e9df9-be4b-4531-858e-098ff8c3735c"));

            // Fetch wallet histories for the admin
            var walletHistories = await _walletService.GetWalletHistoryByWallet(adminWallet.Id);

            // Check if the transaction ID exists in any wallet history note
            bool transactionIdExists = walletHistories.Any(history => history.Note.Contains(enrollment.TransactionId.ToString()));

            if (!transactionIdExists)
            {
                // If the transaction ID does not exist, proceed with processing

                // Set the UTC offset for UTC+7
                TimeSpan utcOffset = TimeSpan.FromHours(7);

                // Get the current UTC time
                DateTime utcNow = DateTime.UtcNow;

                // Convert the UTC time to UTC+7
                DateTime localTime = utcNow + utcOffset;

                var tutor = await _tutorService.Get(enrollment.Transaction.Course.TutorId ?? Guid.Empty);
                if (tutor.IsFreelancer == false)
                {
                    // Update admin's wallet balance
                    var updatedAdminWallet = new WalletRequest()
                    {
                        Balance = adminWallet.Balance + (enrollment.Transaction.Amount / 24000) * 0.2m,
                        AccountId = adminWallet.AccountId,
                    };

                    // Save the updated admin wallet
                    await _walletService.Update(adminWallet.Id, updatedAdminWallet);

                    // Save admin wallet history
                    var adminWalletHistory = new WalletHistoryRequest()
                    {
                        WalletId = adminWallet.Id,
                        Note = $@"+ {(enrollment.Transaction.Amount / 24000) * 0.2m} $ from {enrollment.Transaction.Learner.Account.FullName} by transaction {enrollment.TransactionId} at {enrollment.Transaction.TransactionDate}",
                        TransactionDate = localTime
                    };
                    await _walletHistoryService.Create(adminWalletHistory);


                    var center = await _centerService.Get(tutor.CenterId ?? Guid.Empty);
                    // Update center's wallet balance
                    var updatedCenterWallet = new WalletRequest()
                    {
                        Balance = center.Account.Wallet.Balance + (enrollment.Transaction.Amount / 24000) * 0.8m,
                        AccountId = center.AccountId,
                    };

                    // Save the updated center wallet
                    await _walletService.Update(center.Account.Wallet.Id, updatedCenterWallet);

                    // Save center wallet history
                    var centerWalletHistory = new WalletHistoryRequest()
                    {
                        WalletId = center.Account.Wallet.Id,
                        Note = $@"+ {(enrollment.Transaction.Amount / 24000) * 0.8m}$ from {enrollment.Transaction.Learner.Account.FullName} by transaction {enrollment.TransactionId} at {enrollment.Transaction.TransactionDate}",
                        TransactionDate = localTime
                    };
                    await _walletHistoryService.Create(centerWalletHistory);

                }



                if (tutor.IsFreelancer == true)
                {
                    // Update admin's wallet balance
                    var updatedAdminWallet = new WalletRequest()
                    {
                        Balance = adminWallet.Balance + (enrollment.Transaction.Amount / 24000) * 0.2m,
                        AccountId = adminWallet.AccountId,
                    };

                    // Save the updated admin wallet
                    await _walletService.Update(adminWallet.Id, updatedAdminWallet);

                    // Save admin wallet history
                    var adminWalletHistory = new WalletHistoryRequest()
                    {
                        WalletId = adminWallet.Id,
                        Note = $@"+ {(enrollment.Transaction.Amount / 24000) * 0.2m} $ from {enrollment.Transaction.Learner.Account.FullName} by transaction {enrollment.TransactionId} at {enrollment.Transaction.TransactionDate}",
                        TransactionDate = localTime
                    };
                    await _walletHistoryService.Create(adminWalletHistory);


                    // Update tutor's wallet balance
                    var updatedTutorWallet = new WalletRequest()
                    {
                        Balance = tutor.Account.Wallet.Balance + (enrollment.Transaction.Amount / 24000) * 0.8m,
                        AccountId = tutor.AccountId,
                    };

                    // Save the updated tutor wallet
                    await _walletService.Update(tutor.Account.Wallet.Id, updatedTutorWallet);

                    // Save tutor wallet history
                    var tutorWalletHistory = new WalletHistoryRequest()
                    {
                        WalletId = tutor.Account.Wallet.Id,
                        Note = $@"+ {(enrollment.Transaction.Amount / 24000) * 0.8m}$ from {enrollment.Transaction.Learner.Account.FullName} by transaction {enrollment.TransactionId} at {enrollment.Transaction.TransactionDate}",
                        TransactionDate = localTime
                    };
                    await _walletHistoryService.Create(tutorWalletHistory);
                }

            }
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
        private async Task<bool> IsAfterAllClassModuleStartDatesAsync(EnrollmentResponse enrollment)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get all class modules for the course
            var classModules = await _courseService.GetAllClassModulesByCourse(enrollment.Transaction.CourseId ?? Guid.Empty);

            // If there are no class modules, return false
            if (!classModules.Any())
            {
                return false;
            }

            // Find the highest start date among the class modules
            DateTime? highestStartDate = classModules.Max(module => module.StartDate);

            // Check if the highest start date is null
            if (highestStartDate.HasValue)
            {
                // Get the current UTC time
                DateTime utcNow = DateTime.UtcNow;

                // Convert the UTC time to UTC+7
                DateTime localTime = utcNow + utcOffset;

                // Check if the current time is after the highest start date
                return localTime > highestStartDate.Value;
            }
            else
            {
                // Handle the case where all start dates are null
                // You may want to return true or false depending on the requirement
                return false;
            }
        }



    }
}
