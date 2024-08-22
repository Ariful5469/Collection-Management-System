using System;
using System.Collections;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();

        string[] inputArray = input.Split(' ');
        int[] nums = Array.ConvertAll(inputArray, int.Parse);

        ArrayList v=new ArrayList();
        foreach(int x in nums)
        {

            v.Add(x);

        }
        Console.WriteLine("Before : ");

        foreach (int x in v)
        {
            Console.WriteLine(x);
        }
        v.Sort();
        v.Remove(2);
        int xx=v.BinarySearch(2);
        
        Console.WriteLine($"index {xx}");
        Console.WriteLine("After : ");

        foreach (int x in v)
        {
            Console.WriteLine(x);
        }






    }



}