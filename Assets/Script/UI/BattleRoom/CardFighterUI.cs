﻿using UnityEngine;
using System.Collections;

public class CardFighterUI : MonoBehaviour
{
	public CardFighter fighter;

	public CardSmallUI smallUI;

	public CardBigUI bigUI;

	UIWidget container;

	void Awake()
	{
		container = gameObject.GetComponent<UIWidget>();
	}

	public int ID
	{
		get
		{
			if (fighter != null)
				return fighter.ID;

			return 0;
		}
	}

	public void ShowUI(bool small)
	{
		if (small)
		{
			//smallUI.gameObject.SetActive(true);
			//bigUI.gameObject.SetActive(false);
			smallUI.ShowActive();
			bigUI.Hide();
		}
		else
		{
			//smallUI.gameObject.SetActive(false);
			//bigUI.gameObject.SetActive(true);
			bigUI.ShowActive();
			smallUI.Hide();
		}

		SetActive(true);
	}

	public void ShowDead()
	{
		ShowUI(true);
		smallUI.ShowDead();
	}

	public void InitCardUI(CardFighter card)
	{
		this.fighter = card;
		this.name = "Card" + ID;
		smallUI.SetFighter(card);
		bigUI.SetFighter(card);
	}

	public void SetActive(bool show)
	{
		if (show)
			container.alpha = 1f;
		else
			container.alpha = 0f;

		//gameObject.SetActive(false);
		//gameObject.SetActive(true);
		// container.ParentHasChanged();
		UIWidget[] childs = gameObject.GetComponentsInChildren<UIWidget>();
		foreach (UIWidget widget in childs)
			widget.ParentHasChanged();
	}

	public void ShowCardDamage(int damage)
	{
		bigUI.ShowDamage(damage);
	}

	public void AttackChange(int att)
	{

	}

	public void ShowCardCure(int cure)
	{

	}

	public void MaxHpChange(int num)
	{

	}

	public IEnumerator ShowSkill(int skillID)
	{
		GameObject effect = ResManager.LoadSkillEffect(skillID);
		Animator animator = effect.GetComponent<Animator>();
		// animator.animation.clip.
		// AnimatorController controll = effect.GetComponent<AnimatorController>();


		yield return null;
	}

	public void StandUp()
	{
		bigUI.StandUp();
	}

	public void SitDown()
	{
		bigUI.SitDown();
	}

	public void RoundEnd()
	{
		smallUI.RoundEnd();
	}

	public void SetDirection(PlayerDirection pos)
	{
		if (pos == PlayerDirection.Up)
			bigUI.upLength = -15;
		else
			bigUI.upLength = 15;
	}

}


