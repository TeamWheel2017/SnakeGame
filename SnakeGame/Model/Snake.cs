using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	class SnakeEnumerator : IEnumerator<Block>
	{
		private readonly Block head;
		private Block current;

		public Block Current
		{
			get { return current; }
		}

		object IEnumerator.Current
		{
			get { return Current; }
		}

		public SnakeEnumerator(Block head)
		{
			this.head = head;
			current = head;
		}

		public void Dispose() { }

		public bool MoveNext()
		{
			if (current == head)
			{
				return true;
			}

			if (current.Next != null)
			{
				current = current.Next;
				return true;
			}
			else
			{
				return false;
			}
		}

		public void Reset()
		{
			current = head;
		}
	}

	public class Snake : IEnumerable<Block>
	{
		private readonly Game game;
		private readonly Block head;
		private Block tail;
		private int length;

		public Block Head { get => head; }
		public int Length { get => length; }

		public Snake(Game game)
		{
			this.game = game;

			//snake 초기화
			this.head = new Block(new Coord(game.Setting.BoardWidth, game.Setting.BoardHeight), Direction.Left, null);
			Block temp = new Block(head);
			tail = new Block(temp);
			length = 3;
		}

		#region IEnumerable 구현부
		public IEnumerator<Block> GetEnumerator()
		{
			return new SnakeEnumerator(head);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator<Block>)GetEnumerator();
		}
		#endregion
	}
}
