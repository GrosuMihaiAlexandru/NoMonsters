using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterUnlocker : MonoBehaviour
{
    
    public GameObject scrollPanel;

    // The shown images of every character
    public List<Image> charactersDisplay;

    // List of unlocked character sprites
    public List<Sprite> unlockedCharacters;
    // List of locked character sprites
    public List<Sprite> lockedCharacters;
    public List<Cost> characterCosts = new List<Cost>();

    public Button button;

    public Text costText;

    public Image currencyIcon;

    public Sprite fishIcon;
    public Sprite gFishIcon;
    public Sprite specialCurencyIcon;

    private ScrollRectSnap rectSnap;

    private int lastSelectedCharacter;
    private int selectedCharacter;

    public Sprite playButtonSprite;
    public Sprite unlockButtonSprite;

    public MainScreenManager screenManager;

    // Start is called before the first frame update
    void Start()
    {
        rectSnap = GetComponent<ScrollRectSnap>();
        // setting costs
        characterCosts.Add(new Cost(Cost.Type.fish, 0));
        characterCosts.Add(new Cost(Cost.Type.fish, 1000));
        characterCosts.Add(new Cost(Cost.Type.specialCurrency, 300));
        characterCosts.Add(new Cost(Cost.Type.fish, 5000));
        characterCosts.Add(new Cost(Cost.Type.gFish, 100));
        characterCosts.Add(new Cost(Cost.Type.fish, 10000));
        characterCosts.Add(new Cost(Cost.Type.gFish, 250));

        foreach (Transform t in scrollPanel.transform)
        {
            charactersDisplay.Add(t.GetComponent<Image>());
        }

        // reading saved data
        UpdateCharacters();

         lastSelectedCharacter = -1;
    }

    private void UpdateCharacters()
    {
        for (int i = 0; i < charactersDisplay.Count; i++)
        {
            if (GameManager.instance.GetCharacterUnlockStatus(i))
            {
                charactersDisplay[i].sprite = unlockedCharacters[i];
            }
            else
            {
                charactersDisplay[i].sprite = lockedCharacters[i];
            }
        }
    }

    public void BuyCharacter()
    {
        int character = GameManager.instance.selectedCharacter;
    }

    private void Update()
    {
        selectedCharacter = rectSnap.MinImageNum;
        GameManager.instance.selectedCharacter = selectedCharacter;
        if (lastSelectedCharacter == selectedCharacter)
        {
            return;
        }

        UpdateCharacterSelectUI();
        
        lastSelectedCharacter = selectedCharacter;
    }

    private void UpdateCharacterSelectUI()
    {
        Debug.Log(selectedCharacter);
        if (GameManager.instance.GetCharacterUnlockStatus(selectedCharacter))
        {
            button.image.sprite = playButtonSprite;
            costText.gameObject.SetActive(false);

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(MainScreenManager.PlayGame);
            button.interactable = true;
        }
        else // character locked
        {
            button.image.sprite = unlockButtonSprite;
            costText.gameObject.SetActive(true);


            switch (characterCosts[selectedCharacter].GetCurrency())
            {
                case Cost.Type.fish:
                    currencyIcon.sprite = fishIcon;
                    if (GameManager.instance.Fish < characterCosts[selectedCharacter].GetCost())
                    {
                        costText.text = GameManager.instance.Fish + "/";
                        button.interactable = false;
                    }
                    else
                    {
                        costText.text = characterCosts[selectedCharacter].GetCost() + "/";
                        button.interactable = true;
                    }
                    break;
                case Cost.Type.gFish:
                    currencyIcon.sprite = gFishIcon;
                    if (GameManager.instance.GFish < characterCosts[selectedCharacter].GetCost())
                    {
                        costText.text = GameManager.instance.GFish + "/";
                        button.interactable = false;
                    }
                    else
                    {
                        costText.text = characterCosts[selectedCharacter].GetCost() + "/";
                        button.interactable = true;
                    }
                    break;
                case Cost.Type.specialCurrency:
                    currencyIcon.sprite = specialCurencyIcon;
                    if (GameManager.instance.Candy < characterCosts[selectedCharacter].GetCost())
                    {
                        costText.text = GameManager.instance.Candy + "/";
                        button.interactable = false;
                    }
                    else
                    {
                        costText.text = characterCosts[selectedCharacter].GetCost() + "/";
                        button.interactable = true;
                    }
                    break;
            }
            costText.text += characterCosts[selectedCharacter].GetCost().ToString();

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(UnlockMethod);
        }
    }

    private void UnlockMethod()
    {
        switch (characterCosts[selectedCharacter].GetCurrency())
        {
            case Cost.Type.fish:
                if (GameManager.instance.Fish >= characterCosts[selectedCharacter].GetCost())
                {
                    GameManager.instance.UnlockCharacter(selectedCharacter);
                    GameManager.instance.RemoveFish(characterCosts[selectedCharacter].GetCost());
                    GameManager.instance.SaveProgress();

                    screenManager.UpdateDisplay();
                }
                break;
            case Cost.Type.gFish:
                if (GameManager.instance.GFish >= characterCosts[selectedCharacter].GetCost())
                {
                    GameManager.instance.UnlockCharacter(selectedCharacter);
                    GameManager.instance.RemoveGfish(characterCosts[selectedCharacter].GetCost());
                    GameManager.instance.SaveProgress();

                    screenManager.UpdateDisplay();
                }
                break;
            case Cost.Type.specialCurrency:
                if (GameManager.instance.Candy >= characterCosts[selectedCharacter].GetCost())
                {
                    GameManager.instance.UnlockCharacter(selectedCharacter);
                    GameManager.instance.RemoveCandy(characterCosts[selectedCharacter].GetCost());
                    GameManager.instance.SaveProgress();

                    screenManager.UpdateDisplay();
                }
                break;
        }
        UpdateCharacters();
        UpdateCharacterSelectUI();
    }
}
