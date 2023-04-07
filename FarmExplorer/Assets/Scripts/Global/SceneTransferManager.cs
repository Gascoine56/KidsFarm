using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransferManager : MonoBehaviour
{
    private const string CROSSFADE_TRIGGER = "LevelEnd";
    public static SceneTransferManager Instance { get; private set; }

    [SerializeField] private Animator crossfadeAnimator;
    [SerializeField] private float crossFadeWaitTimeSeconds = 1; 


    private void Awake()
    {
        if (Instance != null) print("There is more than one Player instance");
        Instance = this;
    }
    public void LoadLevel(string sceneName, Vector3 vehiclePosition, Sprite selectedVehicleSprite)
    {
        SceneTransferDataStore.SetVehicleLocation(vehiclePosition);
        SceneTransferDataStore.SetSelectedVehicleSprite(selectedVehicleSprite);
        StartCoroutine(CrossfadeTransition(sceneName));        
    }

    public void ReturnToMapScene()
    {
        StartCoroutine(CrossfadeTransition(GetMainMapSceneNameFromIndex()));
    }

    private IEnumerator CrossfadeTransition(string sceneName)
    {
        crossfadeAnimator.SetTrigger(CROSSFADE_TRIGGER);

        yield return new WaitForSeconds(crossFadeWaitTimeSeconds);

        SceneManager.LoadScene(sceneName);
    }

    public static string GetMainMapSceneNameFromIndex()
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(0);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }
}
