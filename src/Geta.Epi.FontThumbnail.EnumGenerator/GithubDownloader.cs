using Octokit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Geta.Epi.FontThumbnail.EnumGenerator
{
    public static class GithubDownloader
    {
        public static async Task<Stream> DownloadLatestReleaseAsync(string owner, string repo)
        {
            var gitHubClient = new GitHubClient(new ProductHeaderValue("Geta.Epi.FontThumbnail.EnumGenerator"));
            var latestRelease = await gitHubClient.Repository.Release.GetLatest(owner, repo);
            var fileName = Path.GetFileName(latestRelease.ZipballUrl);

            Console.WriteLine(
                "The latest release is tagged at {0} and is named {1}",
                latestRelease.TagName,
                latestRelease.Name);

            var response = await gitHubClient.Connection.Get<object>(new Uri(latestRelease.ZipballUrl), TimeSpan.FromMinutes(1));
            var bytes = response.HttpResponse.Body as byte[];

            return new MemoryStream(bytes);
        }
    }
}
