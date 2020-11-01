using System;
using System.Text;

namespace texternamespace
{

    public class texter
    {
      public static string substrings(string mem1, string mem2,string mem3)
     {
          string outtext="";
         try
         {
            int startindex = mem3.IndexOf(mem1)+mem1.Length;
            int finishindex = mem3.IndexOf(mem2, startindex);
            outtext = mem3.Substring(startindex, finishindex - startindex);
         }
         catch (Exception ex) { outtext = ex.ToString();}
          return outtext;
     }
  
    
    
    
    
    
    }
}
