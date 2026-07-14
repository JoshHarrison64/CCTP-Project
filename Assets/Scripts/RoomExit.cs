using UnityEngine;

public enum RoomSocketDirection
{
    North,
    South,
    East,
    West
}

public class RoomExit : MonoBehaviour
{
    [SerializeField] private RoomSocketDirection exitType;

    private int roomDepth; // how many iterations of the generation has taken place, used to limit the size of the generated map
    private bool hasGeneratedRoom;

    public RoomSocketDirection ExitType => exitType;

    public void Initialize(int depth)
    {
        roomDepth = depth;
    }

    void Start()
    {
        CreateRoom(roomDepth);
    }

    void CreateRoom(int roomDepth)
    {
        if (RoomGenerator.Instance == null)
        {
            return;
        }

        if (hasGeneratedRoom)
        {
            return;
        }

        hasGeneratedRoom = true;
        RoomGenerator.Instance.CreateRoom(this, roomDepth + 1);
    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.forward * 1.5f, 0.25f);
        Gizmos.DrawLine(Vector3.zero, Vector3.right * 0.75f);

        Gizmos.matrix = Matrix4x4.identity;
    }
}
