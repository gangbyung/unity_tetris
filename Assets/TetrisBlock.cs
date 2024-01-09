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
            //회전하기
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
                CheckForLines();//줄이 꽉찼는지 확인

                if (IsGameOver()) // 게임 종료 조건 확인
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
        for(int i=height-1; i>=0; i--) //테트리스 블록을 맨 윗줄부터 아래까지 검색
        {
            if (HasLine(i)) //줄이 꽉차있을경우
            {
                DeleteLine(i); // 그줄을 삭제
                RowDown(i); // 줄을 한칸 내림
                GameManager.instance.addScore(1);

            }
            
            Debug.Log(scoreGame);
        }
    }
    
    bool HasLine(int i) //줄이 블록으로 꽉차있는지 검색
    {
        for(int j = 0; j < width; j++) //줄을 0~9까지 검색
        {
            if(grid[j,i] == null) //비어있다면
                return false; //false 리턴
        }
        return true; //꽉차있다면 true리턴
        
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
        // 블록이 맨 위에 도달했을 때 게임 종료
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
        // 게임 종료 시 수행할 동작을 추가합니다.
        Debug.Log("게임 종료!");
        GameManager.instance.isGameOver = true;
        
        GameManager.instance.EndGame();
        this.enabled = false;
        // 여기에 게임 종료 UI를 나타내거나 다른 작업을 수행할 수 있습니다.
    }
}
