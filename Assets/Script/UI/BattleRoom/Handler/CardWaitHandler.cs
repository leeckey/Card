using UnityEngine;
using System.Collections;

public class CardWaitHandler : BaseHandler
{
	protected override void InitHandle()
	{
		handler = roomUI.CardWait;
	}
}
