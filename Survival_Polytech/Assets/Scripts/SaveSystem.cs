using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class SaveSystem : MonoBehaviour {

    #region Singleton

    public static SaveSystem Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Slomalis' seivi chini daun");
            return;
        }
        Instance = this;
    }

    #endregion 

    public GameObject hero;
    public GameObject[] npc;

    [HideInInspector]
    public Save save = new Save();
    private string path;

    private List<GameObject> tempToDel;
    private List<GameObject> tempToCreate;

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "Save.json");

        if (File.Exists(path))
        {
            string loadedData = File.ReadAllText(path);
            int charsCount = loadedData.Length;
            byte[] bytes = new byte[charsCount / 2];
            for (int i = 0; i < charsCount; i += 2) bytes[i / 2] = Convert.ToByte(loadedData.Substring(i, 2), 16);
            loadedData = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            save = JsonUtility.FromJson<Save>(loadedData);
            // save = JsonUtility.FromJson<Save>(File.ReadAllText(path));
            // LoadFromLastSave();
        }
        else
        {
            Debug.Log("Сохранений нет, а возможно ты просто лох");
        }
        tempToDel = new List<GameObject>();
        tempToCreate = new List<GameObject>();
    }

    public void LoadFromLastSave()
    {
        Debug.Log("Start loading last save");
        
        //---Objects to del---
        foreach (GameObject objDel in save.objectsToDel)
        {
            // ?
            Destroy(objDel);
            //objDel.SetActive(false);
        }

        //---Objects to create---
        foreach (GameObject objCr in save.objectsToCreate)
        {
            Instantiate(objCr);    
        }

        //---Objects in inventory---        
        Inventory.Instance.RemoveAll();
        foreach (Item item in save.objectsInInventory)
           Inventory.Instance.Add(item);

        //---Hero---
        StartCoroutine(PlayerPositionSetup());

        hero.GetComponent<CharacterStats>().SetAllStats(save.characterStats);
        hero.GetComponent<CharacterStats>().SetAllPlayerStates(save.playerStates);

        //---NPC---
        for (int i = 0; i < npc.Length; i++)
        {
            npc[i].GetComponent<Transform>().position = new Vector3(save.npcPositions[i].x, save.npcPositions[i].y, save.npcPositions[i].z);
        }
        Debug.Log("Last save was successfully loaded");
    }

    private IEnumerator PlayerPositionSetup()
    {
        yield return new WaitForSeconds(0.075f);
        hero.GetComponent<NavMeshAgent>().ResetPath();
        hero.GetComponent<Transform>().position = new Vector3(save.positionX, save.positionY, save.positionZ);
    }

    public void SaveLastState()
    {
        Debug.Log("Start saving");

        //---Hero---
        save.positionX = hero.GetComponent<Transform>().position.x;
        save.positionY = hero.GetComponent<Transform>().position.y;
        save.positionZ = hero.GetComponent<Transform>().position.z;
        
        save.characterStats = hero.GetComponent<CharacterStats>().GetAllStats();
        save.playerStates = hero.GetComponent<CharacterStats>().GetAllPlayerStates();

        //---NPC---
        save.npcPositions.Clear();
        for (int i = 0; i < npc.Length; i++)
        {            
            save.npcPositions.Add(npc[i].GetComponent<Transform>().position);
        }
        //---Objects to del---          [Destroy(gameObject); +> AddToObjectsToDel(gameObject);]
        save.objectsToDel.AddRange(tempToDel);
        tempToDel.Clear();

        //---Objects to create---       [Instantiate(gameObject); +> AddToObjectsToCreate(gameObject);] 
        save.objectsToCreate.AddRange(tempToCreate);
        tempToCreate.Clear();

        //---Objects in inventory---
        // sv.objectsInInventory.Clear();
        save.objectsInInventory = new List<Item>(Inventory.Instance.items);

        //---Saving to File---
        byte[] bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(save));
        string hex = BitConverter.ToString(bytes);
        File.WriteAllText(path, hex.Replace("-", ""));
        //  File.WriteAllText(path, JsonUtility.ToJson(save));
        Debug.Log("Game saved");
    }

    public void AddToObjectsToDel(GameObject obj)
    {
        /*if (!sv.objectsToDel.Contains(obj))
            sv.objectsToDel.Add(obj); */
        tempToDel.Add(obj);
    }

    public void AddToObjectsToCreate(GameObject obj)
    {
        tempToCreate.Add(obj);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveLastState();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadFromLastSave();
        }
    }

    private void OnApplicationQuit()
    {
        SaveLastState();
        
        Debug.Log("Game was automatically saved on exit");
    }
}
[Serializable]
public class Save
{
    public List<Vector3> npcPositions;
    public List<GameObject> objectsToDel;
    public List<GameObject> objectsToCreate;
    public List<Item> objectsInInventory;

    public float positionX;
    public float positionY;
    public float positionZ;

    public float[] characterStats;
    public bool[] playerStates;   

    public void CLearAll()
    {
        npcPositions.Clear();
        objectsToDel.Clear();
        objectsToCreate.Clear();
        objectsInInventory.Clear();
        positionX = 0;
        positionY = 0;
        positionZ = 0;
        characterStats = new float[0];
        playerStates = new bool[0];        
    }
}
