using UnityEngine;
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
	public CardAreaInit cardInitArea;

	// 等待区域
	public CardAreaWait cardWaitArea;

	// 战斗区域
	public CardAreaFight cardFightArea;

	// 死亡区域
	public CardAreaDead cardDeadArea;

	// 所有卡牌
	List<CardFighterUI> allCards;

	public CardFighterUI cardPrefab;

	/// <summary>
	/// 获得对应的卡牌
	/// </summary>
	public CardFighterUI GetCardByID(int id)
	{
		return allCards.Find(card => card.ID == id);
	}

	// 初始化数据
	public void InitPlayerInfo(PlayerFighter fighter)
	{
		this.ID = fighter.ID;
		this.maxHp = fighter.maxHp;
		this.hp = fighter.maxHp;

		allCards = new List<CardFighterUI>();
		foreach (CardFighter card in fighter.allCard)
		{
			CardFighterUI newCardUI = NGUITools.AddChild(cardInitArea.cardParent, cardPrefab.gameObject).GetComponent<CardFighterUI>();
			newCardUI.InitCardUI(card);
			allCards.Add(newCardUI);
		}
		allCards.ForEach(card => cardInitArea.cards.Add(card));
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

	public float ShowCard(int cardID)
	{
		CardFighterUI card = GetCardByID(cardID);
		return cardFightArea.ShowCard(card);
	}


	/// <summary>
	/// 卡牌回到牌堆
	/// </summary>
	public float CardBack(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);
		
		RemoveCard(action, card);
		return cardInitArea.AddCard(card);
	}
	
	/// <summary>
	/// 卡牌进入墓地
	/// </summary>
	public float CardDead(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);

		RemoveCard(action, card);
		return cardDeadArea.AddCard(card);
	}
	
	/// <summary>
	/// 卡牌进入战斗
	/// </summary>
	public float CardFight(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);

		RemoveCard(action, card);
		return cardFightArea.AddCard(card);
	}
	
	/// <summary>
	/// 卡牌进入等待区
	/// </summary>
	public float CardWait(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);
		
		RemoveCard(action, card);
		return cardWaitArea.AddCard(card);
	}


	void RemoveCard(BaseAction action, CardFighterUI card)
	{
		if (action.sourceArea == CardArea.InitArea)
			cardInitArea.RemoveCard(card);
		else if (action.sourceArea == CardArea.WaitArea)
			cardWaitArea.RemoveCard(card);
		else if (action.sourceArea == CardArea.FightArea)
			cardFightArea.RemoveCard(card);
		else if (action.sourceArea == CardArea.DeadArea)
			cardDeadArea.RemoveCard(card);
	}

	/// <summary>
	/// Clears the area.
	/// </summary>
	public void ClearArea()
	{
		cardInitArea.ClearCard();
		cardWaitArea.ClearCard();
		cardFightArea.ClearCard();
		cardDeadArea.ClearCard();
	}

}
