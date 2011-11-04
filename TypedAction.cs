namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public abstract class TypedAction<TServices, TArg> : IAction<TServices>
    {
        public abstract RequestParser<TArg> RequestParser { get; }
        public abstract IResource Execute(TServices services, TArg query);

        public IResource Execute(TServices services, Bag<string, string> arguments)
        {
            var arg = RequestParser.Parse(arguments);
            return Execute(services, arg);
        }
    }
}