using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	public enum Direction
	{
		Up, Down, Left, Right
	}

	public class Block
	{
		private readonly Coord pos;
		private Direction dir;
		private Block next;

		public Coord Pos { get => pos; }
		public Direction Dir
		{
			get { return dir; }
			set { dir = value; }
		}
		public Block Next { get => next; }

		public Block(Coord pos, Direction dir, Block next)
		{
			this.pos = pos;
			this.dir = dir;
			this.next = next;
		}

		public Block(Block prevTail)
		{
			switch (prevTail.Dir)
			{
				case Direction.Up:
					this.pos = new Coord(prevTail.Pos.X, prevTail.Pos.Y + 1);
					break;
				case Direction.Down:
					this.pos = new Coord(prevTail.Pos.X, prevTail.Pos.Y - 1);
					break;
				case Direction.Left:
					this.pos = new Coord(prevTail.Pos.X + 1, prevTail.Pos.Y);
					break;
				case Direction.Right:
					this.pos = new Coord(prevTail.Pos.X - 1, prevTail.Pos.Y);
					break;
			}

			this.dir = prevTail.dir;
			this.next = null;

			prevTail.next = this;
		}

		public void Move(GameSetting setting)
		{
			if (setting.IsInfBoard)
			{
				MoveOnInfiniteBoard(setting.BoardWidth, setting.BoardHeight);
			}
			else
			{
				MoveOnFiniteBoard();
			}
		}

		#region Move 메소드 도우미 메소드들
		private void MoveOnInfiniteBoard(int width, int height)
		{
			switch(dir)
			{
				case Direction.Up:
					pos.Y = (pos.Y + height - 1) % height;
					break;
				case Direction.Down:
					pos.Y = (pos.Y + 1) % height;
					break;
				case Direction.Left:
					pos.X = (pos.X + width - 1) % width;
					break;
				case Direction.Right:
					pos.X = (pos.X + 1) % width;
					break;
			}
		}
		private void MoveOnFiniteBoard()
		{
			switch (dir)
			{
				case Direction.Up:
					pos.Y--;
					break;
				case Direction.Down:
					pos.Y++;
					break;
				case Direction.Left:
					pos.X--;
					break;
				case Direction.Right:
					pos.X++;
					break;
			}
		}
		#endregion

		public void PassDir()
		{
			if(next != null)
			{
				next.dir = dir;
			}
		}
	}
}
