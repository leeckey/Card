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

	public UILabel damageLabel;

	public GameObject effectParent;

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

	public PlayerDirection direction = PlayerDirection.Up;

	void Awake()
	{
		damageLabel.text = string.Empty;
	}

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
			card.Reset();
			CardFighterUI newCardUI = NGUITools.AddChild(cardInitArea.cardParent, cardPrefab.gameObject).GetComponent<CardFighterUI>();
			newCardUI.InitCardUI(card);
			allCards.Add(newCardUI);
			newCardUI.SetDirection(direction);
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

		damageLabel.text = "-" + damage;
		damageLabel.alpha = 1f;
		Vector3 pos = damageLabel.transform.localPosition;
		HOTween.From(damageLabel.transform, 0.3f, new TweenParms().Prop("localPosition", new Vector3(pos.x, pos.y - 30, pos.z)));
		HOTween.To(damageLabel, 0.3f, new TweenParms().Prop("alpha", 0f).Delay(2f));
	}


	public void ShowCure(int cure)
	{
		hp += cure;
		if (hp > maxHp)
			hp = maxHp;
		
		hpLabel.text = string.Format("HP:{0}", hp.ToString());
		
		HOTween.To(hpBar, 0.2f, new TweenParms().Prop("fillAmount", (float)hp / maxHp));
		HOTween.To(hpBarBack, 0.8f, new TweenParms().Prop("fillAmount", (float)hp / maxHp).Delay(0.2f));
		
		damageLabel.text = "+" + cure;
		damageLabel.alpha = 1f;
		Vector3 pos = damageLabel.transform.localPosition;
		HOTween.From(damageLabel.transform, 0.3f, new TweenParms().Prop("localPosition", new Vector3(pos.x, pos.y - 30, pos.z)));
		HOTween.To(damageLabel, 0.3f, new TweenParms().Prop("alpha", 0f).Delay(2f));
	}

	public IEnumerator ShowCard(int cardID)
	{
		CardFighterUI card = GetCardByID(cardID);
		yield return StartCoroutine(cardFightArea.ShowCard(card));
	}

	public IEnumerator ShowLastCard()
	{
		yield return StartCoroutine(cardFightArea.ShowCard(null));
	}

	/// <summary>
	/// 卡牌回到牌堆
	/// </summary>
	public IEnumerator CardBack(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);
		
		RemoveCard(action, card);
		yield return StartCoroutine(cardInitArea.AddCard(card));
	}
	
	/// <summary>
	/// 卡牌进入墓地
	/// </summary>
	public IEnumerator CardDead(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);

		RemoveCard(action, card);
		yield return StartCoroutine(cardDeadArea.AddCard(card));
	}

	public void ShowSkill(int skillID)
	{
		GameObject effect = ResManager.LoadSkillEffect(skillID);
		if (effect == null)
			return;
		
		GameObject go = NGUITools.AddChild(effectParent, effect);
		
		go.GetComponent<SpriteRenderer>().sortingOrder = 10;
		StartCoroutine(DestroyEffect(go));
	}

	IEnumerator DestroyEffect(GameObject go)
	{
		yield return null;
		
		Animator animator = go.GetComponent<Animator>();
		AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
		
		yield return new WaitForSeconds(info.length);
		
		DestroyObject(go);
	}
	
	/// <summary>
	/// 卡牌进入战斗
	/// </summary>
	public IEnumerator CardFight(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);

		RemoveCard(action, card);
		yield return StartCoroutine(cardFightArea.AddCard(card));
	}
	
	/// <summary>
	/// 卡牌进入等待区
	/// </summary>
	public IEnumerator CardWait(BaseAction action)
	{
		CardFighterUI card = GetCardByID(action.targetID);
		
		RemoveCard(action, card);
		yield return StartCoroutine(cardWaitArea.AddCard(card));
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

public enum PlayerDirection
{
	Up,
	Down
}
