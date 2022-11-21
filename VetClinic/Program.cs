using VetClinic;
using VetClinic.Modules;

/*
Програма для ведення обліку відвідувачів ветеринарної клініки
Основний функціонал (5):
    1. Додання нової тварини в систему клініки. (0.5)
    2. Пошук тварини за заданим критерієм. (0.5)
    3. Оновлення даних про стан тварини (дата звернення, дата виписки). (0.5)
    4. Огляд попередніх звернень тварини. (0.5)
    5. Створення паспорту для тварини (опис тварини, назва та дата щеплень) *щеплення – не єдина процедура у клініці. (1)
    6. Видалення тварини із системи клініки. (1)
    7. Статистика відвідувань за день/тиждень/місяць/рік. (1)
Додатковий функціонал (10):
    8. Отримати список тварин, яким в найближчий час необхідно буде зробити щеплення (3)
    9. Додати сутність «клієнт» та реалізувати можливість одному клієнту мати декілька тварин (2)
    10.Додати основні функції (додання, пошук, оновлення, видалення) для клієнта, а також перегляд тварин певного клієнта (5)
*/

// var clinic = new ConsoleClinic("VetClinic");
// var bob = new Client("Bob");
// var josh = new Client("Josh");
// var dog = new Animal("Beethoven", "dog", bob);
// var cat = new Animal("Bern", "cat", josh);
// clinic.AddAnimal(dog);
// clinic.AddAnimal(cat);
//
// var path = "/home/slamaio/repos/DotnetProjects/VetClinic/VetClinic/";
// var filename = $"{clinic.Name}_{clinic.GetType()}_{DateTime.Now.Ticks}.clinic.backup";
// var filePath = ClinicBackup.Export(clinic, path+filename);
//
// var consoleClinic = ClinicBackup.Import(filePath) as ConsoleClinic;

var consoleClinic = new ConsoleClinic("VetClinic");

// Preload the data
var bob = new Client("Bob");
var josh = new Client("Josh");
var dog = new Animal("Beethoven", "dog", bob);
var cat = new Animal("Bern", "cat", josh);
consoleClinic.AddAnimal(dog);
consoleClinic.AddAnimal(cat);
consoleClinic.AddVisit(dog, DateTime.Parse("05/10/2022"));

consoleClinic.MainLoop();