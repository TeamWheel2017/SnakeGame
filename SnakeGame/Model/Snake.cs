using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	public class Snake
	{
		private readonly Game game;
		private readonly List<Block> body;

		public Block Head { get => body[0]; }

		public Snake(Game game)
		{
			this.game = game;

			body = new List<Block>
			{
				new Block(new Coord(game.Setting.BoardWidth / 2, game.Setting.BoardHeight / 2), Direction.Left),
				new Block(new Coord(game.Setting.BoardWidth / 2 + 1, game.Setting.BoardHeight / 2), Direction.Left),
				new Block(new Coord(game.Setting.BoardWidth / 2 + 2, game.Setting.BoardHeight / 2), Direction.Left),
			};
		}
		
		public void Move()
		{
			//이동
			foreach(var item in body)
			{
				if(game.Setting.IsInfBoard == true)
				{
					switch(item.Dir)
					{
						case Direction.Up:
							item.Pos.Y = (item.Pos.Y + game.Setting.BoardHeight - 1) % game.Setting.BoardHeight;
							break;
						case Direction.Down:
							item.Pos.Y = (item.Pos.Y + 1) % game.Setting.BoardHeight;
							break;
						case Direction.Left:
							item.Pos.X = (item.Pos.X + game.Setting.BoardWidth - 1) % game.Setting.BoardWidth;
							break;
						case Direction.Right:
							item.Pos.X = (item.Pos.X + 1) % game.Setting.BoardWidth;
							break;
					}
				}
				else
				{
					switch (item.Dir)
					{
						case Direction.Up:
							item.Pos.Y--;
							break;
						case Direction.Down:
							item.Pos.Y++;
							break;
						case Direction.Left:
							item.Pos.X--;
							break;
						case Direction.Right:
							item.Pos.X++;
							break;
					}
				}
			}

			//방향 전달
			for(int i = body.Count - 1; i > 0; i--)
			{
				body[i].Dir = body[i - 1].Dir;
			}
		}

		public void AddNewBlock()
		{
			Block tail = body[body.Count - 1];

			switch(tail.Dir)
			{
				case Direction.Up:
					body.Add(new Block(new Coord(tail.Pos.X, tail.Pos.Y + 1), tail.Dir));
					break;
				case Direction.Down:
					body.Add(new Block(new Coord(tail.Pos.X, tail.Pos.Y - 1), tail.Dir));
					break;
				case Direction.Left:
					body.Add(new Block(new Coord(tail.Pos.X + 1, tail.Pos.Y), tail.Dir));
					break;
				case Direction.Right:
					body.Add(new Block(new Coord(tail.Pos.X - 1, tail.Pos.Y), tail.Dir));
					break;
			}
		}
	}
}
