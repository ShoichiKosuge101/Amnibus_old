using UnityEngine;

namespace Player
{
    public class PlayerController
         : MonoBehaviour
    {
        public float moveSpeed = 5.0f;
        
        private void Update()
        {
            var moveX = Input.GetAxis("Horizontal");
            var moveY = Input.GetAxis("Vertical");
            var move = new Vector3(moveX, moveY, 0) * moveSpeed * Time.deltaTime;
            transform.position += move;
        }
    }
}