using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
	private	GameObject[]	imageHP;
	private	int				currentHP;
	
	public	float			invincibilityDuration;		// 무적 지속시간
	private	bool			isInvincibility = false;	// 무적 여부

	private	SoundController	soundController;
	private	SpriteRenderer	spriteRenderer;

	private	Color			originColor;

	private void Awake()
	{
		soundController	= GetComponentInChildren<SoundController>();
		spriteRenderer	= GetComponent<SpriteRenderer>();

		currentHP		= imageHP.Length;
		originColor		= spriteRenderer.color;
	}

	public bool TakeDamage(int damage)
	{
		// 무적 상태일 때는 체력이 감소하지 않는다
		if ( isInvincibility == true ) return false;

		if ( currentHP-damage > 0 )
		{
			soundController.Play(0);
			StartCoroutine(nameof(OnInvincibility));

			currentHP = currentHP-damage;
			if ( damage > 1)
			{
				if ( damage == 4 )
				{
					imageHP[currentHP+3].SetActive(false);
					imageHP[currentHP+2].SetActive(false);
				}
				imageHP[currentHP+1].SetActive(false);
			}
			imageHP[currentHP].SetActive(false);
		}
		else
		{
			return true;
		}

		return false;
	}
	
	private IEnumerator OnInvincibility()
	{
		isInvincibility = true;

		float current = 0;
		float percent = 0;
		float colorSpeed = 10;

		while ( percent < 1 )
		{
			current += Time.deltaTime;
			percent = current / invincibilityDuration;

			spriteRenderer.color = Color.Lerp(originColor, Color.black, Mathf.PingPong(Time.time * colorSpeed, 1));

			yield return null;
		}

		spriteRenderer.color = originColor;
		isInvincibility		 = false;
	}
}
