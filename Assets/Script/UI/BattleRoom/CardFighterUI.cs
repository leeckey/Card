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
			smallUI.gameObject.SetActive(true);
			bigUI.gameObject.SetActive(false);
		}
		else
		{
			smallUI.gameObject.SetActive(false);
			bigUI.gameObject.SetActive(true);
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
	}

	public void SetActive(bool show)
	{
		if (show)
			container.alpha = 1f;
		else
			container.alpha = 0f;
	}
}
