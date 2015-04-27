﻿using UnityEngine;
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

	/// <summary>
	/// 增加回合数
	/// </summary>
	public IEnumerator AddRound(BaseAction action)
	{
		yield return StartCoroutine(battleRound.AddRound(action));
	}

	public IEnumerator EndRound(BaseAction action)
	{
		playerground0.ClearArea();
		playerground1.ClearArea();

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}

	public IEnumerator ShowDamage(BaseAction action)
	{
		DamageNotifyAction damageAction = action as DamageNotifyAction;
		if (action.targetID == playerground0.ID)
		{
			playerground0.ShowDamage(damageAction.damage);
		} 
		else if (action.targetID == playerground1.ID)
		{
			playerground1.ShowDamage(damageAction.damage);
		}
		else
		{
			CardFighterUI cardUI = GetCardUI(action.targetID);
			if (cardUI != null)
				cardUI.ShowCardDamage(damageAction.damage);
		}

		yield return new WaitForSeconds(BattleTime.DAMAGE_SHOW_TIME);
	}

	public IEnumerator AttackChange(BaseAction action)
	{
		AttackChangeAction attackChangeAction = action as AttackChangeAction;
		CardFighterUI cardUI = GetCardUI(action.targetID);
		if (cardUI != null)
			cardUI.AttackChange(attackChangeAction.num);

		yield return new WaitForSeconds(BattleTime.ATTACK_CHANGE_TIME);
	}

	public IEnumerator CardCure(BaseAction action)
	{
		CureNotifyAction cureAction = action as CureNotifyAction;
		CardFighterUI cardUI = GetCardUI(action.targetID);
		if (cardUI != null)
			cardUI.ShowCardCure(cureAction.cure);
		
		yield return new WaitForSeconds(BattleTime.DAMAGE_SHOW_TIME);
	}

	public IEnumerator MaxHpChange(BaseAction action)
	{
		MaxHpChangeAction maxHpAction = action as MaxHpChangeAction;
		CardFighterUI cardUI = GetCardUI(action.targetID);
		if (cardUI != null)
			cardUI.MaxHpChange(maxHpAction.num);
		
		yield return new WaitForSeconds(BattleTime.ATTACK_CHANGE_TIME);
	}

	public CardFighterUI GetCardUI(int id)
	{
		CardFighterUI cardUI = playerground0.GetCardByID(id);
		if (cardUI == null)
			cardUI = playerground1.GetCardByID(id);

		return cardUI;
	}

	public IEnumerator CastSkill(BaseAction action)
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

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}

	/// <summary>
	/// 卡牌回到牌堆
	/// </summary>
	public IEnumerator CardBack(BaseAction action)
	{
		GetPlayer(action.sourceID).CardBack(action);

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}

	/// <summary>
	/// 卡牌进入墓地
	/// </summary>
	public IEnumerator CardDead(BaseAction action)
	{
		GetPlayer(action.sourceID).CardDead(action);

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}

	/// <summary>
	/// 卡牌进入战斗
	/// </summary>
	public IEnumerator CardFight(BaseAction action)
	{
		if (GetPlayer(action.sourceID).ShowLastCard())
			yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);

		GetPlayer(action.sourceID).CardFight(action);

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}

	/// <summary>
	/// 卡牌进入等待区
	/// </summary>
	public IEnumerator CardWait(BaseAction action)
	{
		GetPlayer(action.sourceID).CardWait(action);

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
	}

	public IEnumerator CardShow(BaseAction action)
	{
		int cardID = action.sourceID;
		if (action.sourceArea != CardArea.None)
			cardID = action.targetID;

		if (playerground0.GetCardByID(cardID) != null)
			playerground0.ShowCard(cardID);
		else if (playerground1.GetCardByID(cardID) != null)
			playerground1.ShowCard(cardID);

		yield return new WaitForSeconds(BattleTime.CARD_MOVE_TIME);
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
