using GameOfLife;

int height;
int width;
string[,] gameField;

Console.Write("Enter the height of the field: ");
height = Convert.ToInt32(Console.ReadLine());

Console.Write("Enter the height of the field: ");
width = Convert.ToInt32(Console.ReadLine());

Field field = new Field(height, width);
gameField = field.CreateField();
field.DrawField(gameField);

gameField = field.SeedField();

Iteration iteration = new Iteration();
iteration.CheckCells(gameField);

field.DrawField(iteration.FieldRefresh(gameField));