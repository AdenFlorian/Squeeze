using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SqueezeLauncher
{
    public class UpdateClient
	{
		readonly HttpClient _client = new HttpClient {
			//BaseAddress = new Uri("https://squeeze.AdenFlorian.com/update/")
			BaseAddress = new Uri("http://localhost:8080/")
		};

		public async Task<GameVersion> GetLatestVersionNumber()
		{
			try {
				var resp = await _client.GetAsync("/version/newest");
				var content = await resp.Content.ReadAsStringAsync();

				Console.WriteLine("Content: " + content);
				Console.WriteLine("StatusCode: " + resp.StatusCode);
				Console.WriteLine("ReasonPhrase: " + resp.ReasonPhrase);

				return GameVersion.FromString(content);
			} catch (HttpRequestException ex) {
				Console.WriteLine("Status: " + ex);
				Console.WriteLine("Message: " + ex.Message);
				throw;
			}
		}

		public async Task<string> DownloadGameZipByVersion(GameVersion versionToDownload)
		{
			try {
				var resp = await _client.GetAsync($"/package/{versionToDownload}");
				using (var contentStream = await resp.Content.ReadAsStreamAsync())
				using (var downloadedFile = File.Create($"{versionToDownload}.zip")) {
					await contentStream.CopyToAsync(downloadedFile);

					Console.WriteLine("StatusCode: " + resp.StatusCode);
					Console.WriteLine("ReasonPhrase: " + resp.ReasonPhrase);

					return downloadedFile.Name;
				}
			} catch (HttpRequestException ex) {
				Console.WriteLine("Status: " + ex);
				Console.WriteLine("Message: " + ex.Message);
				throw;
			}
		}
	}
}
