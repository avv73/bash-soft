﻿using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.IO;
using BashSoft.StaticData;
using System;
using System.IO;

namespace BashSoft.Judge
{
    public class Tester : IContentComparer
    {
        private string GetMismatchPath(string expectedOutputPath)
        {
            int indexOf = expectedOutputPath.LastIndexOf('\\');
            string directoryPath = expectedOutputPath.Substring(0, indexOf);
            string finalDirectory = directoryPath + @"\Mismatches.txt";
            return finalDirectory;
        }

        public void CompareContent(string userOutputPath, string expectedOutputPath)
        {
            OutputWriter.WriteMessageOnNewLine("Reading files...");

            string mismatchPath = GetMismatchPath(expectedOutputPath);
            try
            {
                string[] actualOutputLines = File.ReadAllLines(userOutputPath);
                string[] expectedOutputLines = File.ReadAllLines(expectedOutputPath);

                bool hasMismatch;
                string[] mismatches = GetLinesWithPossibleMismatches(actualOutputLines, expectedOutputLines, out hasMismatch);

                PrintOutput(mismatches, hasMismatch, mismatchPath);
                OutputWriter.WriteMessageOnNewLine("Files read!");
            }
            catch (DirectoryNotFoundException dnfex)
            {
                throw new InvalidPathException();
            }
        }

        private void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
        {
            if (hasMismatch)
            {
                foreach (string line in mismatches)
                {
                    OutputWriter.WriteMessageOnNewLine(line);
                }

                File.WriteAllLines(mismatchPath, mismatches);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine("Files are identical. There are no mismatches.");
            }
        }

        private string[] GetLinesWithPossibleMismatches(string[] actualOutputString, string[] expectedOutputString, out bool hasMismatch)
        {
            hasMismatch = false;
            string output = string.Empty;

            OutputWriter.WriteMessageOnNewLine("Comparing files...");

            int minOutputLines = actualOutputString.Length;
            if (actualOutputString.Length != expectedOutputString.Length)
            {
                hasMismatch = true;
                minOutputLines = Math.Min(actualOutputString.Length, expectedOutputString.Length);
                OutputWriter.DisplayException(ExceptionMessages.ComparisonOfFilesWithDifferentSizes);
            }

            string[] mismatches = new string[minOutputLines];
            for (int index = 0; index < minOutputLines; index++)
            {
                string actualLine = actualOutputString[index];
                string expectedLine = expectedOutputString[index];

                if (!actualLine.Equals(expectedLine))
                {
                    hasMismatch = true;
                    output = string.Format("Mismatch at line {0} -- expected: \"{1}\", actual: \"{2}\"", index, expectedLine, actualLine);
                    output += Environment.NewLine;
                }
                else
                {
                    output = actualLine;
                    output += Environment.NewLine;
                }

                mismatches[index] = output;
            }

            return mismatches;
        }
    } 
}

