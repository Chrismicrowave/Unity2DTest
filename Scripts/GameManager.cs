using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (Application.isPlaying)
            UnityEditor.SceneVisibilityManager.instance.Show(gameObject, false);
        
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public GameObject hud;
    public GameObject menu;

    //Logic
    public int yuans;
    public int experience;
    public bool enemychasing;
    public int bobaCount;
    public int bobaCountMax;
    public float bobaEnterTime;

    public Vector2 playerFacing;


    //float text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        
    }

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += yuans.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString() + "|";
        s += bobaCount.ToString();

        PlayerPrefs.SetString("SaveState", s);

        Debug.Log("Save State");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change player skin -skip
        yuans = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        weapon.SetWeaponLevel(int.Parse(data[3]));
        bobaCount = int.Parse(data[4]);

        if (GetCurrentLevel() !=1)
            player.SetLevel(GetCurrentLevel());

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;

        Debug.Log("Load State");

    }

    //upgrade weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if (yuans >= weaponPrices[weapon.weaponLevel])
        {
            yuans -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //experience system
    public int GetCurrentLevel()
    {
        int r = 0; //level
        int add = 0; //exp threshold

        while (experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count) //max level
                return r;
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0; // get xp out of table i.e. 4

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        player.OnLevelUp();
    }

}
