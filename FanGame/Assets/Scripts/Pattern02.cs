using System.Collections;
using UnityEngine;

public class Pattern02 : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warningImages; // 경고 이미지
    [SerializeField]
    private GameObject[] playerObjects; // 플레이어 오브젝트
    [SerializeField]
    private float spawnCycle = 1; // 생성 주기
    [SerializeField]
    private float playerWarningDistanceThreshold = 0.1f; // 플레이어와 경고의 위치 차이 임계값

    private void OnEnable()
    {
        StartCoroutine(nameof(Process));
    }

    private void OnDisable()
    {
        // 다음 사용을 위해 플레이어 오브젝트의 상태를 초기화
        for (int i = 0; i < playerObjects.Length; ++i)
        {
            playerObjects[i].SetActive(false);
            playerObjects[i].GetComponent<MovingEntity>().Reset();
        }

        StopCoroutine(nameof(Process));
    }

    private IEnumerator Process()
    {
        // 패턴 시작 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        // 플레이어의 등장 위치를 겹치지 않는 임의의 위치로 설정
        int[] numbers = Utils.RandomNumbers(2, 2);

        int index = 0;
        while (index < numbers.Length)
        {
            StartCoroutine(nameof(SpawnPlayer), index);

            index++;

            yield return new WaitForSeconds(spawnCycle);
        }

        // 패턴 종료 전 잠시 대기하는 시간
        yield return new WaitForSeconds(1);

        // 패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPlayer(int index)
    {
        // 경고 이미지 활성/비활성
        warningImages[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warningImages[index].SetActive(false);

        // 플레이어 오브젝트 활성화
        playerObjects[index].SetActive(true);

        // 플레이어와 경고의 위치 차이를 모니터링하고 일정 거리 내에 있으면 플레이어 오브젝트를 비활성화
        while (Vector3.Distance(playerObjects[index].transform.position, warningImages[index].transform.position) > playerWarningDistanceThreshold)
        {
            yield return null; // 프레임 대기
        }

        playerObjects[index].SetActive(false);
    }
}
