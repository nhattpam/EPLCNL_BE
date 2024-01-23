CREATE TABLE [Account] (
  [id] uniqueidentifier PRIMARY KEY,
  [email] varchar(255),
  [password] varchar(255),
  [full_name] varchar(255),
  [phone_number] varchar(10),
  [image_url] varchar(2000),
  [date_of_birth] date,
  [gender] bit,
  [address] varchar(255),
  [is_active] bit,
  [is_deleted] bit,
  [role_id] uniqueidentifier,
  [created_date] datetime,
  [created_by] uniqueidentifier,
  [updated_date] datetime,
  [updated_by] uniqueidentifier
)
GO

CREATE TABLE [Role] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255)
)
GO

CREATE TABLE [Tutor] (
  [id] uniqueidentifier PRIMARY KEY,
  [account_id] uniqueidentifier,
  [is_freelancer] bit,
  [center_id] uniqueidentifier,
  [staff_id] uniqueidentifier
)
GO

CREATE TABLE [Learner] (
  [id] uniqueidentifier PRIMARY KEY,
  [account_id] uniqueidentifier
)
GO

CREATE TABLE [PaperWork] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [description] varchar(255),
  [paper_work_url] varchar(MAX),
  [paper_work_type_id] uniqueidentifier,
  [tutor_id] uniqueidentifier
)
GO

CREATE TABLE [PaperWorkType] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255)
)
GO

CREATE TABLE [Center] (
  [id] uniqueidentifier PRIMARY KEY,
  [account_id] uniqueidentifier,
  [name] varchar(255),
  [description] varchar(255),
  [address] varchar(1000),
  [email] varchar(255),
  [is_active] bit,
  [staff_id] uniqueidentifier
)
GO

CREATE TABLE [Staff] (
  [id] uniqueidentifier PRIMARY KEY,
  [account_id] uniqueidentifier
)
GO

CREATE TABLE [Course] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [description] varchar(255),
  [code] varchar(100),
  [image_url] varchar(2000),
  [tutor_id] uniqueidentifier,
  [stock_price] decimal,
  [is_active] bit,
  [is_online_class] bit,
  [rating] float,
  [category_id] uniqueidentifier,
  [tags] varchar(255)
)
GO

CREATE TABLE [Category] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [description] varchar(255)
)
GO

CREATE TABLE [CourseType] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [category_id] uniqueidentifier
)
GO

CREATE TABLE [Enrollment] (
  [id] uniqueidentifier PRIMARY KEY,
  [learner_id] uniqueidentifier,
  [course_id] uniqueidentifier,
  [enrolled_date] datetime,
  [status] varchar(255),
  [total_grade] float
)
GO

CREATE TABLE [Module] (
  [id] uniqueidentifier PRIMARY KEY,
  [course_id] uniqueidentifier,
  [name] varchar(255),
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [Lesson] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [video_url] varchar(MAX),
  [reading] varchar(MAX),
  [module_id] uniqueidentifier,
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [LessonMaterial] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(100),
  [material_url] varchar(MAX),
  [lesson_id] uniqueidentifier,
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [Quiz] (
  [id] uniqueidentifier PRIMARY KEY,
  [module_id] uniqueidentifier,
  [class_practice_id] uniqueidentifier,
  [name] varchar(255),
  [grade_to_pass] float,
  [deadline] time,
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [QuizAttempt] (
  [id] uniqueidentifier PRIMARY KEY,
  [quiz_id] uniqueidentifier,
  [learner_id] uniqueidentifier,
  [attempted_date] datetime,
  [open_time] time,
  [close_time] time,
  [total_grade] float
)
GO

CREATE TABLE [Assignment] (
  [id] uniqueidentifier PRIMARY KEY,
  [question_text] varchar(255),
  [open_time] time,
  [close_time] time,
  [module_id] uniqueidentifier,
  [deadline] time,
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [AssignmentAttempt] (
  [id] uniqueidentifier PRIMARY KEY,
  [assignment_id] uniqueidentifier,
  [learner_id] uniqueidentifier,
  [answer_text] varchar(MAX),
  [attempted_date] datetime,
  [total_grade] float
)
GO

CREATE TABLE [Question] (
  [id] uniqueidentifier PRIMARY KEY,
  [question_text] varchar(MAX),
  [question_image_url] varchar(MAX),
  [question_audio_url] varchar(MAX),
  [default_grade] float,
  [created_date] datetime,
  [updated_date] datetime,
  [quiz_id] uniqueidentifier
)
GO

CREATE TABLE [QuestionAnswer] (
  [id] uniqueidentifier PRIMARY KEY,
  [question_id] uniqueidentifier,
  [answer_text] varchar(100),
  [position] int,
  [is_answer] bit
)
GO

CREATE TABLE [Certificate] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [CertificateCourse] (
  [certificate_id] uniqueidentifier,
  [course_id] uniqueidentifier,
  [description] varchar(255)
)
GO

CREATE TABLE [ProfileCertificate] (
  [learner_id] uniqueidentifier,
  [certificate_id] uniqueidentifier,
  [status] varchar(255)
)
GO

CREATE TABLE [Forum] (
  [id] uniqueidentifier PRIMARY KEY,
  [course_id] uniqueidentifier
)
GO

CREATE TABLE [AccountForum] (
  [learner_id] uniqueidentifier,
  [tutor_id] uniqueidentifier,
  [forum_id] uniqueidentifier,
  [message] varchar(MAX),
  [messaged_date] datetime
)
GO

CREATE TABLE [ClassType] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [course_id] uniqueidentifier
)
GO

CREATE TABLE [ClassModule] (
  [id] uniqueidentifier PRIMARY KEY,
  [start_date] datetime,
  [class_type_id] uniqueidentifier
)
GO

CREATE TABLE [ClassLesson] (
  [id] uniqueidentifier PRIMARY KEY,
  [class_hours] varchar(255),
  [class_url] varchar(MAX),
  [class_module_id] uniqueidentifier,
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [ClassTopic] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [description] varchar(255),
  [material_url] varchar(MAX),
  [created_date] datetime,
  [updated_date] datetime,
  [class_lesson_id] uniqueidentifier
)
GO

CREATE TABLE [ClassMaterial] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(100),
  [material_url] varchar(MAX),
  [class_topic_id] uniqueidentifier,
  [created_date] datetime,
  [updated_date] datetime
)
GO

CREATE TABLE [ClassPractice] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(255),
  [class_topic_id] uniqueidentifier
)
GO

