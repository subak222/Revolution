using System.Collections;
using UnityEngine;

public class ApearPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject[] warning;
    [SerializeField]
    private GameObject[] text;
    [SerializeField]
    private GameObject plane;

    private AudioSource apear;

    private void OnEnable()
    {
        StartCoroutine(nameof(Pattern));
        plane.SetActive(true);
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Pattern));
    }

    private IEnumerator Pattern()
    {
        yield return new WaitForSeconds(0.5f);

        int index = 0;
        while (index < warning.Length)
        {
            StartCoroutine(nameof(SpawnPlayerPattern2), index);

            index++;

            yield return new WaitForSeconds(3f);plane.SetActive(false);
        }

        for (int i = 0; i < text.Length; ++i)
        {
            text[i].SetActive(false);
            text[i].GetComponent<MovingEntity>().Reset();
        }
        
        gameObject.SetActive(false);
    }

    private IEnumerator SpawnPlayerPattern2(int index)
    {
        // 경고 이미지 활성/비활성
        warning[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        warning[index].SetActive(false);

        // 플레이어 오브젝트 활성화
        text[index].SetActive(true);
    }
}
