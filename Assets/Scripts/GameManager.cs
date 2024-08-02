using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isGameOver = false;
    public bool isGameWin = false;

    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject gameFailPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isGameOver ==true)
        {
            if (isGameWin)
            {
                gameWinPanel.SetActive(true);
                gameFailPanel.SetActive(false);
            }
            else
            {
                gameWinPanel.SetActive(false);
                gameFailPanel.SetActive(true);
            }
        }
    }
}
