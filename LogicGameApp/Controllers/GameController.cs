using LogicGameApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace LogicGameApp.Controllers
{
	public class GameController : ControllerBase
	{
		public GameController()
		{

		}

		public string[] GameSet()
		{
			return GameData.GetGameData().Colors.Keys.ToArray();
		}

		//[HttpGet]
		public string[] NewGame()
		{
			var colorKeys = GameData.GetGameData().Colors.Keys.ToArray();
			return new string[] { colorKeys[0], colorKeys[1], colorKeys[2], colorKeys[3] };
		}

		//[HttpPut]
		public int Check([FromBody] string[] set)
		{
			var originalSet = NewGame();
			int res = 0;
			for(int i = 0; i < originalSet.Length; i++)
			{
				if (originalSet[i] == set[i])
					res++;
			}
			Thread.Sleep(3000);
			return res;
		}
	}
}
