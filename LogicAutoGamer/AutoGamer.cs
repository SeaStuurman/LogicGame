using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LogicAutoGamer
{
	internal class AutoGamer
	{
		private Random rnd;
		private HttpClient httpClient;
		private int step = 0;
		private string[] colors;
		public int Step => step;

		public AutoGamer() 
		{
			rnd = new Random((int)DateTime.Now.Ticks);
			httpClient = new HttpClient();
			var addr = Environment.GetEnvironmentVariable("LOGIC_GAME_HTTP") ?? "http://localhost:5172/Game/";
			httpClient.BaseAddress = new Uri(addr);
			httpClient.Timeout = new TimeSpan(0, 0, 10);
		}

		private async Task<bool> InitGame()
		{
			var t = await httpClient.GetAsync("GameSet");
			colors = await t.Content.ReadFromJsonAsync<string[]>();
			return colors.Any() ? true : false;
		}

		public async Task<bool> StartGame()
		{
			bool initState = false;
			try
			{
				initState = await InitGame();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			if (initState)
			{
				Console.WriteLine("0\t* * * *");
				return true;
			}
			else
			{
				Console.WriteLine("Ошибка инициализации сервера!");
				return false;
			}
		}

		public async Task<int> Check(string[] set)
		{
			var content = JsonContent.Create(set, typeof(string[]));
			var res = await httpClient.PutAsync("Check", content);
			var r = await res.Content.ReadFromJsonAsync(typeof(int));
			return (int)r;
		}

		public IEnumerable<string> GetSet()
		{
			var t = colors.ToArray();
			rnd.Shuffle(t);
			return t.Take(4);
		}

		public async Task Start()
		{
			bool state = await StartGame();
			if (state)
			{
				bool win = false;
				int step = 1;
				while (!win)
				{
					var set = GetSet().ToArray();
					Console.Write($"{step}\t{set[0]} {set[1]} {set[2]} {set[3]}\t");
					step++;
					int checkEst = 0;
					try
					{
						checkEst = await Check(set);
					}
					catch(Exception e) 
					{
						Console.WriteLine();
						Console.WriteLine(e.Message);
						break;
					}
					Console.WriteLine(checkEst);
					if (checkEst == 4)
					{
						Console.WriteLine("Победа!");
						win = true;
					}
				}
			}
			Console.ReadLine();
		}

	}
}
