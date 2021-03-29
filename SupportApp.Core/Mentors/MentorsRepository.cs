using Microsoft.EntityFrameworkCore;
using SupportApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp.Core.Mentors
{
    public class MentorsRepository : IMentorsRepository
    {      
        private readonly ApplicationDbContext _dbContext;
        public MentorsRepository( ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Mentor> CreateMentor(User input)
        {
            var newMentor = new Mentor()
            {
                UserId =input.Id
            };
            _dbContext.Mentors.Add(newMentor);
            await _dbContext.SaveChangesAsync();
            return newMentor;
            
        }

        public async Task<Mentor> GetMentorByUserName(string userName)
        {
            var mentor = await _dbContext.Mentors.Where(x => x.User.UserName.Contains(userName)).FirstOrDefaultAsync();
            return mentor;           
        }
    }
}
