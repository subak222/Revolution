using System.Collections;
using UnityEngine;

public class Pattern01 : MonoBehaviour
{
    [SerializeField]
    private GameObject  gochujangPrefabs;    //고추장 프리팹
    [SerializeField]
    private float       spawnCycle;          //적 생성 주기

    private AudioSource king3;

    private void Awake()
    {
        king3 = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Gochujang));    
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Gochujang));    
    }

    private IEnumerator Gochujang()
    {
        //패턴 시작 전 잠시 대기하는 시간
        float waitTime = 1;
        yield return new WaitForSeconds(waitTime);

        while ( true )
        {
            // 음성 사운드는 재생이 종료되면 
            if ( king3.isPlaying == false )
            {
                king3.Play();
            }

            Vector3 position = new Vector3(Random.Range(Constants.min.x, Constants.max.x), Constants.max.y, 0);
            Instantiate(gochujangPrefabs, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnCycle);
        }
    }
}
