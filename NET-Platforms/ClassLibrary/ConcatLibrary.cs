using System;

namespace ClassLibrary
{
    public class ConcatLibrary
    {
        public static string GetHelloWorld(string userName)
        {
            return $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} Hello, {userName}!";
        }

    }
}
