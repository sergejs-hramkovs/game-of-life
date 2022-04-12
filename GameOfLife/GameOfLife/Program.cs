using GameOfLife;

int height;
int width;
int generation = 1;
string[,] gameField;

Console.Write("Enter the height of the field: ");
height = Convert.ToInt32(Console.ReadLine());

Console.Write("Enter the height of the field: ");
width = Convert.ToInt32(Console.ReadLine());

Field field = new Field(height, width);
Iteration iteration = new Iteration();

gameField = field.CreateField();
field.DrawField(gameField);

gameField = field.SeedField();

Console.Clear();

while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape))
{
    Console.WriteLine("Press ESC to stop");
    Console.WriteLine();
    Console.WriteLine($"Generation: {generation}");
    field.DrawField(gameField);
    iteration.CheckCells(gameField);
    gameField = iteration.FieldRefresh(gameField);
    Thread.Sleep(1000);
    generation++;
    Console.Clear();
}

