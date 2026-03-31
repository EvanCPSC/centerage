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
        if (command == "g" || command == "give" || command == "giveitem")
        {
            GiveItem(value);
        } else if (command == "clear" || command == "cl") {
            commands.text = "::Debug Console::";
        } else
        {
            InvalidCommand(input);
        }
    }

    public void OutputCommand(string input)
    {
        commands.text += $"\n{input}";
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

}