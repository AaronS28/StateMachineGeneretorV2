using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StateMachineGeneretorV2.Base
{
    public class BaseGenerator
    {
        protected static void CreateFile(string pathString, string fileContent)
        {
            File.WriteAllText(pathString, fileContent);
            Console.WriteLine("File {0} written.", pathString.Replace("\\", "/"));
        }
    }
}
