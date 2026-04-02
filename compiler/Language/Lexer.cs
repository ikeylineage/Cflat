using Tokens;

namespace TheLexer {
    public class Lexer {
        private string Text;
        private int Pos;
        private char? CurrentChar;

        public Lexer(string text) {
            Text = text;
            Pos = 0;
            CurrentChar = Text[0];
        }

        public void Advance() { //advances position
            Pos++;
            if (Pos < Text.Length) {
                CurrentChar = Text[Pos];
            }
            else {
                CurrentChar = null;
            }
        }

        public void SkipWhitespace() {
            while (CurrentChar != null && char.IsWhiteSpace(CurrentChar.Value)) {
                Advance();
            }
        }

        public string ReadNumber() { //will read numbers and the parser will decide the type of number (int, float, etc)
            string numberString = "";

            while (CurrentChar != null && char.IsDigit(CurrentChar.Value)) {
                numberString += CurrentChar.Value;
                Advance();
            }

            return numberString;
        }

        public char? Peek() { //peeks at the next position in the variable. Used for multi character symbols
            int peekPos = Pos + 1;
            if (peekPos < Text.Length) {
                return Text[peekPos];
            }
            return null;
        }

        public string ReadIdentifier() { //reads an entire word at once for identifiers
            string result = "";
            while (CurrentChar != null && char.IsLetterOrDigit(CurrentChar.Value ) || CurrentChar == '_') {
                result += CurrentChar.Value;
                Advance();
            }
            return result;
        }

        public string? ReadChar() {
            Advance(); //skip opening '
            string? result = CurrentChar.ToString();
            Advance(); //skip char
            Advance(); //skip closing '
            return result;
        }

        public string ReadString() { //reads a string until the closing "
            string result = "";
            Advance(); //skips opening "
            while (CurrentChar != null && CurrentChar != '"') {
                result += CurrentChar.Value;
                Advance();
            }
            Advance(); //skips closing "
            return result;
        }

        public void SkipComment() {
            while (CurrentChar != null && CurrentChar != '\n') {
                Advance();
            }
        }

        public Token GetNextToken() {
            SkipWhitespace();

            switch (CurrentChar) {
                case null:
                    return new Token(TokenTypes.EOF, null);
                
                case '/' when Peek() == '/':
                    SkipComment();
                    return GetNextToken();
                
                case '"':
                    return new Token(TokenTypes.CBString, ReadString());
                
                case '\'':
                    return new Token(TokenTypes.CBChar, ReadChar());
                
                case var c when char.IsDigit(c.Value):
                    return new Token(TokenTypes.Number, ReadNumber());
                
                case var c when char.IsLetter(c.Value) || c == '_':
                    string word = ReadIdentifier();
                    switch (word) {
                        case "int": return new Token(TokenTypes.CBInt, word);
                        case "longint": return new Token(TokenTypes.LongInt, word);
                        case "bigint": return new Token(TokenTypes.BigInt, word);
                        case "float": return new Token(TokenTypes.CBFloat, word);
                        case "longfloat": return new Token(TokenTypes.LongFloat, word);
                        case "bigfloat": return new Token(TokenTypes.BigFloat, word);
                        case "string": return new Token(TokenTypes.CBString, word);
                        case "char": return new Token(TokenTypes.CBChar, word);
                        case "bool": return new Token(TokenTypes.CBBool, word);
                        case "if": return new Token(TokenTypes.CBIf, word);
                        case "elseif": return new Token(TokenTypes.CBElseIf, word);
                        case "else": return new Token(TokenTypes.CBElse, word);
                        case "while": return new Token(TokenTypes.CBWhile, word);
                        case "for": return new Token(TokenTypes.CBFor, word);
                        case "foreach": return new Token(TokenTypes.CBForeach, word);
                        case "in": return new Token(TokenTypes.CBIn, word);
                        case "func": return new Token(TokenTypes.Function, word);
                        case "and": return new Token(TokenTypes.And, word);
                        case "or": return new Token(TokenTypes.CBOr, word);
                        case "give": return new Token(TokenTypes.Give, word);
                        case "return": return new Token(TokenTypes.Return, word);
                        case "let": return new Token(TokenTypes.Let, word);
                        case "abyss": return new Token(TokenTypes.Abyss, word);
                        case "true": return new Token(TokenTypes.True, word);
                        case "false": return new Token(TokenTypes.False, word);
                        case "null": return new Token(TokenTypes.CBNull, word);
                        case "use": return new Token(TokenTypes.Use, word);
                        case "struct": return new Token(TokenTypes.CBStruct, word);
                        case "enum": return new Token(TokenTypes.CBEnum, word);
                        case "module": return new Token(TokenTypes.Module, word);
                        case "break": return new Token(TokenTypes.CBBreak, word);
                        case "continue": return new Token(TokenTypes.CBContinue, word);
                        case "try": return new Token(TokenTypes.Try, word);
                        case "except": return new Token(TokenTypes.Except, word);
                        case "const": return new Token(TokenTypes.CBConst, word);
                        case "match": return new Token(TokenTypes.Match, word);
                        case "when": return new Token(TokenTypes.When, word);
                        case "whennot": return new Token(TokenTypes.WhenNot, word);
                        default: return new Token(TokenTypes.Identifier, word);
                    }
                
                
                case '=' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.Compare, "==");
                case '=' when Peek() == '>': Advance(); Advance(); return new Token(TokenTypes.FatArrow, "=>");
                case '=': Advance(); return new Token(TokenTypes.Assign, "=");

                case '!' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.NotEquals, "!=");
                case '!': Advance(); return new Token(TokenTypes.Not, "!");

                case '+' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.PlusAssign, "+=");
                case '+': Advance(); return new Token(TokenTypes.Plus, "+");

                case '-' when Peek() == '>': Advance(); Advance(); return new Token(TokenTypes.Arrow, "->");
                case '-' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.MinusAssign, "-=");
                case '-': Advance(); return new Token(TokenTypes.Minus, "-");

                case '*' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.MulAssign, "*=");
                case '*': Advance(); return new Token(TokenTypes.Mul, "*");

                case '/' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.DivAssign, "/=");
                case '/': Advance(); return new Token(TokenTypes.Div, "/");

                case '%' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.ModuloAssign, "%=");
                case '%': Advance(); return new Token(TokenTypes.Modulo, "%");

                case '>' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.GreaterEquals, ">=");
                case '>': Advance(); return new Token(TokenTypes.Greater, ">");
                
                case '<' when Peek() == '=': Advance(); Advance(); return new Token(TokenTypes.LesserEquals, "<=");
                case '<': Advance(); return new Token(TokenTypes.Lesser, "<");

                case '.': Advance(); return new Token(TokenTypes.Dot, ".");
                case ',': Advance(); return new Token(TokenTypes.Comma, ",");

                case ';': Advance(); return new Token(TokenTypes.SemiColon, ";");
                case ':': Advance(); return new Token(TokenTypes.Colon, ":");

                case '(': Advance(); return new Token(TokenTypes.LParen, "(");
                case ')': Advance(); return new Token(TokenTypes.RParen, ")");
                case '[': Advance(); return new Token(TokenTypes.LBracket, "[");
                case ']': Advance(); return new Token(TokenTypes.RBracket, "]");
                case '{': Advance(); return new Token(TokenTypes.LCurlyBrace, "{");
                case '}': Advance(); return new Token(TokenTypes.RCurlyBrace, "}"); 

                default: throw new Exception($"UNKNOWN CHARACTER: {CurrentChar}");
            }
        }
    }
}