CREATE TABLE [Feedback] (
  [id] uniqueidentifier PRIMARY KEY,
  [feedback_content] varchar(MAX),
  [image_url] varchar(MAX),
  [created_date] datetime,
  [learner_id] uniqueidentifier,
  [course_id] uniqueidentifier
)
GO

CREATE TABLE [Transaction] (
  [id] uniqueidentifier PRIMARY KEY,
  [payment_method_id] uniqueidentifier,
  [amount] decimal,
  [status] varchar(50),
  [transaction_date] datetime,
  [learner_id] uniqueidentifier,
  [course_id] uniqueidentifier,
  [refund_status] bit
)
GO

CREATE TABLE [Wallet] (
  [id] uniqueidentifier PRIMARY KEY,
  [balance] decimal,
  [transaction_date] datetime,
  [account_id] uniqueidentifier
)
GO

CREATE TABLE [PaymentMethod] (
  [id] uniqueidentifier PRIMARY KEY,
  [name] varchar(100)
)
GO

CREATE TABLE [RefundRequest] (
  [id] uniqueidentifier PRIMARY KEY,
  [transaction_id] uniqueidentifier,
  [requested_date] datetime,
  [approved_date] datetime,
  [status] varchar(50),
  [reason] varchar(1000)
)
GO

CREATE TABLE [Survey] (
  [id] uniqueidentifier PRIMARY KEY,
  [survey_question] varchar(255),
  [survey_answer] varchar(255)
)
GO

CREATE TABLE [AccountSurvey] (
  [id] uniqueidentifier PRIMARY KEY,
  [survey_id] uniqueidentifier,
  [learner_id] uniqueidentifier,
  [answer] varchar(255)
)
GO

ALTER TABLE [Account] ADD FOREIGN KEY ([role_id]) REFERENCES [Role] ([id])
GO

ALTER TABLE [Wallet] ADD FOREIGN KEY ([account_id]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Enrollment] ADD FOREIGN KEY ([learner_id]) REFERENCES [Learner] ([id])
GO

ALTER TABLE [Enrollment] ADD FOREIGN KEY ([course_id]) REFERENCES [Course] ([id])
GO

ALTER TABLE [Course] ADD FOREIGN KEY ([tutor_id]) REFERENCES [Tutor] ([id])
GO

ALTER TABLE [Center] ADD FOREIGN KEY ([account_id]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Forum] ADD FOREIGN KEY ([course_id]) REFERENCES [Course] ([id])
GO

ALTER TABLE [AccountForum] ADD FOREIGN KEY ([forum_id]) REFERENCES [Forum] ([id])
GO

ALTER TABLE [AccountForum] ADD FOREIGN KEY ([tutor_id]) REFERENCES [Tutor] ([id])
GO

ALTER TABLE [AccountForum] ADD FOREIGN KEY ([learner_id]) REFERENCES [Learner] ([id])
GO

ALTER TABLE [CertificateCourse] ADD FOREIGN KEY ([certificate_id]) REFERENCES [Certificate] ([id])
GO

ALTER TABLE [CertificateCourse] ADD FOREIGN KEY ([course_id]) REFERENCES [Course] ([id])
GO

ALTER TABLE [ProfileCertificate] ADD FOREIGN KEY ([learner_id]) REFERENCES [Learner] ([id])
GO

