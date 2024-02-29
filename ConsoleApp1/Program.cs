bool isRunning = true;
Person[] database = new Person[0];
bool isLoggedIn = false;
Person user = null;


while (isRunning)
{
    ShowMenu();
    bool isValid = true;
    int result = 0;
    while (isValid)
    {
        isValid = int.TryParse(Console.ReadLine(), out result);
        if (!isValid)
        {
            ShowMenu();
        }
        else
        {
            break;
        }
    }


    if (result == 1)
    {
        string username;
        string password;
        ShowGetUserPassword(out username, out password);
        if (ContainsPerson(username, database) == -1)
        {
            AddPerson(username, password);
        }
        else
        {
            Console.WriteLine("User with that username already exists");
        }
    }
    else if (result == 2)
    {
        ShowPersons();
    }
    else if (result == 3)
    {
        if (!isLoggedIn)
        {
            string username;
            string password;
            ShowGetUserPassword(out username, out password);
            int userIndex = ContainsPerson(username, database);
            Console.WriteLine(user);
            if (userIndex == -1)
            {
                Console.WriteLine("Username or password is incorrect");
            }
            else
            {
            isLoggedIn = true;
            user = database[userIndex];
            Console.WriteLine($"Logged in as {user.Username}");
            }
        }
        else
        {
            isLoggedIn = false;
            user = null;
            Console.WriteLine("Logged out");
        }
    }
    else if (result == 4)
    {
        if (!isLoggedIn) isRunning = false;
        else
        {
            ShowMarket();
            Console.WriteLine("What do you want to buy: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Purchase(choice);

        }
    }
    else if (result == 5 && isLoggedIn)
    {
        IncreaseBalance();
    }
    else if (result == 6 && isLoggedIn)
    {
        isRunning = false;
    }
    else
    {
        Console.WriteLine("Wrong input");
    }
}

void AddPerson(string username, string password)
{
    Person person = new Person(username, password);
    Array.Resize(ref database, database.Length+1);
    database[database.Length - 1] = person;
}

int ContainsPerson(string username, Person[] db)
{
    for (int i = 0; i < db.Length; i++)
    {
        if (db[i].Username == username)
        {
            return i;
        }
    }
    return -1;
}

void ShowMenu()
{
    Console.WriteLine("1. Add Person");
    Console.WriteLine("2. Show Persons");
    Console.WriteLine(isLoggedIn ? "3. Logout" : "3. Login");
    if (isLoggedIn)
    {
        Console.WriteLine("4. Market");
        Console.WriteLine("5. Increase Balance");
        Console.WriteLine("6. Exit");
    }
    else
    {
        Console.WriteLine("4. Exit");
    }
}


void ShowGetUserPassword(out string username, out string password)
{
    Console.WriteLine("Enter your username: ");
    username = Console.ReadLine();
    Console.WriteLine("Enter your password: ");
    password = Console.ReadLine();
}

void ShowPersons()
{
    foreach(Person person in database)
    {
        Console.WriteLine($"{person.Username} => {person.Balance}");
    }
}


void ShowMarket()
{
    Console.WriteLine("What do you want to buy: ");
    Console.WriteLine("1. PlayStation 5 => 499.99$");
    Console.WriteLine("2. Dragon Lore AWP => 799.99$");
    Console.WriteLine("3. Elden Ring => 99.99$");
    Console.WriteLine("4. FIFA => 49.99$");
}


void IncreaseBalance()
{
    Console.WriteLine($"Your current balance: {user.Balance}\nHow much do you want to top up your balance: ");
    double balance = Convert.ToInt32(Console.ReadLine());
    user.Balance += balance;
    Console.WriteLine($"Your new balance: {user.Balance}");
}

void Purchase(int choice)
{
    double productValue = choice switch
    {
        1 => 499.99,
        2 => 799.99,
        3 => 99.99,
        4 => 49.99,
        _ => 0
    };
    if (productValue == 0)
    {
        Console.WriteLine("Wrong input");
    }
    Console.WriteLine("Processing...");
    Thread.Sleep(5000);
    productValue += productValue * 0.05;
    if (productValue > user.Balance)
    {
        Console.WriteLine("You do not have enough balance");
    }
    else
    {
        user.Balance -= productValue;
        Console.WriteLine("Purchase Successful");
        Console.WriteLine($"Your current balance => {user.Balance}");
    }
}