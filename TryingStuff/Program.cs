// See https://aka.ms/new-console-template for more information

using TryingStuff;
using TryingStuff.DependencyInjector;
using TryingStuff.Memory;

Console.WriteLine("Hello, World!");

var services = DependencyServiceBuilder.Build();

services.RegisterSingleton<IRandomGuidGenerator, RandomGuidGenerator>();
services.RegisterTransient<RandomGuidGenerator>();

var container = services.GenerateContainer();

var serviceA1 = container.GetService<RandomGuidGenerator>();
var serviceA2 = container.GetService<RandomGuidGenerator>();
var serviceB1 = container.GetService<IRandomGuidGenerator>();
var serviceB2 = container.GetService<IRandomGuidGenerator>();

Console.WriteLine(serviceA1.RandomGuid.ToString());
Console.WriteLine(serviceA2.RandomGuid.ToString());

Console.WriteLine(serviceB1.RandomGuid.ToString());
Console.WriteLine(serviceB2.RandomGuid.ToString());

Console.ReadKey();

var memory = new MemoryCache();

memory.PushObject("reference", new object());
var obj = memory.GetObject<object>("reference1");

Console.WriteLine(obj);

Console.ReadKey();