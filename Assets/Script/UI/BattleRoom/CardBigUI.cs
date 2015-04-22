using UnityEngine;
using System.Collections;

public class CardBigUI : MonoBehaviour
{
	public UILabel cardName;

	public UILabel attLabel;

	public UILabel hpLabel;

	public UILabel levelLabel;

	public UITexture cardTextrue;

	public UISprite cardBg;

	CardFighter card;

	public void SetCard(CardFighter card)
	{
		this.card = card;

		cardTextrue.mainTexture = ResManager.LoadCardTexture(card.cardData.templateID);
		cardBg.spriteName = "CardBg_" + card.cardData.country;
	}

	public void Reset()
	{
		cardName.text = card.cardData.name;
		attLabel.text = string.Format("AT {0}", card.Attack);
		hpLabel.text = string.Format("HP {1}", card.MaxHP);
		levelLabel.text = card.Level.ToString();
	}
}
