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
    public class ContentViewGenerator : BaseGenerator
    {
        public static void GenereteContentsViews(StateMachine stateMachine, string destinyFolder)
        {
            foreach (string view in stateMachine.Views)
            {
                CreateContentView(view, destinyFolder);
            }
        }

        private static void CreateContentView(string viewName, string destinyFolder)
        {
            try
            {
                FileModel fileModel = new FileModel();
                fileModel.ClassName = viewName;
                fileModel.SolutionName = ConfigValues.SolutionName;
                var a = ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ViewAtributeTemplate.cshtml");
               
                if (!Directory.Exists(destinyFolder))
                {
                    DirectoryInfo newDirectory = Directory.CreateDirectory(destinyFolder);
                    CreateFile(newDirectory + "\\" + fileModel.ClassName + ".xaml", CreateFileFromTemplete(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ContentViewTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
                    CreateContentViewCS(viewName, newDirectory.ToString());
                }
                else 
                {
                    CreateFile(destinyFolder + "\\" + fileModel.ClassName + ".xaml", CreateFileFromTemplete(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ContentViewTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
                    CreateContentViewCS(viewName, destinyFolder);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string CreateFileFromTemplete(FileModel fileModel, string templete)
        {
            return Engine.Razor.RunCompile(templete, Guid.NewGuid().ToString(), null, fileModel);
        }

        //Crear cs
        private static void CreateContentViewCS(string viewName, string destinyFolder)
        {
            try
            {
                FileModel fileModel = new FileModel();
                fileModel.ClassName = viewName;
                fileModel.SolutionName = ConfigValues.SolutionName;
                fileModel.Properties = viewName + "Model";
                CreateFile(destinyFolder + "\\" + fileModel.ClassName + ".xaml.cs", CreateFileFromTemplete(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ContentViewCSTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
