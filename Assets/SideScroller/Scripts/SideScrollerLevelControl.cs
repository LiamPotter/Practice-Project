using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class SideScrollerLevelControl : MonoBehaviour {	

	public Transform BottomSpawnPosition;


	public float StartingTimeBetweenObstacles;

	public float MinTimeBetweenObstacles;

	public float TimeRampSpeed;

	private float currentTimeBetweenObstacles;

	private float obstacleSpawnTimer;


	public float ObstacleStartingSpeed;

	public float ObstacleMaxSpeed;

	public float ObstacleSpeedRamp;

	private float currentObstacleSpeed;

	public List<SideScrollerObstacle> Obstacles;

	private int obstacleSelection=0;

	private SideScrollerObstacle spawnedObstacle;

	public Text ScoreText;

	public GameObject RetryPanel;

	private int currentTimeScore;

	private bool playerAlive = true;

	// Use this for initialization
	void Start () 
	{
		currentTimeBetweenObstacles = StartingTimeBetweenObstacles;
		obstacleSpawnTimer = currentTimeBetweenObstacles;
		currentObstacleSpeed = ObstacleStartingSpeed;
		StartCoroutine(ScoreTimer());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!playerAlive)
			return;
		if(CheckTimer())
		{
			SpawnObstacle();
		}
	}

	private bool CheckTimer()
	{
		if(obstacleSpawnTimer<currentTimeBetweenObstacles)
		{
			obstacleSpawnTimer += Time.deltaTime;
			return false;
		}
		else
		{
			obstacleSpawnTimer = 0;

			currentTimeBetweenObstacles =
				Mathf.Max(currentTimeBetweenObstacles - TimeRampSpeed, 
				MinTimeBetweenObstacles);	
			
			return true;
		}
	}

	private void SpawnObstacle()
	{
		obstacleSelection = Random.Range(0, Obstacles.Count);
		spawnedObstacle =
			Instantiate(Obstacles[obstacleSelection],
			BottomSpawnPosition.position, 
			Quaternion.identity);
		spawnedObstacle.StartMoving(currentObstacleSpeed);

		currentObstacleSpeed =
				Mathf.Min(currentObstacleSpeed + ObstacleSpeedRamp,
				ObstacleMaxSpeed);
	}

	private IEnumerator ScoreTimer()
	{
		while(playerAlive)
		{
			yield return new WaitForSeconds(1);
			currentTimeScore +=1;

			ScoreText.text = currentTimeScore.ToString();
		}
	}
	public void PlayerLost()
	{
		playerAlive = false;
		RetryPanel.SetActive(true);
	}

	public void RetryLevel()
	{
		SceneManager.LoadScene(0);
	}

	void OnDisable()
	{
		playerAlive = false;
	}
}
