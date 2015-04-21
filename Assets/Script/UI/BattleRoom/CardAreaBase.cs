using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 战斗区域基类
/// </summary>
public class CardAreaBase : MonoBehaviour
{
	// 
	public GameObject cardParent;

	// 
	public List<CardFighterUI> cards = new List<CardFighterUI>();
	
	// 是否存在某个卡牌
	public bool ContainsCard(CardFighterUI card)
	{
		return cards.Contains(card);
	}

	/// <summary>
	/// 增加一个卡牌
	/// </summary>
	public virtual float AddCard(CardFighterUI card)
	{
		cards.Add(card);
		return 1f;
	}

	/// <summary>
	/// 移除区域中一个卡牌
	/// </summary>
	public virtual float RemoveCard(CardFighterUI card)
	{
		if (cards.Contains(card))
			cards[cards.IndexOf(card)] = null;

		return 0f;
	}

	/// <summary>
	/// 获得某张卡牌的位置
	/// </summary>
	public virtual Vector3 GetPos(CardFighterUI card = null)
	{
		return gameObject.transform.position;
	}

	/// <summary>
	/// 清理卡牌数组
	/// </summary>
	public virtual void ClearCard()
	{
		cards.RemoveAll(card => card == null);
	}
	
}
