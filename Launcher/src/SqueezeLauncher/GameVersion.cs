using System;
using System.Collections.Generic;
using System.Linq;

namespace SqueezeLauncher
{
	public struct GameVersion
	{
		public int MajorVersion;
		public int MinorVersion;
		public int PatchVersion;

		public string FullVersion => $"{MajorVersion}.{MinorVersion}.{PatchVersion}";

		public static GameVersion FromString(string fullVersion)
		{
			var splitString = fullVersion.Split('.');

			ValidateNumberOfSections(fullVersion, splitString);

			ValidateValidIntegers(fullVersion, splitString);

			var version = new GameVersion {
				MajorVersion = int.Parse(splitString[0]),
				MinorVersion = int.Parse(splitString[1]),
				PatchVersion = int.Parse(splitString[2])
			};

			ValidateThatThereAreNoNegativeNumbers(fullVersion, version);

			return version;
		}

		static void ValidateNumberOfSections(string fullVersion, string[] splitString)
		{
			if (splitString.Length != 3) {
				throw new ArgumentException("Invalid version string, must be in format x.x.x where x is a positive integer"
				                            + "\nReceived version string: " + fullVersion);
			}
		}

		static void ValidateValidIntegers(string fullVersion, IEnumerable<string> splitString)
		{
			int parsedNumber;

			if (splitString.Any(x => int.TryParse(x, out parsedNumber) == false)) {
				throw new ArgumentException("Invalid version string, " +
				                            "part of the version string was not a valid integer, " +
				                            "version string must be in format x.x.x where x is a positive integer"
				                            + "\nReceived version string: " + fullVersion);
			}
		}

		static void ValidateThatThereAreNoNegativeNumbers(string fullVersion, GameVersion version)
		{
			if (version.MajorVersion < 0) {
				throw new ArgumentException("Invalid version string, " +
				                            "the major version was a negative number when all the numbers should be positive integers, " +
				                            "version string must be in format x.x.x where x is a positive integer"
				                            + "\nReceived version string: " + fullVersion);
			}

			if (version.MinorVersion < 0) {
				throw new ArgumentException("Invalid version string, " +
				                            "the minor version was a negative number when all the numbers should be positive integers, " +
				                            "version string must be in format x.x.x where x is a positive integer"
				                            + "\nReceived version string: " + fullVersion);
			}

			if (version.PatchVersion < 0) {
				throw new ArgumentException("Invalid version string, " +
				                            "the patch version was a negative number when all the numbers should be positive integers, " +
				                            "version string must be in format x.x.x where x is a positive integer"
				                            + "\nReceived version string: " + fullVersion);
			}
		}

		public GameVersion(int majorVersion, int minorVersion, int patchVersion)
		{
			MajorVersion = majorVersion;
			MinorVersion = minorVersion;
			PatchVersion = patchVersion;
		}

		public static bool operator >(GameVersion version1, GameVersion version2)
		{
			return Compare(version1, version2) == -1;
		}

		public static bool operator <(GameVersion version1, GameVersion version2)
		{
			return Compare(version1, version2) == 1;
		}

		static int Compare(GameVersion version1, GameVersion version2)
		{
			if (version1.MajorVersion > version2.MajorVersion) {
				return -1;
			}
			if (version1.MajorVersion < version2.MajorVersion) {
				return 1;
			}

			if (version1.MinorVersion > version2.MinorVersion) {
				return -1;
			}
			if (version1.MinorVersion < version2.MinorVersion) {
				return 1;
			}

			if (version1.PatchVersion > version2.PatchVersion) {
				return -1;
			}
			if (version1.PatchVersion < version2.PatchVersion) {
				return 1;
			}

			return 0;
		}

		public override string ToString()
		{
			return FullVersion;
		}
	}
}
