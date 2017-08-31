using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Model
{
	public class ItemManager
	{
		private readonly Game game;
		private List<Item> uneatenItems;
		private List<Item> eatenItems;

		private int tickCounter;

		public ItemManager(Game game)
		{
			this.game = game;
			uneatenItems = new List<Item>();
			eatenItems = new List<Item>();

			tickCounter = 0;
		}
		
		public void ReduceLifeSpanAndCreateNewItem()
		{
			tickCounter++;

			//uneatenItems LifeSpan 깎기
			for (int i = uneatenItems.Count - 1; i >= 0; i--)
			{
				var current = uneatenItems[i];

				current.LifeSpan--;

				if(current.LifeSpan == 0)
				{
					uneatenItems.Remove(current);
				}
			}

			//eatenItems EffectLifeSpan 깎기
			for(int i = eatenItems.Count - 1; i >= 0; i--)
			{
				var current = eatenItems[i];

				current.EffectLifeSpan--;

				if (current.EffectLifeSpan == 0)
				{
					ResetEffect(current.Idx);
					eatenItems.Remove(current);
				}
			}

			//새로운 Item 생성
			if(tickCounter == 5)
			{
				uneatenItems.Add(CreateNewFood());
			}

			if(tickCounter == 10)
			{
				uneatenItems.Add(CreateNewItem());

				tickCounter = 0;
			}
		}

		public void ChkEatenAndSetEffect()
		{
			for(int i = 0; i < uneatenItems.Count; i++)
			{
				var current = uneatenItems[i];

				if (game.Snake.Head.Pos == current.Pos)
				{
					SetEffect(current.Idx);
					
					if(current.EffectLifeSpan > 0) //지속 효과일 떄만 eatenItems에 추가한다.
					{
						eatenItems.Add(current);
					}

					uneatenItems.Remove(current);

					break; //어차피 다른 item들과 위치가 겹칠 리 없음
				}
			}
		}

		public bool IsOnItems(Coord pos)
		{
			foreach(var item in uneatenItems)
			{
				if(item.Pos == pos)
				{
					return true;
				}
			}
			return false;
		}

		private void SetEffect(ItemIdx idx)
		{
			switch(idx)
			{
				case ItemIdx.Food1:
					game.AddScore(10);
					break;
				case ItemIdx.Food2:
					game.AddScore(20);
					break;
				case ItemIdx.Food3:
					game.AddScore(40);
					break;
				case ItemIdx.Food4:
					game.AddScore(80);
					break;
				case ItemIdx.Food5:
					game.AddScore(160);
					break;
				case ItemIdx.Food6:
					game.AddScore(320);
					break;
				case ItemIdx.Food7:
					game.AddScore(640);
					break;
				case ItemIdx.ScoreMulti2:
					game.AddScore(50);
					game.Setting.ScoreMultiplier *= 2;
					break;
				case ItemIdx.ScoreMulti4:
					game.AddScore(50);
					game.Setting.ScoreMultiplier *= 4;
					break;
				case ItemIdx.TailCut3:
					game.Snake.CutTail(3);
					game.AddScore(50);
					break;
				case ItemIdx.TailCut5:
					game.Snake.CutTail(5);
					game.AddScore(50);
					break;
				case ItemIdx.FoodBomb:
					for(int i = 0; i < 5; i++)
					{
						CreateNewFood();
					}
					break;
				case ItemIdx.ItemBomb:
					for (int i = 0; i < 5; i++)
					{
						CreateNewItem();
					}
					break;
				case ItemIdx.ClearItemsOnBoard:
					uneatenItems.Clear();
					break;
				case ItemIdx.ResetAllItems:
					foreach(var item in eatenItems)
					{
						ResetEffect(item.Idx);
					}
					eatenItems.Clear();
					break;
				case ItemIdx.Confusion:
					game.Setting.IsConfusion = true;
					break;
			}
		}

		private void ResetEffect(ItemIdx idx)
		{
			switch(idx)
			{
				case ItemIdx.ScoreMulti2:
					game.Setting.ScoreMultiplier /= 2;
					break;
				case ItemIdx.ScoreMulti4:
					game.Setting.ScoreMultiplier /= 4;
					break;
				case ItemIdx.Confusion:
					game.Setting.IsConfusion = false;
					break;
				default: //Food1 ~ Food7, TailCut3, TailCut5, FoodBomb, ItemBomb, ClearItemsOnBoard, ResetAllItems
					break;
			}
		}

		private Item CreateNewFood()
		{
			Random rand = new Random();

			int probability = rand.Next(0, 127);

			Coord newPos = game.GetRandomEmptyPos();

			if(probability < 64)
			{
				return new Item(ItemIdx.Food1, newPos, 100, 0);
			}
			else if (probability < 64 + 32)
			{
				return new Item(ItemIdx.Food2, newPos, 100, 0);
			}
			else if (probability < 64 + 32 + 16)
			{
				return new Item(ItemIdx.Food3, newPos, 50, 0);
			}
			else if (probability < 64 + 32 + 16 + 8)
			{
				return new Item(ItemIdx.Food4, newPos, 50, 0);
			}
			else if (probability < 64 + 32 + 16 + 8 + 4)
			{
				return new Item(ItemIdx.Food5, newPos, 20, 0);
			}
			else if (probability < 64 + 32 + 16 + 8 + 4 + 2)
			{
				return new Item(ItemIdx.Food6, newPos, 20, 0);
			}
			else
			{
				return new Item(ItemIdx.Food7, newPos, 10, 0);
			}
		}

		private Item CreateNewItem()
		{
			Random rand = new Random();

			int probability = rand.Next(0, 9);

			Coord newPos = game.GetRandomEmptyPos();
			
			switch (probability)
			{
				case 0:
					return new Item(ItemIdx.ScoreMulti2, newPos, 50, 20);
				case 1:
					return new Item(ItemIdx.ScoreMulti4, newPos, 20, 20);
				case 2:
					return new Item(ItemIdx.TailCut3, newPos, 50, 20);
				case 3:
					return new Item(ItemIdx.TailCut5, newPos, 20, 20);
				case 4:
					return new Item(ItemIdx.FoodBomb, newPos, 50, 0);
				case 5:
					return new Item(ItemIdx.ItemBomb, newPos, 50, 0);
				case 6:
					return new Item(ItemIdx.ClearItemsOnBoard, newPos, 50, 0);
				case 7:
					return new Item(ItemIdx.ResetAllItems, newPos, 50, 0);
				case 8:
					return new Item(ItemIdx.Confusion, newPos, 50, 0);
				default:
					throw new Exception("wrong creation of item");
			}
		}
	}
}
