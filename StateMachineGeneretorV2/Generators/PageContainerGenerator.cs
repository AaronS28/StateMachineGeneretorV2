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
    public class PageContainerGenerator : BaseGenerator
    {

        const string fileName = "PageContainer.cs";

        public static void CreatePageContainer(StateMachine stateMachine, string destiniyFolder)
        {
            PageContainerModel fileModel = new PageContainerModel();
            fileModel.ClassName = ConfigValues.SolutionName;
            fileModel.Properties = CreatePageContainerProperties(stateMachine.Pages, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageContainerPropertyTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());
            fileModel.Constructors = CreatePageContainerConstructor(stateMachine.Pages, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageContainerInitView.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());
            fileModel.Delegate = CreatePageContainerSetDelegate(stateMachine.Pages, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageContainerDelegateView.cshtml"), System.Text.Encoding.UTF8).ReadToEnd());
            CreateFile(destiniyFolder+ "\\PageContainer.cs", CreatePageContainerClass(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.PageContainerTemplete.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
        }

        private static string CreatePageContainerProperties(List<PageModel> views, string template)
        {
            string result = "";
            foreach (PageModel item in views)
            {
                result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, item) + "\n";
            }

            return result;
        }

        private static string CreatePageContainerConstructor(List<PageModel> views, string template)
        {
            string result = "";
            foreach (PageModel item in views)
            {
                result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, item) + "\n";
            }

            return result;
        }

        private static string CreatePageContainerSetDelegate(List<PageModel> views, string template)
        {
            string result = "";
            foreach (PageModel item in views)
            {
                result = result + Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), null, item) + "\n";
            }

            return result;
        }


        private static string CreatePageContainerClass(FileModel fileModel, string templete)
        {
            return Engine.Razor.RunCompile(templete, Guid.NewGuid().ToString(), null, fileModel);
        }


    }

    public class PageContainerModel : FileModel
    {
        public string Delegate { get; set; }
    }
}
