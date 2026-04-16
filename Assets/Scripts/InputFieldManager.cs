using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldManager : MonoBehaviour
{
    public TMP_InputField debugField;
    public TextMeshProUGUI commands;

    void Start()
    {
        debugField.onEndEdit.AddListener(SubmitCommand);
    }

    public void OnEnable()
    {
        commands.text = "::Debug Console::";
        debugField.text = "";
        debugField.Select();
        debugField.ActivateInputField();
    }
    private void SubmitCommand(string input)
    {
        input.Trim();
        input.ToLower();
        string command = "";
        foreach (char c in input)
        {
            if (c == ' ')
            {
                break;
            }
            command += c;
        }
        string value = input.Substring(input.IndexOf(" ")+1);
        if (command == "`")
        {
            Debug.Log("closed console");
        }
        else if (command == "clear" || command == "cl")
        {
            commands.text = "::Debug Console::";
        }
        else if (command == "help" || command == "h")
        {
            Help();
        }
        else if (command == "giveitem" || command == "give" || command == "g")
        {
            GiveItem(value);
        }
        else if (command == "resetpickaxe" || command == "resetpick" || command == "rp")
        {
            ResetPickaxe();
        }
        else if (command == "listitems" || command == "list" || command == "ls")
        {
            ListItems();
        }
        else if (command == "resetplayer" || command == "reset" || command == "r")
        {
            ResetPlayer();
        }
        else
        {
            InvalidCommand(input);
        }

        debugField.text = "";
        debugField.Select();
        debugField.ActivateInputField();
    }

    public void OutputCommand(string input)
    {
        commands.text += $"\n: {input}";
        Debug.Log(input);
    }

    public void InvalidCommand(string input)
    {
        OutputCommand($"Invalid Command '{input}'");
    }
    public void InvalidItem(string input)
    {
        OutputCommand($"Invalid Item '{input}'");
    }

    public void Help()
    {
        commands.text += $"\n::List of Commands::";
        commands.text += $"\n: help | h => list of commands";
        commands.text += $"\n: giveitem | give | g <item name> => adds item to player item list";
        commands.text += $"\n: resetpickaxe | resetpick | rp => destroys pickaxe object";
        commands.text += $"\n: listitems | list | ls => lists currently held items";
        commands.text += $"\n: resetplayer | reset | r => sets player to default stats and items";
        Debug.Log("help wanted");
    }

    public void GiveItem(string item)
    {
        foreach (GameObject i in GameManager.Instance.items)
        {
            ItemManager itemScript = i.GetComponent<ItemManager>();
            if (itemScript.itemName.ToLower() == item)
            {
                itemScript.GiveStat();
                PlayerStats.playerItems.Add(item);

                OutputCommand($"Given Item: {item}");
                return;
            }
        }
        InvalidItem(item);
    }

    public void ListItems()
    {
        string res = "";
        foreach (string i in PlayerStats.playerItems)
        {
            res += i + ", ";
        }
        if (res.Length > 0)
        {
            res = res.Substring(0, res.Length-2);
        } else
        {
            res = "none :(";
        }
        OutputCommand($"Current Items: {res}");
    }

    public void ResetPickaxe()
    {
        GameManager.Instance.ResetPickaxe();
        OutputCommand($"Reset Pickaxe");
    }

    public void ResetPlayer()
    {
        PlayerStats.SetDefaultStats();
        OutputCommand($"Reset Player Stats and Items");
    }

}