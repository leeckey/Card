using UnityEngine;
using System.Collections;

public class CardFighterUI : MonoBehaviour
{
	public CardFighter fighter;

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
