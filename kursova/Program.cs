using ArrayRelatedFunctions;

public class Program
{
    public static void Main(string[] args)
    {
        string repeatProgramAgain;
        do
        {
            int size = ConsoleUI.GetSize();
            float min = ConsoleUI.GetMin();
            float max = ConsoleUI.GetMax(min);
            float[] array = ArrayRelatedFunctions.ArrayManager.CreateArray(size, min, max, ConsoleUI.GetGenerationType());
            AlgorithmType algorithmType = ConsoleUI.GetAlgorithmType();
            SortDirection sortDirection = ConsoleUI.GetSortDirection();

            Console.WriteLine("Do you want to star programm again (1 - yes; 0 - no): ");
            repeatProgramAgain = Console.ReadLine();
        } while (repeatProgramAgain == "1");

    }

    
}