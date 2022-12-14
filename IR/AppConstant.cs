namespace IR
{
    public static class AppConstant
    {
        public const string StopListPath = @"assets\StopList.txt";
        public const string RelevantDocumentsPath = @"assets\RelevantDocuments.txt";
        public const string DocumentDirectory = @"assets\documents";
        public const string QueriesDirectory = @"assets\queries";
        public const string QueryCosDirectory = @"assets\queries-cos";
        public const string DocumentExtension = ".TXT";
        public const string QueryExtension = ".txt";
        public const string StpExtension = ".stp";
        public const string SfxExtension = ".sfx";
        public const string StpDirectory = @"assets\stp";
        public const string SfxDirectory = @"assets\sfx";
        public const string BooleanInvertedFile = @"assets\boolean-inverted-file.csv";
        public const string TFIDFInvertedFile = @"assets\tfidf-inverted-file.csv";
        public const string QueryCosFile = @"assets\query-{0}-cos.csv";
        public const string AveragePath = @"assets\queries-result\{0}-result.csv";
        
        public const string CsvDelimiter = ",";

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
