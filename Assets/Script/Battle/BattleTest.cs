﻿using UnityEngine;
using System.Collections;

/// <summary>
/// 战斗测试
/// </summary>
public class BattleTest : MonoBehaviour
{
	public bool enable = true;

	void Awake()
	{
		DataManager.GetInstance().Init();

		if (!enable)
			return;

		TestFight();

		Time.timeScale = 2f;
		//foreach (SkillData data in DataManager.GetInstance().skillData.Values)
		//	print(data.id);
	}

	void TestFight()
	{
		PlayerFighter player0 = new PlayerFighter();
		player0.ID = 10000;
		player0.maxHp = player0.HP = 10000;
		PlayerFighter player1 = new PlayerFighter();
		player1.ID = 10001;
		player1.maxHp = player1.HP = 10000;
		BattleRoom room = new BattleRoom();

		for (int i = 1; i <= 2; i++)
		{
			CardData cardData = new CardData();
			cardData.cardTemplateID = i;
			cardData.cardLevel = 10;
			cardData.ID = i;
			CardFighter card = CardFighter.NewCard(cardData);

			if (i < 2)
			{
				card.owner = player0;
				player0.allCard.Add(card);
			}
			else
			{
				card.owner = player1;
				player1.allCard.Add(card);
			}
		}

		room.SetFighters(player0, player1);
		room.StartFight();

		gameObject.GetComponentInChildren<BattleRoomUI>().gameObject.AddComponent<BattleControl>().StartFight(room);
	}
}
