using SupportApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SupportApp.Core.Mentors
{
    public interface IMentorsRepository
    {
        Task<Mentor> CreateMentor(User input);

        Task<Mentor> GetMentorByUserName(string userName);
    }
}
