using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSlotsMenu : MonoBehaviour
{
    [Header("Menu Navigation")]
    [SerializeField] private MainMenu mainMenu;
    [Header("Confirmation Popup")]
    [SerializeField] private ConfirmationPopupMenu confirmationPopupMenu;

    private SaveSlot[] saveSlots;

    private bool isLoadingGame = false;

    public void OnBackClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

    public void OnClearClicked(SaveSlot saveSlot)
    {
        confirmationPopupMenu.ActivateMenu(
            "Are you sure you want to delete this saved data?",
            // function to execute if we select 'yes'
            () => {
                DataPersistanceManager.instance.DeleteProfileData(saveSlot.GetProfileId());
                ActivateMenu(isLoadingGame);
            },
            // function to execute if we select 'cancel'
            () => {
                ActivateMenu(isLoadingGame);
            }
        );
    }

    public void OnSaveSlotClicked(SaveSlot saveSlot)
    {
        // case - loading game
        if (isLoadingGame)
        {
            DataPersistanceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            SaveGameAndLoadScene();
        }
        // case - new game, but the save slot has data
        else if (saveSlot.hasData)
        {
            confirmationPopupMenu.ActivateMenu(
                "Starting a New Game with this slot will override the currently saved data. Are you sure?",
                // function to execute if we select 'yes'
                () => {
                    DataPersistanceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
                    DataPersistanceManager.instance.NewGame();
                    SaveGameAndLoadScene();
                },
                // function to execute if we select 'cancel'
                () => {
                    this.ActivateMenu(isLoadingGame);
                }
            );
        }
        // case new game, and the save slot has no data
        else
        {
            DataPersistanceManager.instance.ChangeSelectedProfileId(saveSlot.GetProfileId());
            DataPersistanceManager.instance.NewGame();
            SaveGameAndLoadScene();
        }
    }

    private void SaveGameAndLoadScene()
    {
        // save the game anytime before loading a new scene
        DataPersistanceManager.instance.SaveGame();
        // load the scene - which in turn saveds the game because of OnSceneUnloaded() in the DataPersistanceManager
        SceneManager.LoadSceneAsync("Training Grounds");
    }

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlot>();
    }

    public void ActivateMenu(bool isLoadingGame)
    {
        // set this menu to be active
        this.gameObject.SetActive(true);

        // set mode
        this.isLoadingGame = isLoadingGame;

        // load all of the profiles that exist
        Dictionary<string, GameData> profilesGameData = DataPersistanceManager.instance.GetAllProfilesGameData();

        // loop through each save slot in the UI and set the content appropriately
        foreach (SaveSlot saveSlot in saveSlots)
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileId(), out profileData);
            saveSlot.SetData(profileData);
            if (profileData == null && isLoadingGame)
            {
                saveSlot.SetInteractable(false);
            }
            else
            {
                saveSlot.SetInteractable(true);
            }
        }
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
