using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float swerveRange;
    
    void Update()
    {
        MovePlayer();
    }
    
    private void MovePlayer()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float moveDirection = touch.deltaPosition.x;

                var localPosition = transform.localPosition;
                float newX = Mathf.Clamp(localPosition.x + moveDirection * speed * Time.deltaTime, -swerveRange, swerveRange);
                localPosition = new Vector3(newX, localPosition.y, localPosition.z);
                transform.localPosition = localPosition;
            }
        }

    }
}
