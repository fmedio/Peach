using System;

namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public interface IVerb<TServices> : ILinkable
    {
        Func<TServices, Bag<string, string>, IResource> Action { get; }
    }

    public abstract class TypedVerb<TServices, TNoun> : IVerb<TServices>
    {
        public abstract NounParser<TNoun> NounParser { get; }
        public abstract Func<TServices, TNoun, IResource> Behavior { get; }

        

        public abstract string Name { get; }

        public Func<TServices, Bag<string, string>, IResource> Action
        {
            get { return (services, bag) => Behavior(services, NounParser.Parse(bag)); }
        }

        
    }
}