using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public static class Extensions
    {
        public static IEnumerable<List<T>> Batch<T>(this IEnumerable<T> enumerable, int batchSize)
        {
            int doneSoFar = 0;
            var batch = new List<T>();

            foreach (var e in enumerable)
            {
                batch.Add(e);
                doneSoFar++;
                if (doneSoFar%batchSize == 0)
                {
                    yield return batch;
                    batch = new List<T>();
                }
            }

            if (batch.Count != 0)
            {
                yield return batch;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T value in enumerable)
            {
                action(value);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            int currentOffset = 0;
            foreach (T value in enumerable)
            {
                action(value, currentOffset++);
            }
        }

        public static void PipeTo(this Stream src, Stream dest)
        {
            int size = 4096;
            var buffer = new byte[size];

            for (int read = src.Read(buffer, 0, size); read != 0; read = src.Read(buffer, 0, size))
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
            }
            catch (Exception e)
            {
            }
        }
    }
}