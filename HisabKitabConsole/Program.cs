//See https://aka.ms/new-console-template for more information
using HisabKitabDAL.Models;
using HisabKitabDAL;
HKRepository repository = new HKRepository();
//bool result = repository.AddUser("name1", "password9");
int id = repository.GetUserId("alter01");
Console.WriteLine(id);