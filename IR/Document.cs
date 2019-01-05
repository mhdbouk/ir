﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IR
{
    public class Document
    {
        private readonly string _documentPath;
        private readonly string _query;
        public string FileNameWithoutExtension { get; private set; }
        private string _stpFile => $"{AppConstant.StpDirectory}\\{FileNameWithoutExtension}{AppConstant.StpExtension}";
        private string _sfxFile => $"{AppConstant.SfxDirectory}\\{FileNameWithoutExtension}{AppConstant.SfxExtension}";
        private List<string> _documentWords;
        private List<string> _documentStpWords;
        public List<string> StemmedTerms { get; private set; }
        private readonly DocumentTerms _terms;
        public double CosValue { get; set; }
        /// <summary>
        /// Create new instance of Document. We will use this document to apply the project steps on it
        /// </summary>
        /// <param name="path">Path of the document on disk</param>
        /// <param name="terms">Existing document terms</param>
        public Document(string path, DocumentTerms terms)
        {
            _documentPath = path;
            _terms = terms;
            FileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
            PrepareDocument();
        }
        /// <summary>
        /// Create new instance of Document when the user submit a query
        /// </summary>
        /// <param name="terms">Existing document terms</param>
        /// <param name="query">The user query submited to the system</param>
        public Document(DocumentTerms terms, string query)
        {
            _terms = terms;
            _query = query;
            FileNameWithoutExtension = "UserQuery";
            PrepareQueryDocument();
        }

        private void PrepareQueryDocument()
        {
            ApplyForDocument(_query);
        }

        /// <summary>
        /// Read all the text from the _documentPath and save them after remove all delimiters into list of _documentWords
        /// </summary>
        private void PrepareDocument()
        {
            string document = File.ReadAllText(_documentPath);

            ApplyForDocument(document);
        }

        private void ApplyForDocument(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
            {
                return;
            }

            _documentWords = document
                                // Trim on all delimiters and remove empty entries
                                .Split(AppConstant.GetDelimiters(), StringSplitOptions.RemoveEmptyEntries)
                                // Trim each word with the Delimiters for Trim list
                                .Select(x => x.Trim(AppConstant.GetDelimitersForTrim()))
                                .Where(x => !x.IsInteger())
                                .ToList();
        }

        /// <summary>
        /// Generate The document stp list and save it to disk with extension .stp
        /// </summary>
        /// <param name="stopList">Instance of existing stopList that has the stop words</param>
        /// <returns></returns>
        public async Task GenerateStpFileAsync(StopList stopList)
        {
            IsStopListValid(stopList);

            _documentStpWords = _documentWords.Where(x => !stopList.StopWords.Any(s => s == x)).Select(x => x.ToLower()).ToList();

            await File.WriteAllLinesAsync($"{_stpFile}", _documentStpWords.ToArray());
        }
        /// <summary>
        /// Check if stopList is not null, if null then throw exception
        /// </summary>
        /// <param name="stopList">Instance of stopList to check</param>
        private void IsStopListValid(StopList stopList)
        {
            if (stopList == null)
            {
                throw new ArgumentNullException(nameof(stopList));
            }
        }
        /// <summary>
        /// Generate new document with .sfx extension after removing the suffixes using porter algorithm
        /// </summary>
        /// <returns></returns>
        public async Task GenerateStemmedFileAsync()
        {
            if (_documentStpWords == null)
            {
                return;
            }

            StemmedTerms = _documentStpWords.Select(term => Porter2Stemmer.EnglishPorter2Stemmer.Instance.Stem(term).Value).ToList();

            // Add terms into DocumentTerms
            StemmedTerms.ForEach(x => _terms.Add(x));

            await File.WriteAllLinesAsync($"{_sfxFile}", StemmedTerms.ToArray());
        }

        /// <summary>
        /// Check if term exist in the document. return <see langword="true"/> if <paramref name="term"/> exist in the list of stemmed document terms
        /// </summary>
        /// <param name="term">The term looking for</param>
        /// <returns></returns>
        public bool IsTermExist(string term)
        {
            return StemmedTerms != null && StemmedTerms.Contains(term, StringComparer.OrdinalIgnoreCase);
        }
        /// <summary>
        /// Get the count of term in document.
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        public int Count(string term)
        {
            return StemmedTerms.Count(x => x == term);
        }
    }
}
