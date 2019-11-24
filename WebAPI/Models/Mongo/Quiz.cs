using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Mongo
{
    public class Quiz
    {
        public string  UserId { get; set; }
        public string  Username { get; set; }
        public string  Email { get; set; }
        public string  Domain { get; set; }
        public List<CriQnFeedback> criQnFeedbacks { get; set; }
    }
}
