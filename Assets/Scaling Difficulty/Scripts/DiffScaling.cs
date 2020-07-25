using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffScaling : MonoBehaviour {

	private int currLevel;
	public int CurrentLevel { get { return currLevel; }set { } }

	public float CurrentDifficulty { get { return CalculateDifficulty(currLevel); } set {  } }

	public float StartingDiff=1f,Steepness=0.5f, Delay=1f, Smoothness=0.5f;

	public void SetCurrentLevel(int toValue)
	{
		currLevel = toValue;
	}

	public void IncremementLevel(int toIncrement)
	{
		currLevel += toIncrement;
	}

	public float CalculateDifficulty(int level)
	{
		float diff;
		diff = Mathf.Pow(Mathf.Pow(StartingDiff * Steepness, Smoothness) * Mathf.Pow(Steepness, Delay), -level);
		return diff;
	}
}
