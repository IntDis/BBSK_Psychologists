﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime Date  { get; set; }
        public DateTime Time  { get; set; }
        public Psychologist Psychologist { get; set; }

    }
}
