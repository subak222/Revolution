using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EgoRockPattern : MonoBehaviour
{
    [SerializeField] private GameObject[] warningkatana;
    [SerializeField] private GameObject[] warningCircle;
    [SerializeField] private GameObject[] warningBullet;
    [SerializeField] private GameObject[] katana;
    [SerializeField] private GameObject[] katanaCircle;
    [SerializeField] private GameObject[] gun;
    [SerializeField] private GameObject[] bullet;
    [SerializeField]private Animator Leeon;
    [SerializeField]private Animator Jack;
    private AudioSource EgoRock;
    

    void Start()
    {
        EgoRock = GetComponent<AudioSource>();
        Leeon.SetBool("rotaion", false);
        Jack.SetBool("rotaion", false);
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(Pattern));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(Pattern));
        EgoRock.Stop();
        Leeon.SetBool("rotaion", false);
        Jack.SetBool("rotaion", false);
    }

    private IEnumerator Pattern()
    {
        StartCoroutine(nameof(Katana));
        for (int i = 0; i < warningkatana.Length; i++)
        {
            katana[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(nameof(Circle));
        yield return new WaitForSeconds(2f);
        StartCoroutine(nameof(Gun));
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < bullet.Length; i++)
        {
            bullet[i].SetActive(false);
            bullet[i].GetComponent<MovingEntity>().Reset();
        }
        for (int i = 0; i < 2; i++)
        {
            gun[i].SetActive(false);
            katana[i].GetComponent<MovingEntity>().Reset();
        }
        gameObject.SetActive(false);
    }

    private IEnumerator Katana()
    {
        for (int i = 0; i < warningkatana.Length; i++)
        {
            warningkatana[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < warningkatana.Length; i++)
        {
            katana[i].SetActive(true);
            warningkatana[i].SetActive(false);
        }
    }

    private IEnumerator Circle()
    {
        for (int i = 0; i < warningCircle.Length; i++)
        {
            warningCircle[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0;i < katanaCircle.Length; i++)
        {
            warningCircle[i].SetActive(false);
            katanaCircle[i].SetActive(true);
            Leeon.SetBool("rotaion", true);
            Jack.SetBool("rotaion", true);
        }
    }

    private IEnumerator Gun()
    {
        for (int i = 0; i<gun.Length ; i++)
        {
            gun[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i =0; i < warningBullet.Length; i++)
        {
            warningBullet[i].SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        for (int i =0; i < bullet.Length; i++)
        {
            warningBullet[i].SetActive(false);
            bullet[i].SetActive(true);
        }
    }
}
