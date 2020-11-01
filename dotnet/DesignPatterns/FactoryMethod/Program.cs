using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
  class Program
  {
    public class MainApp
    {
      public static void Main()
      {
        // an array of creators
        Creator[] creators = { new ConcreteCreatorA(), new ConcreteCreatorB() };
        // iterate over creators and create products
        foreach (Creator creator in creators)
        {
          Product product = creator.FactoryMethod();
          Console.WriteLine("Created {0}", product.GetType());
        }
        // Wait for user
        Console.Read();
      }
    }
    // Product
    abstract class Product
    { }
    // "ConcreteProductA"
    class ConcreteProductA : Product
    { }
    // "ConcreteProductB"
    class ConcreteProductB : Product
    { }
    // "Creator"
    abstract class Creator
    {
      public abstract Product FactoryMethod();
    }
    // "ConcreteCreatorA"
    class ConcreteCreatorA : Creator
    {
      public override Product FactoryMethod()
      {
        return new ConcreteProductA();
      }
    }
    // "ConcreteCreatorB"
    class ConcreteCreatorB : Creator
    {
      public override Product FactoryMethod()
      {
        return new ConcreteProductB();
      }
    }
  }
}
