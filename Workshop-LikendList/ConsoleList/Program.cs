using DoublyLinkedList;
using System.Collections;

var list = new DLinkedList<String>();
var option = string.Empty;
var value = string.Empty;

do
{
    option = Menu();
    switch (option)
    {
        case "1":
            Console.Write("Enter a value: ");
            value = Console.ReadLine() ?? string.Empty;
            list.Add(value);
            break;
        case "2":
            list.ShowForward();
            break;
        case "3":
            list.ShowBack();
            break;
        case "4":
            list.DescendingOrder();
            break;
        case "5":
            list.ShowMode();
            break;
        case "6":
            list.ShowChart();
            break;
        case "7":
            Console.Write("Enter the element to search: ");
            string dato = Console.ReadLine()!;
            list.ItExists(dato);
            break;
        case "8":
            Console.Write("Enter the element to delete: ");
            string toDelete = Console.ReadLine()!;
            list.RemoveOccurrence(toDelete);
            break;
        case "9":
            Console.Write("Enter the element to delete all occurrences: ");
            string toDeleteAll = Console.ReadLine()!;
            list.RemoveAllOccurrences(toDeleteAll);
            break;
        case "0":
            Console.WriteLine("Exiting...");
            break;
        case "P":
            Console.WriteLine(list.ToString());
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
} while (option != "0");

string Menu()
{
    Console.WriteLine("1. Add Item");
    Console.WriteLine("2. Show forward");
    Console.WriteLine("3. Show Back");
    Console.WriteLine("4. Descending Order");
    Console.WriteLine("5. Show Modas");
    Console.WriteLine("6. Show graph"); 
    Console.WriteLine("7. ItExists");
    Console.WriteLine("8. Delete first occurrence");
    Console.WriteLine("9. Delete all occurrences");
    Console.WriteLine("P. Show List");
    Console.WriteLine("0. Exit");
    Console.Write("Enter your option: ");
    return Console.ReadLine() ?? string.Empty;
}
