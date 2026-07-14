using UnityEngine;

public class RoomEntrance : MonoBehaviour
{
    [SerializeField] private RoomSocketDirection entranceType;

    public RoomSocketDirection EntranceType => entranceType;

    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(Vector3.forward * 1.5f, 0.25f);
        Gizmos.DrawLine(Vector3.zero, Vector3.right * 0.75f);

        Gizmos.matrix = Matrix4x4.identity;
    }
}
