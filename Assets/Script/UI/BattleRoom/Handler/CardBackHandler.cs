using UnityEngine;
using System.Collections;

public class CardBackHandler : BaseHandler
{
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.CardBack);
	}
}
