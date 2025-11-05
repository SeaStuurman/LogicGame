// See https://aka.ms/new-console-template for more information
using LogicAutoGamer;

Console.WriteLine("Игра Логика. Отгадай набор цветов.");

var gamer = new AutoGamer();
await gamer.Start();
