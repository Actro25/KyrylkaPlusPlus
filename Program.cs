using KyrylkaPlusPlus;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program {
    public static void showTokens(IEnumerable<Token> tokens)
    {
        int maxPad = 0;
        foreach (var token in tokens)
        {
            maxPad = (token.kind.ToString().Length > maxPad) ? token.kind.ToString().Length : maxPad;
        }

        foreach (var token in tokens)
        {
            Console.WriteLine(token.kind + new string(' ', maxPad - token.kind.ToString().Length) + "\t -> \t" + $"'{token.value}'");
        }
    }
    public static void Main() {
        Lexer lex = new Lexer("30 + 21 - () / and + AND or OR o D");
        var data = lex.GetTokens();
        showTokens(data);
    }


}