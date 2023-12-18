using Microsoft.EntityFrameworkCore;
using WebApplicationForTests.Models;

namespace WebApplicationForTests.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestResult> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Results)
                .WithOne(r => r.User);

            modelBuilder.Entity<Test>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Test>()
                .HasMany(t => t.Questions)
                .WithOne(q => q.Test);
            modelBuilder.Entity<Test>()
                .HasMany(t => t.Results)
                .WithOne(r => r.Test);

            modelBuilder.Entity<Question>()
                .HasKey(q => q.Id);
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question);

            modelBuilder.Entity<Answer>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<TestResult>()
                .HasKey(a => a.Id);
            modelBuilder.Entity<TestResult>()
                .Property(tr => tr.Score)
                .HasPrecision(3, 2);

            modelBuilder.Entity<Test>().HasData(
                new Test { Id = 1, Title = "Вступ у C#" },
                new Test { Id = 2, Title = "Базові елементи мови C#" },
                new Test { Id = 3, Title = "Поняття бази даних" }
                );

            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, Content = "1. Керованим кодом можуть називати програму створену на:", TestId = 1 },
                new Question { Id = 2, Content = "2. Рядок коду Console.WriteLine(\"Hello, World!\"); виведе на консоль", TestId = 1 },
                new Question { Id = 3, Content = "3. Методом для вирішення проблеми виведення української літери \"і\" є", TestId = 1 },
                new Question { Id = 4, Content = "4. Знак $ в рядку Console.WriteLine($\"Привіт {name}\")", TestId = 1 },
                new Question { Id = 5, Content = "5. Чи забезпечує мова C# та фреймворк .NET автоматичне збирання сміття?", TestId = 1 },
                new Question { Id = 6, Content = "6. string? name означає що змінна name може зберігати", TestId = 1 },
                new Question { Id = 7, Content = "7. Що таке алгоритм у програмуванні?", TestId = 1 },
                new Question { Id = 8, Content = "8. Що таке оператор у програмуванні?", TestId = 1 },
                new Question { Id = 9, Content = "9. Що таке цикл у програмуванні?", TestId = 1 },
                new Question { Id = 10, Content = "10. Що таке масив у програмуванні?", TestId = 1 },

                new Question { Id = 11, Content = "1. Що таке змінна у програмуванні?", TestId = 2 },
                new Question { Id = 12, Content = "2. При виконанні коду string name; string Name;", TestId = 2 },
                new Question { Id = 13, Content = "3. Рядок коду Console.WriteLine(2 & 5); виведе на консоль", TestId = 2 },
                new Question { Id = 14, Content = "4. Після виконання коду int x = 5; int z1 = x++; int z2 = ++x; змінні z1 і z2", TestId = 2 },
                new Question { Id = 15, Content = "5. Тип даних object", TestId = 2 },
                new Question { Id = 16, Content = "6. Безпечними автоматичними перетвореннями можуть бути перетворення з типу float в", TestId = 2 },
                new Question { Id = 17, Content = "7. int x = 0; x +=10; Після виконання коду змінна x міститиме?", TestId = 2 },
                new Question { Id = 18, Content = "8. byte a = 4; byte b = a + 70; Результатом виконання цього коду b буде?", TestId = 2 },
                new Question { Id = 19, Content = "9. Оператор sizeof() – дозволяє:", TestId = 2 },
                new Question { Id = 20, Content = "10. Оберіть ідентифікатор, який відповідає правилам найменування Camel case:", TestId = 2 },

                new Question { Id = 21, Content = "1. База даних - це:", TestId = 3 },
                new Question { Id = 22, Content = "2. До функцій СУБД не відносять", TestId = 3 },
                new Question { Id = 23, Content = "3. Що являє собою СУБД?", TestId = 3 },
                new Question { Id = 24, Content = "4. Що таке \"реляцiйнi бази даних\"?", TestId = 3 },
                new Question { Id = 25, Content = "5. Поле, значення в якому не повинні повторюватися, називається ...", TestId = 3 },
                new Question { Id = 26, Content = "6. Які типи зв’язків можуть існувати між таблицями баз даних?", TestId = 3 },
                new Question { Id = 27, Content = "7. Без яких об'єктів не може існувати база даних?", TestId = 3 },
                new Question { Id = 28, Content = "8. Якими бувають моделі зберігання даних?", TestId = 3 },
                new Question { Id = 29, Content = "9. У якому режимі можна доопрацювати й відредагувати форму?", TestId = 3 },
                new Question { Id = 30, Content = "10. Коли місце збереження інформації стає базою даних?", TestId = 3 }
                );

            modelBuilder.Entity<Answer>().HasData(
                new Answer { Id = 1, Content = "a) C++", IsCorrect = false, QuestionId = 1 },
                new Answer { Id = 2, Content = "b) С", IsCorrect = false, QuestionId = 1 },
                new Answer { Id = 3, Content = "c) C#", IsCorrect = true, QuestionId = 1 },
                new Answer { Id = 4, Content = "d) Всі відповіді вірні", IsCorrect = false, QuestionId = 1 },

                new Answer { Id = 5, Content = "a) Вірної відповіді немає", IsCorrect = false, QuestionId = 2 },
                new Answer { Id = 6, Content = "b) Hello World", IsCorrect = false, QuestionId = 2 },
                new Answer { Id = 7, Content = "c) Hello, World! підкреслені лінією", IsCorrect = false, QuestionId = 2 },
                new Answer { Id = 8, Content = "d) Hello, World!", IsCorrect = true, QuestionId = 2 },

                new Answer { Id = 9, Content = "a) Проблем ніяких не існує", IsCorrect = false, QuestionId = 3 },
                new Answer { Id = 10, Content = "b) Console.OutputEncoding = System.Text.Encoding.Unicode;", IsCorrect = true, QuestionId = 3 },
                new Answer { Id = 11, Content = "c) Вірної відповіді немає", IsCorrect = false, QuestionId = 3 },
                new Answer { Id = 12, Content = "d) Українську літеру \"і\" вивести неможливо", IsCorrect = false, QuestionId = 3 },

                new Answer { Id = 13, Content = "a) повідомляє, що програмісти мають багато грошей", IsCorrect = false, QuestionId = 4 },
                new Answer { Id = 14, Content = "b) дає можливість компілятору розпізнати name як змінну та підставити в неї значення з програми", IsCorrect = true, QuestionId = 4 },
                new Answer { Id = 15, Content = "c) є стандартним синтаксисом і нічого не означає, а без цього символу працювати не буде ", IsCorrect = false, QuestionId = 4 },
                new Answer { Id = 16, Content = "d) Вірної відповіді немає", IsCorrect = false, QuestionId = 4 },

                new Answer { Id = 17, Content = "a) Так", IsCorrect = true, QuestionId = 5 },
                new Answer { Id = 18, Content = "b) Вірної відповіді немає", IsCorrect = false, QuestionId = 5 },
                new Answer { Id = 19, Content = "c) Ні", IsCorrect = false, QuestionId = 5 },
                new Answer { Id = 20, Content = "d) У мові C# та фреймворк .NET не існує збирання сміття", IsCorrect = false, QuestionId = 5 },

                new Answer { Id = 21, Content = "a) рядок, який закінчується знаком запитання", IsCorrect = false, QuestionId = 6 },
                new Answer { Id = 22, Content = "b) рядок або ціле число", IsCorrect = false, QuestionId = 6 },
                new Answer { Id = 23, Content = "c) рядок або NULL", IsCorrect = true, QuestionId = 6 },
                new Answer { Id = 24, Content = "d) string? name є невірним синтаксисом", IsCorrect = false, QuestionId = 6 },

                new Answer { Id = 25, Content = "a) Це послідовність дій, необхідних для вирішення певної задачі.", IsCorrect = true, QuestionId = 7 },
                new Answer { Id = 26, Content = "b) Це спосіб збереження даних у файл.", IsCorrect = false, QuestionId = 7 },
                new Answer { Id = 27, Content = "c) Це спосіб виконання певного коду декілька разів.", IsCorrect = false, QuestionId = 7 },
                new Answer { Id = 28, Content = "d) Це функція, яка приймає вхідні дані та повертає результат.", IsCorrect = false, QuestionId = 7 },

                new Answer { Id = 29, Content = "a) Це ім'я, яке використовується для запуску програми.", IsCorrect = false, QuestionId = 8 },
                new Answer { Id = 30, Content = "b) Це функція, яка виконує певні дії.", IsCorrect = false, QuestionId = 8 },
                new Answer { Id = 31, Content = "c) Це символ або команда, яка виконує певні дії.", IsCorrect = true, QuestionId = 8 },
                new Answer { Id = 32, Content = "d) Це вираз, який виконується декілька разів у програмі.", IsCorrect = false, QuestionId = 8 },

                new Answer { Id = 33, Content = "a) Це спосіб перевірки умови та виконання певних дій в залежності від цієї умови.", IsCorrect = true, QuestionId = 9 },
                new Answer { Id = 34, Content = "b) Це спосіб повторення певного коду декілька разів.", IsCorrect = false, QuestionId = 9 },
                new Answer { Id = 35, Content = "c) Це функція, яка виконує певні дії.", IsCorrect = false, QuestionId = 9 },
                new Answer { Id = 36, Content = "d) Це спосіб збереження даних у файл.", IsCorrect = false, QuestionId = 9 },

                new Answer { Id = 37, Content = "a) Це функція, яка виконує певні дії.", IsCorrect = false, QuestionId = 10 },
                new Answer { Id = 38, Content = "b) Це спосіб збереження даних у файл.", IsCorrect = false, QuestionId = 10 },
                new Answer { Id = 39, Content = "c) Це спосіб виконання певного коду декілька разів.", IsCorrect = false, QuestionId = 10 },
                new Answer { Id = 40, Content = "d) Це колекція даних, яку можна зберегти та опрацювати як одне ціле.", IsCorrect = true, QuestionId = 10 },

                new Answer { Id = 41, Content = "a) Це місце, де зберігаються дані у програмі.", IsCorrect = true, QuestionId = 11 },
                new Answer { Id = 42, Content = "b) Це функція, яка виконує певні дії.", IsCorrect = false, QuestionId = 11 },
                new Answer { Id = 43, Content = "c) Це ім'я, яке використовується для запуску програми.", IsCorrect = false, QuestionId = 11 },
                new Answer { Id = 44, Content = "d) Це вираз, який виконується декілька разів у програмі.", IsCorrect = false, QuestionId = 11 },

                new Answer { Id = 45, Content = "a) Вірної відповіді немає", IsCorrect = false, QuestionId = 12 },
                new Answer { Id = 46, Content = "b) На другому рядку коду буде помилка, оскільки назва змінної не може починатись з літери у верхньому регістрі", IsCorrect = true, QuestionId = 12 },
                new Answer { Id = 47, Content = "c) Створятся дві змінні типу String", IsCorrect = false, QuestionId = 12 },
                new Answer { Id = 48, Content = "d) Виведе помилку, оскільки два рази оголошується змінна name", IsCorrect = false , QuestionId = 12 },

                new Answer { Id = 49, Content = "a) 7", IsCorrect = false, QuestionId = 13 },
                new Answer { Id = 50, Content = "b) 0", IsCorrect = true, QuestionId = 13 },
                new Answer { Id = 51, Content = "c) 1", IsCorrect = false, QuestionId = 13 },
                new Answer { Id = 52, Content = "d) 10", IsCorrect = false, QuestionId = 13 },

                new Answer { Id = 53, Content = "a) будуть рівні між собою", IsCorrect = false, QuestionId = 14 },
                new Answer { Id = 54, Content = "b) на третьому рядку видасть помилку", IsCorrect = false, QuestionId = 14 },
                new Answer { Id = 55, Content = "c) на другому видасть помилку", IsCorrect = false, QuestionId = 14 },
                new Answer { Id = 56, Content = "d) відрізнятимуться", IsCorrect = true, QuestionId = 14 },

                new Answer { Id = 57, Content = "a) може зберігати значення будь-якого типу даних, оскільки він є базовим для всіх класів .NET.", IsCorrect = true, QuestionId = 15 },
                new Answer { Id = 58, Content = "b) немає такого типу на С#", IsCorrect = false, QuestionId = 15 },
                new Answer { Id = 59, Content = "c) займає 8 байт на 64-розрядній платформі", IsCorrect = false, QuestionId = 15 },
                new Answer { Id = 60, Content = "d) це цілочисельний тип данних", IsCorrect = false, QuestionId = 15 },

                new Answer { Id = 61, Content = "a) double", IsCorrect = true, QuestionId = 16 },
                new Answer { Id = 62, Content = "b) decimal", IsCorrect = false, QuestionId = 16 },
                new Answer { Id = 63, Content = "c) long", IsCorrect = false, QuestionId = 16 },
                new Answer { Id = 64, Content = "d) Вірної відповіді немає", IsCorrect = false, QuestionId = 16 },

                new Answer { Id = 65, Content = "a) 10", IsCorrect = true, QuestionId = 17 },
                new Answer { Id = 66, Content = "b) буде помилка при компіляції", IsCorrect = false, QuestionId = 17 },
                new Answer { Id = 67, Content = "c) 20", IsCorrect = false, QuestionId = 17 },
                new Answer { Id = 68, Content = "d) 0", IsCorrect = false, QuestionId = 17 },

                new Answer { Id = 69, Content = "a) 0", IsCorrect = false, QuestionId = 18 },
                new Answer { Id = 70, Content = "b) буде помилка при компіляції", IsCorrect = false, QuestionId = 18 },
                new Answer { Id = 71, Content = "c) 74", IsCorrect = true, QuestionId = 18 },
                new Answer { Id = 72, Content = "d) 70", IsCorrect = false, QuestionId = 18 },

                new Answer { Id = 73, Content = "a) не використовується в C#", IsCorrect = false, QuestionId = 19 },
                new Answer { Id = 74, Content = "b) отримати значення змінної у квадраті", IsCorrect = false, QuestionId = 19 },
                new Answer { Id = 75, Content = "c) отримати розмір значення в байтах для зазначеного типу", IsCorrect = true, QuestionId = 19 },
                new Answer { Id = 76, Content = "d) тримати максимально можливе значення для зазначеного типу", IsCorrect = false, QuestionId = 19 },

                new Answer { Id = 77, Content = "a) myVariable", IsCorrect = true, QuestionId = 20 },
                new Answer { Id = 78, Content = "b) MyVariable", IsCorrect = false, QuestionId = 20 },
                new Answer { Id = 79, Content = "c) XNA", IsCorrect = false, QuestionId = 20 },
                new Answer { Id = 80, Content = "d) _myvariable", IsCorrect = false, QuestionId = 20 },

                new Answer { Id = 81, Content = "a) сукупність програм для зберігання і обробки великих масивів інформації", IsCorrect = false, QuestionId = 21 },
                new Answer { Id = 82, Content = "b) інтерфейс, що підтримує наповнення і маніпулювання даними;", IsCorrect = false, QuestionId = 21 },
                new Answer { Id = 83, Content = "c) це сховище даних про деяку предметну область, організоване у вигляді спеціальної структури", IsCorrect = true, QuestionId = 21 },
                new Answer { Id = 84, Content = "d) певна сукупність інформації", IsCorrect = false, QuestionId = 21 },

                new Answer { Id = 85, Content = "a) пошук інформації в БД", IsCorrect = false, QuestionId = 22 },
                new Answer { Id = 86, Content = "b) вивчення інформації", IsCorrect = false, QuestionId = 22 },
                new Answer { Id = 87, Content = "c) редагування БД", IsCorrect = false, QuestionId = 22 },
                new Answer { Id = 88, Content = "d) виконання нескладних розрахунків", IsCorrect = true, QuestionId = 22 },

                new Answer { Id = 89, Content = "a) Програми для роботи з електронними таблицями", IsCorrect = false, QuestionId = 23 },
                new Answer { Id = 90, Content = "b) Програми, що забезпечують взаємодію користувача з базою даних", IsCorrect = true, QuestionId = 23 },
                new Answer { Id = 91, Content = "c) Програми для зберігання даних на носіях інформації", IsCorrect = false, QuestionId = 23 },
                new Answer { Id = 92, Content = "d) Програми для забезпечення взаємодії користувачів з таблицями даних", IsCorrect = false, QuestionId = 23 },

                new Answer { Id = 93, Content = "a) бази, данi в яких ієрархічно підпорядковані один одному", IsCorrect = false, QuestionId = 24 },
                new Answer { Id = 94, Content = "b) бази, данi в яких розмiщенi у виглядi взаємопов'язаних таблиць", IsCorrect = true, QuestionId = 24 },
                new Answer { Id = 95, Content = "c) бази, данi в яких розмiщенi у єдинiй прямокутнiй таблицi", IsCorrect = false, QuestionId = 24 },
                new Answer { Id = 96, Content = "d) бази даних з великою кiлькiстю iнформацiї", IsCorrect = false, QuestionId = 24 },

                new Answer { Id = 97, Content = "a) ключовим", IsCorrect = true, QuestionId = 25 },
                new Answer { Id = 98, Content = "b) числовим", IsCorrect = false, QuestionId = 25 },
                new Answer { Id = 99, Content = "c) текстовим", IsCorrect = false, QuestionId = 25 },
                new Answer { Id = 100, Content = "d) визначеним", IsCorrect = false, QuestionId = 25 },

                new Answer { Id = 101, Content = "a) один до кожного, кожен до багатьох, багато до всіх", IsCorrect = false, QuestionId = 26 },
                new Answer { Id = 102, Content = "b) один до жодного, один до двох, багато до багатьох", IsCorrect = false, QuestionId = 26 },
                new Answer { Id = 103, Content = "c) один до одного, один до багатьох, багато до багатьох", IsCorrect = true, QuestionId = 26 },
                new Answer { Id = 104, Content = "d) один за всіх і всі за одного", IsCorrect = false, QuestionId = 26 },

                new Answer { Id = 105, Content = "a) без таблиць", IsCorrect = true, QuestionId = 27 },
                new Answer { Id = 106, Content = "b) без модулів", IsCorrect = false, QuestionId = 27 },
                new Answer { Id = 107, Content = "c) без звітів", IsCorrect = false, QuestionId = 27 },
                new Answer { Id = 108, Content = "d) без запитів", IsCorrect = false, QuestionId = 27 },

                new Answer { Id = 109, Content = "a) ієрархічна, логічна та арифметична", IsCorrect = false, QuestionId = 28 },
                new Answer { Id = 110, Content = "b) ієрархічна, мережева і таблична", IsCorrect = false, QuestionId = 28 },
                new Answer { Id = 111, Content = "c) ієрархічна, мережева і реляційна", IsCorrect = true, QuestionId = 28 },
                new Answer { Id = 112, Content = "d) мережева, таблична та реляційна", IsCorrect = false, QuestionId = 28 },

                new Answer { Id = 113, Content = "a) у режимі Конструктора", IsCorrect = true, QuestionId = 29 },
                new Answer { Id = 114, Content = "b) у режимі Таблиць", IsCorrect = false, QuestionId = 29 },
                new Answer { Id = 115, Content = "c) у режимі Фільтри", IsCorrect = false, QuestionId = 29 },
                new Answer { Id = 116, Content = "d) у режимі Створення", IsCorrect = false, QuestionId = 29 },

                new Answer { Id = 117, Content = "a) якщо забезпечена секретність даних", IsCorrect = false, QuestionId = 30 },
                new Answer { Id = 118, Content = "b) якщо інформація має виключно текстовий характер", IsCorrect = false, QuestionId = 30 },
                new Answer { Id = 119, Content = "c) як тільки якась інформація занесена до пам'яті комп'ютера", IsCorrect = false, QuestionId = 30 },
                new Answer { Id = 120, Content = "d) якщо дані об'єднані зв'язками та спільною структурою", IsCorrect = true, QuestionId = 30 }
                );
        }
    }
}
