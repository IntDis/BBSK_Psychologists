﻿using System;
using System.Collections.Generic;
using BBSK_Psycho.DataLayer;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models.Responses
{
    public class UpdatePsychologistResponse
    {

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public Gender gender { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int WorkExperience { get; set; }

        public string PasportData { get; set; }

        public List<string> Education { get; set; }  // "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education"

        public CheckStatus checkStatus { get; set; }        //Enum

        public List<string>? TherapyMethods { get; set; }

        public List<string>? Problems { get; set; }

        public decimal Price { get; set; }

        public Dictionary<String, List<String>> Schedule { get; set; }

        public string DenyMessage { get; set; }
    }
}
