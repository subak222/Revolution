using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplainText : MonoBehaviour
{
    [SerializeField]
    private Text            explain;
    [SerializeField]
    private GameObject      explainBox;
    private ChoosePlayer    choosePlayer;
    void Start()
    {
        choosePlayer = GameObject.Find("GameController").GetComponent<ChoosePlayer>();
    }

    void Update()
    {
        if (choosePlayer.playerCount == 0) explain.text = "단체 파트 데미지 반감";
        else if (choosePlayer.playerCount == 1) explain.text = "스피드 1.5배\n잭님 파트 데미지 반감\n오뉴님 파트 데미지 2배";
        else if (choosePlayer.playerCount == 2) explain.text = "점프 최대 횟수 3회\n오뉴님 파트 데미지 반감\n제미니님 파트 데미지 2배";
        else if (choosePlayer.playerCount == 3) explain.text = "사이즈 반감\n제미니님 파트 데미지 반감\n리온님 파트 데미지 2배";
        else if (choosePlayer.playerCount == 4) explain.text = "회피율 5%\n리온님 파트 데미지 없음\n류님 파트 데미지 2배";
        else if (choosePlayer.playerCount == 5) explain.text = "무적시간 증가\n류님 파트 데미지 반감\n잭님 파트 데미지 2배";
    }

    public void Explain()
    {
        explainBox.SetActive(true);
    }

    public void Cancel()
    {
        explainBox.SetActive(false);
    }
}
