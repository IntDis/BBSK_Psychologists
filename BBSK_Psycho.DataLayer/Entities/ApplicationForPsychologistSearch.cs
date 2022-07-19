using BBSK_Psycho.DataLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities
{
    public class ApplicationForPsychologistSearch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Description { get; set; }

        public Gender PsychologistGender { get; set; } // enum

        public decimal CostMin { get; set; }

        public decimal CostMax { get; set; }

        public DateTime Date { get; set; }

        public TimeOfDay Time { get; set; } // enum

        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}
