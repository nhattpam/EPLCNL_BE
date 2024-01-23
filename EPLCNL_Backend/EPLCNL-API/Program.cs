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
using Service.TutorService;
using Service.TransactionsService;
using Service.WalletsService;

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
builder.Services.AddScoped<ITutorService, TutorService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IWalletService, WalletService>();

builder.Services.AddAutoMapper(typeof(ApplicationMapper));
builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(p =>
            p.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
});
builder.Services.AddEndpointsApiExplorer();

// Config JWT for swagger
builder.Services.ConfigureSwaggerGen(c =>
{
    // ... your existing swagger configuration
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Authentication & Authorization
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
