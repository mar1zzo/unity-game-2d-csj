using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int totalCoins;
    public Text coinText;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins()
    {
        totalCoins++;
        coinText.text = totalCoins.ToString();
    }

    public void LoseHealth(float value)
    {
        healthBar.fillAmount = value / 10;
    }
}
