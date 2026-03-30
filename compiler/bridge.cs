using System.Runtime.InteropServices;

namespace MyLanguage;

internal static class RuntimeBridge {
    [DllImport("native", CallingConvention = CallingConvention.Cdecl)]
    public static extern void run_bytecode(string input);
}