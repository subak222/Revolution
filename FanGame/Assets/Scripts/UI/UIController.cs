using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private	GameController	gameController;

	[Header("Main UI")]
	[SerializeField]
	private GameObject		mainPanel;
	[SerializeField]
	private TextMeshProUGUI	textMainGrade;

	[Header("Game UI")]
	[SerializeField]
	private GameObject		gamePanel;
	[SerializeField]
	private	TextMeshProUGUI	textScore;

	[Header("Result UI")]
	[SerializeField]
	private GameObject 		resultPanel;
	[SerializeField]
	private TextMeshProUGUI	textResultScore;
	[SerializeField]
	private TextMeshProUGUI	textResultGrade;
	[SerializeField]
	private Text			textResultTalk;
	[SerializeField]
	private TextMeshProUGUI	textResultHighScore;

	private void Awake()
	{
		// 처음 씬이 시작되어 Main UI가 활성화 상태일 때 최고 등급 불러오기
		textMainGrade.text = PlayerPrefs.GetString("HIGHGRADE");
	}

	public void GameStrat()
	{
		mainPanel.SetActive(false);
		gamePanel.SetActive(true);
	}

	public void GameOver()
	{
		int currentScore = (int)gameController.CurrentScore;

		// 현재 점수 출력
		textResultScore.text = currentScore.ToString();
		// 현재 등급 출력, 현재 등급에 해당하는 대사 출력
		CalculateGradeAndTalk(currentScore);
		// 최고 점수 출력
		CalculateHighScore(currentScore);

		gamePanel.SetActive(false);
		resultPanel.SetActive(true);
	}

	public void GoToMainMenu()
	{
		// 플레레이어 위치, 점수, 체력 등 초기화할 게 많기 때문에 그냥 현재 씬을 다시 로드
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void GoToYoutube()
	{
		Application.OpenURL("https://www.youtube.com/channel/UCjBMozaocv9AYsGRVqUVImQ");
	}

    private void Update()
	{
		textScore.text = gameController.CurrentScore.ToString("F0");
	}

	private void CalculateGradeAndTalk(int score)
	{
		if ( score < 2000 )
		{
			textResultGrade.text = "F";
			textResultTalk.text = "에이~\n장난이죠?";
		}
		else if ( score < 3000 )
		{
			textResultGrade.text = "D";
			textResultTalk.text = "설마... 이게 본실력?";
		}
		else if ( score < 4000 )
		{
			textResultGrade.text = "C";
			textResultTalk.text = "이정도는 해야\n사람이죠~";
		}
		else if ( score < 5000 )
		{
			textResultGrade.text = "B";
			textResultTalk.text = "오올~";
		}
		else
		{
			textResultGrade.text = "A";
			textResultTalk.text = "당신은'쌉고수'입니다.";
		}
	}

	private void CalculateHighScore(int score)
	{
		int highScore = PlayerPrefs.GetInt("HIGHSCORE");

		// 최고 점수보다 높은 점수를 획들했을 때
		if ( score > highScore)
		{
			// 최고 등급 갱신
			PlayerPrefs.SetString("HIGHGRADE", textResultGrade.text);
			//최고 점수 갱신
			PlayerPrefs.SetInt("HIGHSCORE", score);

			textResultHighScore.text = score.ToString();
		}
		else
		{
			textResultHighScore.text = highScore.ToString();
		}
	}
}