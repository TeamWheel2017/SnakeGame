using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	public enum ItemIdx
	{
		//Foods
		Food1, Food2, Food3, Food4, Food5, Food6, Food7,

		//Item
		ScoreMulti2, ScoreMulti4, TailCut3, TailCut5,

		//debuf
		Confusion, 
	}

	public class Item
	{
		private readonly ItemIdx idx;
		private readonly Coord pos;
		private int lifeSpan;
		private int effectLifeSpan;

		public ItemIdx Idx { get => idx; }
		public Coord Pos { get => pos; }
		public int LifeSpan
		{
			get { return lifeSpan; }
			set { lifeSpan = value; }
		}
		public int EffectLifeSpan
		{
			get { return effectLifeSpan; }
			set { effectLifeSpan = value; }
		}

		public Item(ItemIdx idx, Coord pos, int lifeSpan, int effectLifeSpan)
		{
			this.idx = idx;
			this.pos = pos;
			this.lifeSpan = lifeSpan;
			this.effectLifeSpan = effectLifeSpan;
		}
		
	}
}
