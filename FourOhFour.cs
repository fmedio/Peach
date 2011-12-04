using System;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class FourOhFour<TServices> : IVerb<TServices>
    {
        

        public Func<TServices, Bag<string, string>, IResource> Action
        {
            get { return (services, bag) => new ForOhFourPage(); }
        }

        public string Name
        {
            get { return "404"; }
        }

        
    }
}