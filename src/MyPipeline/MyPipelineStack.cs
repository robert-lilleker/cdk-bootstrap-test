using Amazon.CDK;
using Amazon.CDK.Pipelines;
using Amazon.CDK.AWS.CodePipeline;
using Amazon.CDK.AWS.CodePipeline.Actions;

namespace MyPipeline
{
    public class MyPipelineStack : Stack
    {
        internal MyPipelineStack(Construct scope, string id, IStackProps props=null) : base(scope, id, props)
        {
            var sourceArtifact = new Artifact_();
            var cloudAssemblyArtifact = new Artifact_();

            var pipeline = new CdkPipeline(this, "Pipeline", new CdkPipelineProps
            {
                PipelineName = "MyAppPipeline",
                CloudAssemblyArtifact = cloudAssemblyArtifact,
                SourceAction = new GitHubSourceAction(new GitHubSourceActionProps
                {
                    ActionName = "GitHub",
                    Output = sourceArtifact,
                    OauthToken = SecretValue.SecretsManager("github-token"),
                    Trigger = GitHubTrigger.POLL,
                    Owner = "robert-lilleker",
                    Repo = "cdk-bootstrap-test"
                }),
                SynthAction = SimpleSynthAction.StandardNpmSynth(new StandardNpmSynthOptions
                {
                    SourceArtifact = sourceArtifact,
                    CloudAssemblyArtifact = cloudAssemblyArtifact,
                    BuildCommand = "npm run build"
                })
            });
        }
    }
}