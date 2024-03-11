using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace Lab_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Зміна кодування консолі для відображення українських букв
            Console.OutputEncoding = UTF8Encoding.UTF8;


            // Ініціалізація колекцій
            List<Teacher> teachers = new List<Teacher>
            {
                new Teacher { Id = 1, FullName = "Перлов Василь Сергійович", Faculty = "ФІОТ", Department = "Кафедра ІСТ" },
                new Teacher { Id = 2, FullName = "Сковорода Олена Михайлівна", Faculty = "ФІОТ", Department = "Кафедра ІПІ" },
                new Teacher { Id = 3, FullName = "Григорчук Оксана Василівна", Faculty = "ФІОТ", Department = "Кафедра ОТ" },
                new Teacher { Id = 4, FullName = "Іванов Назар Остапенко", Faculty = "ФІОТ", Department = "Кафедра ІСТ" },
                new Teacher { Id = 5, FullName = "Карпачов Дмитро Васильович", Faculty = "ФІОТ", Department = "Кафедра ОТ" },
                new Teacher { Id = 6, FullName = "Якуш Сергій Володимирович", Faculty = "ФІОТ", Department = "Кафедра ІПІ" },
                new Teacher { Id = 7, FullName = "Підмогильна Олена Михайлівна", Faculty = "ФІОТ", Department = "Кафедра ОТ" },
                new Teacher { Id = 8, FullName = "Шкара Василь Сименович", Faculty = "ФІОТ", Department = "Кафедра ІСТ" },
            };

            List<Subject> subjects = new List<Subject>
            {
                new Subject { Id = 1, Name = "Програмування" },
                new Subject { Id = 2, Name = "Вища математика" },
                new Subject { Id = 3, Name = "Англійська" },
                new Subject { Id = 4, Name = "Бази даних" },
                new Subject { Id = 5, Name = "Системна інженерія" },
                new Subject { Id = 6, Name = "Кодування" },
                new Subject { Id = 7, Name = "Теорія автоматичного керування" },
                new Subject { Id = 8, Name = "Фізкультура" },
            };


            List<Classroom> classrooms = new List<Classroom>
            {
                new Classroom { Id = 1, Name = "101" },
                new Classroom { Id = 2, Name = "102" },
                new Classroom { Id = 3, Name = "103" },
                new Classroom { Id = 4, Name = "104" },

            };

            List<Group> groups = new List<Group>
            {
                new Group { Id = 1, Name = "Група 11" },
                new Group { Id = 2, Name = "Група 12" },
                new Group { Id = 3, Name = "Група 13" },
                new Group { Id = 4, Name = "Група 14" },
            };

            List<TeacherSubject> teacherSubjects = new List<TeacherSubject>
            {
                new TeacherSubject { TeacherId = 1, SubjectId = 1 },
                new TeacherSubject { TeacherId = 2, SubjectId = 2 },
                new TeacherSubject { TeacherId = 3, SubjectId = 3 },
                new TeacherSubject { TeacherId = 4, SubjectId = 4 },
                new TeacherSubject { TeacherId = 5, SubjectId = 5 },
                new TeacherSubject { TeacherId = 6, SubjectId = 6 },
                new TeacherSubject { TeacherId = 7, SubjectId = 7 },
                new TeacherSubject { TeacherId = 8, SubjectId = 8 },
                new TeacherSubject { TeacherId = 1, SubjectId = 8 },
                new TeacherSubject { TeacherId = 2, SubjectId = 3 },
            };


            // -----------------------------Запити-----------------------------------------------------------------------------
            // 1. 
            Console.WriteLine("Вивести список вчителів з кафедрою \"Кафедра ІСТ\"");
            var teachersIST = teachers.Where(t => t.Department == "Кафедра ІСТ");
            foreach (var teacher in teachersIST)
            {
                Console.WriteLine(teacher);
            }

            // 2. 
            Console.WriteLine("\n\n\nВивести список предметів, які викладає вчитель з Id=1");
            var teacher1Subjects = teacherSubjects.Where(ts => ts.TeacherId == 1).Select(ts => subjects.First(s => s.Id == ts.SubjectId));
            foreach (var subject in teacher1Subjects)
            {
                Console.WriteLine(subject);
            }

            // 3. 
            Console.WriteLine("\n\n\nВивести список груп та кількість вчителів, які їх ведуть");
            var groupTeacherCount = teachers.GroupBy(t => t.Department).Select(g => new { Department = g.Key, TeacherCount = g.Count() });
            foreach (var item in groupTeacherCount)
            {
                Console.WriteLine($"Department: {item.Department}, Teacher count: {item.TeacherCount}");
            }

            // 4. 
            Console.WriteLine("\n\n\nВивести список вчителів, які ведуть курс \"Програмування\" та \"Бази даних\"");
            var teachersTeachingProgrammingAndDB = teacherSubjects
                .Where(ts => ts.SubjectId == 1 || ts.SubjectId == 4)
                .Select(ts => teachers.First(t => t.Id == ts.TeacherId))
                .Distinct();
            foreach(var techers in teachersTeachingProgrammingAndDB)
            {
                Console.WriteLine(techers);
            }

            // 5. 
            Console.WriteLine("\n\n\nВивести список предметів, які веде вчитель, якого звати Олена");
            var olenaTeachers = teachers.Where(t => t.FullName.Contains("Олена"));
            var olenaTeacherSubjects = teacherSubjects.Where(ts => olenaTeachers.Any(ot => ot.Id == ts.TeacherId)).Select(ts => subjects.First(s => s.Id == ts.SubjectId));
            foreach (var item in olenaTeachers)
            {
                Console.WriteLine(item);
            }

            // 6. 
            Console.WriteLine("\n\n\nВивести список вчителів, які ведуть курс \"Фізкультура\", відсортований за прізвищем");
            var physicalEducationTeachers = teacherSubjects
                .Where(ts => ts.SubjectId == 8)
                .Select(ts => teachers.First(t => t.Id == ts.TeacherId))
                .OrderBy(t => t.FullName.Split(' ').Last());
            foreach(var physicalEducationTeacher in physicalEducationTeachers)
            {
                Console.WriteLine(physicalEducationTeacher);
            }

            // 7. 
            Console.WriteLine("\n\n\nВивести список вчителів, згрупований за факультетом та кафедрою");
            var departmentTeachers = teachers.GroupBy(t => new { t.Faculty, t.Department });
            foreach (var group in departmentTeachers)
            {
                Console.WriteLine($"Faculty: {group.Key.Faculty}, Department: {group.Key.Department}");
                foreach (var teacher in group)
                {
                    Console.WriteLine($" - {teacher.FullName}");
                }
            }

            // 8. 
            Console.WriteLine("\n\n\nПобудувати список пар \"Вчитель - Предмет\", які викладаються");
            var teacherSubjectPairs = teachers
                .Join(teacherSubjects, t => t.Id, ts => ts.TeacherId, (t, ts) => new { Teacher = t, SubjectId = ts.SubjectId })
                .Join(subjects, ts => ts.SubjectId, s => s.Id, (ts, s) => new { ts.Teacher, Subject = s });
            foreach (var pair in teacherSubjectPairs)
            {
                Console.WriteLine($"Teacher: {pair.Teacher.FullName}, Subject: {pair.Subject.Name}");
            }

            // 9. 
            Console.WriteLine("\n\n\nВивести список кафедр без повторень");
            var uniqueDepartments = teachers.Select(t => t.Department).Distinct();
            foreach (var department in uniqueDepartments)
            {
                Console.WriteLine(department);
            }

            // 10. 
            Console.WriteLine("\n\n\nВивести список предметів, в яких немає викладачів");
            var subjectsWithoutTeachers = subjects.Where(s => !teacherSubjects.Any(ts => ts.SubjectId == s.Id));
            foreach(var subject in subjectsWithoutTeachers) 
            {
                Console.WriteLine(subject);
            }

            // 11.
            Console.WriteLine("\n\n\nВивести список вчителів, які не ведуть жодного предмета");
            var teachersWithoutSubjects = teachers.Where(t => !teacherSubjects.Any(ts => ts.TeacherId == t.Id));
            foreach (var teacher in teachersWithoutSubjects)
            {
                Console.WriteLine(teacher);
            }

            // 12.
            Console.WriteLine("\n\n\nВивести список груп, які не мають викладачів");
            var groupsWithoutTeachers = groups.Where(g => !teachers.Any(t => t.Department == g.Name));
            foreach (var group in groupsWithoutTeachers)
            {
                Console.WriteLine(group);
            }

            // 13.
            Console.WriteLine("\n\n\nВивести список вчителів, які ведуть хоча б один предмет на кафедрі \"Кафедра ІПІ\"");
            var teachersFromIPI = teachers.Where(t => t.Department == "Кафедра ІПІ" && teacherSubjects.Any(ts => ts.TeacherId == t.Id));
            foreach (var teacher in teachersFromIPI)
            {
                Console.WriteLine(teacher);
            }

            // 14.
            Console.WriteLine("\n\n\nВивести кількість викладачів на кожній кафедрі");
            var departmentTeacherCount = teachers.GroupBy(t => t.Department).Select(g => new { Department = g.Key, TeacherCount = g.Count() });
            foreach (var item in departmentTeacherCount)
            {
                Console.WriteLine($"Department: {item.Department}, Teacher count: {item.TeacherCount}");
            }

            // 15.
            Console.WriteLine("\n\n\nВивести назви предметів, які веде хоча б один викладач з факультету \"ФІОТ\"");
            var subjectsFromFIOT = teacherSubjects
                .Where(ts => teachers.Any(t => t.Faculty == "ФІОТ" && t.Id == ts.TeacherId))
                .Select(ts => subjects.First(s => s.Id == ts.SubjectId))
                .Distinct();
            foreach (var subject in subjectsFromFIOT)
            {
                Console.WriteLine(subject);
            }

            // 16.
            Console.WriteLine("\n\n\nВивести список вчителів, які ведуть більше одного предмету");
            var teachersTeachingMultipleSubjects = teacherSubjects
                .GroupBy(ts => ts.TeacherId)
                .Where(g => g.Count() > 1)
                .Select(g => teachers.First(t => t.Id == g.Key));
            foreach (var teacher in teachersTeachingMultipleSubjects)
            {
                Console.WriteLine(teacher);
            }

            // 17.
            Console.WriteLine("\n\n\nВивести список вчителів, які ведуть тільки один предмет");
            var teachersTeachingSingleSubject = teacherSubjects
                .GroupBy(ts => ts.TeacherId)
                .Where(g => g.Count() == 1)
                .Select(g => teachers.First(t => t.Id == g.Key));
            foreach (var teacher in teachersTeachingSingleSubject)
            {
                Console.WriteLine(teacher);
            }

            // 18.
            Console.WriteLine("\n\n\nВивести список вчителів та кафедр, на яких вони працюють, відсортований за назвою кафедри");
            var teachersWithDepartmentsSorted = teachers.OrderBy(t => t.Department).Select(t => new { Teacher = t.FullName, Department = t.Department });
            foreach (var item in teachersWithDepartmentsSorted)
            {
                Console.WriteLine($"Teacher: {item.Teacher}, Department: {item.Department}");
            }

            // 19.
            Console.WriteLine("\n\n\nВивести список кабінетів, в яких ведуться заняття");
            var occupiedClassrooms = teacherSubjects
                .Join(classrooms, ts => ts.SubjectId, c => c.Id, (ts, c) => c)
                .Distinct();
            foreach (var classroom in occupiedClassrooms)
            {
                Console.WriteLine(classroom.Name);
            }

            // 20.
            Console.WriteLine("\n\n\nВивести повне ПІБ викладача");
            var teachersFullName = teachers.Select(t => t.FullName);
            foreach (var subject in teachersFullName)
            {
                Console.WriteLine(subject);
            }
        }
    }

    /// <summary>
    /// Клас викладача
    /// </summary>
    public class Teacher
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }

        public override string ToString()
        {
            return $"{Id} {FullName} {Faculty} {Department}";
        }
    }

    /// <summary>
    /// Клас предмета
    /// </summary>
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
    /// <summary>
    /// Клас який описує зв'язок викладачів та предметів
    /// </summary>
    public class TeacherSubject
    {
        public int TeacherId { get; set; }
        public int SubjectId { get; set; }
        
        public override string ToString()
        {
            return $"{TeacherId} {SubjectId}";
        }
    }

    /// <summary>
    /// Клас навчального класу
    /// </summary>
    public class Classroom
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// Клас навчальної групи
    /// </summary>
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }

    /// <summary>
    /// Клас уроку
    /// </summary>
    public class Lesson
    {
        public int Id { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Classroom Classroom { get; set; }
        public List<Group> Groups { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"id = {Id} Викладач = {Teacher.FullName} Предмет = {Subject.Name}";
        }
    }

}
