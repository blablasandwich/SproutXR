using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileEntry : MonoBehaviour 
{
    public InputField InputField { get { return inputField; } }
    public InputField customInputField;
    private InputField inputField;

	private void Awake()
	{
        if(customInputField)
        {
            inputField = customInputField;
        }
        else inputField = transform.GetChild(0).GetComponent<InputField>();
	}

	private void Start()
	{
		if(inputField)
            inputField.text = "";
        
	}
}
