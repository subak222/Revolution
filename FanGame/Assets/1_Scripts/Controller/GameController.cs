using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{	
	[SerializeField]
	private UIController		uIController;

	[SerializeField]
	//private GameObject		pattern01;
	private PatternController	patternController;
	private readonly float		scoreScale = 20;	// 점수 증가 계수 (읽기전용)

	private GameSprite			gameSprite;
	
	// 플레이어 점수 (죽지않고 버틴 시간)
	public	float	CurrentScore	{ private set; get; } = 0;
	public 	bool 	IsGamePlay		{ private set; get; } = false;

	private void Start()
	{
		gameSprite = GameObject.Find("Player").GetComponent<GameSprite>();
	}
	public void GameStart()
	{
		uIController.GameStrat();

		//pattern01.SetActive(true);
		patternController.GameStart();

		IsGamePlay = true;

		gameSprite.ChangeSprite();
	}

	public void GameExit()
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.ExitPlaymode();
		#else
		Application.Quit();
		#endif
	}

	public void GameOver()
	{
		uIController.GameOver();

		//pattern01.SetActive(false);
		patternController.GameOver();

		IsGamePlay = false;
	}

	private void Update()
	{
		if ( IsGamePlay == false ) return;

		CurrentScore += Time.deltaTime * scoreScale;
	}
}
