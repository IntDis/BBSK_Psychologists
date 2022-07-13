using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities;

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }

    public DateTime RegistrationDate
    {
        get
        {
            return DateTime.Now;
        }
        set { }
       
    }

    public bool IsDeleted { get; set; }
 


    public List<Order> Orders { get; set; }
    public List<Comment> Comments { get; set; }
    public List <ApplicationForPsychologistSearch> ApplicationForPsychologistSearch { get; set; }
}
