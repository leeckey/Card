using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class CardBigUI : MonoBehaviour
{
	public UILabel cardName;

	public UILabel attLabel;

	public UILabel hpLabel;

	public UILabel levelLabel;

	public UITexture cardTextrue;

	public UISprite cardBg;

	public UILabel damageLabel;

	public UIWidget container;
	CardFighter card;
	int curHp;

	public float upLength = 10f;

	void Awake()
	{
		Hide();
	}

	public void SetFighter(CardFighter card)
	{
		this.card = card;

		cardTextrue.mainTexture = ResManager.LoadCardMinTexture(card.cardData.templateID);
		cardBg.spriteName = "CardBg_" + card.cardData.country;
	}

	public void ShowActive()
	{
		cardName.text = card.cardData.name;
		attLabel.text = card.Attack.ToString();
		hpLabel.text = card.MaxHP.ToString();
		levelLabel.text = "LV." + card.Level.ToString();
		curHp = card.MaxHP;
		damageLabel.text = string.Empty;
		damageLabel.alpha = 0f;
		container.alpha = 1f;
	}

	public void Hide()
	{
		container.alpha = 0f;
	}

	public void ShowDamage(int damage)
	{
		damageLabel.text = "-" + damage;
		damageLabel.alpha = 1f;
		TweenText.Begin(hpLabel, 0.3f, curHp, curHp - damage);
		Vector3 pos = damageLabel.transform.localPosition;
		HOTween.From(damageLabel.transform, 0.3f, new TweenParms().Prop("localPosition", new Vector3(pos.x, pos.y - 50, pos.z)));
		//HOTween.From(damageLabel, 0.3f, new TweenParms().Prop("alpha", 1f));
		HOTween.To(damageLabel, 0.3f, new TweenParms().Prop("alpha", 0f).Delay(2f));

		curHp -= damage;
	}

	public void ShowCure(int cure)
	{
		damageLabel.text = "+" + cure;
		damageLabel.alpha = 1f;
		TweenText.Begin(hpLabel, 0.3f, curHp, curHp + cure);
		Vector3 pos = damageLabel.transform.localPosition;
		HOTween.From(damageLabel.transform, 0.3f, new TweenParms().Prop("localPosition", new Vector3(pos.x, pos.y - 50, pos.z)));
		//HOTween.From(damageLabel, 0.3f, new TweenParms().Prop("alpha", 1f));
		HOTween.To(damageLabel, 0.3f, new TweenParms().Prop("alpha", 0f).Delay(2f));
	}

	public void StandUp()
	{
		Vector3 pos = transform.localPosition;
		HOTween.To(transform, 0.3f, new TweenParms().Prop("localPosition", new Vector3(pos.x, pos.y + upLength, pos.z)));
	}
	
	public void SitDown()
	{
		Vector3 pos = transform.localPosition;
		HOTween.To(transform, 0.3f, new TweenParms().Prop("localPosition", new Vector3(pos.x, pos.y - upLength, pos.z)));
	}
}
