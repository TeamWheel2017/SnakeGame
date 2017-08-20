using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	public class Coord
	{
		public int X { get; set; }
		public int Y { get; set; }
		
		public Coord(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Coord coord = obj as Coord;
			if (coord == null)
			{
				return false;
			}

			return Equals(coord);
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public bool Equals(Coord coord)
		{
			return (coord.X == X) && (coord.Y == Y);
		}
	}
}
