using UnityEngine;

public class VehicleSelector : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button button1;
    [SerializeField] UnityEngine.UI.Button button2;
    [SerializeField] Canvas canvasToDisableOnGameStart;

    private void Awake()
    {
        if (SceneTransferDataStore.GetHideCanvasForVehicleSelection()) DisableCanvas();
    }
    public void ChooseVehicleButton1()
    {
        SetVehicleProperties(button1.image.sprite);
    }
    public void ChooseVehicleButton2()
    {
        SetVehicleProperties(button2.image.sprite);
    }

    private void SetVehicleProperties(Sprite sprite)
    {
        SceneTransferDataStore.StoreSelectedVehicleData(sprite);
        Vehicle.Instance.ChangeVehicleSprite();
        DisableCanvas();
    }

    private void DisableCanvas()
    {
        canvasToDisableOnGameStart.gameObject.SetActive(false);
    }
}
