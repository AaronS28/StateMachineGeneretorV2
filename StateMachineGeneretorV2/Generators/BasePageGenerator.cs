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
    public class BasePageGenerator : BaseGenerator
    {
        const string fileName = "BasePage.cs";
        public static void CreateBasePage(StateMachine stateMachine , string destinyFolder)
        {
            FileModel fileModel = new FileModel();
            fileModel.SolutionName = ConfigValues.SolutionName;
            fileModel.Methods = CreateVirtualMethods(stateMachine.BaseMethods, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.VirtualMethodTemplate.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());
            
            if (!Directory.Exists(destinyFolder))
            {
                DirectoryInfo newDirectory = Directory.CreateDirectory(destinyFolder);
                CreateFile(newDirectory + "\\" + fileName, CreateBasePageClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.BasePageTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
            }
            else
            {
                CreateFile(destinyFolder + "\\" + fileName, CreateBasePageClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.BasePageTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
            }
        }

        private static string CreateVirtualMethods(List<string> methodNames, string template)
        {
            string result = "";
            foreach (string item in methodNames)
            {
                result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, new { item }) + "\n";
            }

            return result;
        }

        private static string CreateBasePageClass(FileModel fileModel, string templete)
        {
            return Engine.Razor.RunCompile(templete, Guid.NewGuid().ToString(), null, fileModel);
        }
    }
}
