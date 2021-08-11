using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPipeline
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new MyPipelineStack(app, "MyPipelineStack", new StackProps
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = "442608252338",
                    Region = "eu-west-2"
                }
            });
            app.Synth();
        }
    }
}