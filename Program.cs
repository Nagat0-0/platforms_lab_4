using System;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        ResearchTeamCollection collection = new ResearchTeamCollection();
        collection.AddDefaults();

        Console.WriteLine("*** Вміст колекції ***");
        Console.WriteLine(collection.ToString());

        Console.WriteLine("*** Демонстрація сортування за реєстраційним номером ***");
        collection.SortByRegistrationNumber();
        Console.WriteLine(collection.ToShortList());

        Console.WriteLine("*** Демонстрація сортування за назвою теми дослідження ***");
        collection.SortByResearchTopic();
        Console.WriteLine(collection.ToShortList());

        Console.WriteLine("*** Демонстрація сортування за кількістю публікацій ***");
        collection.SortByPublicationsCount();
        Console.WriteLine(collection.ToShortList());

        Console.WriteLine("*** Мінімальне значення реєстраційного номеру***");
        Console.WriteLine(collection.MinRegistrationNumber);
        Console.WriteLine();

        Console.WriteLine("*** Команди з тривалістю дослідження 2 роки***");
        foreach (var team in collection.TwoYearsDuration)
        {
            Console.WriteLine(team.ToShortString());
        }
        Console.WriteLine();

        Console.WriteLine("*** Команди з 2 учасниками ***");
        foreach (var team in collection.NGroup(2))
        {
            Console.WriteLine(team.ToShortString());
        }
        Console.WriteLine();

        int count = 0;
        bool isValid = false;
        
        while (!isValid)
        {
            Console.Write("Введіть кількість елементів для TestCollections->");
            string? input = Console.ReadLine();
            
            if (int.TryParse(input, out count) && count > 0)
            {
                isValid = true;
            }
            else
            {
                Console.WriteLine("Помилка введення. Будь ласка, введіть додатнє ціле число.");
            }
        }
        Console.WriteLine();

        TestCollections testCollections = new TestCollections(count);
        testCollections.MeasureSearchTimes();
    }
}