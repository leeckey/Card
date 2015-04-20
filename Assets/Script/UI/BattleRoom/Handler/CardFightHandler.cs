using UnityEngine;
using System.Collections;

public class CardFightHandler : BaseHandler
{
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.CardFight);
	}
}
