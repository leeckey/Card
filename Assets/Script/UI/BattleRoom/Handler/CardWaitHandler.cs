using UnityEngine;
using System.Collections;

public class CardWaitHandler : BaseHandler
{
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.CardWait);
	}
}
