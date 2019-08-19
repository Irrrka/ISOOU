namespace ISOOU.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;

    internal class CriteriasSeeder : ISeeder
    {
        public async Task SeedAsync(ISOOUDbContext dbContext, IServiceProvider serviceProvider)
        {

            if (!dbContext.Criterias.Any())
            {

                dbContext.Criterias.AddRange(
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.ParentPermanentCitySofiaCriteria),
                        DisplayName = "Постоянен адрес на територията на Столична община на поне единия от родителите",
                        Scores = GlobalConstants.ParentPermanentCitySofiaCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.ParentCurrentCitySofiaCriteria),
                        DisplayName = "Настоящ адрес на територията на Столична община на поне единия от родителите",
                        Scores = GlobalConstants.ParentCurrentCitySofiaCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.ParentPermanentDistrictSchoolCriteria),
                        DisplayName = "Постоянен адрес на територията на административния район на училището на поне единия от родителите",
                        Scores = GlobalConstants.ParentPermanentDistrictSchoolCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.ParentCurrentDistrictSchoolCriteria),
                        DisplayName = "Настоящ адрес на територията на административния район на училището на поне единия от родителите",
                        Scores = GlobalConstants.ParentCurrentDistrictSchoolCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.MotherHasWorkCriteria),
                        DisplayName = "Работещ родител",
                        Scores = GlobalConstants.MotherHasWorkCriteria,
                    },
                    new Criteria
                      {
                          Name = nameof(GlobalConstants.FatherHasWorkCriteria),
                          DisplayName = "Работещ родител",
                          Scores = GlobalConstants.FatherHasWorkCriteria,
                      },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.ParentHasWorkInDistrictSchoolCriteria),
                        DisplayName = "Работещ родител в района на училището",
                        Scores = GlobalConstants.ParentHasWorkInDistrictSchoolCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.HasVisitKGCriteria),
                        DisplayName = "Дете, което е посещавало общинска детска градина, регистрирана на Столична община",
                        Scores = GlobalConstants.HasVisitKGCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.HasNoParentCriteria),
                        DisplayName = "Дете с двама починали родители",
                        Scores = GlobalConstants.HasNoParentCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.HasOneParentCriteria),
                        DisplayName = "Дете с един починал родител",
                        Scores = GlobalConstants.HasOneParentCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.HasManyBrothersOrSistersCriteria),
                        DisplayName = "Дете, което живее в семейство с повече деца",
                        Scores = GlobalConstants.HasManyBrothersOrSistersCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.HasSENCriteria),
                        DisplayName = "Дете със специални образователни потребности",
                        Scores = GlobalConstants.HasSENCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.HasDeseasCriteria),
                        DisplayName = "Дете с хронични заболявания",
                        Scores = GlobalConstants.HasDeseasCriteria,
                    },
                    new Criteria
                    {
                        Name = nameof(GlobalConstants.HasAllTheImmunizations),
                        DisplayName = "Дете с всички задължителни имунизации",
                        Scores = GlobalConstants.HasAllTheImmunizations,
                    });
            }
        }
    }
}