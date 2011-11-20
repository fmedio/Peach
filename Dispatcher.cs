using System.Collections.Generic;
using System.Linq;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class Dispatcher<TServices>
    {
        private readonly IDictionary<string, IVerb<TServices>> _verbs;
        private readonly IVerb<TServices> _notFound;

        public Dispatcher(IEnumerable<IVerb<TServices>> verbs, IVerb<TServices> notFound)
        {
            _notFound = notFound;
            _verbs = verbs.ToDictionary(id => id.Name);
        }

        public IVerb<TServices> Dispatch(string name)
        {
            if (_verbs.ContainsKey(name))
            {
                return _verbs[name];
            }

            return _notFound;
        }
    }
}