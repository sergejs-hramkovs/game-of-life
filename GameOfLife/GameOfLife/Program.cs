using GameOfLife;

int height;
int width;

Console.Write("Enter the height of the field: ");
height = Convert.ToInt32(Console.ReadLine());

Console.Write("Enter the height of the field: ");
width = Convert.ToInt32(Console.ReadLine());

Field field = new Field(height, width);
field.CreateField();
field.DrawField();
field.SeedField();