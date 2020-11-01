using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LasyInitialisation
{
  class Program
  {
    static void Main(string[] args)
    {
      System.Console.WriteLine("First call to instance BigObject...");
      //создание объекта происходит только при этом обращении к объекту
      System.Console.WriteLine(BigObject.Instance);
      System.Console.WriteLine("Second cal to instance BigObject...");
      System.Console.WriteLine(BigObject.Instance);
      //окончание программы
      System.Console.ReadLine();
    }
  }

  public class LazyInitialization<T> where T : new()
  {
    protected LazyInitialization()
    {
    }

    private static T _instance;

    public static T Instance
    {
      get
      {
        if (_instance == null)
        {
          lock (typeof(T))
          {
            if (_instance == null)
              _instance = new T();
          }
        }

        return _instance;
      }
    }
  }

  public sealed class BigObject : LazyInitialization<BigObject>
  {
    public BigObject()
    {
      //Большая работа
      System.Threading.Thread.Sleep(3000);
      System.Console.WriteLine("Instance of BigObject is created");
    }
  }

}
