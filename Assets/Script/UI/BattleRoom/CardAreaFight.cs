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

	/// <summary>
	/// 增加一个卡牌
	/// </summary>
	public override float AddCard(CardFighterUI card)
	{
		cards.Add(card);

		// 显示这张卡牌到等待区域
		card.transform.parent = cardParent.transform;
		HOTween.To(card.transform, 0.3f, new TweenParms().Prop("position", cardAreas[cards.Count - 1].transform.position));
		card.SetActive(true);
		
		return 1f;
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
