using System.Collections;
using UnityEngine;

public class FlowerPattern : MonoBehaviour
{
    [SerializeField]
    private MovementTransform2D boss;           // 보스 오브젝트
    [SerializeField]
    private GameObject bossProjectile;          // 보스 발사체
    [SerializeField]
    private float attackRate = 0.5f;               // 보스 공격 주기

    private AudioSource flowerDance;           // 오디오 소스 추가

    private void Awake()
    {
        flowerDance = GetComponent<AudioSource>(); // 오디오 소스 가져오기
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
        flowerDance.Stop();
    }

    private void OnDisable()
    {
        boss.GetComponent<MovingEntity>().Reset();
        StopAllCoroutines(); // 모든 코루틴 중지

        // 패턴 끝날 때 오디오 정지
        flowerDance.Stop();
    }

    private IEnumerator Process()
    {
        // 패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        // 보스가 아래로 이동
        yield return StartCoroutine(nameof(MoveDown));

        int count = 0;
        flowerDance.Play();
        while ( count < 2)
        {
            CircleFire();

            count++;

            yield return new WaitForSeconds(attackRate);
        }

        // 패턴 종료 후 보스 오브젝트 비활성화
        boss.gameObject.SetActive(false);

        // 패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator MoveDown()
    {
        // 목표 위치
        float bossDestinationY = 2;


        // 보스 오브젝트 활성화
        boss.gameObject.SetActive(true);

        // 보스가 목표위치까지 이동했는지 검사
        while (true)
        {
            if (boss.transform.position.y <= bossDestinationY)
            {
                boss.MoveTo(Vector3.zero);

                yield break;
            }

            yield return null;
        }
    }

    private void CircleFire()
    {
        int count = 15;                         // 발사체 생성 개수
        float intervalAngle = 360 / count;      // 발사체 사이의 각도

        // 원 형태로 방사하는 발사체 생성 (count 개수만큼)
        for (int i = 0; i < count; ++i)
        {
            // 발사체 생성
            GameObject clone = Instantiate(bossProjectile, boss.transform.position, Quaternion.identity);

            // 발사체 이동 방향 (각도)
            float angle = intervalAngle * i;
            // 발사체 이동 방향 (벡터)
            float x = Mathf.Cos(angle * Mathf.PI / 180.0f);    // Cos(각도), 라디안 단위의 각도 표현을 위해 PI / 180을 곱함
            float y = Mathf.Sin(angle * Mathf.PI / 180.0f);    // Sin(각도), 라디안 단위의 각도 표현을 위해 PI / 180을 곱함
            // 발사체 이동 방향 설정
            clone.GetComponent<MovementTransform2D>().MoveTo(new Vector2(x, y));
        }
    }
}
