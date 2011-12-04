using System;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class InvalidRequestException : Exception
    {
        public InvalidRequestException(string key) : base("Invalid key '" + key + "' for request")
        {
        }
    }
}