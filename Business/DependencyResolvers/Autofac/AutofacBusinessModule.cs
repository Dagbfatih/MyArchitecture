using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuestionManager>().As<IQuestionService>(); // RegisterType = singleton
            builder.RegisterType<EfQuestionDal>().As<IQuestionDal>();
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<OptionManager>().As<IOptionService>();
            builder.RegisterType<EfOptionDal>().As<IOptionDal>();
            builder.RegisterType<QuestionCategoryManager>().As<IQuestionCategoryService>();
            builder.RegisterType<EfQuestionCategoryDal>().As<IQuestionCategoryDal>();
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

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
