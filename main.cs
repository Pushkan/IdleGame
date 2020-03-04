using System;
using System.Threading;

class MainClass {
  
  static string textError;

  //Текущие координаты курсора
  static int origCol;
  static int origRow;
  //Координаты ввода команд
  static int enterCol = 1;
  static int enterRow = 6;

  static double cash = 10050;

  static double salary = 0;
  static double taxes = 0;
  
  //Obligation
  static int obligations = 0;
  static double plusObligation = 1.2;
  static double minusObligation = 0.95;
  static double kObligation = 0.986;
  static double currentKObligation = 1;
  static double costObligation = 20;
  //Papers
  static int papers = 0;
  static double plusPapers = 5.6;
  static double minusPapers = 4.4;
  static double kPapers = 0.991;
  static double currentKPapers = 1;
  static double costPapers = 100;

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
      Timer timer = new Timer(tm, num, 0, 40);      

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
        CheckSalaryAndTaxes();
        cash += salary - taxes;

        origRow = Console.CursorTop;
        origCol = Console.CursorLeft;
        WriteAt($"$ {cash:f2}  + {salary:f2}  - {taxes:f2}  {textError}", 1, 1);
        WriteAt($"1 - Купить облигацию ${costObligation:f2} (+{currentKObligation * plusObligation :f2}, -{(2 - currentKObligation) * minusObligation :f2}); 2 - Купить ценные бумаги ${costPapers:f2} (+{currentKPapers * plusPapers :f2}, -{(2 - currentKPapers) * minusPapers :f2})", 1, 3);
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
      
    string command = Console.ReadLine();
    
    if(command == "1")
    {
      cash -= costObligation;        
      obligations++;
      double tempCurrentKObligation = Math.Pow(kObligation, obligations);
      if(plusObligation * tempCurrentKObligation > minusObligation * (2 - tempCurrentKObligation))
      {
        currentKObligation = tempCurrentKObligation;
      }
      costObligation *= (2 - currentKObligation);
      ClearConsoleLine(enterRow);
      Console.SetCursorPosition(enterCol, enterRow);
    }
    else if(command == "2")
    {
      cash -= costPapers;        
      papers++;
      double tempCurrentKPapers = Math.Pow(kPapers, papers);
      if(plusPapers * tempCurrentKPapers > minusPapers * (2 - tempCurrentKPapers))
      {
        currentKPapers = tempCurrentKPapers;
      }
      costPapers *= (2 - currentKPapers);
      ClearConsoleLine(enterRow);
      Console.SetCursorPosition(enterCol, enterRow);
    }
    else
    {
      ClearConsoleLine(enterRow);
      Console.SetCursorPosition(enterCol, enterRow);

    }
  }

  static void CheckSalaryAndTaxes()
  {
    salary = 0;
    taxes = 0;
    for(int i = 0; i < obligations; i++)
    {
      
      double currentPlusObligation = plusObligation * Math.Pow(kObligation, i);
      double currentMinusObligation = (2 - Math.Pow(kObligation, i)) * minusObligation;
      
      if(currentPlusObligation < currentMinusObligation)
      {
        currentPlusObligation = plusObligation * currentKObligation;
        currentMinusObligation = minusObligation * (2 - currentKObligation);
      }
      salary += currentPlusObligation;
      taxes += currentMinusObligation;
    }

    for(int i = 0; i < papers; i++)
    {
      double currentPlusPapers = plusPapers * Math.Pow(kPapers, i);
      double currentMinusPapers = (2 - Math.Pow(kPapers, i)) * minusPapers;
      
      if(currentPlusPapers < currentMinusPapers)
      {
        currentPlusPapers = plusPapers * currentKPapers;
        currentMinusPapers = minusPapers * (2 - currentKPapers);
      }
      salary += currentPlusPapers;
      taxes += currentMinusPapers;
    }
  }

}