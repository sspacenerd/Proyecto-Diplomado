using System;
using System.Threading.Tasks;

public static class TaskUtils
{
    // Usando referencias de:
    // https://stackoverflow.com/questions/69282112/how-to-do-waituntil-in-async
    public static async Task WaitUntil(Func<bool> predicate, int sleep = 50)
    {
        while (!predicate())
        {
            await Task.Delay(sleep);
        }
    }
}
