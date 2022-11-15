// See https://aka.ms/new-console-template for more information

using TryingStuff;
using TryingStuff.DependencyInjector;
using TryingStuff.Memory;

Console.WriteLine("Hello, World!");

var services = DependencyServiceBuilder.Build();

// services.RegisterSingleton<IRandomGuidGenerator, RandomGuidGenerator>();
services.RegisterTransient<RandomGuidGenerator>();

var container = services.GenerateContainer();

var service1 = container.GetService<RandomGuidGenerator>();
var service2 = container.GetService<RandomGuidGenerator>();

Console.WriteLine(service1.RandomGuid.ToString());
Console.WriteLine(service2.RandomGuid.ToString());

Console.ReadKey();

var memory = new MemoryCache();

memory.PushObject("reference", new object());
var obj = memory.GetObject<object>("reference1");

Console.WriteLine(obj);

Console.ReadKey();