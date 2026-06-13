using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyrylkaPlusPlus
{
    public struct Lexer
    {
        private string value;
        private int currentIndex = 0;
        public Lexer(string value)
        {
            this.value = value;
        }
        public IEnumerable<Token> GetTokens()
        {
            while (true)
            {
                var token = NextToken();

                if (token.kind == Token.KindOfToken.EOF) {
                    yield break;
                }

                yield return token;
            }
        }

        private Token NextToken()
        {
            try
            {
                StringBuilder wholeToken = new StringBuilder();
                var tokenFirst = Current();

                if (tokenFirst.kind == Token.KindOfToken.EOF) {
                    Reset();
                    return tokenFirst;
                }

                MoveNext();

                wholeToken.Append(tokenFirst.value);

                if (tokenFirst.kind is Token.KindOfToken.Plus or Token.KindOfToken.Minus or Token.KindOfToken.Slash or Token.KindOfToken.Star)
                    return tokenFirst;

                Token tokenSecond = Current();

                while (tokenSecond.kind == tokenFirst.kind)
                {
                    wholeToken.Append(tokenSecond.value);

                    MoveNext();

                    tokenSecond = Current();
                }


                var returnToken = new Token { value = wholeToken.ToString(), kind = tokenFirst.kind };
                if (returnToken.value.Length > 1 && returnToken.kind == Token.KindOfToken.Letter)
                    returnToken.kind = Token.KindOfToken.Letters;

                return returnToken;
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException("Enumerator is out of bounds.");
            }
        }

        private bool MoveNext()
        {
            currentIndex++;
            return currentIndex < value.Length;
        }

        private Token Current() {
            if (currentIndex < value.Length)
            {
                return Token.NextToken(value[currentIndex]);
            }
            return new Token { value = "", kind = Token.KindOfToken.EOF };
        }

        private Token Peek()
        {
            if (currentIndex + 1 < value.Length)
            {
                return Token.NextToken(value[currentIndex + 1]);
            }
            return new Token { value = "", kind = Token.KindOfToken.EOF };
        }

        private void Reset() {
            currentIndex = 0;
        }
    }
}
