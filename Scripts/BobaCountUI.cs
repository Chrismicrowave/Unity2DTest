using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BobaCountUI : MonoBehaviour
{
    public Image[] bobas;
    public Sprite boba;

    private int bobaCountTemp;

    // Start is called before the first frame update
    void Start()
    {
        bobaCountTemp = GameManager.instance.bobaCount;
        InitBobasUI();
    }

    private void Update()
    {

        if (GameManager.instance.bobaCount != bobaCountTemp)
        {
            InitBobasUI();
            bobaCountTemp = GameManager.instance.bobaCount;
        }
    }

    public void InitBobasUI()
    {
        
            //clear bobacount
            for (int i = 0; i < GameManager.instance.bobaCountMax; i++)
            {
                bobas[i].gameObject.SetActive(false);
            }

            
            //redraw current bobacount
            for (int i = 0; i < GameManager.instance.bobaCount; i++)
            {
                bobas[i].gameObject.SetActive(true);
                bobas[i].sprite = boba;
            }


         
        
    }

}
