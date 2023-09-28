using System.Collections;
using UnityEngine;

public class KingPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImages; // 패턴2의 경고 이미지
    [SerializeField]
    private GameObject[] playerObjects; // 패턴2의 플레이어 오브젝트
    [SerializeField]
    private float spawnCyclePattern2 = 1; // 패턴2의 생성 주기
    [SerializeField]
    private float spawnDelay = 2.5f;

    [SerializeField]
    private GameObject gochujangPrefab; // 패턴1의 고추장 프리팹
    [SerializeField]
    private int maxEnemyCountPattern1; // 패턴1의 적 생성 숫자
    [SerializeField]
    private float spawnCyclePattern1; // 패턴1의 적 생성 주기

    private AudioSource king;
    private bool isPatternRunning = false;

    private void Awake()
    {
        king = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (!isPatternRunning)
        {
            StartCoroutine(nameof(CombinedPatterns));
        }
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(CombinedPatterns));
    }

    private IEnumerator CombinedPatterns()
    {
        isPatternRunning = true;

        // 패턴 2 실행
        int index = 0;
        while (index < warningImages.Length)
        {
            StartCoroutine(nameof(SpawnPlayerPattern2), index);

            index++;

            yield return new WaitForSeconds(spawnCyclePattern2);
        }

        // 패턴 2가 끝난 후 플레이어 오브젝트를 비활성화하고 초기화
        for (int i = 0; i < playerObjects.Length; ++i)
        {
            playerObjects[i].SetActive(false);
            playerObjects[i].GetComponent<MovingEntity>().Reset();
        }

        // 패턴 2가 끝난 후 대기
        yield return new WaitForSeconds(spawnDelay);

        // 패턴 1 실행
        int countPattern1 = 0;
        while (countPattern1 < maxEnemyCountPattern1)
        {
            // 사운드 재생
            if (!king.isPlaying)
            {
                king.Play();
            }

            Vector3 position = new Vector3(Random.Range(Constants.min.x, Constants.max.x), Constants.max.y, 0);
            Instantiate(gochujangPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnCyclePattern1);

            countPattern1++;
        }

        // 패턴 오브젝트 비활성화
        gameObject.SetActive(false);

        isPatternRunning = false; // 패턴 실행이 끝났음을 표시
    }

    private IEnumerator SpawnPlayerPattern2(int index)
    {
        // 경고 이미지 활성/비활성
        warningImages[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[index].SetActive(false);

        // 플레이어 오브젝트 활성화
        playerObjects[index].SetActive(true);
    }
}
