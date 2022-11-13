﻿// See https://aka.ms/new-console-template for more information

using TryingStuff;

Console.WriteLine("Hello, World!");

var services = new DependencyCollection();
// services.RegisterSingleton<IRandomGuidGenerator, RandomGuidGenerator>();
services.RegisterTransient<IRandomGuidGenerator, RandomGuidGenerator>();

var container = services.GenerateContainer();

var service1 = container.GetService<IRandomGuidGenerator>();
var service2 = container.GetService<IRandomGuidGenerator>();

Console.WriteLine(service1.RandomGuid.ToString());
Console.WriteLine(service2.RandomGuid.ToString());

Console.ReadKey();