using System;
using System.Collections.Generic;
using System.IO;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var value in enumerable)
            {
                action(value);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, System.Action<T, int> action)
        {
            int currentOffset = 0;
            foreach (var value in enumerable)
            {
                action(value, currentOffset++);
            }
        } 

        public static void PipeTo(this Stream src, Stream dest)
        {
            int size = 4096;
            byte[] buffer = new byte[size];

            for (var read = src.Read(buffer, 0, size); read != 0; read = src.Read(buffer, 0, size))
            {
                dest.Write(buffer, 0, read);
            }
        }

        public static void CreateMaybe(this DirectoryInfo directoryInfo)
        {
            if (!Directory.Exists(directoryInfo.FullName))
            {
                Directory.CreateDirectory(directoryInfo.FullName);
            }
        }

        public static void DeleteMaybe(this DirectoryInfo directoryInfo)
        {
            try
            {
                Directory.Delete(directoryInfo.FullName, true);
            } catch (Exception e)
            {
                
            }
        }
    }
}