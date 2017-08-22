using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	public class Eatable
	{
		private readonly int idx;
		private readonly Coord pos;
		private int lifeSpan;
		private int effectLifeSpan;
		private readonly int score;

		public int Idx { get => idx; }
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
		public int Score { get => score; }

		public Eatable(int idx, Coord pos, int lifeSpan, int effectLifeSpan, int score)
		{
			this.idx = idx;
			this.pos = pos;
			this.lifeSpan = lifeSpan;
			this.effectLifeSpan = effectLifeSpan;
			this.score = score;
		}

		public abstract void SetEffect();
		public abstract void ResetEffect();


		//TODO : Eatable 효과를 어디서 설정할지 결정하기
		//Plan A. Eatable의 idx를 확인해서 Eatable Manager에서 설정/해제
		//Plan B. Eatable이 직접 Set, Reset 메소드 사용하여 설정/해제
	}
}
