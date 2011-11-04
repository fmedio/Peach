using System;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class ActionId<TServices> : ILinkable
    {
        public String Name { get; private set; }
        public Func<IAction<TServices>> MakeAction { get; private set; }

        public ActionId(string name, Func<IAction<TServices>> makeAction)
        {
            Name = name;
            MakeAction = makeAction;
        }
    }
}