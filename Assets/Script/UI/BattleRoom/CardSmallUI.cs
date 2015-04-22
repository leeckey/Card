using UnityEngine;
using System.Collections;

public class CardSmallUI : MonoBehaviour
{
	public UITexture cardIcon;

	public UILabel waitRound;
	public GameObject roundGo;

	public UILabel attLabel;

	public UILabel hpLabel;

	CardFighter card;
	int round;

	public void SetFighter(CardFighter card)
	{
		this.card = card;

		cardIcon.mainTexture = ResManager.LoadCardIcon(card.cardData.templateID);

		ShowActive();
	}

	public void RoundEnd()
	{
		if (round > 0)
			round --;

		waitRound.text = round.ToString();
	}

	public void ShowActive()
	{
		attLabel.text = string.Format("AT {0}", card.Attack);
		hpLabel.text = string.Format("HP {1}", card.MaxHP);
		round = card.waitRound;
		waitRound.text = round.ToString();
		roundGo.SetActive(true);
	}


	public void ShowDead()
	{
		attLabel.text = string.Empty;
		hpLabel.text = string.Empty;
		roundGo.SetActive(false);
		cardIcon.shader = Shader.Find("_Lucifer_UI_Gray");
	}

}
