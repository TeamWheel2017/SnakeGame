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

		public GameSetting(int boardWidth, int boardHeight, bool isInfBoard)
		{
			BoardWidth = boardWidth;
			BoardHeight = boardHeight;
			IsInfBoard = isInfBoard;
		}
	}

	public class Game
	{
		private GameSetting setting;
		private readonly Snake snake;

		public GameSetting Setting { get => setting; }
		public Snake Snake { get => snake; }

		public Game(int width, int height, bool isInfBoard)
		{
			setting = new GameSetting(width, height, isInfBoard);
			snake = new Snake(this);
		}

		public void Tick()
		{
			//TODO : 메서드 완성
		}

		public void MoveSnakeUp()
		{
			snake.Head.Dir = Direction.Up;
		}

		public void MoveSnakeDown()
		{
			snake.Head.Dir = Direction.Down;
		}

		public void MoveSnakeLeft()
		{
			snake.Head.Dir = Direction.Left;
		}

		public void MoveSnakeRight()
		{
			snake.Head.Dir = Direction.Right;
		}
	}
}
