using System;
using UnityEngine;

public class TopDownCharacterMover : MonoBehaviour
{

    private inputHandler _input;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        _input = GetComponent<inputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        MoveTowardTarget(targetVector);
    }

    private void MoveTowardTarget(Vector3 targetVector)
    {
        throw new NotImplementedException();
    }
}
