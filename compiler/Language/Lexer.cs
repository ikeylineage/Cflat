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
            while (CurrentChar != null && char.IsLetterOrDigit(CurrentChar.Value)) {
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

        //TODO: public Token GetNextToken() {}
    }
}