using System;
using SqueezeLauncher;
using Xunit;

namespace SqueezeLauncherTest
{
    public class GameVersionTest
	{
		[Fact]
		public void FromString_IncorrectNumberOfSections_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("0"));
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("0.0"));
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("0.0.0.0"));
		}

		[Fact]
		public void FromString_Letters_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("a.1.2"));
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("1.b.2"));
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("1.2.c"));
		}

		[Fact]
		public void FromString_NegativeInteger_ThrowsArgumentException()
		{
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("-1.0.0"));
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("1.-9.0"));
			Assert.Throws<ArgumentException>(() => GameVersion.FromString("1.9.-42"));
		}
		
		[Fact]
		public void GreaterThanOperator()
		{
			var V_6_0_0 = GameVersion.FromString("6.0.0");
			var V_5_5_5 = GameVersion.FromString("5.5.5");
			var V_5_5_0 = GameVersion.FromString("5.5.0");
			var V_5_1_0 = GameVersion.FromString("5.1.0");
			var V_5_0_0 = GameVersion.FromString("5.0.0");

			Assert.False(V_5_5_5 > V_5_5_5);

			Assert.True(V_6_0_0 > V_5_0_0);
			Assert.False(V_5_0_0 > V_6_0_0);

			Assert.True(V_5_1_0 > V_5_0_0);
			Assert.False(V_5_0_0 > V_5_1_0);

			Assert.True(V_5_5_5 > V_5_5_0);
			Assert.False(V_5_5_0 > V_5_5_5);
		}


		[Fact]
		public void LessThanOperator()
		{
			var V_6_0_0 = GameVersion.FromString("6.0.0");
			var V_5_5_5 = GameVersion.FromString("5.5.5");
			var V_5_5_0 = GameVersion.FromString("5.5.0");
			var V_5_1_0 = GameVersion.FromString("5.1.0");
			var V_5_0_0 = GameVersion.FromString("5.0.0");

			Assert.False(V_5_5_5 < V_5_5_5);

			Assert.True(V_5_0_0 < V_6_0_0);
			Assert.False(V_6_0_0 < V_5_0_0);

			Assert.True(V_5_0_0 < V_5_1_0);
			Assert.False(V_5_1_0 < V_5_0_0);

			Assert.True(V_5_5_0 < V_5_5_5);
			Assert.False(V_5_5_5 < V_5_5_0);
		}
	}
}
