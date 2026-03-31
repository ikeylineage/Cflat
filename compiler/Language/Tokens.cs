namespace Tokens {
    public enum TokenTypes { //CB is used to differenciate from C# keywords
        Number,
        Plus,
        Minus,
        Mul,
        Div,
        Modulo,
        LParen,
        RParen,
        LBracket, //[
        RBracket, //]
        LCurlyBrace, //{
        RCurlyBrace, //}
        CBIf,
        CBElseIf,
        CBElse,
        CBWhile,
        CBFor,
        CBForeach, //Foreach i in myList[]
        In,
        Assign, //=
        Not, //!
        Compare, //==
        NotEquals, //!=
        Greater, //>
        Lesser, //<
        PlusAssign, //+=
        MinusAssign, //-=
        MulAssign, //*=
        DivAssign, // /=
        ModuloAssign, //%=
        GreaterEquals, //>=
        LesserEquals, //<=
        Function, 
        And,
        Or,
        Give, //assign in a function
        Return,
        Let, //local variable
        CBString,
        CBChar,
        CBInt, //16 bit int
        LongInt, //32 bit int
        BigInt, //64 bit int
        CBFloat, //16 bit float
        LongFloat, //32 bit float
        BigFloat, //64 bit float
        CBBool,
        Abyss, //no return type
        True,
        False,
        CBNull,
        CBUsing,
        Dot, //member access
        Comma,
        Identifier, //naming variables/functions
        CBStruct, //struct
        CBEnum, //enum
        CBNamespace,
        Break,
        Continue,
        Try, //runs {code}
        Except, //if try {code} fails Except run {code}
        Const,
        Comment,
        Arrow, //for function return type
        SemiColon,
        EOF,
    }

    public class Token {
        public TokenTypes Type;
        public string? Value;

        public Token(TokenTypes type, string? value) {
            Type = type;
            Value = value;
        }

        public void PrintToken() {
            Console.WriteLine($"TOKEN: {Type} VALUE: {Value}");
        }
    }
}