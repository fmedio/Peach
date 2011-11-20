using System;
using System.Text;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public abstract class NounParser<T>
    {
        public abstract T Parse(Bag<string, string> bag);
        public abstract Bag<string, string> Serialize(T tee);

        protected string RequiredString(Bag<string, string> args, string key)
        {
            if (args.ContainsKey(key))
            {
                return args[key];
            } 

            throw new InvalidRequestException(key);
        }

        protected int RequiredInt(Bag<string, string> args, string key)
        {
            try
            {
                return Int32.Parse(args[key]);
            } catch (Exception e)
            {
                throw new InvalidRequestException(key);
            }
        }

        public string ToUrlTarget(T tee)
        {
            var result = new StringBuilder("?");
            Serialize(tee).ForEach(kvp => result.Append(String.Format("{0}={1}&", kvp.Key, Secure(kvp.Value))));
            return result.ToString();
        }

        private string Secure(string s)
        {
            return System.Security.SecurityElement.Escape(s);
        }
    }
}