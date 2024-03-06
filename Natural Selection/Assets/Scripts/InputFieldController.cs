using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldController : MonoBehaviour
{
    TMP_InputField inputField;

    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValidateInput += delegate (string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
    }

    private char MyValidate(char addedChar)
    {
        if (!char.IsDigit(addedChar))
        {
            addedChar = '\0'; 
        }
        return addedChar;
    }
}
