using BusinessObject.UnitOfWork;
using Data.Models;
using Data.UnitOfWork;
using Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service;
using Service.CategoriesService;
using Service.CentersService;
using Service.CertificateCoursesService;
using Service.CertificatesService;
using Service.ClassLessonsService;
using Service.ClassMaterialsService;
using Service.ClassModulesService;
using Service.ClassPracticesService;
using Service.ClassPraticesService;
using Service.ClassTopicsService;
using Service.ClassTypesService;
using Service.CoursesService;
using Service.AccountForumsService;
using Service.AccountsService;
using Service.AccountSurveysService;
using Service.AssignmentAttemptsService;
using Service.AssignmentsService;
using System.Text.Json.Serialization;
using Service.EnrollmentsService;
using Service.FeedbacksService;
using Service.ForumsService;
using Service.LearnersService;
using Service.LessonsService;
using Service.LessonMaterialsService;
using Service.ModulesService;
using Service.PaperWorksService;
using Service.PaperWorkTypesService;
using Service.PaymentMethodsService;
using Service.ProfileCertificatesService;
using Service.QuestionsService;
using Service.QuestionAnswersService;
using Service.QuizzesService;
using Service.QuizAttemptsService;
using Service.RefundRequestsService;
using Service.StaffsService;
using Service.SurveysService;
using Service.TutorService;
using Service.TransactionsService;
using Service.WalletsService;
using ViewModel;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Read the connection string from appsettings.json
string connectionString = configuration.GetConnectionString("MyCnn");

// Add DbContext using the connection string
builder.Services.AddDbContext<EPLCNLContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// Add other services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICenterService, CenterService>();
builder.Services.AddScoped<ICertificateCourseService, CertificateCourseService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IClassLessonService, ClassLessonService>();
builder.Services.AddScoped<IClassMaterialService, ClassMaterialService>();
builder.Services.AddScoped<IClassModuleService, ClassModuleService>();
builder.Services.AddScoped<IClassPracticeService, ClassPracticeService>();
builder.Services.AddScoped<IClassTopicService, ClassTopicService>();
builder.Services.AddScoped<IClassTypeService, ClassTypeService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountForumService, AccountForumService>();
builder.Services.AddScoped<IAccountSurveyService, AccountSurveyService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<IAssignmentAttemptService, AssignmentAttemptService>();
builder.Services.AddScoped<IEnrollmentService, EnrollmentService>();
builder.Services.AddScoped<IFeedbackService, FeedbackService>();
builder.Services.AddScoped<IForumService, ForumService>();
builder.Services.AddScoped<ILearnerService, LearnerService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILessonMaterialService, LessonMaterialService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<IPaperWorkService, PaperWorkService>();
builder.Services.AddScoped<IPaperWorkTypeService, PaperWorkTypeService>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodService>();
builder.Services.AddScoped<IProfileCertificateService, ProfileCertificateService>();
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IQuestionAnswerService, QuestionAnswerService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuizAttemptService, QuizAttemptService>();
builder.Services.AddScoped<IRefundRequestService, RefundRequestService>();
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IWalletService, WalletService>();

builder.Services.AddAutoMapper(typeof(ApplicationMapper));

//khai app AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));


builder.Services.AddSwaggerGen();

//JWT
var secretKey = builder.Configuration["AppSettings:SecretKey"];
var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            //tu cap token
            ValidateIssuer = false,
            ValidateAudience = false,

            //ky vao token
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EPLCNL API",
        Description = "EPLCNL Management API",
        TermsOfService = new Uri("https://EPLCNL.com"),
        Contact = new OpenApiContact
        {
            Name = "EPLCNL Company",
            Email = "EPLCNL@gmail.com",
            Url = new Uri("https://twitter.com/EPLCNL"),
        },
        License = new OpenApiLicense
        {
            Name = "EPLCNL Open License",
            Url = new Uri("https://EPLCNL.com"),
        }
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token with Bearer format"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{ }
    }
});

});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//khai bao
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Shows UseCors with CorsPolicyBuilder.
// with a named pocili
app.UseCors("AllowOrigin");

app.Run();
