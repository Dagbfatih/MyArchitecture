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

        public static string AuthorizationDenied = "Authorization Denied";
        public static string SuccessfulLogin = "Giriş Başarılı";
        public static string UserRegistered = "Kayıt Başarılı";
        public static string PasswordError = "Parola Hatası";
        public static string UserNotFound = "Kullanıcı Bulunamadı";
        public static string UserAlreadyExists = "Kullanıcı Mevcut";
        public static string AccessTokenCreated = "Token Oluşturuldu";

        public static string TestCreated = "Test Oluşturuldu";
        public static string TestDeleted = "Test Silindi";

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
    }
}
