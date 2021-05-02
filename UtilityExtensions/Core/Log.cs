using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace UtilityExtensions.Core
{
    public static class Log
    {
        /// <summary>
        /// String representation of the current time in 'yyyy-MM-dd HH:mm:ss' format
        /// </summary>
        public static string Time => DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        public enum ExceptionFlags
        {
            /// <summary>
            /// No extra information, shows only the exception message
            /// </summary>
            None = 0,

            /// <summary>
            /// Show the name of the file where the exception happened
            /// </summary>
            FileName = 1,

            /// <summary>
            /// Show the exception's type
            /// </summary>
            ExceptionType = 2,

            /// <summary>
            /// Show the name of the method where the exception happened
            /// </summary>
            MethodName = 4,

            /// <summary>
            /// Show the line number where the exception happened
            /// </summary>
            Line = 8,

            /// <summary>
            /// Show all information
            /// </summary>
            All = FileName | ExceptionType | MethodName | Line
        }

        public static string Exception(Exception e,
                                       ExceptionFlags flags = ExceptionFlags.All,
                                       [CallerFilePath] string path = "")
        {
            if (e == null)
                return "";

            StackTrace st = new StackTrace(e, true);

            //Get the first stack frame
            StackFrame frame = st.GetFrame(st.FrameCount - 1);

            var namesList = new List<string>();

            if (flags.HasFlag(ExceptionFlags.FileName))
            {
                //Get the file name
                string fileName = Path.GetFileName(path);

                if (!string.IsNullOrEmpty(fileName))
                    namesList.Add(fileName);
            }

            if (flags.HasFlag(ExceptionFlags.MethodName))
            {
                //Get the method
                var methodInfo = frame.GetMethod();

                string methodName = methodInfo.DeclaringType.FullName + "." + methodInfo.Name;

                if (!string.IsNullOrEmpty(methodName))
                    namesList.Add(methodName);
            }

            if (flags.HasFlag(ExceptionFlags.ExceptionType))
            {
                //Get the exception type
                string exceptionType = e.GetType().ToString();

                if (!string.IsNullOrEmpty(exceptionType))
                    namesList.Add(exceptionType);
            }

            var concatString = "";
            if (namesList.Count > 0)
            {
                concatString = string.Join(" - ", namesList) + ": ";
            }

            string lineNumber = "";
            if (flags.HasFlag(ExceptionFlags.Line))
            {
                //Get the line number
                int line = frame.GetFileLineNumber();

                lineNumber = $" at line {line}";
            }

            return $"{concatString}'{e.Message}'{lineNumber}";
        }
    }
}