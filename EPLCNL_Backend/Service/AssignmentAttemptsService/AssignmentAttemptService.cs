using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.AssignmentsService;
using Service.CertificatesService;
using Service.ModulesService;
using Service.ProfileCertificatesService;
using Service.QuizAttemptsService;
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
        private IModuleService _moduleService;
        private ICertificateService _certificateService;
        private IProfileCertificateService _profileCertificateService;
        private readonly IAssignmentService _assignmentService;

        public AssignmentAttemptService(IUnitOfWork unitOfWork, IMapper mapper,
            IModuleService moduleService, ICertificateService certificateService,
            IProfileCertificateService profileCertificateService, IAssignmentService assignmentService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _moduleService = moduleService;
            _certificateService = certificateService;
            _profileCertificateService = profileCertificateService;
            _assignmentService = assignmentService;
        }

        public async Task<List<AssignmentAttemptResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                                            .Include(x => x.Assignment)
                                                .ThenInclude(x => x.Module)
                                                      .ThenInclude(x => x.Course)
                                            .ProjectTo<AssignmentAttemptResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }
        public async Task<AssignmentAttemptResponse> Get(Guid id)
        {
            try
            {
                AssignmentAttempt assignmentAttempt = null;
                assignmentAttempt = await _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Assignment)
                        .ThenInclude(x => x.Module)
                            .ThenInclude(x => x.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (assignmentAttempt == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<AssignmentAttempt, AssignmentAttemptResponse>(assignmentAttempt);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<AssignmentAttemptResponse> Create(AssignmentAttemptRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var assignmentAttempt = _mapper.Map<AssignmentAttemptRequest, AssignmentAttempt>(request);
                assignmentAttempt.Id = Guid.NewGuid();
                assignmentAttempt.AttemptedDate = localTime;
                assignmentAttempt.TotalGrade = 0;
                await _unitOfWork.Repository<AssignmentAttempt>().InsertAsync(assignmentAttempt);
                await _unitOfWork.CommitAsync();

                //double? courseScore = 0;
                //var assignmentAttemptNow = await Get(assignmentAttempt.Id);
                //var quizzes = await _moduleService.GetAllQuizzesByModule(assignmentAttemptNow.Assignment.ModuleId ?? new Guid());

                //if (quizzes != null)
                //{
                //    foreach (var quizz in quizzes)
                //    {
                //        if (quizz != null && quizz.GradeToPass.HasValue)
                //        {
                //            courseScore += quizz.GradeToPass.Value;
                //        }
                //        // Add handling if GradeToPass is null
                //    }
                //}

                // var assignments = await _moduleService.GetAllAssignmentsByModule(assignmentAttemptNow.Assignment.ModuleId ?? new Guid());
                //if (assignments != null)
                //{
                //    foreach (var assignment in assignments)
                //    {
                //        if (assignment != null && assignment.GradeToPass.HasValue)
                //        {
                //            courseScore += assignment.GradeToPass.Value;
                //        }
                //        // Add handling if GradeToPass is null
                //    }
                //}

                //double? score = 0;

                //// Get the assignment attempts list asynchronously
                //var assignmentAttempts = await GetAll();
                //var filteredAssignmentAttempts = assignmentAttempts
                //   .Where(x => x.LearnerId == assignmentAttemptNow.LearnerId
                //   && x.Assignment.ModuleId == assignmentAttemptNow.Assignment.ModuleId).ToList();
                //// Get the quiz attempts list asynchronously
                //var quizAttempts = await GetAllQuizAttempts();
                //var filteredQuizAttempts = quizAttempts
                //    .Where(x => x.LearnerId == assignmentAttemptNow.LearnerId
                //    && x.Quiz.ModuleId == assignmentAttemptNow.Assignment.ModuleId).ToList();

                //// Find the quiz attempt with the highest TotalGrade
                //var highestGradeAttemptQuiz = filteredQuizAttempts.OrderByDescending(x => x.TotalGrade).FirstOrDefault();

                //if (highestGradeAttemptQuiz != null && assignmentAttemptNow.LearnerId == highestGradeAttemptQuiz.LearnerId)
                //{
                //    score += highestGradeAttemptQuiz.TotalGrade;
                //}

                //// Find the assignment attempt with the highest TotalGrade
                //var highestGradeAttemptAssignment = filteredAssignmentAttempts.OrderByDescending(x => x.TotalGrade).FirstOrDefault();
                //// Add the total grades of assignment attempts
                //if (highestGradeAttemptAssignment != null && assignmentAttemptNow.LearnerId == highestGradeAttemptAssignment.LearnerId)
                //{
                //    score += highestGradeAttemptAssignment.TotalGrade;
                //}

                //var certificates = await _certificateService.GetAll();
                //var profileCertificates = await _profileCertificateService.GetAll();
                //var certificate = certificates.FirstOrDefault(x => x.CourseId == assignmentAttemptNow.Assignment.Module.CourseId);
                //if (certificate != null && score >= courseScore)
                //{
                //    var existingProfileCertificate = profileCertificates.FirstOrDefault(pc => pc.LearnerId == assignmentAttemptNow.LearnerId && pc.CertificateId == certificate.Id);

                //    if (existingProfileCertificate == null)
                //    {
                //        var profileCertificate = new ProfileCertificate()
                //        {
                //            LearnerId = assignmentAttemptNow.LearnerId,
                //            CertificateId = certificate.Id,
                //            Status = "DONE"
                //        };

                //        var profileCertificateRequest = _mapper.Map<ProfileCertificate, ProfileCertificateRequest>(profileCertificate);

                //        // Assuming _profileCertificateService.Create method accepts ProfileCertificateRequest object
                //        await _profileCertificateService.Create(profileCertificateRequest);
                //    }

                //}

                return _mapper.Map<AssignmentAttempt, AssignmentAttemptResponse>(assignmentAttempt);
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
                AssignmentAttempt assignmentAttempt = _unitOfWork.Repository<AssignmentAttempt>()
                            .Find(x => x.Id == id);
                if (assignmentAttempt == null)
                {
                    throw new Exception();
                }
                assignmentAttempt = _mapper.Map(request, assignmentAttempt);
                await _unitOfWork.Repository<AssignmentAttempt>().UpdateDetached(assignmentAttempt);
                await _unitOfWork.CommitAsync();

                double? courseScore = 0;
                var assignmentAttemptNow = await Get(assignmentAttempt.Id);
                var quizzes = await _moduleService.GetAllQuizzesByModule(assignmentAttemptNow.Assignment.ModuleId ?? new Guid());

                if (quizzes != null)
                {
                    foreach (var quizz in quizzes)
                    {
                        if (quizz != null && quizz.GradeToPass.HasValue)
                        {
                            courseScore += quizz.GradeToPass.Value;
                        }
                        // Add handling if GradeToPass is null
                    }
                }

                var assignments = await _moduleService.GetAllAssignmentsByModule(assignmentAttemptNow.Assignment.ModuleId ?? new Guid());
                if (assignments != null)
                {
                    foreach (var assignment in assignments)
                    {
                        if (assignment != null && assignment.GradeToPass.HasValue)
                        {
                            courseScore += assignment.GradeToPass.Value;
                        }
                        // Add handling if GradeToPass is null
                    }
                }

                double? score = 0;

                // Get the assignment attempts list asynchronously
                var assignmentAttempts = await GetAll();
                var filteredAssignmentAttempts = assignmentAttempts
                   .Where(x => x.LearnerId == assignmentAttemptNow.LearnerId
                   && x.Assignment.ModuleId == assignmentAttemptNow.Assignment.ModuleId).ToList();
                // Get the quiz attempts list asynchronously
                var quizAttempts = await GetAllQuizAttempts();
                var filteredQuizAttempts = quizAttempts
                    .Where(x => x.LearnerId == assignmentAttemptNow.LearnerId
                    && x.Quiz.ModuleId == assignmentAttemptNow.Assignment.ModuleId).ToList();

                // Find the quiz attempt with the highest TotalGrade
                var highestGradeAttemptQuiz = filteredQuizAttempts.OrderByDescending(x => x.TotalGrade).FirstOrDefault();

                if (highestGradeAttemptQuiz != null && assignmentAttemptNow.LearnerId == highestGradeAttemptQuiz.LearnerId)
                {
                    score += highestGradeAttemptQuiz.TotalGrade;
                }

                // Find the assignment attempt with the highest TotalGrade
                var highestGradeAttemptAssignment = filteredAssignmentAttempts.OrderByDescending(x => x.TotalGrade).FirstOrDefault();
                // Add the total grades of assignment attempts
                if (highestGradeAttemptAssignment != null && assignmentAttemptNow.LearnerId == highestGradeAttemptAssignment.LearnerId)
                {
                    score += highestGradeAttemptAssignment.TotalGrade;
                }

                var certificates = await _certificateService.GetAll();
                var profileCertificates = await _profileCertificateService.GetAll();
                var certificate = certificates.FirstOrDefault(x => x.CourseId == assignmentAttemptNow.Assignment.Module.CourseId);
                if (certificate != null && score >= courseScore)
                {
                    var existingProfileCertificate = profileCertificates.FirstOrDefault(pc => pc.LearnerId == assignmentAttemptNow.LearnerId && pc.CertificateId == certificate.Id);

                    if (existingProfileCertificate == null)
                    {
                        var profileCertificate = new ProfileCertificate()
                        {
                            LearnerId = assignmentAttemptNow.LearnerId,
                            CertificateId = certificate.Id,
                            Status = "DONE"
                        };

                        var profileCertificateRequest = _mapper.Map<ProfileCertificate, ProfileCertificateRequest>(profileCertificate);

                        // Assuming _profileCertificateService.Create method accepts ProfileCertificateRequest object
                        await _profileCertificateService.Create(profileCertificateRequest);
                    }

                }
                return _mapper.Map<AssignmentAttempt, AssignmentAttemptResponse>(assignmentAttempt);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        public async Task<List<PeerReviewResponse>> GetAllPeerReviewsByAssignmentAttempt(Guid id)
        {
            var assignmentAttempt = await _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (assignmentAttempt == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var peerReviews = _unitOfWork.Repository<PeerReview>().GetAll()
                .Where(t => t.AssignmentAttemptId == id)
                .ProjectTo<PeerReviewResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return peerReviews;
        }

        public async Task<List<AssignmentAttemptResponse>> GetAllPeerReviewsByAssignment(Guid assignmentId, Guid learnerId)
        {
            var attemptsByAssignment = await _assignmentService.GetAllAssignmentAttemptsByAssignment(assignmentId);
            var attemptsByAssignmentNoLoginLearner = new List<AssignmentAttemptResponse>();
            foreach (var attempt in attemptsByAssignment)
            {
                if (attempt.LearnerId != learnerId)
                {
                    attemptsByAssignmentNoLoginLearner.Add(attempt);
                }
            }
            return attemptsByAssignmentNoLoginLearner;
        }
    }
}
