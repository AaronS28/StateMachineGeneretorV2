using System;
using System.Collections.Generic;
using System.IO;
using RazorEngine;
using RazorEngine.Templating;
using StateMachineGeneretorV2.Base;
using StateMachineGeneretorV2.Extractor;
using StateMachineGeneretorV2.Supports;

namespace StateMachineGeneretorV2.Generators
{
    public class ViewContainerGenerator : BaseGenerator
    {

        const string fileName = "ViewContainer.cs";

        public static void CreateViewContainer(StateMachine stateMachine, string destinyFolder)
        {
            FileModel fileModel = new FileModel();
            fileModel.SolutionName = ConfigValues.SolutionName;
            fileModel.Properties = CreateViewContainerProperties(stateMachine.Views, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ViewContainerPropertyTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());
            fileModel.Constructors = CreateViewContainerConstructor(stateMachine.Views, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ViewContainerInitView.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());

            if (!Directory.Exists(destinyFolder))
            {
                DirectoryInfo newDirectory = Directory.CreateDirectory(destinyFolder);
                CreateFile(newDirectory + "\\ViewContainer.cs", CreateViewContainerClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ViewContainerTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
            }
            else 
            {
                CreateFile(destinyFolder + "\\ViewContainer.cs", CreateViewContainerClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ViewContainerTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
            }
        }

        private static string CreateViewContainerProperties(List<string> views, string template)
        {
            string result = "";
            foreach (string item in views)
            {
                result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, new { item }) + "\n";
            }

            return result;
        }

        private static string CreateViewContainerConstructor(List<string> views, string template)
        {
            string result = "";
            foreach (string item in views)
            {
                result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, new { item }) + "\n";
            }

            return result;
        }

        private static string CreateViewContainerClass(FileModel fileModel, string templete)
        {
            return Engine.Razor.RunCompile(templete, Guid.NewGuid().ToString(), null, fileModel);
        }
    }
}
