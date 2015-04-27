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
	public override void AddCard(CardFighterUI card)
	{
		cards.Add(card);

		//if (CenterArea(cards.Count - 1))
		//	yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);

		// 显示这张卡牌到等待区域

		HOTween.To(card.transform, BattleTime.CARD_MOVE_TIME, 
		           new TweenParms().Prop("position", cardAreas[cards.Count - 1].transform.position).Delay(BattleTime.CARD_MOVE_TIME).OnComplete(() => {
			if (card != null)
			{
				card.transform.parent = cardAreas[cards.Count - 1].transform;
				card.ShowUI(false);
			}
		}));
		card.SetActive(true);
	}

	public bool ShowCard(CardFighterUI card)
	{
		int index = cards.Count;
		if (card != null)
			index = cards.IndexOf(card);

		return CenterArea(index);
	}

	public bool CenterArea(int index)
	{
		if (index < centerIndex - 2)
		{
			OnCenter(index + 2);
			return true;
		}
		else if (index > centerIndex + 2)
		{
			OnCenter(index - 2);
			return true;
		}

		return false;
	}

	void OnCenter(int index)
	{
		centerIndex = index;
		uiCenter.CenterOn(cardAreas[index].transform);
	}
	

	public override void ClearCard()
	{
		base.ClearCard();
		for (int i = 0; i < cards.Count; i++)
		{
			cards[i].transform.parent = cardAreas[i].transform;
			HOTween.To(cards[i].transform, BattleTime.CARD_MOVE_TIME, new TweenParms().Prop("position", cardAreas[i].transform.position));
		}
	}
}
