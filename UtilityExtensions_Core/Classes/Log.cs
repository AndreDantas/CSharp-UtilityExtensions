using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace UtilityExtensions_Core.Classes
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
            /// Show the line and column number where the exception happened
            /// </summary>
            LineAndColumn = 8,

            /// <summary>
            /// Show all information
            /// </summary>
            All = FileName | ExceptionType | MethodName | LineAndColumn
        }

        public static string Exception(Exception e, ExceptionFlags flags = ExceptionFlags.All)
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
                string fileName = Path.GetFileName(frame.GetFileName());

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

            string lineAndCol = "";
            if (flags.HasFlag(ExceptionFlags.LineAndColumn))
            {
                //Get the line number
                int line = frame.GetFileLineNumber();

                //Get the column number
                int col = frame.GetFileColumnNumber();

                lineAndCol = $" at line {line} col {col}";
            }

            return $"{concatString}'{e.Message}'{lineAndCol}";
        }
    }
}