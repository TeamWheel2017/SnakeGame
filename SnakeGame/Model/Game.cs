using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	public class GameSetting
	{
		public int BoardWidth { get; private set; }
		public int BoardHeight { get; private set; }
		public bool IsInfBoard { get; private set; }
		public int ScoreMultiplier { get; set; }
		public int SnakeTick { get; set; }
		public bool IsConfusion { get; set; }

		public GameSetting(int boardWidth, int boardHeight, bool isInfBoard)
		{
			BoardWidth = boardWidth;
			BoardHeight = boardHeight;
			IsInfBoard = isInfBoard;

			ScoreMultiplier = 1;
			SnakeTick = 10;
			IsConfusion = false;
		}
	}

	public class Game
	{
		private GameSetting setting;
		private readonly Snake snake;
		private readonly ItemManager manager;
		private int score;

		public GameSetting Setting { get => setting; }
		public Snake Snake { get => snake; }
		public int Score { get => score; }

		public event EventHandler GameOver;

		public Game(int width, int height, bool isInfBoard)
		{
			setting = new GameSetting(width, height, isInfBoard);
			snake = new Snake(this);
			manager = new ItemManager(this);

			score = 0;
		}

		public void Tick()
		{
			manager.Tick();
			
			snake.Move();

			//Collision 검사
			if(snake.IsCollisionOccured())
			{
				GameOver?.Invoke(this, EventArgs.Empty);
			}

			//Score 추가
			AddScore(1);

			//Eaten 검사 및 처리
			manager.ChkEatableAndSetEffect();
		}

		public void MoveSnakeUp()
		{
			if(setting.IsConfusion == true) //비정상
			{
				snake.Head.Dir = Direction.Down;
			}
			else //정상
			{
				snake.Head.Dir = Direction.Up;
			}
		}

		public void MoveSnakeDown()
		{
			if (setting.IsConfusion == true) //비정상
			{
				snake.Head.Dir = Direction.Up;
			}
			else //정상
			{
				snake.Head.Dir = Direction.Down;
			}
			
		}

		public void MoveSnakeLeft()
		{
			if (setting.IsConfusion == true) //비정상
			{
				snake.Head.Dir = Direction.Right;
			}
			else //정상
			{
				snake.Head.Dir = Direction.Left;
			}
		}

		public void MoveSnakeRight()
		{
			if (setting.IsConfusion == true) //비정상
			{
				snake.Head.Dir = Direction.Left;
			}
			else //정상
			{
				snake.Head.Dir = Direction.Right;
			}
		}

		public void AddScore(int score)
		{
			this.score += score * setting.ScoreMultiplier;
		}

		public Coord GetRandomEmptyPos()
		{
			Random rand = new Random();

			Coord pos = new Coord(0, 0);

			while (true)
			{
				pos.X = rand.Next(0, setting.BoardWidth);
				pos.Y = rand.Next(0, setting.BoardHeight);

				if(snake.IsOnSnake(pos) || manager.IsOnItems(pos))
				{
					continue;
				}
				else
				{
					break;
				}
			}

			return pos;
		}
	}
}
