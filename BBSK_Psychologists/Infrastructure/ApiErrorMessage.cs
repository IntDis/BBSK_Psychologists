namespace BBSK_Psycho.Infrastructure
{
    public static class ApiErrorMessage
    {
        public const string NameIsRequired = "Вы не ввели свое имя";
        public const string PasswordLengthIsLessThanAllowed = "Длина пароля меньше 8-ми символов";
        public const string PasswordIsRequired = "Вы не ввели пароль";
        public const string InvalidCharacterInEmail = "В email присутсвует недопустимый символ";
        public const string EmailIsRequire = "Вы не ввели email";
        public const string PhoneNumberIsRequired = "Вы не ввели номер телефона";
        public const string DescriptionIsRequired = "Вы не ввели описание проблемы";
        public const string PsychologistGenderIsRequired = "Вы не выбрали пол";
        public const string CostMinIsRequired = "Вы не ввели минимальную стоимость";
        public const string CostMaxIsRequired = "Вы не ввели максимальную стоимость";
        public const string DateIsRequired = "Вы не выбрали удобную дату";
        public const string TimeIsRequired = "Вы не выбрали удобное время";
        public const string ClientIdIsRequired = "Вы не выбрали id клиента";
        public const string PsychologistIdIsRequired = "Вы не выбрали id психолога ";
        public const string CostIsRequired = "Вы не указали стоимость";
        public const string DurationIsRequired = "Вы не выбрали промежуток времени";
        public const string MessageIsRequired = "Вы не описали свои проблемы";
        public const string SessionDateIsRequired = "Вы не выбрали дату сессии";
        public const string InvalidDate = "Не верный формат даты";
        public const string InvalidPhoneNumber = "Не верный формат номера телефона";
        public const string OrderDateIsRequired = "Не указана дата заказа";
        public const string OrderStatusIsRequired = "Не выбран статус ордера";
        public const string OrderPaymentStatusIsRequired = "Не выбран статус оплаты";
        public const string RatingIsRequired = "Выберети оценку от 1-го до 5-ти";
        public const string TheNumberOfCharactersExceedsTheAllowedValue = "Количество символов превышает допустимое значение";
        public const string LastNameIsRequired = "Поле с фамилией обязательно для заполнения";
        public const string PatronymicIsRequired = "Вы не ввели отчество";
        public const string BirthDateIsRequired = "Вы не выбрали дату рождения";
        public const string WorkExperienceIsRequired = "Укажите опыт работы в годах";
        public const string PassportDataIsRequired = "Укажите серию и номер паспорта";
        public const string EducationIsRequired = "Укажите ваше образование";
        public const string TherapyMethodsIsRequired = "Укажите терапевтические методы";
        public const string ProblemsIsRequired = "Введите проблемы, с которыми вы работаете";
        public const string LengthExceeded = "Превышена допустимая длина";
        public const string LalaIsRequired = "";
        public const string NoErrorForTest = "";
    }
}