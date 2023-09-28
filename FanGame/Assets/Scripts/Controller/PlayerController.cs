using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
	private	KeyCode				jumpKey = KeyCode.Space;
	[SerializeField]
	private GameController		gameController;

	private	MovementRigidbody2D	movement2D;
	private	PlayerHP			playerHP;
	private ChoosePlayer		choosePlayer;

	private void Awake()
	{
		movement2D	= GetComponent<MovementRigidbody2D>();
		playerHP	= GetComponent<PlayerHP>();
		choosePlayer = GameObject.Find("GameController").GetComponent<ChoosePlayer>();
	}

	private void Update()
	{
		if ( gameController.IsGamePlay == false ) return;

		UpdateMove();
		UpdateJump();
	}

	private void UpdateMove()
	{
		// left, a = -1  /  none = 0  /  right, d = +1
		float x = Input.GetAxisRaw("Horizontal");

		// 좌우 이동
		movement2D.MoveTo(x);
	}

	private void UpdateJump()
	{
		if ( Input.GetKeyDown(jumpKey) )
		{
			movement2D.JumpTo();
		}
		else if ( Input.GetKey(jumpKey) )
		{
			movement2D.IsLongJump = true;
		}
		else if ( Input.GetKeyUp(jumpKey) )
		{
			movement2D.IsLongJump = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) 
    {
		bool isDie = false;
		// 단체 파트 데미지
        if ( other.CompareTag("All") )
		{
			if ( choosePlayer.playerCount == 0 )
			{
				isDie = playerHP.TakeDamage(1);
			}
			else
			{
				isDie = playerHP.TakeDamage(2);
			}
		}

		//잭 파트 데미지
		else if (other.CompareTag("Jack"))
		{
			if (choosePlayer.playerCount == 1)
			{
				isDie = playerHP.TakeDamage(1);
			}
			else if (choosePlayer.playerCount == 3)
			{
				isDie = playerHP.TakeDamage(4);
			}
			else
			{
				isDie = playerHP.TakeDamage(2);
			}
		}

		//오뉴 파트 데미지
		else if (other.CompareTag("Onyu"))
		{
			if (choosePlayer.playerCount == 2)
			{
				isDie = playerHP.TakeDamage(1);
			}
			else if (choosePlayer.playerCount == 4)
			{
				isDie = playerHP.TakeDamage(4);
			}
			else
			{
				isDie = playerHP.TakeDamage(2);
			}
		}

		//제미니 파트 데미지
		else if (other.CompareTag("Gamini"))
		{
			if (choosePlayer.playerCount == 3)
			{
				isDie = playerHP.TakeDamage(1);
			}
			else if (choosePlayer.playerCount == 1)
			{
				isDie = playerHP.TakeDamage(4);
			}
			else
			{
				isDie = playerHP.TakeDamage(2);
			}
		}

		//류 파트 데미지
		else if (other.CompareTag("Ryu"))
		{
			if (choosePlayer.playerCount == 4)
			{
				isDie = playerHP.TakeDamage(1);
			}
			else if (choosePlayer.playerCount == 1)
			{
				isDie = playerHP.TakeDamage(4);
			}
			else
			{
				isDie = playerHP.TakeDamage(2);
			}
		}

		if (isDie == true)
		{
			Die();
		}
	}

	private void Die()
	{
		GetComponent<Collider2D>().enabled = false;
		gameController.GameOver();
	}
}
