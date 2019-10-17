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
    public class ViewModelGenerator : BaseGenerator
    {
        public static void GenereteViewModels(StateMachine stateMachine, string destinyFolder)
        {
            foreach (string view in stateMachine.Views)
            {
                CreateViewModel(view + "Model", destinyFolder);
            }
        }

        private static void CreateViewModel(string viewModelName, string destinyFolder)
        {
            try
            {
                FileModel fileModel = new FileModel();
                fileModel.ClassName = viewModelName;
                fileModel.SolutionName = ConfigValues.SolutionName;
                CreateFile(destinyFolder + "\\" + fileModel.ClassName + ".cs", CreateFileFromTemplete(fileModel, new StreamReader(ConfigValues.Assembly.GetManifestResourceStream("StateMachineGeneretorV2.Templetes.ViewModelGenerator.cshtml"), System.Text.Encoding.UTF8).ReadToEnd()));
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


    }
}
