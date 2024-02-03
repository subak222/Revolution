using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
	private	GameObject[]	imageHP;
    [SerializeField]
    private GameObject      miss;
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

    public bool TakeDamage(int damage, int playerCount)
    {
        // 무적 상태일 때는 체력이 감소하지 않습니다.
        if (isInvincibility == true) return false;

        // 추가: playerCount가 4이고, 5% 확률로 회피를 시도합니다.
        if (playerCount == 4 && Random.Range(0f, 1f) <= 0.05f)
        {
            // 회피에 성공했을 때 여기에 추가적인 처리를 할 수 있습니다.
            StartCoroutine(ShowEvadeText());
            return false;  // 데미지를 받지 않고 함수를 종료합니다.
        }

        // 여기서부터는 회피하지 못했을 때의 처리입니다.
        if (currentHP - damage > 0)
        {
            soundController.Play(0);
            StartCoroutine(nameof(OnInvincibility));

            currentHP = currentHP - damage;
            if (damage > 1)
            {
                if (damage == 4)
                {
                    imageHP[currentHP + 3].SetActive(false);
                    imageHP[currentHP + 2].SetActive(false);
                }
                imageHP[currentHP + 1].SetActive(false);
            }
            imageHP[currentHP].SetActive(false);
        }
        else
        {
            return true;  // 만약 체력이 0 이하로 떨어졌다면 true를 반환하여 플레이어의 사망을 나타냅니다.
        }

        return false;  // 데미지를 받았지만 회피에 실패했음을 나타냅니다.
    }

    IEnumerator ShowEvadeText()
    {
        // "회피" 텍스트를 어딘가에 표시하고 1초 동안 보여준 뒤 사라지게 합니다.
        // 이 부분은 실제로 플레이어의 상태를 변경하거나, UI에 텍스트를 표시하는 등으로 수정할 수 있습니다.
        Debug.Log("회피!");
        miss.SetActive(true);

        yield return new WaitForSeconds(1f);

        Debug.Log("회피 종료!");
        miss.SetActive(false);
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
