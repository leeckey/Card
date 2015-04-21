using UnityEngine;
using System.Collections;

public class CardDeadHandler : BaseHandler 
{
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.CardDead);
	}
}
