// See https://aka.ms/new-console-template for more information

using TryingStuff;
using TryingStuff.DependencyInjector;
using TryingStuff.Memory;
using TryingStuff.StateMachine;

Console.WriteLine("Hello, World!");

var machine = new StateMachineBuilder()
    .WithInitialState("A")
    .WithTransition("A", "B", "1")
    .WithTransition("A", "C", "0")
    .WithTransition("B", "C", "0")
    .WithTransition("B", "A", "1")
    .WithTransition("C", "A", "1")
    .WithTransition("C", "B", "0")
    .Build();

Console.WriteLine(machine.CurrentState.Name);
machine.Process("0");
Console.WriteLine(machine.CurrentState.Name);
machine.Process("0");
Console.WriteLine(machine.CurrentState.Name);
machine.Process("1");
Console.WriteLine(machine.CurrentState.Name);


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