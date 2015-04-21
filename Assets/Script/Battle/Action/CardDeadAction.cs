using UnityEngine;
using System.Collections;

/// <summary>
/// 卡牌进入墓地
/// </summary>
public class CardDeadAction : BaseAction
{

	CardDeadAction(int ownerID, int cardID, CardArea area)
	{
		type = ActionType.CardDead;
		sourceID = ownerID;
		targetID = cardID;
		sourceArea = area;
	}
	
	public override string ToString()
	{
		return string.Format("卡牌{0}进入墓地", targetID);
	}
	
	public static CardDeadAction GetAction(int ownerID, int cardID, CardArea area)
	{
		return new CardDeadAction(ownerID, cardID, area);
	}
}
