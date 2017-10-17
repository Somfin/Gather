using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class GameSave
{
    [SerializeField]
    private string level;
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;
    [SerializeField]
    private List<string> items;
    [SerializeField]
    private List<string> loadout;

    public GameSave(string level, float x, float y, List<string> items, List<string> loadout)
    {
        this.level = level;
        this.x = x;
        this.y = y;
        this.items = items;
        this.loadout = loadout;
    }

    public GameSave(GameObject cassidy, GameObject savePoint)
    {
        this.level = SceneManager.GetActiveScene().name;
        this.x = savePoint.transform.position.x;
        this.y = savePoint.transform.position.y;
        this.items = cassidy.GetComponent<CassidyInventorySystem>().GetCurrentItems().Select(i => i.name).ToList();
        this.loadout = cassidy.GetComponent<CassidyCombat>().GetCurrentLoadout().Select(i => i.name).ToList();
    }

    public static string CurrentFilePath()
    {
        return FilePath(GameState.Instance.currentFile.ToString());
    }

    public static string FilePath(string game)
    {
        return Application.persistentDataPath + "\\TestFile" + game + ".gather";
    }

    public static bool HasFile(GameState.File file)
    {
        return File.Exists(FilePath(file.ToString()));
    }

    public static void LoadFromFile(GameObject cassidy, string filename)
    {
        var data = File.ReadAllText(filename);
        var save = JsonUtility.FromJson<GameSave>(data);
        save.Load(cassidy);
    }

    public void Load(GameObject cassidy)
    {
        SceneHandler.Instance.LoadScene(level);
        GameState.Instance.StartCoroutine(SpawnCassidy(cassidy));
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        Debug.Log(json);
        using (var save = File.CreateText(CurrentFilePath()))
        {
            save.Write(json);
        }
        Debug.Log(Application.persistentDataPath);
    }

    private void FillInventory(GameObject instance)
    {
        var itemAssembler = new ItemAssembler();
        var inventory = instance.GetComponentInChildren<CassidyInventorySystem>();
        foreach (string item in items)
        {
            inventory.AddItem(itemAssembler.RetrieveItem(item));
        }
    }

    private void SetupLoadout(GameObject instance)
    {
        var combat = instance.GetComponentInChildren<CassidyCombat>();
        combat.ClearLoadout();
        var items = instance.GetComponentInChildren<CassidyInventorySystem>().GetCurrentItems();
        foreach(string item in loadout)
        {
            combat.AddToLoadout(items.FirstOrDefault(i => i.name == item));
        }
    }

    private IEnumerator SpawnCassidy(GameObject cassidy)
    {
        yield return new WaitForSeconds(0.5f);
        var cassidyInstance = SceneHandler.Instance.InstantiateInPersistentScene(cassidy);
        cassidyInstance.transform.position = new Vector2(x, y);
        FillInventory(cassidyInstance);
        SetupLoadout(cassidyInstance);
        GameState.Instance.SetState(GameState.State.PLAY);
    }
}
