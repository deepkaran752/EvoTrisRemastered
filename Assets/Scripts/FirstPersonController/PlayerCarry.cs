using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [SerializeField] Transform carryingPoint;
    private GrabObject carriedObj;
    private float throwSpeed = 2f;

    public bool IsCarrying => carriedObj != null;
    public void CarryObject(GrabObject gObj)
    {
        carriedObj = gObj;

        carriedObj.transform.SetParent(carryingPoint);
        carriedObj.transform.localPosition = Vector3.zero;
        carriedObj.transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        if (carriedObj == null)
            return;

        carriedObj.transform.SetParent(null);
        GrabObject obj = carriedObj;
        carriedObj = null;

        CoroutineUtility.While(
            () =>
            {
                obj.transform.position += Vector3.down * throwSpeed * Time.deltaTime;
            },
            () => !Physics.Raycast(obj.transform.position, Vector3.down, 0.05f)
        );
    }

    public void Drop(Transform destinationPosition)
    {
        if (carriedObj == null)
            return;

        carriedObj.transform.SetParent(null);
        GrabObject obj = carriedObj;
        carriedObj = null;

        CoroutineUtility.InvokeAfter(
            () =>
            {
                obj.transform.position = destinationPosition.position;
                obj.transform.rotation = Quaternion.identity;
                obj.GetSetCarryState = babbarversestudios.CarryState.Carried;
            },
            0.5f
        );

    }
}
