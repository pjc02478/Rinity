using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Nancy;
using Nancy.ModelBinding;
using Nancy.Hosting.Self;

using CommandLine;
using CommandLine.Text;

namespace roslyn_test_2
{
    /* data-models */
    public class Method
    {
        public String name { get; set; }
        public String returnType { get; set; }
    }

    /* protocol-models */
    public class ParseOne
    {
        public class Request
        {
            public String src { get; set; }
        }
        public class Response
        {
            public String className { get; set; }
            public List<Method> methods { get; set; }

            public Response()
            {
                methods = new List<Method>();
            }
        }
    }

    public class CodeAnalysis : NancyModule
    {
        public CodeAnalysis()
        {
            Post["/ca/parse_one"] = x => {
                var request = this.Bind<ParseOne.Request>();
                var response = new ParseOne.Response();

                var tree = CSharpSyntaxTree.ParseText(request.src);
                var methods =
                    tree.GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>();
                var klass =
                    tree.GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>().First();

                response.className = klass.Identifier.ValueText;

                foreach (var method in methods)
                {
                    response.methods.Add(new Method()
                    {
                        name = method.Identifier.ValueText,
                        returnType = method.ReturnType.ToString()
                    });
                }

                return Response.AsJson(response);
            };
        }
    }
    class Options
    {
        [Option('p', "port", DefaultValue = 9900,
            HelpText = "port")]
        public int port { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options) == false)
                return;

            

            var src = new StreamReader("../../src.txt").ReadToEnd();

            

            using (var host = new NancyHost(new Uri("http://localhost:9900")))
            {
                host.Start();
                Console.ReadLine();
            }
        }
    }
}
