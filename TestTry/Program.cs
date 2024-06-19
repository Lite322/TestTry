using TestTry;

while(true)
{
    try
    {
        Console.WriteLine("Выберите задачу:");
        Console.WriteLine("1) Создать пользователей и их базы данных \n2) Создать базу данных с таблицей User с пользователями \n3) Сделать бэкап \n4) Получить логины (не работает) \n5) Сделать всё");
        
        List<User> users = new List<User>();

        switch (Console.ReadLine())
        {
            case "1": AddUsers(users); Console.WriteLine(); break;
            case "2": Metod(users); Console.WriteLine(); break;
            case "3": BackUp(); Console.WriteLine(); break;
            case "4": Logins(); Console.WriteLine(); break;
            case "5": DoAll(); Console.WriteLine(); break;
            default: Console.WriteLine("Неизвестная команда"); Console.WriteLine(); break;
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.ToString());
        continue;
    }
}
void AddUsers(List<User> users)
{
    string cs = GetConnectingString();

    Console.WriteLine("Введите префикс для пользователя");
    string pref = Console.ReadLine();
    Console.WriteLine("Введите префикс для базы данных");
    string dbpref = Console.ReadLine();


    Console.WriteLine("Введите кол-во пользователей");
    int count = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите длину пароля");
    int leghtpass = Convert.ToInt32(Console.ReadLine());


    users = UsersGenerator.Add(pref, dbpref, count, leghtpass, cs);
    Console.WriteLine("Результат:");

    foreach (var item in users)
        Console.WriteLine($"Пользователь: {item.Name}, База данных: {item.NameDB}, Пароль: {item.Password}");
    Console.WriteLine("_____________END_____________");
}

void Metod(List<User> users)
{
    if (users.Count > 0)
    {
        string cs = GetConnectingString();

        Console.WriteLine("Укажите название создаваемой БД");
        string db_name = Console.ReadLine();

        try
        {
            ServiceSQL.CreateDataBase(db_name, cs);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Такая база уже есть. Таблица будет создана в ней.");
        }

        try
        {
            ServiceSQL.CreateTableUser(db_name, cs);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Таблица User уже есть в это базе. Данные будут добавлены в неё.");
        }


        for (int i = 0; i < users.Count; i++)
        {
            ServiceSQL.AddUsersForTableUser(db_name, cs, users[i]);
        }
        Console.WriteLine($"Создано 10 пользователей в базе данных {db_name}");

        foreach (var s in ServiceSQL.GetUsers(db_name, cs))
            Console.WriteLine(s);
    }
    Console.WriteLine("Вы не создали новых пользователей. Воспользуйтесь первой командой.");
}

void BackUp()
{
    string cs = GetConnectingString();

    Console.WriteLine("Введите название БД, бэкап которой необходимо сделать");

    string db_name = Console.ReadLine();

    Console.WriteLine("Введите директорию, где будет создан файл бэкапа");
    string dir = Console.ReadLine();
    Console.WriteLine(ServiceSQL.BackUpDateBase(db_name, dir, cs));
}

void Logins()
{
    string cs = GetConnectingString();

    Console.WriteLine(ServiceSQL.GetLogins(cs));
}

void DoAll()
{
    string cs = GetConnectingString();

    Console.WriteLine("Введите префикс для пользователя");
    string pref = Console.ReadLine();
    Console.WriteLine("Введите префикс для базы данных");
    string dbpref = Console.ReadLine();


    Console.WriteLine("Введите кол-во пользователей");
    int count = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine("Введите длину пароля");
    int leghtpass = Convert.ToInt32(Console.ReadLine());


    var list = UsersGenerator.Add(pref, dbpref, count, leghtpass, cs);
    Console.WriteLine("Результат:");

    foreach (var item in list)
        Console.WriteLine($"Пользователь: {item.Name}, База данных: {item.NameDB}, Пароль: {item.Password}");
    Console.WriteLine("_____________END_____________");
    Console.WriteLine();

    Console.WriteLine("Укажите название создаваемой БД");
    string db_name = Console.ReadLine();

    try
    {
        ServiceSQL.CreateDataBase(db_name, cs);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Такая база уже есть. Таблица будет создана в ней.");
    }

    try
    {
        ServiceSQL.CreateTableUser(db_name, cs);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Таблица User уже есть в это базе. Данные будут добавлены в неё.");
    }


    for (int i = 0; i < list.Count; i++)
    {
        ServiceSQL.AddUsersForTableUser(db_name, cs, list[i]);
    }
    Console.WriteLine($"Создано {list.Count} пользователей в базе данных {db_name}");

    foreach (var s in ServiceSQL.GetUsers(db_name, cs))
        Console.WriteLine(s);
    
    Console.WriteLine();

    Console.WriteLine("Введите директорию, где будет создан файл бэкапа");
    string dir = Console.ReadLine();
    Console.WriteLine(ServiceSQL.BackUpDateBase(db_name, dir, cs));
}

string GetConnectingString()
{
    //Console.WriteLine("Введите адрес сервера");
    //string server = Console.ReadLine();

    //Console.WriteLine("Введите логин для sa");
    //string login = Console.ReadLine();

    //Console.WriteLine("Введите пароль для sa");
    //string password = Console.ReadLine();

    //return $"server = {server}; user id = {login}; password = {password}; trustservercertificate = true";
    return $"server = localhost; user id = sa; password = Db_05; trustservercertificate = true"; 
}