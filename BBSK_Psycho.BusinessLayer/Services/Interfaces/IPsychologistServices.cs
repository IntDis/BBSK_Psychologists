using BBSK_Psycho.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.BusinessLayer.Services.Interfaces
{
    public interface IPsychologistServices
    {
        Psychologist? GetPsychologist(int id, ClaimModel claim);
        List<Psychologist> GetAllPsychologists(ClaimModel claim);
        public int AddCommentToPsyhologist(Comment comment, int psychologistId, ClaimModel claim);
        List<Comment> GetCommentsByPsychologistId(int id, ClaimModel claim);
        List <Order> GetOrdersByPsychologistId(int id, ClaimModel claim);
        int AddPsychologist(Psychologist psychologist);
        void UpdatePsychologist(Psychologist psychologist, int id, ClaimModel claim);
        void DeletePsychologist(int id, ClaimModel claim);
    }
}
