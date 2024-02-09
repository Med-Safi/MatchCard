using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    
    public static  int yCoords = 5;
    public static int xCoords = 4;
    public int cardNeeded;
    public int cardPositions;
    private float spaceBetweenSquares = 3f;
    
    public List<GameObject> cardPrefabs;
    public List<GameObject> slots = new List<GameObject>();
    
    private int[] arrayOfCard = new int[100];
    // Start is called before the first frame update
    void Start()
    {
        InitialiseArraryFill();
        CardPositions();
    }
    // Update is called once per frame
    void Update()
    {
    }
    void CardPositions()
    {
        float spawnPosX ;
        float spawnPosY ;
        int index = 0;
        
        for (int y = 0; y < yCoords; y++)
        {
            for (int x = 0; x < xCoords; x++)
            {
                GameObject go = Instantiate(cardPrefabs[arrayOfCard[index]-1], Vector3.zero, Quaternion.identity) as GameObject;
                go.name = "slot" + index.ToString();
                index ++; 
                spawnPosX = x * spaceBetweenSquares;
                spawnPosY = y * spaceBetweenSquares;
                go.transform.position = new Vector3(spawnPosX, spawnPosY, 0);                
                slots.Add(go);
                go.transform.parent = gameObject.transform;
            }
        }
    }
    void InitialiseArraryFill()
    {
        cardPositions = xCoords * yCoords;
        cardNeeded = cardPositions / 2;

        for (int i= 0; i < cardNeeded; i++)
        {
            arrayOfCard[i] = i+1;
        }
        
        for (int i = cardNeeded; i < cardPositions; i++)
        {
            arrayOfCard[i] = i - (cardNeeded-1);
        }
        for (int i = 0; i < cardPositions; i++)
        {
            int randomPosition = Random.Range(0, (cardPositions-1));
            int temp = arrayOfCard[i];
            arrayOfCard[i] = arrayOfCard[randomPosition];
            arrayOfCard[randomPosition] = temp;
        }
    }
    public void LoadNextLevel()
    {
        xCoords += 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log (xCoords);
    }
    
    public Vector3 CalculateGridCenter()
    {

        float gridWidth = (xCoords - 1) * spaceBetweenSquares; 
        float gridHeight = (yCoords - 1) * spaceBetweenSquares; 

        Vector3 bottomLeft = new Vector3(0, 0, 0); 
        Vector3 topRight = new Vector3(gridWidth, gridHeight, 0);

        Vector3 center = (bottomLeft + topRight) / 2;

        return center;
    }
}
