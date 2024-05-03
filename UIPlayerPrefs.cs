// This script updates a TextMeshProUGUI component with the value stored in PlayerPrefs based on the specified text name.

using UnityEngine;
using TMPro;

public class UIPlayerPrefs : MonoBehaviour
{
    public string textName;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        textMeshPro.text = PlayerPrefs.GetInt(textName).ToString();
    }
}
