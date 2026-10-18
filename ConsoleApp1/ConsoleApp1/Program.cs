using ConsoleApp1;

Console.WriteLine("Hello, World!");

//List<User> users = [];
var db = new DatabaseResource();

var users = await db.GetAsync();


PrintUsers(users);
Console.WriteLine("Thread Get Data from Db");


static void PrintUsers(List<User> users)
{
    foreach (var user in users)
    {
        Console.WriteLine($"User Id : {user.Id}");
        Console.WriteLine($"User Name : {user.Name}");
    }
}
