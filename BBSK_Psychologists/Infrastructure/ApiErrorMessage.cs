﻿namespace BBSK_Psycho.Infrastructure
{
    public static class ApiErrorMessage
    {
        public const string NameIsRequired = "Вы не ввели свое имя";
        public const string PasswordLengthIsLessThanAllowed = "Длина пароля меньше 8-ми символов";
        public const string PasswordIsRequire = "Вы не ввели пароль";
        public const string InvalidCharacterInEmail = "В email присутсвует недопустимый символ";
        public const string EmailIsRequire = "Вы не ввели email";
        public const string PhoneNumberIsRequired = "Вы не ввели номер телефона";
        public const string DescriptionIsRequired = "Вы не ввели описание проблемы";
        public const string PsychologistGenderIsRequired = "Вы не выбрали пол";
        public const string CostMinIsRequired = "Вы не ввели минимальную стоимость";
        public const string CostMaxIsRequired = "Вы не ввели максимальную стоимость";
        public const string DateIsRequired = "Вы не выбрали удобную дату";
        public const string TimeIsRequired = "Вы не выбрали удобное время";
        public const string ClientIdIsRequired = "Вы не выбрали id клиента ";
        public const string CostIsRequired = "Вы не указали стоимость";
        public const string DurationIsRequired = "Вы не выбрали промежуток времени";
        public const string MessageIsRequired = "Вы не описали свои проблемы";
        public const string SessionDateIsRequired = "Вы не выбрали дату сессии";
        public const string InvalidDate = "Не верный формат даты";
        public const string OrderDateIsRequired = "Не указана дата заказа";
        public const string OrderStatusIsRequired = "Не выбран статус ордера";
        public const string OrderPaymentStatusIsRequired = "Не выбран статус оплаты";
        public const string LastNameIsRequired = "Вы не ввели фамилию";
        public const string PatronymicIsRequired = "Вы не ввели отчество";
        public const string BirthDateIsRequired = "Дата рождения обязательна к заполнению";
        public const string WorkExperienceIsRequired = "Опыт работы обязателен к заполнению";
        public const string PassportDataIsRequired = "Паспортные данные не введены";
        public const string EducationIsRequired = "Данные об образовании не введены";
        public const string TherapyMethodsIsRequired = "Данные о методах работы не введены";
        public const string ProblemsIsRequired = "Проблемы, с которыми вы работаете, обязательны к заполнению";
    }
}
