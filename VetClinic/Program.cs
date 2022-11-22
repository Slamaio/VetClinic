using VetClinic.Modules;

var clinic = new Clinic("VetClinic");

// Preload the data
var bob = new Client("Bob");
var josh = new Client("Josh");
var dog = new Animal("Beethoven", "dog", bob);
var cat = new Animal("Bern", "cat", josh);
clinic.AddAnimal(dog);
clinic.AddAnimal(cat);
clinic.AddVisit(dog, DateTime.Parse("05/10/2022"));

var consoleClinic = new ConsoleClinic(clinic);


consoleClinic.MainLoop();