using UnityEngine;
using System.Collections;

public class CardDeadHandler : BaseHandler 
{
	protected override void InitHandle()
	{
		handler = roomUI.CardDead;
	}
}
