﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

/// <summary>
/// 玩家显示区域
/// </summary>
public class PlayerGround : MonoBehaviour
{
	public int ID;

	public UILabel nameLabel;
	
	public UITexture icon;
	
	public UISprite hpBar;
	public UISprite hpBarBack;
	public UILabel hpLabel;

	int hp;
	int maxHp;

	// 初始区域
	public CardInitArea cardInitArea;

	// 等待区域
	public CardWaitArea cardWaitArea;

	// 战斗区域
	public CardFightArea cardFightArea;

	// 死亡区域
	public CardDeadArea cardDeadArea;

	// 所有卡牌
	List<CardFighter> allCards;

	/// <summary>
	/// 获得对应的卡牌
	/// </summary>
	public CardFighter GetCardByID(int id)
	{
		return allCards.Find(card => card.ID == id);
	}

	// 初始化数据
	public void InitPlayerInfo(PlayerFighter fighter)
	{
		this.ID = fighter.ID;
		this.maxHp = fighter.maxHp;
		this.hp = fighter.maxHp;

		allCards = new List<CardFighter>();
		foreach (CardFighter card in fighter.allCard)
		{
			allCards.Add(card);
		}
		allCards.ForEach(card => cardInitArea.AddCard(card));
	}

	public void ShowDamage(int damage)
	{
		hp -= damage;
		if (hp > maxHp)
			hp = maxHp;

		hpLabel.text = string.Format("HP:{0}", hp.ToString());

		HOTween.To(hpBar, 0.2f, new TweenParms().Prop("fillAmount", (float)hp / maxHp));
		HOTween.To(hpBarBack, 0.8f, new TweenParms().Prop("fillAmount", (float)hp / maxHp).Delay(0.2f));
	}


	public float InitAreaIn(BaseAction action)
	{
		return 0f;
	}

	public float InitAreaOut(BaseAction aciton)
	{
		return 0f;
	}

	public float WaitAreaIn(BaseAction aciton, Vector3 pos)
	{
		return 1f;
	}

	public float WaitAreaOut(BaseAction action, Vector3 pos)
	{
		return 1f;
	}

	public float FightAreaIn(BaseAction action)
	{
		return 0f;
	}

	public float FightAreaOut(BaseAction action)
	{
		return 0f;
	}

	public float DeadAreaIn(BaseAction action, Vector3 pos)
	{
		return 1f;
	}

	public float DeadAreaOut(BaseAction action, Vector3 pos)
	{
		return 1f;
	}

	/// <summary>
	/// 卡牌回到牌堆
	/// </summary>
	public float CardBack(BaseAction action)
	{
		CardBackAction cardBackAction = action as CardBackAction;

		CardFighter card = GetCardByID(cardBackAction.targetID);

		// 等待中的卡牌回到牌堆
		if (cardBackAction.sourceArea == CardArea.WaitArea)
			return cardWaitArea.RemoveCard(card, cardInitArea.GetPos());

		// 战斗中的卡牌回到牌堆
		if (cardBackAction.sourceArea == CardArea.FightArea)
			return cardFightArea.RemoveCard(card, cardInitArea.GetPos());

		// 死亡的卡牌回到牌堆
		if (cardBackAction.sourceArea == CardArea.DeadArea)
			return cardDeadArea.RemoveCard(card, cardInitArea.GetPos());

		cardInitArea.AddCard(card);
		return 0f;
	}
	
	/// <summary>
	/// 卡牌进入墓地
	/// </summary>
	public float CardDead(BaseAction action)
	{
		CardDeadAction cardDeadAction = action as CardDeadAction;

		CardFighter card = GetCardByID(cardDeadAction.targetID);

		// 等待中的卡牌进入墓地
		if (cardDeadAction.sourceArea == CardArea.WaitArea)
			return cardWaitArea.RemoveCard(card, cardDeadArea.GetPos());
		
		// 战斗中的卡牌进入墓地
		if (cardDeadAction.sourceArea == CardArea.FightArea)
			return cardFightArea.RemoveCard(card, cardDeadArea.GetPos());

		cardDeadArea.AddCard(card);
		return 0f;
	}
	
	/// <summary>
	/// 卡牌进入战斗
	/// </summary>
	public float CardFight(BaseAction action)
	{
		CardFightAction cardFightAction = action as CardFightAction;

		CardFighter card = GetCardByID(cardFightAction.targetID);

		// 等待中的卡牌进入战斗
		if (cardFightAction.sourceArea == CardArea.WaitArea)
		{
			Vector3 pos = cardWaitArea.GetPos(card);
			cardWaitArea.RemoveCard(card, pos);
			return cardFightArea.AddCard(card, pos);
		}
		
		// 死亡的卡牌进入战斗
		if (cardDeadArea.ContainsCard(card))
		{
			cardDeadArea.RemoveCard(card);
			return cardFightArea.AddCard(card, cardDeadArea.GetPos());
		}

		return 0f;
	}
	
	/// <summary>
	/// 卡牌进入等待区
	/// </summary>
	public float CardWait(BaseAction action)
	{
		CardWaitAction cardWaitAction = action as CardWaitAction;

		CardFighter card = GetCardByID(cardWaitAction.targetID);

		// 牌堆的卡牌进入等待区
		if (cardInitArea.ContainsCard(card))
		{
			cardInitArea.RemoveCard(card);
			return cardWaitArea.AddCard(card, cardInitArea.GetPos());
		}
		
		// 战斗中的卡牌进入等待区
		if (cardFightArea.ContainsCard(card))
		{
			cardWaitArea.AddCard(card);
			return cardFightArea.RemoveCard(card, cardWaitArea.GetPos(card));
		}
		
		// 死亡的卡牌进入等待区
		if (cardDeadArea.ContainsCard(card))
		{
			cardDeadArea.RemoveCard(card);
			return cardWaitArea.AddCard(card, cardDeadArea.GetPos());
		}

		return 0f;
	}
}
