using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Proteus.Utility.UnitTest
{
    public class TableNameEncoder
    {
        public string Encode(string inputString)
        {
            if (!inputString.Contains("."))
                return string.Format("[{0}]", inputString);

            var delimiter = ".".ToCharArray();

            var result = new StringBuilder();

            foreach (string element in inputString.Split(delimiter))
            {
                result.Append(string.Format("[{0}].", element));
            }

            return result.ToString().TrimEnd(delimiter);
        }

    }
}
