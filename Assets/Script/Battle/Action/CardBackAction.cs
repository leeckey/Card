using UnityEngine;
using System.Collections;

/// <summary>
/// 回到牌堆
/// </summary>
public class CardBackAction : BaseAction
{

	CardBackAction(int ownerID, int cardID, CardArea area)
	{
		type = ActionType.CardBack;
		sourceID = ownerID;
		targetID = cardID;
		sourceArea = area;
	}
	
	public override string ToString()
	{
		return string.Format("卡牌{0}回到牌堆", targetID);
	}
	
	public static CardBackAction GetAction(int ownerID, int cardID, CardArea area)
	{
		return new CardBackAction(ownerID, cardID, area);
	}
}
