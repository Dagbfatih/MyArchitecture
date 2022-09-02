using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Mail;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionManager>().As<IQuestionService>(); // RegisterType = singleton
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<OptionManager>().As<IOptionService>();
            builder.RegisterType<EfOptionDal>().As<IOptionDal>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();
            builder.RegisterType<EfTestDal>().As<ITestDal>();
            builder.RegisterType<TestManager>().As<ITestService>();
            builder.RegisterType<EfTestQuestionDal>().As<ITestQuestionDal>();
            builder.RegisterType<TestQuestionManager>().As<ITestQuestionService>();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();
            builder.RegisterType<QuestionTicketManager>().As<IQuestionTicketService>();
            builder.RegisterType<EfQuestionTicketDal>().As<IQuestionTicketDal>();
            builder.RegisterType<TestTicketManager>().As<ITestTicketService>();
            builder.RegisterType<EfTestTicketDal>().As<ITestTicketDal>();
            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<EfCustomerDal>().As<ICustomerDal>();
            builder.RegisterType<RoleManager>().As<IRoleService>();
            builder.RegisterType<EfRoleDal>().As<IRoleDal>();
            builder.RegisterType<ProfileImageManager>().As<IProfileImageService>();
            builder.RegisterType<EfProfileImageDal>().As<IProfileImageDal>();
            builder.RegisterType<LanguageManager>().As<ILanguageService>();
            builder.RegisterType<EfLanguageDal>().As<ILanguageDal>();
            builder.RegisterType<TranslateManager>().As<ITranslateService>();
            builder.RegisterType<EfTranslateDal>().As<ITranslateDal>();
            builder.RegisterType<TestResultManager>().As<ITestResultService>();
            builder.RegisterType<EfTestResultDal>().As<ITestResultDal>();
            builder.RegisterType<QuestionResultManager>().As<IQuestionResultService>();
            builder.RegisterType<EfQuestionResultDal>().As<IQuestionResultDal>();
            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();
            builder.RegisterType<BranchManager>().As<IBranchService>();
            builder.RegisterType<EfBranchDal>().As<IBranchDal>();
            builder.RegisterType<GradeLevelManager>().As<IGradeLevelService>();
            builder.RegisterType<EfGradeLevelDal>().As<IGradeLevelDal>();
            builder.RegisterType<LessonManager>().As<ILessonService>();
            builder.RegisterType<EfLessonDal>().As<ILessonDal>();
            builder.RegisterType<SubjectManager>().As<ISubjectService>();
            builder.RegisterType<EfSubjectDal>().As<ISubjectDal>();
            builder.RegisterType<RefreshTokenManager>().As<IRefreshTokenService>();
            builder.RegisterType<EfRefreshTokenDal>().As<IRefreshTokenDal>();
            builder.RegisterType<CityManager>().As<ICityService>();
            builder.RegisterType<EfCityDal>().As<ICityDal>();
            builder.RegisterType<ProfileInfoManager>().As<IProfileInfoService>();
            builder.RegisterType<EfProfileInfoDal>().As<IProfileInfoDal>();
            builder.RegisterType<ProfileInfoWebsiteManager>().As<IProfileInfoWebsiteService>();
            builder.RegisterType<EfProfileInfoWebsiteDal>().As<IProfileInfoWebsiteDal>();
            builder.RegisterType<SchoolManager>().As<ISchoolService>();
            builder.RegisterType<EfSchoolDal>().As<ISchoolDal>();
            builder.RegisterType<WebsiteManager>().As<IWebsiteService>();
            builder.RegisterType<EfWebsiteDal>().As<IWebsiteDal>();
            builder.RegisterType<DifficultyManager>().As<IDifficultyService>();
            builder.RegisterType<EfDifficultyDal>().As<IDifficultyDal>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
