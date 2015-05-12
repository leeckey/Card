using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

/// <summary>
/// 卡牌死亡区域
/// 移除或增加不需要变现,只显示最后增加进来的卡牌
/// </summary>
public class CardAreaDead : CardAreaBase
{
	/// <summary>
	/// 显示最新的死亡卡牌
	/// </summary>
	public override IEnumerator AddCard(CardFighterUI card)
	{
		cards.Add(card);
		
		// 显示这张卡牌到等待区域
		card.transform.parent = cardParent.transform;
		HOTween.To(card.transform, BattleTime.CARD_MOVE_TIME, new TweenParms().Prop("position", cardParent.transform.position));
		card.ShowDead();

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}
	
}
