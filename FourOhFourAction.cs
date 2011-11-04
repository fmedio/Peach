namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class FourOhFourAction<TServices> : IAction<TServices>
    {
        public static readonly ActionId<TServices> Id = new ActionId<TServices>("404", () => new FourOhFourAction<TServices>()); 

        public IResource Execute(TServices services, Bag<string, string> arguments)
        {
            return new ForOhFourPage();
        }
    }
}