using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IAPProductDisplay : MonoBehaviour 
{
    [Header("UI References")]
    [SerializeField] private Text description = null;
	[SerializeField] private Button purchaseButton;

    
	private void Awake()
	{
        if (description) description.text = "";
		if (purchaseButton) 
		{
			purchaseButton.onClick.RemoveAllListeners();
			purchaseButton.onClick.AddListener(PurchaseProduct);
		}
	}

    #region Event Subscriptions
    private void OnEnable()
    {

    }

    private void OnDisable()
    {
       
    }
    #endregion

    // Initialize the product and display proper details
	

	void Update()
	{
		if (purchaseButton)
		{
			purchaseButton.gameObject.GetComponentInChildren<Text> ().color = (LocalUserData.IsLoggedIn () && LocalUserData.IsSubActive() == false) ? Color.white : Color.grey;
			purchaseButton.interactable = (LocalUserData.IsLoggedIn () && LocalUserData.IsSubActive() == false);
		}
	}


    // Purhcase the product
    void PurchaseProduct()
    {
       
    }

  

}
