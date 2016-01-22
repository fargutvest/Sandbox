using System;

class Animal
{
    public Animal(string n)
    {
        name = n;
    }

    private string name;

    public virtual void Sound()
    {
        Console.WriteLine("Неизвестный Звук !!!");
    }

    public void ShowName()
    {
        Console.WriteLine(name);
    }

}

class Cat : Animal
{
    public Cat(string name) : base(name) { }
    public override void Sound()
    {
        Console.WriteLine("Мяу !!!");
    }
}

class Dog : Animal
{
    public Dog(string name) : base(name) { }
    public override void Sound()
    {
        Console.WriteLine("Гав !!!");
    }
}

class Sample
{
    static void Main()
    {
        try
        {
            Animal[] arr = new Animal[3];
            arr[0] = new Animal("Без имени");
            arr[1] = new Cat("Василий");
            arr[2] = new Dog("Мелодия");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].Sound();
                arr[i].ShowName();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        Console.Read();
    }
}




