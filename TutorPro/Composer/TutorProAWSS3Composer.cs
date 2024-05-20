using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using Umbraco.Cms.Core.Composing;

namespace TutorPro.Composer
{
    public class TutorProAWSS3Composer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            AWSOptions awsOptions = builder.Config.GetAWSOptions();

            var url = builder.Config["AWS:ServiceURL"];
            awsOptions.DefaultClientConfig.ServiceURL = url;
            awsOptions.DefaultClientConfig.DisableHostPrefixInjection = bool.Parse(builder.Config["AWS:DisableHostPrefixInjection"]);
            awsOptions.Credentials = new BasicAWSCredentials(builder.Config["AWS:AccessKey"], builder.Config["AWS:SecretKey"]);

            builder.Services.AddDefaultAWSOptions(awsOptions);
            builder.Services.AddAWSService<IAmazonS3>();
        }
    }
}
