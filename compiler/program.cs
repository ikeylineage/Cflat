using MyLanguage;

Console.WriteLine("C# Compiler: Parsing source...");
string mockBytecode = "PUSH 10; PUSH 20; ADD; PRINT;";

// Call the Rust library
RuntimeBridge.run_bytecode(mockBytecode);