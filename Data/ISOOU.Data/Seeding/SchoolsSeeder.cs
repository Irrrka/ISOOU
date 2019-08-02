namespace ISOOU.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Data.Models.Enums;

    internal class SchoolsSeeder : ISeeder
    {
        public async Task SeedAsync(ISOOUDbContext dbContext, IServiceProvider serviceProvider)
        {
            var englishClass = new Class
            {
                FreeSpots = GlobalConstants.FreeSpotsEnglishLanguage,
                Profile = ClassLanguageType.Английски,
            };
            var russianClass = new Class
            {
                FreeSpots = GlobalConstants.FreeSpotsRussianLanguage,
                Profile = ClassLanguageType.Руски,
            };

            var spanishClass = new Class
            {
                FreeSpots = GlobalConstants.FreeSpotsSpanishLanguage,
                Profile = ClassLanguageType.Испански,
            };

            var chineseClass = new Class
            {
                FreeSpots = GlobalConstants.FreeSpotsChineseLanguage,
                Profile = ClassLanguageType.Китайски,
            };

            await dbContext.Classes.AddAsync(englishClass);
            await dbContext.Classes.AddAsync(russianClass);
            await dbContext.Classes.AddAsync(spanishClass);
            await dbContext.Classes.AddAsync(chineseClass);

            District currDistrict;
            //"Връбница"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Връбница");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "61 ОУ \"Св.св.Kирил и Методий\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Елизабета Колева",
                    Email = "ou_61@abv.bg",
                    PhoneNumber = "02/934-54-89",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.737856000000015%2C23.290851799999928&z=19",
                    URLOfSchool = "http://ou-61.org/",
                    District = currDistrict,
                    Address = "ул. \"Ломско шосе\" № 186",
                },
                new School
                {
                    Name = "62 ОУ \"Христо Ботев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "вр.и.д. Димитър Караиванов",
                    Email = "ou62@abv.bg",
                    PhoneNumber = "02/834-16-17",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.739813100000006%2C23.264302399999906&z=19",
                    URLOfSchool = "http://www.62ou.com/",
                    District = currDistrict,
                    Address = "кв. \"Обеля\", ул. \"Ефрем Чучков\" № 26",
                },
                new School
                {
                    Name = "70 ОУ \"Св.Kлимент Охридски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Величка Таседжикова",
                    Email = "school70@abv.bg",
                    PhoneNumber = "02/827-74-92",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.726794500000004%2C23.28269309999996&z=19",
                    URLOfSchool = "http://www.70ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Адам Мицкевич\" № 10",
                });

            //"Възраждане"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Възраждане");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "46 ОУ \"Kонстантин Фотинов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Маргарита Грънчарова",
                    Email = "kfotinov46@abv.bg",
                    PhoneNumber = "02/831-00-49",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.70317129999998%2C23.316551600000025&z=19",
                    URLOfSchool = "http://46ou.net/",
                    District = currDistrict,
                    Address = "бул. \"Христо Ботев\" № 109",
                },
                new School
                {
                    Name = "67 ОУ \"Васил Друмев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Красимир Вълков",
                    Email = "67_school@mail.orbitel.bg",
                    PhoneNumber = "02/822-36-21",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.69301228461862%2C23.29787870192183&z=19",
                    URLOfSchool = string.Empty,
                    District = currDistrict,
                    Address = "ул. \"Гюешево\" № 63",
                },
                new School
                {
                    Name = "76 ОУ \"Уилям Сароян\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Емилия Стефанова",
                    Email = "oy76@abv.bg",
                    PhoneNumber = "02/987-43-44",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.698193300000014%2C23.3166966&z=19",
                    URLOfSchool = "http://www.76ou.eu/",
                    District = currDistrict,
                    Address = "ул. \"Братя Миладинови\" № 9",
                },
                new School
                {
                    Name = "136 ОУ \"Любен Kаравелов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Галина Сахиева",
                    Email = "l.karavelov@dir.bg",
                    PhoneNumber = "02/821-70-88",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.699712300000016%2C23.299815299999977&z=19",
                    URLOfSchool = string.Empty,
                    District = currDistrict,
                    Address = "ул. \"Димитър Петков\" № 116",
                });

            //"Изгрев"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Изгрев");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "11 ОУ \"Св.Пимен Зографски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Августина Петрова",
                    Email = "zograf011@abv.bg",
                    PhoneNumber = "02/862-41-87",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.66289210000002%2C23.343448500000022&z=19",
                    URLOfSchool = "http://11oy.com",
                    District = currDistrict,
                    Address = "ул. \"Никола Габровски\" № 22",
                });

            //"Илинден"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Илинден");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "43 ОУ \"Христо Смирненски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Сергей Богачев",
                    Email = "ou43@mail.bg",
                    PhoneNumber = "02/822-92-7",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.707044399999994%2C23.30130699999995&z=19",
                    URLOfSchool = "http://43ou.net/",
                    District = currDistrict,
                    Address = "бул. \"Сливница\" № 45",
                },
                new School
                {
                    Name = "45 ОУ \"Kонстантин Величков\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Светла Стефанова",
                    Email = "kv45ou@abv.bg",
                    PhoneNumber = "02/822-14-10",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.711020299999994%2C23.296346800000038&z=19",
                    URLOfSchool = "http://www.45ou.bg/",
                    District = currDistrict,
                    Address = "ул.\"Пловдив\" № 20",
                });

            //"Искър"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Искър");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "4 ОУ \"Проф.Джон Атанасов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Галина Шипкалиева",
                    Email = "school4sofia@abv.bg",
                    PhoneNumber = "02/979-09-63",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.6603857095304%2C23.398583349061255&z=19",
                    URLOfSchool = "http://4ou.clients.info-top.com/",
                    District = currDistrict,
                    Address = "ул. \"Тирана\" № 12",
                },
                new School
                {
                    Name = "89 ОУ \"Д - р Христо Стамболски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Тотка Цветанова",
                    Email = "ou_89@abv.bg",
                    PhoneNumber = "02/979-08-38",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.66520490176343%2C23.394650667105452&z=19",
                    URLOfSchool = "http://www.89ousofia.com/",
                    District = currDistrict,
                    Address = "ж.к. \"Дружба - 1\", ул. \"Чудомир Топлодолски\" № 4",
                },
                new School
                {
                    Name = "150 ОУ \"Цар Симеон I\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Радослав Богданов",
                    Email = "csp_150_ou@abv.bg",
                    PhoneNumber = "02/879-67-63",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.64591895912879%2C23.409892094368388&z=19",
                    URLOfSchool = "http://www.150ou.org/",
                    District = currDistrict,
                    Address = "ж.к. \"Дружба - 2\", ул. \"Делийска воденица\" № 11",
                },
                new School
                {
                    Name = "163 ОУ \"Черноризец Храбър\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Катя Дойнова",
                    Email = "school_163@abv.bg",
                    PhoneNumber = "02/978-07-2",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.64451777594269%2C23.40535987554597&z=19",
                    URLOfSchool = "http://163ou.org/",
                    District = currDistrict,
                    Address = "ж.к. \"Дружба - 2\", ул. \"Обиколна\" № 36",
                });

            //"Лозенец"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Лозенец");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "122 ОУ \"Николай Лилиев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Ивета Германова",
                    Email = "122ou@122ou.com",
                    PhoneNumber = "02/865-03-74",
                    URLOfMap = "https://www.google.com/maps/place/ul.+%22Prezviter+Kozma%22+2,+1421+g.k.+Lozenets,+Sofia/data=!4m2!3m1!1s0x40aa85aa7c2887f3:0x51107996835ba355?sa=X&ved=2ahUKEwiEhZvthK3jAhWKIZoKHTNACGgQ8gEwAHoECAoQAQ",
                    URLOfSchool = "https://122ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Презвитер Козма\" 2",
                },
                new School
                {
                    Name = "107 ОУ \"Хан Крум\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Иванка Маринова",
                    Email = "ou107@abv.bg",
                    PhoneNumber = "02/866-20-29",
                    URLOfMap = "https://www.google.com/maps/d/viewer?ll=42.676406987939345%2C23.329929603647884&z=19&mid=1xYlSUjydIF6BL8ONgmjDwTpoymw",
                    URLOfSchool = "http://www.107ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Димитър Димов\" № 13",
                },
                new School
                {
                    Name = "120 ОУ \"Георги С. Раковски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Цветанка Тонева",
                    Email = "ou120@abv.bg",
                    PhoneNumber = "02/866-56-80",
                    URLOfMap = "https://www.google.com/maps/d/viewer?ll=42.683467139210094%2C23.324429079739616&z=19&mid=1xYlSUjydIF6BL8ONgmjDwTpoymw",
                    URLOfSchool = "http://www.daskalo.com/ou120/",
                    District = currDistrict,
                    Address = "ул. \"Папа Йоан Павел II\" № 7\"",
                },
                new School
                {
                    Name = "139 ОУ \"Захарий Kруша\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Румяна Дончева",
                    Email = "zaharikru6a@abv.bg",
                    PhoneNumber = "02/866-77-41",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.67561873535963%2C23.316023605169903&z=19",
                    URLOfSchool = "http://139ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Димитър Хаджикоцев\" № 44",
                });

            //"Люлин"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Люлин");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "33 ОУ \"Санкт Петербург\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Юлия Георгиева",
                    Email = "lulin_33ou@abv.bg",
                    PhoneNumber = "02/824-88-23",
                    URLOfMap = "https://www.google.com/maps/d/viewer?ll=42.71960002000201%2C23.247150345941122&z=19&mid=1xYlSUjydIF6BL8ONgmjDwTpoymw",
                    URLOfSchool = "http://33ou-lulin.org/",
                    District = currDistrict,
                    Address = "ж.к. \"Люлин - 3\" ул. \"309\" № 8",
                },
                new School
                {
                    Name = "77 ОУ \"Kирил и Методий\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Огнян Халембаков",
                    Email = "sof_77ou@abv.bg",
                    PhoneNumber = "02/826-41-63",
                    URLOfMap = "https://www.google.com/maps/d/viewer?ll=42.719186198461195%2C23.22077131558069&z=19&mid=1xYlSUjydIF6BL8ONgmjDwTpoymw",
                    URLOfSchool = "https://www.77ousofia.com/",
                    District = currDistrict,
                    Address = "ул. \"3 март\" № 45",
                },
                new School
                {
                    Name = "103 ОУ \"Васил Левски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Милена Бенкова",
                    Email = "sofia103@abv.bg",
                    PhoneNumber = "02/824-48-85",
                    URLOfMap = "https://www.google.com/maps/d/viewer?ll=42.71950062210194%2C23.232491870336844&z=19&mid=1xYlSUjydIF6BL8ONgmjDwTpoymw",
                    URLOfSchool = "http://103ouvasillevski.bg/",
                    District = currDistrict,
                    Address = "ж.к. \"Филиповци\"",
                });

            //"Младост"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Младост");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "145 ОУ \"Симеон Радев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Антоанета Михайлова",
                    Email = "oy_145@abv.bg",
                    PhoneNumber = "02/877-41-36",
                    URLOfMap = "https://www.google.com/maps/d/viewer?ll=42.6483661337137%2C23.38520513106971&z=19&mid=1xYlSUjydIF6BL8ONgmjDwTpoymw",
                    URLOfSchool = "http://145ou.com/",
                    District = currDistrict,
                    Address = "ж.к. \"Младост - 1А\", ул. \"Ресен\" № 1",
                },
                new School
                {
                    Name = "82 ОУ \"Васил Априлов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Мартин Ганчев",
                    Email = "school82@gbg.bg",
                    PhoneNumber = "02/973-60-40",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.63069408126569%2C23.4095511607984&z=19",
                    URLOfSchool = "",
                    District = currDistrict,
                    Address = "кв. Горубляне, ул. \"Самоковско шосе\" № 41",
                });

            //"Надежда"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Надежда");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "98 НУ \"Св.св.Kирил и Методий\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Антоанета Михайлова",
                    Email = "nu98@abv.bg",
                    PhoneNumber = "02/938-31-49",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.751002174446086%2C23.316778199507553&z=19",
                    URLOfSchool = "http://nu98.org/",
                    District = currDistrict,
                    Address = "кв. \"Илиянци\", ул. \"Махония\" № 2",
                },
                new School
                {
                    Name = "102 ОУ \"Панайот Волов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Румяна-Колева",
                    Email = "p_volov@abv.bg",
                    PhoneNumber = "02/938-26-54",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.735155932950846%2C23.305273285457247&z=19",
                    URLOfSchool = "http://102ou.com/",
                    District = currDistrict,
                    Address = "ж.к. \"Надежда - 4\", ул. \"Звезда\" № 3",
                },
                new School
                {
                    Name = "141 OУ  \"Народни будители\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Таня Иванова",
                    Email = "141sou@gmail.com",
                    PhoneNumber = "02/896-01-00",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.73851124007748%2C23.30841111339157&z=19",
                    URLOfSchool = "http://141ou.com/",
                    District = currDistrict,
                    Address = "ж.к. \"Свобода\", ул. \"Народни будители\"",
                },
                new School
                {
                    Name = "63 ОУ \"Христо Ботев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Карамфилка Новачкова",
                    Email = "hb_63@abv.bg",
                    PhoneNumber = "02/938-29-26",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.774428881099816%2C23.31212821630595&z=19",
                    URLOfSchool = "http://www.63ou-hbotev.org/",
                    District = currDistrict,
                    Address = "кв. \"Требич\", ул. \"Леденика\" № 12",
                },
                new School
                {
                    Name = "16 ОУ \"Райко Жинзифов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Елизабет Иванова",
                    Email = "ou16@abv.bg",
                    PhoneNumber = "02/938-28-90",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.72596450029161%2C23.300098708440032&z=19",
                    URLOfSchool = string.Empty,
                    District = currDistrict,
                    Address = "ул. \"Дравски бой\" № 7",
                },
                new School
                {
                    Name = "ЦПЛР - Център за изкуства, култура и образование \"София\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Марияна Тафрова",
                    Email = "stcrd@abv.bg",
                    PhoneNumber = "02/936-07-67",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.73274500400355%2C23.30812911725434&z=19",
                    URLOfSchool = "http://www.stcrd.com/",
                    District = currDistrict,
                    Address = "ж.к. \"Надежда-2\", ул. \"Св. Никола Нови\" № 22",
                });

            //"Оборище"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Оборище");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "112 ОУ \"Стоян Заимов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Жанета Бранкова",
                    Email = string.Empty,
                    PhoneNumber = "02/846-73-25",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.69816480820887%2C23.33719763505701&z=19",
                    URLOfSchool = "http://112ou.org/index.php",
                    District = currDistrict,
                    Address = "бул. \"Княз Ал.Дондуков\" № 60",
                },
                new School
                {
                    Name = "129 ОУ \"Антим - I\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Богдана Николова",
                    Email = "ou_antim1@mail.bg",
                    PhoneNumber = "02/944-43-51",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.693452852602846%2C23.34729604338804&z=19",
                    URLOfSchool = "http://129ou-sofia.eu/",
                    District = currDistrict,
                    Address = "ул. \"Султан тепе\" № 1",
                });

            //"Подуяне"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Подуяне");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "199 ОУ \"Св.ап.Йоан Богослов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Мони Иванов",
                    Email = "ou199@mail.bg",
                    PhoneNumber = "02/946 6956",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.713865451563215%2C23.37781863246937&z=19",
                    URLOfSchool = "http://ou-199.webnode.com/",
                    District = currDistrict,
                    Address = "ж.к. \"Левски - Г\", ул. \"Поручик Г.Кюмюрджиев\" № 30",
                },
                new School
                {
                    Name = "143 ОУ \"Георги Бенковски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Ирена Дойчинова",
                    Email = "ou143@abv.bg",
                    PhoneNumber = "02/846-51-67",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.702579461397676%2C23.355941619998248&z=19",
                    URLOfSchool = "http://www.143ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Тодорини кукли\" № 9",
                },
                new School
                {
                    Name = "106 ОУ \"Григорий Цамблак\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Ирена Цукева",
                    Email = "ou106g.camblak@abv.bg",
                    PhoneNumber = "02/945-28-79",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.69955171681993%2C23.38172047016087&z=19",
                    URLOfSchool = "http://www.106ou.info/",
                    District = currDistrict,
                    Address = "ул. \"Григорий Цамблак\" № 18",
                },
                new School
                {
                    Name = "49 ОУ \"Бенито Хуарес\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Яница Милева",
                    Email = "ou49@abv.bg",
                    PhoneNumber = "02/847-22-40",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.700953185764774%2C23.36254368075197&z=19",
                    URLOfSchool = "http://www.49ousofia.com/",
                    District = currDistrict,
                    Address = "ул. \"Константин Фотинов\" № 4",
                },
                new School
                {
                    Name = "42 ОУ \"Хаджи Димитър\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Жулиета Александрова",
                    Email = "hdou42@abv.bg",
                    PhoneNumber = "02/840-34-58",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.7063196653868%2C23.345321196114583&z=19",
                    URLOfSchool = "http://www.42ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Ген.Липранди\" № 5",
                });

            //"Триадица"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Триадица");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "20 ОУ \"Тодор Минков\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Анелия Андреева",
                    Email = "ou20sofia@abv.bg",
                    PhoneNumber = "02/954-91-64",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.688706471214175%2C23.3153743882217&z=19",
                    URLOfSchool = "http://20ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Kняз Борис I\" № 27",
                },
                new School
                {
                    Name = "41 ОУ \"Св.Патриарх Евтимий\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Александра Топалова",
                    Email = "evtimischool@abv.bg",
                    PhoneNumber = "02/987-88-47",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.69248831417614%2C23.31688174914325&z=19",
                    URLOfSchool = "http://www.41ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Цар Самуил\" № 24",
                },
                new School
                {
                    Name = "104 ОУ \"Захари Стоянов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Капка Велинова",
                    Email = "ou_104@abv.bg",
                    PhoneNumber = "02/859-51-92",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.66540256404757%2C23.29758707546307&z=19",
                    URLOfSchool = "http://104ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Костенски водопад\" № 60",
                },
                new School
                {
                    Name = "126 ОУ \"Петко Ю.Тодоров\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Росен Димитров",
                    Email = "ros2@abv.bg",
                    PhoneNumber = "02/859-61-29",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.67361418239139%2C23.2964169355821&z=19",
                    URLOfSchool = "http://www.126ou.net/",
                    District = currDistrict,
                    Address = "бул. \"България\" № 43",
                });

            //"Средец"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Средец");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "6 ОУ \"Граф Игнатиев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Ивелина Спасова",
                    Email = "shesto_ou@abv.bg",
                    PhoneNumber = "02/988-17-13",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.69071236506488%2C23.326956062040722&z=19",
                    URLOfSchool = "http://www.6ou.org/",
                    District = currDistrict,
                    Address = "ул. \"6 - ти септември\" № 16",
                },
                new School
                {
                    Name = "38 ОУ \"Васил Априлов\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Виолета Игова",
                    Email = "aprilov_38ou@abv.bg",
                    PhoneNumber = "02/846-53-58",
                    URLOfMap = "https://www.google.com/maps/d/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.693452852602846%2C23.34729604338804&z=19",
                    URLOfSchool = "http://129ou-sofia.eu/",
                    District = currDistrict,
                    Address = "ул. \"Шипка\" № 40",
                });

            //"Сердика"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Сердика");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "48 ОУ \"Йосиф Kовачев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Даниела Костова",
                    Email = "ou48@abv.bg",
                    PhoneNumber = "02/831-30-87",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.707549538831614%2C23.31991750638224&z=19",
                    URLOfSchool = "http://48ou.net/ekip.html",
                    District = currDistrict,
                    Address = "ул. \"Kлокотница\" № 21",
                },
                new School
                {
                    Name = "58 ОУ \"Сергей Румянцев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Костадин Мустайков",
                    Email = "sergei_rumiancev@abv.bg",
                    PhoneNumber = "02/936-67-55",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.725045292515134%2C23.338714672431365&z=19",
                    URLOfSchool = "http://58ou.net/",
                    District = currDistrict,
                    Address = "ул. \"Железопътна\" № 65",
                },
                new School
                {
                    Name = "59 ОУ \"Васил Левски\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Марияна Божкова",
                    Email = "sou59@mail.bg",
                    PhoneNumber = "02/936-67-16",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.71625383537773%2C23.346681445945933&z=19",
                    URLOfSchool = "http://59-ou-vasil-levski.webnode.com/",
                    District = currDistrict,
                    Address = "ул. \"Кестен\" № 1",
                },
                new School
                {
                    Name = "60 ОУ \"Св.св.Kирил и Методий\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Таня Борисова",
                    Email = "school60@abv.bg",
                    PhoneNumber = "02/936-68-75",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.74206103961984%2C23.344054861202494&z=19",
                    URLOfSchool = "http://www.60ou.org/",
                    District = currDistrict,
                    Address = "ул. \"Hаука\" № 2",
                },
                new School
                {
                    Name = "100 ОУ \"Hайден Геров\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Румяна Темелкова",
                    Email = "oung100@mail.bg",
                    PhoneNumber = "02/931-60-53",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.7145323305044%2C23.301612847306274&z=19",
                    URLOfSchool = "http://www.100oung.com/",
                    District = currDistrict,
                    Address = "ул. \"Иван Йосифов\" № 68",
                });

            //"Красна поляна"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Красна Поляна");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "75 ОУ \"Тодор Kаблешков\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Антон Сивков",
                    Email = "ou75@abv.bg",
                    PhoneNumber = "02/822-15-67",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.69262072002462%2C23.26639706369815&z=19",
                    URLOfSchool = "http://www.75ou.com/",
                    District = currDistrict,
                    Address = "кв. \"Факултета\", ул. \"Възкресение\" № 151",
                },
                new School
                {
                    Name = "92 ОУ \"Димитър Талев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Красимира Орсова",
                    Email = "dimitar_talev@mail.bg",
                    PhoneNumber = "02/828-47-33",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.693349743694945%2C23.279738853631216&z=19",
                    URLOfSchool = "http://www.92dimitartalev.com/",
                    District = currDistrict,
                    Address = "ж.к. \"Красна поляна\", ул. \"Добротич\" № 21",
                },
                new School
                {
                    Name = "147 ОУ \"Йордан Радичков\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Людмил Лачев",
                    Email = "school_147@abv.bg",
                    PhoneNumber = "02/920-80-89",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.698796019217816%2C23.292626518133147&z=19",
                    URLOfSchool = "http://www.147ou.info/",
                    District = currDistrict,
                    Address = "ж.к. \"Разсадника\",ул. \"Д - р Калинков\" № 40",
                });

            //"Красо село"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Красно Село");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "25 ОУ \"Д - р Петър Берон\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Надка Христова",
                    Email = "ou25@abv.bg",
                    PhoneNumber = "02/952-11-70",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.68138061779841%2C23.286544575223047&z=19",
                    URLOfSchool = "http://www.25ou.com/",
                    District = currDistrict,
                    Address = "ул. \"Балканджи Йово\" № 22",
                },
                new School
                {
                    Name = "34 ОУ \"Стою Шишков\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Цеца Тонева",
                    Email = "oy34@abv.bg",
                    PhoneNumber = "02/859-41-31",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.67070518505562%2C23.28634877396496&z=19",
                    URLOfSchool = "http://www.34ou.org/",
                    District = currDistrict,
                    Address = "ул. \"Родопски извор\" № 43",
                },
                new School
                {
                    Name = "142 ОУ с РЧО \"Веселин Ханчев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Юлияна Петрова",
                    Email = "ou142@abv.bg",
                    PhoneNumber = "02/955-58-34",
                    URLOfMap = "https://www.google.com/maps/d/u/0/edit?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.672844367665505%2C23.28047521592964&z=19",
                    URLOfSchool = "http://142ou.org/",
                    District = currDistrict,
                    Address = "ул. \"Пчела\" № 21",
                });

            //"Овча купел"
            currDistrict = dbContext.Districts.FirstOrDefault(n => n.Name == "Овча Купел");
            dbContext.Schools.AddRange(
                new School
                {
                    Name = "53 ОУ \"Hиколай Хрелков\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "Малинка Дилова Левакова",
                    Email = "xrelkov@abv.bg",
                    PhoneNumber = "02/957-69-59",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.67705771188079%2C23.23386114729078&z=19",
                    URLOfSchool = "http://53ou.com/",
                    District = currDistrict,
                    Address = "кв. \"Горна баня\",ул. \"Обзор\" № 6",
                },
                new School
                {
                    Name = "72 ОУ \"Христо Ботев\"",
                    SchoolClasses = new List<SchoolClass>()
                    {
                        new SchoolClass
                        {
                        Class = englishClass,
                        ClassId = englishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = spanishClass,
                        ClassId = spanishClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = russianClass,
                        ClassId = russianClass.Id,
                        },
                        new SchoolClass
                        {
                        Class = chineseClass,
                        ClassId = chineseClass.Id,
                        },
                    },
                    DirectorName = "вр.и.д. Евелина Иванова",
                    Email = "ou72@abv.bg",
                    PhoneNumber = "02/929-53-50",
                    URLOfMap = "https://www.google.com/maps/d/viewer?mid=1xYlSUjydIF6BL8ONgmjDwTpoymw&ll=42.69490282713998%2C23.225142981020326&z=19",
                    URLOfSchool = "http://ou72.org/",
                    District = currDistrict,
                    Address = "кв. \"Суходол\",ул. \"Овче поле\" № 14",
                });
        }
    }
}