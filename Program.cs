using KyrylkaPlusPlus;

class Program {

    public struct Lexer {
        private string value;
        public Lexer(string value) {
            this.value = value;
        }
        public IEnumerable<Token> getTokens() {
            TokenEnumerable tokens = new TokenEnumerable(value.ToArray());
            while (tokens.MoveNext()) {
                yield return tokens.Current;
            }
        }
    }

    public static void Main() {
        Lexer lex = new Lexer("35 + 25");
        var data = lex.getTokens();
        foreach (var item in data) {
            Console.WriteLine(item.kind);
        }
    }
}