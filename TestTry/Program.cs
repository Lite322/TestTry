string cs = "server = 192.168.51.105; user id = Admin; password = Admin; trustservercertificate = true";

Console.WriteLine("Введите префикс для пользователя");
string pref = Console.ReadLine();
Console.WriteLine("Введите префикс для базы данных");
string dbpref = Console.ReadLine();


Console.WriteLine("Введите кол-во пользователей");
int count = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Введите длину пароля");
int leghtpass = Convert.ToInt32(Console.ReadLine());


var list = 