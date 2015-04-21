using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 卡牌进入战斗区
/// </summary>
public class CardFightAction : BaseAction
{
	// 之前所在区域
	public CardArea sourceArea;

	CardFightAction(int ownerID, int cardID, CardArea area)
	{
		type = ActionType.CardFight;
		sourceID = ownerID;
		targetID = cardID;
		sourceArea = area;
	}
	
	public override string ToString()
	{
		return string.Format("卡牌{0}进入战斗区", targetID);
	}
	
	public static CardFightAction GetAction(int ownerID, int cardID, CardArea area)
	{
		return new CardFightAction(ownerID, cardID, area);
	}
}
