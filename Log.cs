using System;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class Log
    {
        public static void Debug(object source, string message)
        {
            Console.WriteLine("DEBUG [{0}]: {1}", source.GetType().Name, message);
        }


        public static void Debug(object source, string message, Exception exception)
        {
            Console.WriteLine("DEBUG [{0}]: {1} {2} \n{3}", source.GetType().Name, exception.GetType().Name, message,
                              exception.StackTrace);
        }

        public static void Error(object source, string message, Exception exception)
        {
            Console.WriteLine("ERROR [{0}]: {1} {2} \n{3}", source.GetType().Name, exception.GetType().Name, message,
                              exception.StackTrace);
        }

        public static void Info(object source, string message)
        {
            Console.WriteLine("INFO [{0}]: {1}", source.GetType().Name, message);            
        }
    }
}