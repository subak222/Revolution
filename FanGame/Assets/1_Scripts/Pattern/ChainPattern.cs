using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject[] chain;
    [SerializeField]
    private GameObject[] chainWarning;

    private AudioSource chains;
    private void Start()
    {
        chains = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        StartCoroutine(nameof(Pattern));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Pattern));
        chains.Stop();
    }

    private IEnumerator Pattern()
    {
        yield return new WaitForSeconds(0.5f);
        int index = 0;
        while (index < chain.Length)
        {
            chainWarning[index].SetActive(true);
            yield return new WaitForSeconds(0.2f);
            chainWarning[index].SetActive(false);
            
            index++;
        }
        index = 0;
        yield return new WaitForSeconds(0.2f);
        chains.Play();
        while (index < chain.Length)
        {
            StartCoroutine(nameof(Pattern2), index);

            index++;
        }
        
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < chain.Length; ++i)
        {
            chain[i].SetActive(false);
            chain[i].GetComponent<MovingEntity>().Reset();
        }

        gameObject.SetActive(false);
    }

    private IEnumerator Pattern2(int index)
    {
        yield return new WaitForSeconds(0.3f);

        chain[index].SetActive(true);
    }
}
