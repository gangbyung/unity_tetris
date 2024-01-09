using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TrtrisBlock : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 1.0f;
    public static int height = 20;
    public static int width = 10;
    public Vector3 rotationPoint;
    private static Transform[,] grid = new Transform[width, height];
    public int scoreGame = 0;
    public Text scoreText;
    public GameObject other;

    void Start()
    {
        
    }
    
    
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //ȸ���ϱ�
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        if (Time.time - previousTime> (Input.GetKey(KeyCode.DownArrow) ? fallTime/10 : fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();//���� ��á���� Ȯ��

                if (IsGameOver()) // ���� ���� ���� Ȯ��
                {
                    GameOver();
                    return;
                }
                this.enabled = false;
                FindObjectOfType<SpawnerTetris>().NewTetris();
            }
            previousTime = Time.time;
        }
    }
    public void CheckForLines()
    {
        for(int i=height-1; i>=0; i--) //��Ʈ���� ����� �� ���ٺ��� �Ʒ����� �˻�
        {
            if (HasLine(i)) //���� �����������
            {
                DeleteLine(i); // ������ ����
                RowDown(i); // ���� ��ĭ ����
                GameManager.instance.addScore(1);

            }
            
            Debug.Log(scoreGame);
        }
    }
    
    bool HasLine(int i) //���� ������� �����ִ��� �˻�
    {
        for(int j = 0; j < width; j++) //���� 0~9���� �˻�
        {
            if(grid[j,i] == null) //����ִٸ�
                return false; //false ����
        }
        return true; //�����ִٸ� true����
        
    }
    

    void DeleteLine(int i)
    {
        for(int j=0; j<width; j++)
        {
            Destroy(grid[j,i].gameObject);
            grid[j,i] = null;
        }       
    }
    
    

    void RowDown(int i)
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0;j< width; j++)
            {
                if (grid[j,y] != null)
                {
                    grid[j,y-1] = grid[j,y];
                    grid[j,y] = null;
                    grid[j,y-1].transform.position -= new Vector3(0,1,0);
                }
            }
        }
    }
    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.position.x);
            int roundY = Mathf.RoundToInt(children.position.y);

            grid[roundX, roundY] = children;
        }
    }
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundX = Mathf.RoundToInt(children.transform.position.x);
            int roundY = Mathf.RoundToInt(children.transform.position.y);

            if (roundX < 0 || roundX >=width || roundY < 0 || roundY >= height)
            {
                return false;
            }
            if (grid[roundX, roundY] != null)
                return false;
        }


        return true;
    }
    bool IsGameOver()
    {
        // ����� �� ���� �������� �� ���� ����
        for (int j = 0; j < width; j++)
        {
            if (grid[j, height - 1] != null)
            {
                return true;
            }
        }
        return false;
    }

    void GameOver()
    {
        // ���� ���� �� ������ ������ �߰��մϴ�.
        Debug.Log("���� ����!");
        GameManager.instance.isGameOver = true;
        
        GameManager.instance.EndGame();
        this.enabled = false;
        // ���⿡ ���� ���� UI�� ��Ÿ���ų� �ٸ� �۾��� ������ �� �ֽ��ϴ�.
    }
}
