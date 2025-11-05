using System.Collections;
using System.Drawing;

namespace LogicGameApp.Models
{
	public class GameData
	{
		private static GameData data;
		private readonly Dictionary<string, Color> colors;
		public Dictionary<string, Color> Colors => colors;

		private GameData()
		{
			colors = new Dictionary<string, Color>();
			colors.Add("Red", Color.Red);
			colors.Add("Green", Color.Green);
			colors.Add("Blue", Color.Blue);
			colors.Add("Black", Color.Black);
			colors.Add("White", Color.White);
			colors.Add("Yell", Color.Yellow);
		}

		public static GameData GetGameData()
		{
			if (data is null)
			{
				data = new GameData();
			}
			return data;
		}
	}
}
