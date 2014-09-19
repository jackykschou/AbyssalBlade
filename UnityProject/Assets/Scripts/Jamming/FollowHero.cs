using UnityEngine;

public class FollowHero : MonoBehaviour
{
    public Transform target;
    public float distance = 3.0f;
    public float height = 3.0f;
    public float damping = 5.0f;
    public bool followBehind = true;

    void Update()
    {
        Vector3 wantedPosition;

        if (followBehind)
            wantedPosition = target.TransformPoint(-distance, height, 0);
        else
            wantedPosition = target.TransformPoint(distance, height, 0);

        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);
    }
}