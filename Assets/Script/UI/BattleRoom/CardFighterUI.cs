using UnityEngine;
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
		}
		else
		{
			//smallUI.gameObject.SetActive(false);
			//bigUI.gameObject.SetActive(true);
		}

		SetActive(true);
	}

	public void ShowDead()
	{
		//ShowUI(true);
		//smallUI.ShowDead();
	}

	public void InitCardUI(CardFighter card)
	{
		this.fighter = card;
		this.name = "Card" + ID;
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

	public float ShowCardDamage(int damage)
	{
		//bigUI.ShowDamage(damage);
		return BattleControl.defaultTime;
	}

	public float AttackChange(int att)
	{
		return BattleControl.defaultTime;
	}

	public float ShowCardCure(int cure)
	{
		return BattleControl.defaultTime;
	}

	public float MaxHpChange(int num)
	{
		return BattleControl.defaultTime;
	}

	public float ShowSkill(int skillID)
	{
		GameObject effect = ResManager.LoadSkillEffect(skillID);
		Animator animator = effect.GetComponent<Animator>();
		// animator.animation.clip.
		// AnimatorController controll = effect.GetComponent<AnimatorController>();

		return BattleControl.defaultTime;
	}

	public void StandUp()
	{

	}

	public void SitDown()
	{

	}
	
}
