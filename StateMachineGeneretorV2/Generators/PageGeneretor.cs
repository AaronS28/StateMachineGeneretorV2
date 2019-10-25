using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using RazorEngine;
using RazorEngine.Templating;
using StateMachineGeneretorV2.Base;
using StateMachineGeneretorV2.Extractor;
using StateMachineGeneretorV2.Supports;

namespace StateMachineGeneretorV2
{
    class PageGeneretor : BaseGenerator
    {
        public static void GeneretePages(StateMachine stateMachine, string destinyFolder)
        {
            foreach(PageModel page in stateMachine.Pages)
            {
                var auxView = page.ClassName.Replace("Page", "View");
                page.CurrentPage = stateMachine.Views.Find(x => x == auxView);
                CreatePage(page, destinyFolder);
            }
        }

        private static void CreatePage(PageModel page, string destinyFolder)
        {
            try
            {
                FileModel fileModel = new FileModel();
                fileModel.ClassName = page.ClassName;
                fileModel.SolutionName = ConfigValues.SolutionName;
                fileModel.Constructors = CreatePageConstructor(page, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageConstructor.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());
                fileModel.Properties = CreateViewsAtributte(page.SubPages, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ViewAtributeTemplate.cshtml") ,System.Text.Encoding.UTF8).ReadToEnd());
                fileModel.Methods = CreateOverrideMethods(page.Methods, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.MethodTemplate.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());

                if (!Directory.Exists(destinyFolder))
                {
                    DirectoryInfo newDirectory = Directory.CreateDirectory(destinyFolder);
                    CreateFile(newDirectory + "\\" + fileModel.ClassName + ".cs", CreatePageClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageTemplate.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
                }
                else 
                {
                    CreateFile(destinyFolder + "\\" + fileModel.ClassName + ".cs", CreatePageClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageTemplate.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string CreatePageClass(FileModel fileModel, string templatePath)
        {
            return Engine.Razor.RunCompile(templatePath, Guid.NewGuid().ToString(), null, fileModel);
        }

        private static string CreateOverrideMethods(List<string> methodNames, string template)
        {
            string result = "";
            if (methodNames.Count != 0)
            {
                foreach (string item in methodNames)
                {
                    result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, new { item }) + "\n";
                    //Determinar si hace change page o change view
                }
            }

            return result;
        }

        private static string CreateViewsAtributte(List<string> viewsName, string template)
        {

            string result = "";
            if (viewsName.Count != 0)
            {
                result = "Views = new List<ViewItem>()\n{";
                for (int index = 0; index < viewsName.Count; index++)
                {
                    result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, new { item = viewsName[index].ToString(), index }) + "\n";
                }

                result = "}\n"
                    + "CurrentView = Views[0].View; \n"
                    + "ViewId = 0;";
            }
            return result;
        }

        private static string CreatePageConstructor(PageModel page, string template)
        {
            return Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, page) + "\n";
        }
    }
}
