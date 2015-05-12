using UnityEngine;
using System.Collections;

public class CardSmallUI : MonoBehaviour
{
	public UITexture cardIcon;

	public UILabel waitRound;
	public GameObject roundGo;

	public UILabel attLabel;

	public UILabel hpLabel;

	public UIWidget container;

	CardFighter card;
	int round;

	public void SetFighter(CardFighter card)
	{
		this.card = card;

		cardIcon.mainTexture = ResManager.LoadCardIcon(card.cardData.templateID);

		// ShowActive();
	}

	public void RoundEnd()
	{
		if (round > 0)
			round --;

		waitRound.text = round.ToString();
	}

	public void ShowActive()
	{
		attLabel.text = "AT:" + card.Attack.ToString();
		hpLabel.text = "HP:" + card.MaxHP.ToString();
		round = card.waitRound;
		waitRound.text = round.ToString();
		roundGo.SetActive(true);
		container.alpha = 1f;
	}


	public void ShowDead()
	{
		attLabel.text = string.Empty;
		hpLabel.text = string.Empty;
		roundGo.SetActive(false);
		cardIcon.shader = Shader.Find("_Lucifer_UI_Gray");
	}

	public void Hide()
	{
		container.alpha = 0f;
	}

}
