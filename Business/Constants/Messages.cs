using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string QuestionAdded = "Question Added";
        public static string QuestionDeleted = "Question Deleted";
        public static string QuestionUpdated = "Question Updated";

        public static string OptionAdded = "Option Added";
        public static string OptionDeleted = "Option Deleted";
        public static string OptionUpdated = "Option Updated";

        public static string CategoryAdded = "Category Added";
        public static string CategoryDeleted = "Category Deleted";
        public static string CategoryUpdated = "Category Updated";

        public static string QuestionCategoryAdded = "Question Category Added";
        public static string QuestionCategoryDeleted = "Question Category Deleted";
        public static string QuestionCategoryUpdated = "Question Category Updated";

        public static string OptionsGot = "All Options Got";
        public static string OptionGot = "Option Got";
        public static string MustBeOnlyOneCorrectOption = "Sadece Bir Doğru Seçenek Olabilir";
        public static string MustBeMinOneCategory = "Must Be Minimum One Category";
        public static string MustBeMinTwoOption = "Minimum İki Seçenek Olmalı";

        public static string AuthorizationDenied = "Yetki Reddedildi";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserRegistered = "Kayıt Başarılı";
        public static string PasswordError = "Parola Hatası";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string UserAlreadyExists = "Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Token Oluşturuldu";

        public static string TestCreated = "Test Oluşturuldu";
        public static string TestDeleted = "Test Silindi";
        public static string TestUpdated = "Test güncellendi";

        public static string DeleteFailed = "Silme İşlemi Başarısız";

        public static string QuestionAddedToTest = "Question Added To Test";
        public static string QuestionDeletedFromTest = "Question Deleted From Test";
        public static string QuestionUpdatedForTest = "Question Updated For Test";


        public static string UserAdded="Kullanıcı Eklendi";
        public static string UserDeleted = "Kullanıcı Silindi";
        public static string UserUpdated = "Kullanıcı Güncellendi";

        public static string QuestionTicketAdded = "Soru Etiketi Eklendi";
        public static string QuestionTicketDeleted = "Soru Etiketi Silindi";
        public static string QuestionTicketUpdated = "Soru Etiketi Güncellendi";

        public static string TestTicketAdded = "Test Etiketi Eklendi";
        public static string TestTicketDeleted = "Test Etiketi Silindi";
        public static string TestTicketUpdated = "Test Etiketi Güncellendi";

        public static string CustomerAdded = "Müşteri Eklendi";
        public static string CustomerDeleted = "Müşteri Silindi";
        public static string CustomerUpdated = "Müşteri Güncellendi";

        public static string RoleAdded = "Rol Eklendi";
        public static string RoleDeleted = "Rol Silindi";
        public static string RoleUpdated = "Rol Güncellendi";

        public static string CategoryExists="Aynı kategori birden fazla eklenemez";
        public static string OptionExists="Aynı seçenekten birden fazla eklenemez";
        public static string QuestionExists = "Aynı soruyu birden fazla eklemeyezsiniz";
        public static string EmailExists = "E-posta adresi mevcut";
        public static string ProfileImagesLimited="En fazla bir resminiz olabilir";

        public static string ProfileImageAdded="Resim eklendi";
        public static string ProfileImageDeleted="Resim silindi";
        public static string ProfileImageUpdated="Resim güncellendi";

       

       

        public static string LanguageCreated="Dil eklendi";
        public static string LanguageDeleted="Dil silindi";
        public static string LanguageUpdated = "Dil güncellendi";

        public static string TranslateCreated = "Çeviri oluşturuldu";
        public static string TranslateDeleted = "Çeviri eklendi";
        public static string TranslateUpdated = "Çeviri güncellendi";

        public static string TestResultCreated = "Test sonucu kaydedildi";
        public static string TestResultDeleted = "Test sonucu silindi";
        public static string TestResultUpdated = "Test sonucu güncellendi";

        public static string QuestionResultCreated = "Soru kaydedildi";
        public static string QuestionResultDeleted = "Soru silindi";
        public static string QuestionResultUpdated = "Soru güncellendi";

        public static string OperationClaimAdded="Operasyon yetkisi oluşturuldu";
        public static string OperationClaimDeleted= "Operasyon yetkisi silindi";
        public static string OperationClaimUpdated= "Operasyon yetkisi güncellendi";

        public static string UserOperationClaimAdded="Kullanıcı yetkisi eklendi";
        public static string UserOperationClaimDeleted = "Kullanıcı yetkisi silindi";
        public static string UserOperationClaimUpdated = "Kullanıcı yetkisi güncellendi";
        internal static string AccountConfirmed;
    }
}
