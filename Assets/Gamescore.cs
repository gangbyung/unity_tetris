using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Gamescore : MonoBehaviour
{
    public TextMeshProUGUI score;


    //싱글톤 패턴
    public static Gamescore instance;//객체에 속해있지않은 놈. 단일한 전역변수

    //객체에서 static 변수로의 접근은 가능하지만
    //반대로s static 변수나 함수에서 객체로 접근 못함.
    public Gamescore()//생성자
    {
        instance = this;
    }

    private void Start()
    {
        
    }
    public void ScorePrint(int number)
    {
        score.text = "Score : " + number.ToString();
    }
    public void claer()
    {
        
    }

}
