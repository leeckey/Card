using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

/// <summary>
/// 卡牌等待区域
/// </summary>
public class CardAreaWait : CardAreaBase
{
	public List<GameObject> cardAreas;

	/// <summary>
	/// 增加一个卡牌
	/// </summary>
	public override IEnumerator AddCard(CardFighterUI card)
	{
		cards.Add(card);
		card.ShowUI(true);

		// 显示这张卡牌到等待区域
		card.transform.parent = cardParent.transform;
		HOTween.To(card.transform, BattleTime.CARD_MOVE_TIME, new TweenParms().Prop("position", cardAreas[cards.Count - 1].transform.position));
		card.SetActive(true);

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}

	public override void ClearCard()
	{
		base.ClearCard();
		for (int i = 0; i < cards.Count; i++)
		{
			HOTween.To(cards[i].transform, BattleTime.CARD_MOVE_TIME, new TweenParms().Prop("position", cardAreas[i].transform.position));
		}
	}
}
