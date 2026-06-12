using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program;

namespace KyrylkaPlusPlus
{
    internal class TokenEnumerable : IEnumerator<Token>
    {
        private int currentIndex = -1;
        private char[] _tokenString;
        public Token Current { get {
                try
                {
                    var token = Token.NextToken(_tokenString[currentIndex]);
                    return token;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException("Enumerator is out of bounds.");
                }
            } }

        public TokenEnumerable(char[] tokenString) {
            this._tokenString = tokenString ?? throw new ArgumentNullException(nameof(tokenString));
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public Token Peek() {
            if (currentIndex + 1 < _tokenString.Length) {
                return new Token { value = "", kind = Token.KindOfToken.EOF };
            }
            return Token.NextToken(_tokenString[currentIndex + 1]);
        }

        public bool MoveNext()
        {
            currentIndex++;
            return currentIndex < _tokenString.Length;
        }

        public void Reset()
        {
            currentIndex = -1;
        }
    }
}
