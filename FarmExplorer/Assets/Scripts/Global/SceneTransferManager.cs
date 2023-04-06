using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransferManager
{
    public static void LoadLevel(string sceneName, Vector3 vehiclePosition, Sprite selectedVehicleSprite)
    {
        SceneTransferDataStore.SetVehicleLocation(vehiclePosition);
        SceneTransferDataStore.SetSelectedVehicleSprite(selectedVehicleSprite);
        SceneManager.LoadScene(sceneName);
    }

    public static void ReturnToMapScene()
    {
        SceneManager.LoadScene(0);
    }
}
