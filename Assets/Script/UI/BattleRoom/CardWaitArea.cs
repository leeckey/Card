using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

/// <summary>
/// 卡牌等待区域
/// </summary>
public class CardWaitArea : CardBaseArea
{
	int MAX_CARD = 5;

	public GameObject cardPrefab;

	public List<GameObject> cardAreas;

	/// <summary>
	/// 增加一个卡牌到区域内,带动画表现
	/// </summary>
	public override float AddCard(CardFighter card, Vector3 pos)
	{
		cards.Add(card);

		// 生成一张卡牌从pos位置移动过来
		GameObject newCard = NGUITools.AddChild(cardAreas[cards.Count - 1], cardPrefab);
		newCard.SetActive(true);
		HOTween.From(newCard.transform, 0.3f, new TweenParms().Prop("position", pos));

		return 0.5f;
	}

	/// <summary>
	/// 增加一个卡牌
	/// </summary>
	public override float AddCard(CardFighter card)
	{
		cards.Add(card);

		// 显示这张卡牌到等待区域
		GameObject newCard = NGUITools.AddChild(cardAreas[cards.Count - 1], cardPrefab);
		newCard.SetActive(true);

		return 0f;
	}

	/// <summary>
	/// 移除区域中一个卡牌
	/// </summary>
	public override float RemoveCard(CardFighter card)
	{
		// 删除显示对象
		if (!cards.Contains(card))
			return 0f;

		DestroyObject(cardAreas[cards.IndexOf(card)].transform.GetChild(0).gameObject);

		return base.RemoveCard(card);
	}

	/// <summary>
	/// 移除区域中一个卡牌,带动画表现
	/// </summary>
	public override float RemoveCard(CardFighter card, Vector3 pos)
	{
		// 删除显示对象
		if (!cards.Contains(card))
			return 0f;

		int index = cards.IndexOf(card);
		cards[index] = null;

		GameObject newCard = cardAreas[index].transform.GetChild(0).gameObject;

		// 显示对象移动到pos位置
		HOTween.To(newCard.transform, 0.3f, new TweenParms().Prop("position", pos).OnComplete(() => {
			DestroyObject(newCard);
		}));

		return 0.5f;
	}
}
