using System;
using System.Collections;
class Program
{
    static void Main()
    {
        string x = Console.ReadLine();
        string[] y = x.Split(' ');
        int[] num = Array.ConvertAll(y,int.Parse);


        ArrayList v=new ArrayList();

        foreach(int i in num)
         {
            v.Add(i);
         }
         
       Dictionary<int,int>mp = new Dictionary<int,int>();
        

        foreach(int i in v)
            Console.WriteLine(i);
        int cnt = 0;
        foreach (int i in v)
        {
            cnt++;
            mp.Add(cnt, i);
      
        }
            

        foreach(var val in mp)
        {
            Console.WriteLine($"{val.Key} + {val.Value} ");
        }







    }

}