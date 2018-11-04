﻿namespace IR
{
    public static class AppConstant
    {
        public const string StopListPath = @"assets\StopList.txt";
        public const string DocumentDirectory = @"assets\documents";
        public const string DocumentExtension = ".txt";
        public const string StpExtension = ".stp";
        public const string SfxExtension = ".sfx";
        public const string StpDirectory = @"assets\stp";
        public const string SfxDirectory = @"assets\sfx";

        private static readonly string[] delimiters = new string[]{ "\r\n",".\r\n", ".\r", "\t", ";" , " & ",
                                            " ^ ",". "," | "," @","@ "," @ "," [ "," ] "," : ",": "," :",
                                            " \" "," ' ","?"," < "," > " ,",","\\","! "," #"," # "," % ","^ "," * ","  ","\t",
                                            " ( "," ) "," - "," _ "," + "," = "," { "," } "," [ "," ] ",":"," ", "“", "”", "—", "’s", "'s"};

        public static string[] GetDelimiters()
        {
            return delimiters;
        }

        private static readonly char[] delimitersForTrim = new char[]{ '\t', ';', '.','|', '@','[',']',':',
                                            '\'','?', '<','>' ,',','\\','!','#','%','^','*',' ',
                                            '(',')','-','_','+','=','{','}',':','“', '”', '—'};

        public static char[] GetDelimitersForTrim()
        {
            return delimitersForTrim;
        }

    }
}
