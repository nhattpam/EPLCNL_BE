using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Models;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Role,RoleRequest>().ReverseMap();
            CreateMap<Role,RoleResponse>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Center, CenterRequest>().ReverseMap();
            CreateMap<Center, CenterResponse>().ReverseMap();
            CreateMap<Certificate, CertificateRequest>().ReverseMap();
            CreateMap<Certificate, CertificateResponse>().ReverseMap();
            CreateMap<CertificateCourse, CertificateCourseRequest>().ReverseMap();
            CreateMap<CertificateCourse, CertificateCourseResponse>().ReverseMap();
            CreateMap<ClassLesson, ClassLessonRequest>().ReverseMap();
            CreateMap<ClassLesson, ClassLessonResponse>().ReverseMap();
            CreateMap<ClassModule, ClassModuleRequest>().ReverseMap();
            CreateMap<ClassModule, ClassModuleResponse>().ReverseMap();
            CreateMap<ClassTopic, ClassTopicRequest>().ReverseMap();
            CreateMap<ClassTopic, ClassTopicResponse>().ReverseMap();
            CreateMap<Course, CourseRequest>().ReverseMap();
            CreateMap<Course, CourseResponse>().ReverseMap();
            CreateMap<Account, AccountRequest>().ReverseMap();
            CreateMap<Account, AccountResponse>().ReverseMap();
            CreateMap<Account, LoginMem>().ReverseMap();
            CreateMap<AccountForum, AccountForumRequest>().ReverseMap();
            CreateMap<AccountForum, AccountForumResponse>().ReverseMap();
            CreateMap<AccountSurvey, AccountSurveyRequest>().ReverseMap();
            CreateMap<AccountSurvey, AccountSurveyResponse>().ReverseMap();
            CreateMap<Assignment, AssignmentRequest>().ReverseMap();
            CreateMap<Assignment, AssignmentResponse>().ReverseMap();
            CreateMap<AssignmentAttempt, AssignmentAttemptRequest>().ReverseMap();
            CreateMap<AssignmentAttempt, AssignmentAttemptResponse>().ReverseMap();
            CreateMap<Enrollment, EnrollmentRequest>().ReverseMap();
            CreateMap<Enrollment, EnrollmentResponse>().ReverseMap();
            CreateMap<Feedback, FeedbackRequest>().ReverseMap();
            CreateMap<Feedback, FeedbackResponse>().ReverseMap();
            CreateMap<Forum, ForumRequest>().ReverseMap();
            CreateMap<Forum, ForumResponse>().ReverseMap();
            CreateMap<Learner, LearnerRequest>().ReverseMap();
            CreateMap<Learner, LearnerResponse>().ReverseMap();
            CreateMap<Lesson, LessonRequest>().ReverseMap();
            CreateMap<Lesson, LessonResponse>().ReverseMap();
            CreateMap<LessonMaterial, LessonMaterialRequest>().ReverseMap();
            CreateMap<LessonMaterial, LessonMaterialResponse>().ReverseMap();
            CreateMap<Module, ModuleRequest>().ReverseMap();
            CreateMap<Module, ModuleResponse>().ReverseMap();
            CreateMap<PaperWork, PaperWorkRequest>().ReverseMap();
            CreateMap<PaperWork, PaperWorkResponse>().ReverseMap();
            CreateMap<PaperWorkType, PaperWorkTypeRequest>().ReverseMap();
            CreateMap<PaperWorkType, PaperWorkTypeResponse>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodRequest>().ReverseMap();
            CreateMap<PaymentMethod, PaymentMethodResponse>().ReverseMap();
            CreateMap<ProfileCertificate, ProfileCertificateRequest>().ReverseMap();
            CreateMap<ProfileCertificate, ProfileCertificateResponse>().ReverseMap();
            CreateMap<Question, QuestionRequest>().ReverseMap();
            CreateMap<Question, QuestionResponse>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerRequest>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerResponse>().ReverseMap();
            CreateMap<Quiz, QuizRequest>().ReverseMap();
            CreateMap<Quiz, QuizResponse>().ReverseMap();
            CreateMap<QuizAttempt, QuizAttemptRequest>().ReverseMap();
            CreateMap<QuizAttempt, QuizAttemptResponse>().ReverseMap();
            CreateMap<Data.Models.RefundRequest, ViewModel.RequestModel.RefundRequest>().ReverseMap();
            CreateMap<Data.Models.RefundRequest, RefundResponse>().ReverseMap();
            CreateMap<Staff, StaffRequest>().ReverseMap();
            CreateMap<Staff, StaffResponse>().ReverseMap();
            CreateMap<Survey, SurveyRequest>().ReverseMap();
            CreateMap<Survey, SurveyResponse>().ReverseMap();
            CreateMap<Account,AccountRequest>().ReverseMap();
            CreateMap<Account,AccountResponse>().ReverseMap();
            CreateMap<AccountForum,AccountForumRequest>().ReverseMap();
            CreateMap<AccountForum,AccountForumResponse>().ReverseMap();
            CreateMap<AccountSurvey,AccountSurveyRequest>().ReverseMap();
            CreateMap<AccountSurvey,AccountSurveyResponse>().ReverseMap();
            CreateMap<Assignment,AssignmentRequest>().ReverseMap();
            CreateMap<Assignment,AssignmentResponse>().ReverseMap();
            CreateMap<AssignmentAttempt,AssignmentAttemptRequest>().ReverseMap();
            CreateMap<AssignmentAttempt,AssignmentAttemptResponse>().ReverseMap();
            CreateMap<Tutor,TutorRequest>().ReverseMap();
            CreateMap<Tutor,TutorResponse>().ReverseMap();
            CreateMap<Transaction,TransactionRequest>().ReverseMap();
            CreateMap<Transaction,TransactionResponse>().ReverseMap();
            CreateMap<Wallet,WalletRequest>().ReverseMap();
            CreateMap<Wallet,WalletResponse>().ReverseMap();
            CreateMap<Report,ReportRequest>().ReverseMap();
            CreateMap<Report,ReportResponse>().ReverseMap();
            CreateMap<Violation,ViolationRequest>().ReverseMap();
            CreateMap<Violation,ViolationResponse>().ReverseMap();
        }
    }
}
