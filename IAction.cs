namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public interface IAction<TServices>
    {
        IResource Execute(TServices services, Bag<string, string> arguments);
    }
}