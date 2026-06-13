using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyrylkaPlusPlus
{
    public struct Token
    {
        public enum KindOfToken
        {
            Plus,
            Minus,
            Slash,
            Star,
            Integer,
            ParenLeft,
            ParenRight,
            WhiteSpace,
            Letter,
            Letters,
            AND,
            OR,
            EOF
        }

        public string value;
        public KindOfToken kind;

        public static Token NextToken(char symbwol) {
            if (symbwol is ' ') return new Token { value = symbwol.ToString(), kind = KindOfToken.WhiteSpace };
            if (symbwol is '+') return new Token { value = symbwol.ToString(), kind = KindOfToken.Plus };
            if (symbwol is '-') return new Token { value = symbwol.ToString(), kind = KindOfToken.Minus };
            if (symbwol is '/') return new Token { value = symbwol.ToString(), kind = KindOfToken.Slash };
            if (symbwol is '*') return new Token { value = symbwol.ToString(), kind = KindOfToken.Star };
            if (symbwol is '(') return new Token { value = symbwol.ToString(), kind = KindOfToken.ParenLeft };
            if (symbwol is ')') return new Token { value = symbwol.ToString(), kind = KindOfToken.ParenRight };
            if (char.IsNumber(symbwol)) return new Token { value = symbwol.ToString(), kind = KindOfToken.Integer };


            return new Token { value = symbwol.ToString(), kind = KindOfToken.Letter};
        }
    }
}
