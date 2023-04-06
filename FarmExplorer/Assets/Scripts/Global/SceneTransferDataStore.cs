using UnityEngine;

public class SceneTransferDataStore : MonoBehaviour
{
    private static Vector3 vehicleLocation = new Vector3(0, 0, 0);
    private static Sprite selectedVehicleSprite;
    private static bool hideCanvasForVehicleSelection = false;

    private static bool gameStarted = false;

    public static void StoreSelectedVehicleData(Sprite sprite)
    {
        SetGameStarted(true);
        SetSelectedVehicleSprite(sprite);
        SetHideCanvasForVehicleSelection(true);
    }
    public static bool GetGameStarted()
    {
        return gameStarted;
    }

    public static void SetGameStarted(bool value)
    {
        gameStarted = value;
    }

    public static bool GetHideCanvasForVehicleSelection()
    {
        return hideCanvasForVehicleSelection;
    }
    public static void SetHideCanvasForVehicleSelection(bool value)
    {
        hideCanvasForVehicleSelection = value;
    }

    public static Sprite GetSelectedVehicleSprite()
    {
        return selectedVehicleSprite;
    }

    public static void SetSelectedVehicleSprite(Sprite sprite)
    {
        selectedVehicleSprite = sprite;
    }

    public static Vector3 GetVehicleLocation()
    {
        return vehicleLocation;
    }

    public static void SetVehicleLocation(Vector3 vehicleLocationData)
    {
        vehicleLocation = vehicleLocationData;
    }
}
