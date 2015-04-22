using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

/// <summary>
/// 卡牌战斗区域
/// </summary>
public class CardAreaFight : CardAreaBase
{
	public List<GameObject> cardAreas;

	public UICenterOnChild uiCenter;

	int centerIndex = 2;

	/// <summary>
	/// 增加一个卡牌
	/// </summary>
	public override float AddCard(CardFighterUI card)
	{
		cards.Add(card);

		float time = CenterArea(cards.Count - 1);

		// 显示这张卡牌到等待区域
		card.transform.parent = cardAreas[cards.Count - 1].transform;
		HOTween.To(card.transform, 0.3f, new TweenParms().Prop("position", cardAreas[cards.Count - 1].transform.position).OnComplete(() => {
			if (card != null)
				card.ShowUI(false);
		}));
		card.SetActive(true);
		
		return BattleControl.defaultTime + time;
	}

	public float ShowCard(CardFighterUI card)
	{
		int index = cards.Count - 1;
		if (card != null)
			index = cards.IndexOf(card);

		return CenterArea(index);
	}

	public float CenterArea(int index)
	{
		if (index < centerIndex - 2)
		{
			OnCenter(index + 2);
			return BattleControl.defaultTime;
		}
		else if (index > centerIndex + 2)
		{
			OnCenter(index - 2);
			return BattleControl.defaultTime;
		}

		return 0;
	}

	void OnCenter(int index)
	{
		centerIndex = index;
		uiCenter.CenterOn(cardAreas[index].transform);
	}

	/// <summary>
	/// 移除区域中一个卡牌
	/// </summary>
	public override float RemoveCard(CardFighterUI card)
	{
		// 删除显示对象
		
		return base.RemoveCard(card);
	}

	public override void ClearCard()
	{
		base.ClearCard();
		for (int i = 0; i < cards.Count; i++)
		{
			HOTween.To(cards[i].transform, 0.3f, new TweenParms().Prop("position", cardAreas[i].transform.position));
		}
	}
}
