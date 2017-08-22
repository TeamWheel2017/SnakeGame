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

		public Coord Pos { get => pos; }
		public Direction Dir
		{
			get { return dir; }
			set { dir = value; }
		}

		public Block(Coord pos, Direction dir)
		{
			this.pos = pos;
			this.dir = dir;
		}
	}
}
