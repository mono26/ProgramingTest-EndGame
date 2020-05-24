using UnityEngine;

public class BillboardTransform : MonoBehaviour
{
    [SerializeField]
    private Transform cameraToBillboard = null;

    private void LateUpdate()
    {
        transform.rotation = cameraToBillboard.transform.rotation;
    }
}
