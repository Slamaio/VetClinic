using VetClinic.Modules;

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