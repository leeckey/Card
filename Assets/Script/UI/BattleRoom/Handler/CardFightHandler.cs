using UnityEngine;
using System.Collections;

public class CardFightHandler : BaseHandler
{
	protected override void InitHandle()
	{
		handler = roomUI.CardFight;
	}
}
