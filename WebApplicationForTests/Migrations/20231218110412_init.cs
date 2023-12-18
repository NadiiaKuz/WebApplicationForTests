using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplicationForTests.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<decimal>(type: "decimal(3,2)", precision: 3, scale: 2, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tests",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Вступ у C#" },
                    { 2, "Базові елементи мови C#" },
                    { 3, "Поняття бази даних" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Content", "TestId" },
                values: new object[,]
                {
                    { 1, "1. Керованим кодом можуть називати програму створену на:", 1 },
                    { 2, "2. Рядок коду Console.WriteLine(\"Hello, World!\"); виведе на консоль", 1 },
                    { 3, "3. Методом для вирішення проблеми виведення української літери \"і\" є", 1 },
                    { 4, "4. Знак $ в рядку Console.WriteLine($\"Привіт {name}\")", 1 },
                    { 5, "5. Чи забезпечує мова C# та фреймворк .NET автоматичне збирання сміття?", 1 },
                    { 6, "6. string? name означає що змінна name може зберігати", 1 },
                    { 7, "7. Що таке алгоритм у програмуванні?", 1 },
                    { 8, "8. Що таке оператор у програмуванні?", 1 },
                    { 9, "9. Що таке цикл у програмуванні?", 1 },
                    { 10, "10. Що таке масив у програмуванні?", 1 },
                    { 11, "1. Що таке змінна у програмуванні?", 2 },
                    { 12, "2. При виконанні коду string name; string Name;", 2 },
                    { 13, "3. Рядок коду Console.WriteLine(2 & 5); виведе на консоль", 2 },
                    { 14, "4. Після виконання коду int x = 5; int z1 = x++; int z2 = ++x; змінні z1 і z2", 2 },
                    { 15, "5. Тип даних object", 2 },
                    { 16, "6. Безпечними автоматичними перетвореннями можуть бути перетворення з типу float в", 2 },
                    { 17, "7. int x = 0; x +=10; Після виконання коду змінна x міститиме?", 2 },
                    { 18, "8. byte a = 4; byte b = a + 70; Результатом виконання цього коду b буде?", 2 },
                    { 19, "9. Оператор sizeof() – дозволяє:", 2 },
                    { 20, "10. Оберіть ідентифікатор, який відповідає правилам найменування Camel case:", 2 },
                    { 21, "1. База даних - це:", 3 },
                    { 22, "2. До функцій СУБД не відносять", 3 },
                    { 23, "3. Що являє собою СУБД?", 3 },
                    { 24, "4. Що таке \"реляцiйнi бази даних\"?", 3 },
                    { 25, "5. Поле, значення в якому не повинні повторюватися, називається ...", 3 },
                    { 26, "6. Які типи зв’язків можуть існувати між таблицями баз даних?", 3 },
                    { 27, "7. Без яких об'єктів не може існувати база даних?", 3 },
                    { 28, "8. Якими бувають моделі зберігання даних?", 3 },
                    { 29, "9. У якому режимі можна доопрацювати й відредагувати форму?", 3 },
                    { 30, "10. Коли місце збереження інформації стає базою даних?", 3 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Content", "IsCorrect", "QuestionId" },
                values: new object[,]
                {
                    { 1, "a) C++", false, 1 },
                    { 2, "b) С", false, 1 },
                    { 3, "c) C#", true, 1 },
                    { 4, "d) Всі відповіді вірні", false, 1 },
                    { 5, "a) Вірної відповіді немає", false, 2 },
                    { 6, "b) Hello World", false, 2 },
                    { 7, "c) Hello, World! підкреслені лінією", false, 2 },
                    { 8, "d) Hello, World!", true, 2 },
                    { 9, "a) Проблем ніяких не існує", false, 3 },
                    { 10, "b) Console.OutputEncoding = System.Text.Encoding.Unicode;", true, 3 },
                    { 11, "c) Вірної відповіді немає", false, 3 },
                    { 12, "d) Українську літеру \"і\" вивести неможливо", false, 3 },
                    { 13, "a) повідомляє, що програмісти мають багато грошей", false, 4 },
                    { 14, "b) дає можливість компілятору розпізнати name як змінну та підставити в неї значення з програми", true, 4 },
                    { 15, "c) є стандартним синтаксисом і нічого не означає, а без цього символу працювати не буде ", false, 4 },
                    { 16, "d) Вірної відповіді немає", false, 4 },
                    { 17, "a) Так", true, 5 },
                    { 18, "b) Вірної відповіді немає", false, 5 },
                    { 19, "c) Ні", false, 5 },
                    { 20, "d) У мові C# та фреймворк .NET не існує збирання сміття", false, 5 },
                    { 21, "a) рядок, який закінчується знаком запитання", false, 6 },
                    { 22, "b) рядок або ціле число", false, 6 },
                    { 23, "c) рядок або NULL", true, 6 },
                    { 24, "d) string? name є невірним синтаксисом", false, 6 },
                    { 25, "a) Це послідовність дій, необхідних для вирішення певної задачі.", true, 7 },
                    { 26, "b) Це спосіб збереження даних у файл.", false, 7 },
                    { 27, "c) Це спосіб виконання певного коду декілька разів.", false, 7 },
                    { 28, "d) Це функція, яка приймає вхідні дані та повертає результат.", false, 7 },
                    { 29, "a) Це ім'я, яке використовується для запуску програми.", false, 8 },
                    { 30, "b) Це функція, яка виконує певні дії.", false, 8 },
                    { 31, "c) Це символ або команда, яка виконує певні дії.", true, 8 },
                    { 32, "d) Це вираз, який виконується декілька разів у програмі.", false, 8 },
                    { 33, "a) Це спосіб перевірки умови та виконання певних дій в залежності від цієї умови.", true, 9 },
                    { 34, "b) Це спосіб повторення певного коду декілька разів.", false, 9 },
                    { 35, "c) Це функція, яка виконує певні дії.", false, 9 },
                    { 36, "d) Це спосіб збереження даних у файл.", false, 9 },
                    { 37, "a) Це функція, яка виконує певні дії.", false, 10 },
                    { 38, "b) Це спосіб збереження даних у файл.", false, 10 },
                    { 39, "c) Це спосіб виконання певного коду декілька разів.", false, 10 },
                    { 40, "d) Це колекція даних, яку можна зберегти та опрацювати як одне ціле.", true, 10 },
                    { 41, "a) Це місце, де зберігаються дані у програмі.", true, 11 },
                    { 42, "b) Це функція, яка виконує певні дії.", false, 11 },
                    { 43, "c) Це ім'я, яке використовується для запуску програми.", false, 11 },
                    { 44, "d) Це вираз, який виконується декілька разів у програмі.", false, 11 },
                    { 45, "a) Вірної відповіді немає", false, 12 },
                    { 46, "b) На другому рядку коду буде помилка, оскільки назва змінної не може починатись з літери у верхньому регістрі", true, 12 },
                    { 47, "c) Створятся дві змінні типу String", false, 12 },
                    { 48, "d) Виведе помилку, оскільки два рази оголошується змінна name", false, 12 },
                    { 49, "a) 7", false, 13 },
                    { 50, "b) 0", true, 13 },
                    { 51, "c) 1", false, 13 },
                    { 52, "d) 10", false, 13 },
                    { 53, "a) будуть рівні між собою", false, 14 },
                    { 54, "b) на третьому рядку видасть помилку", false, 14 },
                    { 55, "c) на другому видасть помилку", false, 14 },
                    { 56, "d) відрізнятимуться", true, 14 },
                    { 57, "a) може зберігати значення будь-якого типу даних, оскільки він є базовим для всіх класів .NET.", true, 15 },
                    { 58, "b) немає такого типу на С#", false, 15 },
                    { 59, "c) займає 8 байт на 64-розрядній платформі", false, 15 },
                    { 60, "d) це цілочисельний тип данних", false, 15 },
                    { 61, "a) double", true, 16 },
                    { 62, "b) decimal", false, 16 },
                    { 63, "c) long", false, 16 },
                    { 64, "d) Вірної відповіді немає", false, 16 },
                    { 65, "a) 10", true, 17 },
                    { 66, "b) буде помилка при компіляції", false, 17 },
                    { 67, "c) 20", false, 17 },
                    { 68, "d) 0", false, 17 },
                    { 69, "a) 0", false, 18 },
                    { 70, "b) буде помилка при компіляції", false, 18 },
                    { 71, "c) 74", true, 18 },
                    { 72, "d) 70", false, 18 },
                    { 73, "a) не використовується в C#", false, 19 },
                    { 74, "b) отримати значення змінної у квадраті", false, 19 },
                    { 75, "c) отримати розмір значення в байтах для зазначеного типу", true, 19 },
                    { 76, "d) тримати максимально можливе значення для зазначеного типу", false, 19 },
                    { 77, "a) myVariable", true, 20 },
                    { 78, "b) MyVariable", false, 20 },
                    { 79, "c) XNA", false, 20 },
                    { 80, "d) _myvariable", false, 20 },
                    { 81, "a) сукупність програм для зберігання і обробки великих масивів інформації", false, 21 },
                    { 82, "b) інтерфейс, що підтримує наповнення і маніпулювання даними;", false, 21 },
                    { 83, "c) це сховище даних про деяку предметну область, організоване у вигляді спеціальної структури", true, 21 },
                    { 84, "d) певна сукупність інформації", false, 21 },
                    { 85, "a) пошук інформації в БД", false, 22 },
                    { 86, "b) вивчення інформації", false, 22 },
                    { 87, "c) редагування БД", false, 22 },
                    { 88, "d) виконання нескладних розрахунків", true, 22 },
                    { 89, "a) Програми для роботи з електронними таблицями", false, 23 },
                    { 90, "b) Програми, що забезпечують взаємодію користувача з базою даних", true, 23 },
                    { 91, "c) Програми для зберігання даних на носіях інформації", false, 23 },
                    { 92, "d) Програми для забезпечення взаємодії користувачів з таблицями даних", false, 23 },
                    { 93, "a) бази, данi в яких ієрархічно підпорядковані один одному", false, 24 },
                    { 94, "b) бази, данi в яких розмiщенi у виглядi взаємопов'язаних таблиць", true, 24 },
                    { 95, "c) бази, данi в яких розмiщенi у єдинiй прямокутнiй таблицi", false, 24 },
                    { 96, "d) бази даних з великою кiлькiстю iнформацiї", false, 24 },
                    { 97, "a) ключовим", true, 25 },
                    { 98, "b) числовим", false, 25 },
                    { 99, "c) текстовим", false, 25 },
                    { 100, "d) визначеним", false, 25 },
                    { 101, "a) один до кожного, кожен до багатьох, багато до всіх", false, 26 },
                    { 102, "b) один до жодного, один до двох, багато до багатьох", false, 26 },
                    { 103, "c) один до одного, один до багатьох, багато до багатьох", true, 26 },
                    { 104, "d) один за всіх і всі за одного", false, 26 },
                    { 105, "a) без таблиць", true, 27 },
                    { 106, "b) без модулів", false, 27 },
                    { 107, "c) без звітів", false, 27 },
                    { 108, "d) без запитів", false, 27 },
                    { 109, "a) ієрархічна, логічна та арифметична", false, 28 },
                    { 110, "b) ієрархічна, мережева і таблична", false, 28 },
                    { 111, "c) ієрархічна, мережева і реляційна", true, 28 },
                    { 112, "d) мережева, таблична та реляційна", false, 28 },
                    { 113, "a) у режимі Конструктора", true, 29 },
                    { 114, "b) у режимі Таблиць", false, 29 },
                    { 115, "c) у режимі Фільтри", false, 29 },
                    { 116, "d) у режимі Створення", false, 29 },
                    { 117, "a) якщо забезпечена секретність даних", false, 30 },
                    { 118, "b) якщо інформація має виключно текстовий характер", false, 30 },
                    { 119, "c) як тільки якась інформація занесена до пам'яті комп'ютера", false, 30 },
                    { 120, "d) якщо дані об'єднані зв'язками та спільною структурою", true, 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TestId",
                table: "Questions",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_TestId",
                table: "Results",
                column: "TestId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UserId",
                table: "Results",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tests");
        }
    }
}
