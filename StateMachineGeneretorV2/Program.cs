using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using RazorEngine;
using RazorEngine.Templating;
using StateMachineGeneretorV2.Extractor;
using StateMachineGeneretorV2.Generators;
using StateMachineGeneretorV2.Supports;

namespace StateMachineGeneretorV2
{
    class Program
    {
        private const string controllerTemplateFileName = "ControllerTemplate.cshtml";

        static void Main(string[] args)
        {
            ConfigValues.Assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Ingrese el nombre de la solucion: ");
            ConfigValues.SolutionName = Console.ReadLine();
            Console.WriteLine("Ingrese la ruta del Archivo: ");
            string originPath = Console.ReadLine();
            Console.WriteLine("Ingrese donde desea guardar los archivos generados: ");
            string destinyPath = Console.ReadLine();

            StateMachine stateMachine = new StateMachine(originPath);
            stateMachine.Run();

            string PageManagerPath = destinyPath + "\\" + ConfigValues.SolutionName + "\\Library\\Managers";
            string ViewModelPath = destinyPath + "\\" + ConfigValues.SolutionName + "\\ViewModels\\Views";
            string ContainerPath = destinyPath + "\\" + ConfigValues.SolutionName + "\\Library\\Navigation";
            string ContentViewPath = destinyPath + "\\" + ConfigValues.SolutionName + "\\Views";
            string BasePath = destinyPath + "\\" + ConfigValues.SolutionName + "\\Library\\Navigation\\Pages";

            PageGeneretor.GeneretePages(stateMachine, BasePath);
            BasePageGenerator.CreateBasePage(stateMachine, BasePath);
            ViewContainerGenerator.CreateViewContainer(stateMachine, ContainerPath);
            PageManagerGenerator.CreatePageManager(stateMachine, PageManagerPath);
            ContentViewGenerator.GenereteContentsViews(stateMachine, ContentViewPath);
            ViewModelGenerator.GenereteViewModels(stateMachine, ViewModelPath);
            PageContainerGenerator.CreatePageContainer(stateMachine, ContainerPath);

            Console.WriteLine("Presione una tecla para salir.");
            Console.ReadLine();
        }
    }
}