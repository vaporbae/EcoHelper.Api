namespace EcoHelper.Test.Infrastructure
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using EcoHelper.Application.Helpers;
    using EcoHelper.Domain.Entities;
    using EcoHelper.Persistence;
    using System.Collections.Generic;

    public static class EcoHelperContextFactory
    {
        public static EcoHelperDbContext Create()
        {
            var options = new DbContextOptionsBuilder<EcoHelperDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging(true)
                .Options;

            var context = new EcoHelperDbContext(options);
            var saltedPassword1 = PasswordHelper.CreateHash("test1234");
            var saltedPassword2 = PasswordHelper.CreateHash("test4321");

            context.Users.AddRange(new[]
            {
                new User {Id = 2, Email = "test1@test.com", Name = "test1", Password = saltedPassword1},
                new User {Id = 3, Email = "test2@test.com", Name = "test2", Password = saltedPassword2},
                new User {Id = 4, Email = "test3@test.com", Name = "test3", Password = saltedPassword2},
                new User {Id = 5, Email = "test4@test.com", Name = "test4", Password = saltedPassword2},
                new User {Id = 6, Email = "test5@test.com", Name = "test5", Password = saltedPassword1},
                new User {Id = 7, Email = "test6@test.com", Name = "test6", Password = saltedPassword1},
                new User {Id = 8, Email = "test7@test.com", Name = "test7", Password = saltedPassword1}
            });

            context.Questions.AddRange(new[]
            {
                new Question{Id=10, QuestionText="test 1", Answers = new List<Answer>()},
                new Question{Id=11, QuestionText="test 2", Answers = new List<Answer>()},
                new Question{Id=12, QuestionText="test 3", Answers = new List<Answer>()},
                new Question{Id=13, QuestionText="test 4", Answers = new List<Answer>()},
                new Question{Id=14, QuestionText="test 5", Answers = new List<Answer>()}
            });

            context.Answers.AddRange(new[]
            {
                new Answer{Id=10, AnswerText="hababa", IsCorrect=false, QuestionId=11},
                new Answer{Id=11, AnswerText="hababa1", IsCorrect=false, QuestionId=11},
                new Answer{Id=12, AnswerText="hababa", IsCorrect=false, QuestionId=11}
            });

            context.BaseVersions.AddRange(new[]
           {
                new BaseVersion{Id=10, Ver=1.01},
                new BaseVersion{Id=11, Ver=1.02},
                new BaseVersion{Id=12, Ver=1.03}
            });

            context.Dumpsters.AddRange(new[]
            {
                new Dumpster{Id=10, Name="test1", InterestingFacts=new List<InterestingFact>(), Garbages=new List<Garbage>()},
                new Dumpster{Id=11, Name="test2", InterestingFacts=new List<InterestingFact>(), Garbages=new List<Garbage>()},
                new Dumpster{Id=13, Name="test3", InterestingFacts=new List<InterestingFact>(), Garbages=new List<Garbage>()},
                new Dumpster{Id=14, Name="test4", InterestingFacts=new List<InterestingFact>(), Garbages=new List<Garbage>()},
                new Dumpster{Id=15, Name="test5", InterestingFacts=new List<InterestingFact>(), Garbages=new List<Garbage>()}


            });

            context.Garbages.AddRangeAsync(new[]
            {
                new Garbage{Id=10, Name="test 1", DumpsterId=11},
                new Garbage{Id=11, Name="test 2", DumpsterId=11}

            });

            context.InterestingFacts.AddRange(new[]
            {
                new InterestingFact{Id=10, Title="test1", Description="test123" },
                new InterestingFact{Id=11, Title="test12", Description="test1234" }
            });


            context.SaveChanges();

            return context;
        }

        public static void Destroy(EcoHelperDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
