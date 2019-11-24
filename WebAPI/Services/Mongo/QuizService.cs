using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Mongo;

namespace WebAPI.Services.Mongo
{
    public class QuizService
    {
        private readonly IMongoCollection<Quiz> _quiz;
        public QuizService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _quiz = database.GetCollection<Quiz>(settings.QuizCollectionName);
        }

        public List<Quiz> Get() =>
            _quiz.Find(user => true).ToList();

        public Quiz Get(string id) =>
            _quiz.Find<Quiz>(user => user.UserId == id).FirstOrDefault();

        public Quiz Create(Quiz user)
        {
            _quiz.InsertOne(user);
            return user;
        }

        
       
    }
}
