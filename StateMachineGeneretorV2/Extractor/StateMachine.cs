using StateMachineGeneretorV2.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StateMachineGeneretorV2.Extractor
{
    public class StateMachine
    {
        public List<string> BaseMethods { get; } = new List<string>(); 
        public List<string> Views { get; } = new List<string>();
        public List<PageModel> Pages { get; } = new List<PageModel>();
        private string FilePath;

        public StateMachine(string filePath)
        {
            FilePath = filePath;
            BaseMethods = new List<string>();
            Views = new List<string>();
            Pages = new List<PageModel>();
        }

        public void Run()
        {
            if (File.Exists(FilePath))
            {
                string line = "";
                var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
                {
                    while (!(Regex.IsMatch(line, "transition")))
                    {
                        if (Regex.IsMatch(line, "name="))
                        {
                            PageModel page = new PageModel();
                            var regexStateId = Regex.Match(line, "\"[0-9]*\"");
                            var regexState = Regex.Match(line, "\"[a-zA-Z]*\"");
                            page.Id = int.Parse(regexStateId.Value.Replace("\"", ""));
                            page.ClassName = regexState.Value.Replace("\"", "");

                            line = streamReader.ReadLine();
                            line = streamReader.ReadLine();
                            line = streamReader.ReadLine();
                            if (Regex.IsMatch(line, "label"))
                            {
                                var subStates = Regex.Match(line, ">.*?<");
                                string subPages = subStates.Value.Replace(" ", "").Replace("<", "").Replace(">", "");
                                string[] innerPages = subPages.Split(';');
                                for (int i = 0; i < innerPages.Length; i++)
                                {
                                    page.SubPages.Add(innerPages[i]);
                                    string auxView = innerPages[i].Replace("Page", "View");
                                    Views.Add(auxView);
                                }
                            }
                            else
                            {
                                string auxView = "";
                                if (page.ClassName.Contains("GroupPage"))
                                {
                                    auxView = page.ClassName.Replace("GroupPage", "View");
                                }
                                else
                                {
                                    auxView = page.ClassName.Replace("Page", "View");
                                }
                                Views.Add(auxView);
                            }
                            Pages.Add(page);
                        }

                        line = streamReader.ReadLine();
                    }
                    while (!(Regex.IsMatch(line, "automaton")))
                    {
                        if (Regex.IsMatch(line, "from"))
                        {
                            var regexStateId = Regex.Match(line, ">.*?<");
                            int stateId = int.Parse(regexStateId.Value.Replace("<", "").Replace(">", ""));
                            line = streamReader.ReadLine();
                            line = streamReader.ReadLine();
                            if (Regex.IsMatch(line, "read"))
                            {
                                var regexMethodName = Regex.Match(line, ">.*?<");
                                string method = regexMethodName.Value.Replace("<", "").Replace(">", "");
                                Pages.Find(x => x.Id == stateId).Methods.Add(method);
                                if (!BaseMethods.Exists(x => x == method))
                                {
                                    BaseMethods.Add(method);
                                }
                            }
                        }
                        line = streamReader.ReadLine();
                    }
                }
            }
        }
    }
}
