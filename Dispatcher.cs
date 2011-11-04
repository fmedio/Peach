using System.Collections.Generic;
using System.Linq;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class Dispatcher<TServices>
    {
        private readonly IDictionary<string, ActionId<TServices>> _actions;
        private readonly ActionId<TServices> _notFound;

        public Dispatcher(IEnumerable<ActionId<TServices>> actionIds, ActionId<TServices> notFound)
        {
            _notFound = notFound;
            _actions = actionIds.ToDictionary(id => id.Name);
        }

        public IAction<TServices> Dispatch(string name)
        {
            if (_actions.ContainsKey(name))
            {
                return _actions[name].MakeAction();
            }

            return _notFound.MakeAction();
        }
    }
}