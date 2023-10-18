using System.Collections;
using UnityEngine;

public class FightPattern : MonoBehaviour
{
    [SerializeField]
    private GameObject circleWarning;
    [SerializeField]
    private GameObject fight;

    private float size = 1.15f; //���ϴ� ������
    public float speed; //Ŀ�� ���� ��

    private float time;
    private Vector2 originScale; //���� ũ��

    private AudioSource fights;

    private void Start()
    {
        fights = GetComponent<AudioSource>();
        originScale = transform.localScale; //���� ũ�� ����
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
