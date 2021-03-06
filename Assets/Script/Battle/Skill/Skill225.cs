﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 降临技能
/// </summary>
public class Skill225 : BaseSkill
{
	// 技能参数
	int param1;

	int param2;

	BaseSkill skill;

	public Skill225(CardFighter card, SkillData skillData, int[] skillParam) : base(card, skillData, skillParam)
	{
		
	}
	
	protected override void InitConfig(SkillData skillData)
	{
		base.InitConfig(skillData);
		
		param1 = skillData.param1;
		param2 = skillData.param2;
	}
	
	public override void RegisterCard(CardFighter card)
	{
		base.RegisterCard(card);
		skill = SkillFactory.GetSkillByID(param1, card, new int[] {param2});
		
		card.AddEventListener(BattleEventType.ON_CARD_PRESENT, OnPresent);
	}
	
	public override void RemoveCard(CardFighter card)
	{
		card.RemoveEventListener(BattleEventType.ON_CARD_PRESENT, OnPresent);
		
		base.RemoveCard(card);
	}
	
	// 给同国家的卡牌加攻击
	void OnPresent(FighterEvent e)
	{
		if (skill != null)
			skill.DoSkill();
	}
}
