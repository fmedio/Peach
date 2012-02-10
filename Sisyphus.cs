using System;
using System.Threading;

namespace Peach
{
    public class Sisyphus<T>
    {
        public T Execute(int howManyTimes, bool giveUpSilently, TimeSpan retryInterval, T defaultValue, Func<T> what)
        {
            Exception lastException = null;
            for (var i = 0; i < howManyTimes; i++)
            {
                try
                {
                    return what();
                } catch (Exception e)
                {
                    lastException = e;
                    Thread.Sleep(retryInterval);
                }
            }
            if (giveUpSilently)
            {
                Log.Debug(this, string.Format("Sisyphus failed after {0} tries", howManyTimes), lastException);
                return defaultValue;
            } else
            {
                throw new Exception(string.Format("Sisyphus failed after {0} tries", howManyTimes), lastException);                
            }
        }
    }
}