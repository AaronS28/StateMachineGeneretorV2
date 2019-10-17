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

            PageGeneretor.GeneretePages(stateMachine, destinyPath);
            BasePageGenerator.CreateBasePage(stateMachine, destinyPath);
            ViewContainerGenerator.CreateViewContainer(stateMachine, destinyPath);
            PageManagerGenerator.CreatePageManager(stateMachine, destinyPath);
            ContentViewGenerator.GenereteContentsViews(stateMachine, destinyPath);
            ViewModelGenerator.GenereteViewModels(stateMachine, destinyPath);
            PageContainerGenerator.CreatePageContainer(stateMachine, destinyPath);
        }


    }
}