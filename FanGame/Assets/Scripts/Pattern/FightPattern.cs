using System.Collections;
using UnityEngine;

public class FightPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject circleWarning;
    [SerializeField]
    private GameObject fight;

    private float size = 1.15f; //원하는 사이즈
    public float speed; //커질 때의 속

    private float time;
    private Vector2 originScale; //원래 크기

    private AudioSource fights;

    private void Start()
    {
        fights = GetComponent<AudioSource>();
        originScale = transform.localScale; //원래 크기 저장
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Pattern));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Pattern));
        gameObject.transform.localScale = originScale;
        fights.Stop();
    }

    private IEnumerator Pattern()
    {
        yield return new WaitForSeconds(0.5f);

        circleWarning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        circleWarning.SetActive(false);

        fight.SetActive(true);

        while (transform.localScale.x < size)
        {
            transform.localScale = originScale * (1f + time * speed);
            time += Time.deltaTime;

            if (transform.localScale.x >= size)
            {
                time = 0;
                break;
            }
            yield return null;
        }

        fights.Stop();

        fight.SetActive(false);
    }
}
