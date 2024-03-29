﻿using BBSK_Psycho.DataLayer.Entities;
using BBSK_Psycho.DataLayer.Enums;

namespace BBSK_Psycho.Models.Responses;

public class GetAllPsychologistsResponse
{
    public string Name { get; set; }

    public string LastName { get; set; }

    public Gender Gender { get; set; }

    public int WorkExperience { get; set; }

    public List<string> Education { get; set; }  // "2013 - Московский Государственный Университет - Факультет - Степень; Dev Education"

    public List<TherapyMethodResponse> TherapyMethods { get; set; }

    public List<ProblemResponse> Problems { get; set; }

    public decimal Price { get; set; }
}

