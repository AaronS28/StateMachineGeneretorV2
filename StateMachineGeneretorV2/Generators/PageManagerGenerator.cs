using RazorEngine;
using RazorEngine.Templating;
using StateMachineGeneretorV2.Base;
using StateMachineGeneretorV2.Extractor;
using StateMachineGeneretorV2.Supports;
using System;
using System.Collections.Generic;
using System.IO;

namespace StateMachineGeneretorV2.Generators
{
    public class PageManagerGenerator : BaseGenerator
    {
        const string fileName = "PageManager.cs";
        public static void CreatePageManager(StateMachine stateMachine , string destiniyFolder)
        {
            FileModel fileModel = new FileModel();
            fileModel.SolutionName = ConfigValues.SolutionName;
            fileModel.Methods = CreateMethods(stateMachine.BaseMethods, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageManagerMethodTemplate.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());
            CreateFile(destiniyFolder + "\\" + fileName, CreatePageManagerClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageManagerTemplate.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
        }

        private static string CreateMethods(List<string> methodNames, string template)
        {
            string result = "";
            foreach (string item in methodNames)
            {
                result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, new { item }) + "\n";
            }

            return result;
        }

        private static string CreatePageManagerClass(FileModel fileModel, string templete)
        {
            return Engine.Razor.RunCompile(templete, Guid.NewGuid().ToString(), null, fileModel);
        }


    }
}
