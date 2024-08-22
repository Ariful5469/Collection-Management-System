using System;

class Program
{
    static void Main()
    {

        String x = Console.ReadLine();
int ans = 0;
for(int i=0;i<x.Length;i++)
{
    int j = x[i] - '0';
    ans += (j * j * j);
}

Console.WriteLine(ans);

    }


}
