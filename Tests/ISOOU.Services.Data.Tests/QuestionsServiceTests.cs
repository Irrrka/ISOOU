namespace ISOOU.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ISOOU.Data;
    using ISOOU.Data.Models;
    using ISOOU.Services.Data.Contracts;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class QuestionsServiceTests : BaseServiceTests
    {
        private IQuestionsService QuestionsServiceMock =>
            this.ServiceProvider.GetRequiredService<IQuestionsService>();

        [Fact]
        public async Task CreateMessage_WithNoExistEmail_ShouldCreateMessage()
        {
            var q1 = new QuestionServiceModel
            {
                Subject = "Zashto ne raboti",
                Content = "Ami ne raboti i ne znam zashto",
            };

            var result = await this.QuestionsServiceMock.CreateMessage(null, q1);

            Assert.True(result == true);
        }

        [Fact]
        public async Task ReadLastMessage_ShouldReturnLastMessage()
        {
            this.SeedTestData(this.DbContext);
            var qLast = new QuestionServiceModel
            {
                Subject = "Zashto zashto zashtooo",
                Content = "Ima tolkova neraboteshti neshta che chak se chudya zashtooo",
            }.To<Question>();
            this.DbContext.Questions.Add(qLast);
            this.DbContext.SaveChanges();

            var actual = await this.QuestionsServiceMock.ReadLastMessage();

            Assert.True(actual.Subject == qLast.Subject);
            Assert.True(actual.Content == qLast.Content);
        }

        [Fact]
        public async Task ReadLastMessage_WithNoMessages_ShouldRetrnNull()
        {
            var actual = await this.QuestionsServiceMock.ReadLastMessage();

            Assert.True(actual == null);
        }

        private void SeedTestData(ISOOUDbContext context)
        {
            context.Questions.AddRange(this.GetTestData());
            context.SaveChanges();
        }

        private List<Question> GetTestData()
        {
            //var user1 = new SystemUser { UserName = "test@test.bg" , };
            var q1 = new Question { Subject = "Zashto ne raboti", Content = "Ami ne raboti i ne znam zashto"};
            var q2 = new Question { Subject = "Zashto raboti", Content = "Ami raboti i znam zashto" };
            return new List<Question>
                                        {
                                            q1, q2,
                                        };
        }
    }
}