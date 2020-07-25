using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DiffUIControl : MonoBehaviour {

	public DiffScaling Scaling;

	public Text DifficultyText;

	public InputField CurrentLevel, StartingDiff, Steepness, Delay, Smoothness;

	public GameObject GraphPanel;

	public List<Text> GraphNodes;

	public List<RectTransform> GraphXPositions;

	public Vector2 minMaxNodePositions;

	// Use this for initialization
	void Start () 
	{
		UpdateAllUI();
	}
	
	private void UpdateAllUI()
	{
		CurrentLevel.text = Scaling.CurrentLevel.ToString();
		StartingDiff.text = Scaling.StartingDiff.ToString();
		Steepness.text = Scaling.Steepness.ToString();
		Delay.text = Scaling.Delay.ToString();
		Smoothness.text = Scaling.Smoothness.ToString();
		DifficultyText.text = "Difficulty: " + Scaling.CurrentDifficulty.ToString();
	}

	private void UpdateDiffUI()
	{
		DifficultyText.text = "Difficulty: "+Scaling.CurrentDifficulty;
	}

	public void ToggleGraph(bool toToggle)
	{
		GraphPanel.SetActive(toToggle);
		if (toToggle)
			UpdateGraph();
	}

	public void UpdateGraph()
	{
		for (int i = 0; i <= 9; i++)
		{
			float tempDiff = Scaling.CalculateDifficulty((i + 1) * 5);
			tempDiff=(float)System.Math.Round(tempDiff, 2);
			GraphNodes[i].text = tempDiff.ToString();
			GraphNodes[i].transform.parent.localPosition = new Vector3(
				GraphXPositions[i].localPosition.x+32.5f,
				GraphYPos(tempDiff));
		}
	}

	private float GraphYPos(float difficulty)
	{
		float minDiff = Scaling.CalculateDifficulty(1);
		float maxDiff = Scaling.CalculateDifficulty(50);
		float normalizedDiff = (difficulty/minDiff)/(maxDiff-minDiff);

		return Mathf.Lerp(minMaxNodePositions.x, minMaxNodePositions.y, normalizedDiff);

	}

    #region Algorithm Values

    public void SetCurrentLevel()
	{
		Scaling.SetCurrentLevel(Int16.Parse(CurrentLevel.text));
		UpdateDiffUI();
	}
	public void IncrementCurrentLevel(int amount)
	{
		Scaling.IncremementLevel(amount);
		CurrentLevel.text = Scaling.CurrentLevel.ToString();
		UpdateDiffUI();
	}
	public void SetStartingDiff()
	{
		Scaling.StartingDiff = float.Parse(StartingDiff.text);
		UpdateDiffUI();
	}
	public void SetSteepness()
	{
		Scaling.Steepness = float.Parse(Steepness.text);
		UpdateDiffUI();
	}
	public void SetDelay()
	{
		Scaling.Delay = float.Parse(Delay.text);
		UpdateDiffUI();
	}
	public void SetSmoothness()
	{
		Scaling.Smoothness = float.Parse(Smoothness.text);
		UpdateDiffUI();
	}

	#endregion
}
