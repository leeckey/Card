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

	CardFighter card;

	int curHp;

	public void SetCard(CardFighter card)
	{
		this.card = card;

		cardTextrue.mainTexture = ResManager.LoadCardTexture(card.cardData.templateID);
		cardBg.spriteName = "CardBg_" + card.cardData.country;
	}

	public void Reset()
	{
		cardName.text = card.cardData.name;
		attLabel.text = card.Attack.ToString();
		hpLabel.text = card.MaxHP.ToString();
		levelLabel.text = card.Level.ToString();
		curHp = card.MaxHP;
	}

	public void ShowDamage(int damage)
	{
		damageLabel.text = "-" + damage;
		TweenText.Begin(hpLabel, 0.3f, curHp, curHp - damage);
		Vector3 pos = damageLabel.transform.localPosition;
		HOTween.From(damageLabel.transform, 0.3f, new TweenParms().Prop("localPosition", new Vector3(pos.x, pos.y + 50, pos.z)));
		HOTween.From(damageLabel, 0.3f, new TweenParms().Prop("alpha", 1f));
	}
}
