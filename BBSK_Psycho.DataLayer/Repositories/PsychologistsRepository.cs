using BBSK_Psycho.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBSK_Psycho.DataLayer.Repositories
{
    public class PsychologistsRepository : IPsychologistsRepository
    {
        private readonly BBSK_PsychoContext _context;

        public PsychologistsRepository(BBSK_PsychoContext context)
        {
            _context = context;
        }

        public Psychologist? GetPsychologist(int id) =>
            _context.Psychologists
            .Include(e => e.Educations)
            .Include(tm => tm.TherapyMethods)
            .Include(p => p.Problems)
            .Include(s => s.Schedules)
            .Include(c => c.Comments)
            .Include(c => c.Comments)
            .FirstOrDefault(p => p.Id == id);

        public List <Psychologist> GetAllPsychologists() => _context.Psychologists.ToList();

        public List<Comment> GetCommentsByPsychologistId(int id) => _context.Comments.Where(p => p.IsDeleted == false && p.Psychologist.Id == id).ToList();

        public int AddPsychologist (Psychologist psychologist)
        {
            _context.Psychologists.Add(psychologist);
            _context.SaveChanges();

            return psychologist.Id;
        }

        public Comment AddCommentToPsyhologist(Comment comment, int psychologistId)
        {
            var psycho = GetPsychologist(psychologistId);
            comment.Psychologist = psycho;

            var client = _context.Clients.FirstOrDefault(c => c.Id == comment.Client.Id);
            comment.Client = client;

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return comment;
        }

        //public int AddRequestPsyhologistSearch(ApplicationForPsychologistSearch applicationForPsychologist)
        //{

        //}

        public void UpdatePsychologist(Psychologist newProperty, int id)
        {  
            var psychologist = GetPsychologist(id);
            psychologist.Gender = newProperty.Gender;
            psychologist.Phone = newProperty.Phone;
            psychologist.TherapyMethods = newProperty.TherapyMethods;
            psychologist.Email = newProperty.Email;
            psychologist.Educations = newProperty.Educations;
            psychologist.Schedules = newProperty.Schedules;
            psychologist.Price = newProperty.Price;
            psychologist.Password = newProperty.Password;
            psychologist.Problems = newProperty.Problems;
            _context.Psychologists.Update(psychologist);
            _context.SaveChanges();
        }

        public void DeletePsychologist (int id)
        {
            var psychologist = _context.Psychologists.FirstOrDefault(o => o.Id == id);
            psychologist.IsDeleted = true;
            _context.SaveChanges();
            //_context.Psychologists.Remove(new Psychologist { Id=id});
            //_context.SaveChanges ();
        }
    }
}
