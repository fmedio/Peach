namespace Peach
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    public class NullRequestParser : RequestParser<Null>
    {
        public override Null Parse(Bag<string, string> bag)
        {
            return new Null();
        }

        public override Bag<string, string> Serialize(Null tee)
        {
            return new Bag<string, string>();
        }
    }
}