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

                if (isSpecialSingleLetter(ref tokenFirst.kind))
                    return tokenFirst;

                Token tokenSecond = Current();

                while (tokenSecond.kind == tokenFirst.kind)
                {
                    wholeToken.Append(tokenSecond.value);

                    MoveNext();

                    tokenSecond = Current();
                }


                var returnToken = new Token { value = wholeToken.ToString(), kind = tokenFirst.kind };

                if (returnToken.kind is Token.KindOfToken.Letters or Token.KindOfToken.Letter)
                    returnToken.kind = specialWords(returnToken.value);

                return returnToken;
            }
            catch (IndexOutOfRangeException)
            {
                throw new InvalidOperationException("Enumerator is out of bounds.");
            }
        }

        private bool isSpecialSingleLetter(ref Token.KindOfToken kind) {
            switch (kind) {
                case Token.KindOfToken.Plus:
                    return true;
                case Token.KindOfToken.Minus:
                    return true;
                case Token.KindOfToken.Star:
                    return true;
                case Token.KindOfToken.Slash:
                    return true;
                default:
                    return false;
            }
        }

        private Token.KindOfToken specialWords(string word) {
            switch (word) {
                case "AND" or "and":
                    return Token.KindOfToken.AND;
                case "OR" or "or":
                    return Token.KindOfToken.OR;
                default:
                    return (word.Length > 1) ? Token.KindOfToken.Letters : Token.KindOfToken.Letter;
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
