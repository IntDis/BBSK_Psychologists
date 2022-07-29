using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities
{
    public class Psychologist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }      
        public string Patronymic { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }      
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? WorkExperience { get; set; }
        public string PasportData { get; set; }
        public CheckStatus CheckStatus { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }


        public List<Order> Orders { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Schedule> Schedules { get; set; }
        public List<TherapyMethod> TherapyMethods { get; set; }
        public List<Problem> Problems { get; set; }
        public List<Education> Educations { get; set; }
    }
}
