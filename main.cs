using System;
using System.Threading;

class MainClass {
  
  //Текущие координаты курсора
  static int origCol;
  static int origRow;
  //Координаты ввода команд
  static int enterCol = 1;
  static int enterRow = 6;

  static double cash = 50;
  //static bool working = false;

  static double salary = 0;
  static double taxes = 0;
  static double generalTaxe = 1;
  
  //Obligation
  static int obligations = 0;
  static double plusObligation = 1.2;
  static double minusObligation = 0.95;
  static double kObligation = 0.97;
  static double currentKObligation = 1;
  static double costObligation = 20;
  //Papers
  static int papers = 0;
  static double plusPapers = 5.6;
  static double minusPapers = 4.4;
  static double kPapers = 0.96;
  static double currentKPaper = 1;
  static double costPaper = 100;

  static void Main(string[] args)
  {
      Console.Clear();
      
      origCol = enterCol;
      origRow = enterRow;
      
      
      Console.SetCursorPosition(origCol, origRow);
      int num = 0; 
      // устанавливаем метод обратного вызова
      TimerCallback tm = new TimerCallback(Count);
      // создаем таймер
      Timer timer = new Timer(tm, num, 0, 400);      

      while(cash > 0)
      {
        //Стартуем игру
        Game();      
      }

      Console.ReadLine();
  }

  public static void Count(object obj)
  {
      
      if(cash > 0)
      {
        CheckSalary();
        CheckTaxes();
        cash += salary - taxes;

        origRow = Console.CursorTop;
        origCol = Console.CursorLeft;
        WriteAt($"$ {cash:f2}  + {salary:f2}  - {taxes:f2}", 1, 1);
        WriteAt($"1 - Купить облигацию ${costObligation:f2} (+{currentKObligation * plusObligation :f2}, -{(2 - currentKObligation) * minusObligation :f2}); 2 - Купить ценные бумаги ${costPaper:f2} (+{currentKPaper * plusPapers :f2}, -{(2 - currentKPaper) * minusPapers :f2})", 1, 3);
        WriteAt("Введите команду", 1, 4);
      }
      else
      {
        WriteAt("Сожалеем", 1, 3);
        WriteAt("Вы проиграли...", 1, 4);
      }
  }

  public static void ClearConsoleLine(int line)
  {
      Console.SetCursorPosition(0, line);
      Console.Write(new string(' ', Console.WindowWidth)); 
      Console.SetCursorPosition(origCol, origRow);
  }

  protected static void WriteAt(string s, int x, int y)
  {
  try
      {          
        ClearConsoleLine(y);
        Console.SetCursorPosition(x, y);
        Console.Write(s);
        Console.SetCursorPosition(origCol, origRow);
      }
  catch (ArgumentOutOfRangeException e)
      {
        Console.Clear();
        Console.WriteLine(e.Message);
      }
  }

  static void Game()
  {
    // if(!working)
    // {
      
      string asd = Console.ReadLine();
      
      if(asd == "1")
      {
        cash -= costObligation;        
        obligations++;
        currentKObligation = Math.Pow(kObligation, obligations);
        costObligation *= (2 - currentKObligation);
        ClearConsoleLine(enterRow);
        Console.SetCursorPosition(enterCol, enterRow);
      }
      else if(asd == "2")
      {
        cash -= costPaper;        
        papers++;
        currentKPaper = Math.Pow(kPapers, papers);
        costPaper *= (2 - currentKPaper);
        ClearConsoleLine(enterRow);
        Console.SetCursorPosition(enterCol, enterRow);
      }
      else
      {
        ClearConsoleLine(enterRow);
        Console.SetCursorPosition(enterCol, enterRow);

      }
    //}
  }

  static void CheckSalary()
  {
    salary = 0;
    for(int i = 0; i < obligations; i++)
    {
      salary += plusObligation * Math.Pow(kObligation, i);
    }
  }

  static void CheckTaxes()
  {
    taxes = 0;
    for(int i = 0; i < obligations; i++)
    {
      taxes += Math.Pow(kObligation, i) * minusObligation;
    }
  }
}