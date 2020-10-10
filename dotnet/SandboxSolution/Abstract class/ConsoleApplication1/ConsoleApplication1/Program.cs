using System;
abstract class Figure
{ // Абстрактный класс
    public Figure(string n)
    {
        name = n;
    }
    private string name;
    public abstract void Draw(); // Абстрактный метод
    public void ShowName()
    {
        Console.WriteLine(name);
    }
}
class Rectangle : Figure
{
    int width;
    int height;
    public Rectangle(int height, int width) : base("Прямоугольник")
    {
        this.width = width;
        this.height = height;
    }
    public override void Draw()
    { // реализация абстрактного метода
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
        }
    }
}

class Sample
{
    static void Main()
    {
        try
        {
            Rectangle rect = new Rectangle(5, 20);
            rect.ShowName();
            rect.Draw();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.Read();
    }
}