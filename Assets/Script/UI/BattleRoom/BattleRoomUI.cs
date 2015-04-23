using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 战斗场景UI
/// </summary>
public class BattleRoomUI : MonoBehaviour
{
	// 回合计数
	public BattleRound battleRound;

	// 玩家1战斗区域
	public PlayerGround playerground0;

	// 玩家2战斗区域
	public PlayerGround playerground1;

	// 
	public void Init(BattleRoom data)
	{
		playerground0.InitPlayerInfo(data.fighter0);
		playerground1.InitPlayerInfo(data.fighter1);
	}

	public float AddRound(BaseAction action)
	{
		battleRound.AddRound(action);

		return BattleControl.defaultTime;
	}

	public float EndRound(BaseAction action)
	{
		playerground0.ClearArea();
		playerground1.ClearArea();
		return BattleControl.defaultTime;
	}

	public float ShowDamage(BaseAction action)
	{
		DamageNotifyAction damageAction = action as DamageNotifyAction;
		if (action.targetID == playerground0.ID)
			playerground0.ShowDamage(damageAction.damage);
		else if (action.targetID == playerground1.ID)
			playerground1.ShowDamage(damageAction.damage);
		else
		{
			CardFighterUI cardUI = GetCardUI(action.targetID);
			if (cardUI != null)
				return cardUI.ShowCardDamage(damageAction.damage);
		}

		return 0;
	}

	public float AttackChange(BaseAction action)
	{
		AttackChangeAction attackChangeAction = action as AttackChangeAction;
		CardFighterUI cardUI = GetCardUI(action.targetID);
		if (cardUI != null)
			return cardUI.AttackChange(attackChangeAction.num);

		return 0;
	}

	public float CardCure(BaseAction action)
	{
		CureNotifyAction cureAction = action as CureNotifyAction;
		CardFighterUI cardUI = GetCardUI(action.targetID);
		if (cardUI != null)
			return cardUI.ShowCardCure(cureAction.cure);
		
		return 0;
	}

	public float MaxHpChange(BaseAction action)
	{
		MaxHpChangeAction maxHpAction = action as MaxHpChangeAction;
		CardFighterUI cardUI = GetCardUI(action.targetID);
		if (cardUI != null)
			return cardUI.MaxHpChange(maxHpAction.num);
		
		return 0;
	}

	public CardFighterUI GetCardUI(int id)
	{
		CardFighterUI cardUI = playerground0.GetCardByID(id);
		if (cardUI == null)
			cardUI = playerground1.GetCardByID(id);

		return cardUI;
	}

	public float CastSkill(BaseAction action)
	{
		SkillStartAction skillStartAction = action as SkillStartAction;

		int cardID = skillStartAction.sourceID;
		CardFighterUI cardUI = GetCardUI(cardID);

		cardUI.StandUp();

		foreach (int id in skillStartAction.targets)
		{
			CardFighterUI target = GetCardUI(id);
			target.ShowSkill(skillStartAction.skillID);
		}

		cardUI.SitDown();

		return BattleControl.defaultTime;
	}

	/// <summary>
	/// 卡牌回到牌堆
	/// </summary>
	public float CardBack(BaseAction action)
	{
		return GetPlayer(action.sourceID).CardBack(action);
	}

	/// <summary>
	/// 卡牌进入墓地
	/// </summary>
	public float CardDead(BaseAction action)
	{
		return GetPlayer(action.sourceID).CardDead(action);
	}

	/// <summary>
	/// 卡牌进入战斗
	/// </summary>
	public float CardFight(BaseAction action)
	{
		return GetPlayer(action.sourceID).CardFight(action);
	}

	/// <summary>
	/// 卡牌进入等待区
	/// </summary>
	public float CardWait(BaseAction action)
	{
		return GetPlayer(action.sourceID).CardWait(action);
	}

	public float CardShow(BaseAction action)
	{
		int cardID = action.sourceID;
		if (action.sourceArea != CardArea.None)
			cardID = action.targetID;

		if (playerground0.GetCardByID(cardID) != null)
			return playerground0.ShowCard(cardID);
		else if (playerground1.GetCardByID(cardID) != null)
			return playerground1.ShowCard(cardID);

		return 0;
	}

	// 获得当前行动的玩家
	PlayerGround GetPlayer(int id)
	{
		if (playerground0.ID == id)
			return playerground0;

		if (playerground1.ID == id)
			return playerground1;

		return null;
	}
}
