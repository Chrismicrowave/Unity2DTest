using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusMenu : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, yuansText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    //public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //character Selection
    //public void OnChacterSelectionClick(bool right)
    //{
    //    if (right)
    //    {
    //        currentCharacterSelection++;

    //        //if we went too far away
    //        if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
    //            currentCharacterSelection = 0;

    //        OnSelectionChanged();
    //    }
    //    else
    //    {
    //        currentCharacterSelection--;

    //        //if we went too far away
    //        if (currentCharacterSelection <0)
    //            currentCharacterSelection = GameManager.instance.playerSprites.Count -1;

    //        OnSelectionChanged();
    //    }
    //}
    //private void OnSelectionChanged()
    //{
    //    characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    //}

    public void playSelect0()
    {
        currentCharacterSelection = 0;
        OnSelectionChanged();
    }
    public void playSelect1()
    {
        currentCharacterSelection = 1;
        OnSelectionChanged();
    }

    private void OnSelectionChanged()
    {
        //characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }


    //weapon upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    public void UpdateMenu()
    {
        //weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "Max";
        else
            upgradeCostText.text = "Update("+ GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString()+")";



        //Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        yuansText.text = GameManager.instance.yuans.ToString();


        //xpBar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (GameManager.instance.GetCurrentLevel() == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " exp";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int targetDiff = currLevelXp - prevLevelXp;
            int currXpIntoLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpIntoLevel / (float)targetDiff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);

            xpText.text = currXpIntoLevel + " / " + targetDiff;
        }

       


    }
}
