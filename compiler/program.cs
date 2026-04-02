using Tokens;
using TheLexer;

public static class Program {
    public static void Main() {
        Console.WriteLine("INPUT TOKEN:");
        string input = File.ReadAllText("test.cflat");

        Lexer lexer = new Lexer(input);
        Token token = lexer.GetNextToken();

        while (token.Type != TokenTypes.EOF) {
            token.PrintToken();
            token = lexer.GetNextToken();
        }
    }
}