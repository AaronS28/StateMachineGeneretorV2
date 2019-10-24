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
            if (!File.Exists(pathString))
            {
                File.WriteAllText(pathString, fileContent);
                Console.WriteLine("File {0} written.", pathString.Replace("\\", "/"));
            }
            else 
            {
                Console.WriteLine("File {0} already exist.", pathString.Replace("\\", "/"));
            }
        }
    }
}
