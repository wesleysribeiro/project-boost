using System;
using UnityEngine;

// Implements the oscillation between a starting point and final point in a given direction
// The final position is calculated by raycasting a vector with the given direction
public class OscillationBehavior : MonoBehaviour
{
    enum Direction {Up, Down, Left, Right};

    [SerializeField] Direction direction = Direction.Down;
    [SerializeField] float periodInSeconds = 50f;

    Vector3 startingPosition;

    Vector3 finalPosition;

    Vector3 directionVec;

    Vector3 DirectionToVector3(Direction dir)
    {
        return dir switch
        {
            Direction.Up => Vector3.up,
            Direction.Down => Vector3.down,
            Direction.Left => Vector3.left,
            Direction.Right => Vector3.right,
            _ => new Vector3(0, 0, 0),
        };
    }

    void Start()
    {
        startingPosition = transform.position;

        directionVec = DirectionToVector3(direction);

        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, directionVec, out hitInfo, Mathf.Infinity ))
        {
            finalPosition = hitInfo.point;
            Debug.DrawLine(startingPosition, finalPosition, Color.green, 100);
            Debug.Log(finalPosition);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 oscilationDir = finalPosition - startingPosition;
        Vector3 sinWaveOutput = oscilationDir * MathF.Sin(2 * MathF.PI * 1/periodInSeconds * Time.time);
        Vector3 boundedSinWaveOutput = (sinWaveOutput + oscilationDir) / 2; // Limiting it from 0 to maxDistance
        // TODO: Maybe we should consider the object extents ?
        transform.position = startingPosition + boundedSinWaveOutput;
    }
}
