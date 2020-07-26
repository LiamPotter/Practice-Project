using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAndRank : MonoBehaviour {

	public LevelScore CurrLevelScore;

	public bool UsePercentages;

	public int PercentMaxScore=100;

	public float BronzePercent=0.1f, SilverPercent=0.5f, GoldPercent=0.8f;

	public void CalcPercentages()
	{
		CurrLevelScore.Bronze = Mathf.RoundToInt(PercentMaxScore * BronzePercent);
		CurrLevelScore.Silver = Mathf.RoundToInt(PercentMaxScore * SilverPercent);
		CurrLevelScore.Gold = Mathf.RoundToInt(PercentMaxScore * GoldPercent);
	}

}
