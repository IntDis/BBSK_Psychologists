using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_DataLayer.Tests.TestCaseSources
{
    public static class OrdersHelper
    {
        private static Random random = new();


        public static Client GetClient()
        {
            Client client = new()
            {
                Id = random.Next(1, 100),
                Name = "Andrew",
                LastName = "Garfield",
                Email = "GA@gmail.com",
                Password = "password",
                BirthDate = DateTime.Parse($"19{random.Next(0, 9)}{random.Next(0, 9)} - 01 - 01"),
                RegistrationDate = DateTime.Parse($"2022 - 06 - {random.Next(0, 2)}{random.Next(1, 9)}"),
                IsDeleted = false,
                Orders = new List<Order>(),
                Comments = new List<Comment>(),
            };

            return client;
        }

        public static Order GetOrder()
        {
            Order order = new()
            {
                Client = GetClient(),
                Psychologist = GetPsychologist(),
                Cost = 5000,
                Duration = (SessionDuration)1,
                Message = "Программирование на C++",
                SessionDate = DateTime.Now.AddDays(1),
                OrderDate = DateTime.Now,
                PayDate = DateTime.Now.AddHours(2),
                OrderStatus = (OrderStatus)random.Next(0, 3),
                OrderPaymentStatus = (OrderPaymentStatus)random.Next(0, 3),
                IsDeleted = false,
            };

            return order;
        }

        public static Order GetOrder(Client client, Psychologist psychologist)
        {
            Order order = GetOrder();

            order.Client.Id = client.Id;
            order.Psychologist.Id = psychologist.Id;
            order.Client = client;
            order.Psychologist = psychologist;

            return order;
        }

        public static Psychologist GetPsychologist()
        {
            Psychologist psychologist = new()
            {
                Name = "лял",
                LastName = "пвфа",
                Patronymic = "ПВАПВА",
                Gender = Gender.Male,
                BirthDate = DateTime.Parse("1210 - 12 - 12"),
                Phone = "85884859",
                Password = "1235345",
                Email = "ros@fja.com",
                WorkExperience = 10,
                PasportData = "23146456",
                CheckStatus = CheckStatus.Completed,
                Price = 2000,
                IsDeleted = false
            };

            return psychologist;
        }
    }
}
