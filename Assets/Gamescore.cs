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


    //�̱��� ����
    public static Gamescore instance;//��ü�� ������������ ��. ������ ��������

    //��ü���� static �������� ������ ����������
    //�ݴ��s static ������ �Լ����� ��ü�� ���� ����.
    public Gamescore()//������
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
