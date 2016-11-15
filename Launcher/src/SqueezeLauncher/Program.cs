using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SqueezeLauncher
{
	public class Program
	{
		static readonly UpdateClient _updateClient = new UpdateClient();

		const string GameFolderName = "Game";
		const string GameExecutablePath = GameFolderName + "/Squeeze.exe";

		public static void Main(string[] args)
		{
			Console.WriteLine("I'm alive!");
			Task.Run(Start).Wait();
		}

		static async Task Start()
		{
			var latestVersionFromServer = await _updateClient.GetLatestVersionNumber();
			var currentLocalVersion = GetCurrentVersion();

			if (currentLocalVersion < latestVersionFromServer) {
				Console.WriteLine("Game client update available!");

				Console.WriteLine($"Downloading {latestVersionFromServer}...");
				var downloadedFileName = await _updateClient.DownloadGameZipByVersion(latestVersionFromServer);
				Console.WriteLine("Downloaded file: " + downloadedFileName);

				Console.WriteLine("Deleting old files...");
				var gameFolder = new DirectoryInfo(GameFolderName);
				if (gameFolder.Exists) {
					Console.WriteLine("Deleting Game folder...");
					gameFolder.Delete(true);
				}

				Console.WriteLine("Unpacking new files...");
				Directory.CreateDirectory(GameFolderName);
				ZipFile.ExtractToDirectory(downloadedFileName, GameFolderName);
				File.Delete(downloadedFileName);

				Console.WriteLine($"Game client has been updated from v{currentLocalVersion} to v{latestVersionFromServer}!");
				Thread.Sleep(1000);
				Console.WriteLine("Launching!");
				Thread.Sleep(500);
				LaunchGame();
			} else {
				Console.WriteLine($"Game client is running the latest version! v{currentLocalVersion}");
				Thread.Sleep(1500);
				Console.WriteLine("Launching!");

				LaunchGame();
			}
		}

		static void LaunchGame()
		{
			Process.Start(GameExecutablePath);
		}

		static GameVersion GetCurrentVersion()
		{
			var version = "";

			var versionFile = new FileInfo(GameFolderName + "/version.txt");

			if (versionFile.Exists) {
				version = File.ReadAllText(GameFolderName + "/version.txt");
			} else {
				version = "0.0.0";
			}
			
			return GameVersion.FromString(version);
		}
	}
}