ALTER TABLE [ProfileCertificate] ADD FOREIGN KEY ([certificate_id]) REFERENCES [Certificate] ([id])
GO

ALTER TABLE [QuestionAnswer] ADD FOREIGN KEY ([question_id]) REFERENCES [Question] ([id])
GO

ALTER TABLE [Lesson] ADD FOREIGN KEY ([module_id]) REFERENCES [Module] ([id])
GO

ALTER TABLE [Quiz] ADD FOREIGN KEY ([module_id]) REFERENCES [Module] ([id])
GO

ALTER TABLE [Quiz] ADD FOREIGN KEY ([class_practice_id]) REFERENCES [ClassPractice] ([id])
GO

ALTER TABLE [Assignment] ADD FOREIGN KEY ([module_id]) REFERENCES [Module] ([id])
GO

ALTER TABLE [Module] ADD FOREIGN KEY ([course_id]) REFERENCES [Course] ([id])
GO

ALTER TABLE [Question] ADD FOREIGN KEY ([quiz_id]) REFERENCES [Quiz] ([id])
GO

ALTER TABLE [QuizAttempt] ADD FOREIGN KEY ([quiz_id]) REFERENCES [Quiz] ([id])
GO

ALTER TABLE [QuizAttempt] ADD FOREIGN KEY ([learner_id]) REFERENCES [Learner] ([id])
GO

ALTER TABLE [LessonMaterial] ADD FOREIGN KEY ([lesson_id]) REFERENCES [Lesson] ([id])
GO

ALTER TABLE [AssignmentAttempt] ADD FOREIGN KEY ([assignment_id]) REFERENCES [Assignment] ([id])
GO

ALTER TABLE [AssignmentAttempt] ADD FOREIGN KEY ([learner_id]) REFERENCES [Learner] ([id])
GO

ALTER TABLE [Transaction] ADD FOREIGN KEY ([learner_id]) REFERENCES [Learner] ([id])
GO

ALTER TABLE [Transaction] ADD FOREIGN KEY ([course_id]) REFERENCES [Course] ([id])
GO

ALTER TABLE [Transaction] ADD FOREIGN KEY ([payment_method_id]) REFERENCES [PaymentMethod] ([id])
GO

ALTER TABLE [RefundRequest] ADD FOREIGN KEY ([transaction_id]) REFERENCES [Transaction] ([id])
GO

ALTER TABLE [PaperWork] ADD FOREIGN KEY ([paper_work_type_id]) REFERENCES [PaperWorkType] ([id])
GO

ALTER TABLE [PaperWork] ADD FOREIGN KEY ([tutor_id]) REFERENCES [Tutor] ([id])
GO

ALTER TABLE [Feedback] ADD FOREIGN KEY ([learner_id]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Feedback] ADD FOREIGN KEY ([course_id]) REFERENCES [Course] ([id])
GO

ALTER TABLE [CourseType] ADD FOREIGN KEY ([category_id]) REFERENCES [Category] ([id])
GO

ALTER TABLE [Course] ADD FOREIGN KEY ([category_id]) REFERENCES [Category] ([id])
GO

ALTER TABLE [AccountSurvey] ADD FOREIGN KEY ([learner_id]) REFERENCES [Learner] ([id])
GO

ALTER TABLE [AccountSurvey] ADD FOREIGN KEY ([survey_id]) REFERENCES [Survey] ([id])
GO

ALTER TABLE [ClassType] ADD FOREIGN KEY ([course_id]) REFERENCES [Course] ([id])
GO

ALTER TABLE [ClassTopic] ADD FOREIGN KEY ([class_lesson_id]) REFERENCES [ClassLesson] ([id])
GO

ALTER TABLE [ClassMaterial] ADD FOREIGN KEY ([class_topic_id]) REFERENCES [ClassTopic] ([id])
GO

ALTER TABLE [ClassLesson] ADD FOREIGN KEY ([class_module_id]) REFERENCES [ClassModule] ([id])
GO

ALTER TABLE [ClassModule] ADD FOREIGN KEY ([class_type_id]) REFERENCES [ClassType] ([id])
GO

ALTER TABLE [ClassPractice] ADD FOREIGN KEY ([class_topic_id]) REFERENCES [ClassTopic] ([id])
GO

ALTER TABLE [Tutor] ADD FOREIGN KEY ([account_id]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Staff] ADD FOREIGN KEY ([account_id]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Learner] ADD FOREIGN KEY ([account_id]) REFERENCES [Account] ([id])
GO

ALTER TABLE [Tutor] ADD FOREIGN KEY ([center_id]) REFERENCES [Center] ([id])
GO

ALTER TABLE [Center] ADD FOREIGN KEY ([staff_id]) REFERENCES [Staff] ([id])
GO

ALTER TABLE [Tutor] ADD FOREIGN KEY ([staff_id]) REFERENCES [Staff] ([id])
GO
