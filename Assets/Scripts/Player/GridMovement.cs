using System;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// グリッド移動
    /// </summary>
    public class GridMovement
        : MonoBehaviour
    {
        public float moveSpeed = 5.0f;
        public float gridSize = 1.0f;
        private Vector3 _targetPosition;
        
        /// <summary>
        /// プレイヤーの状態
        /// </summary>
        private PlayerState _currentState = PlayerState.Idle;

        /// <summary>
        /// 初期化
        /// </summary>
        private void Start()
        {
            // 初期位置をグリッドに合わせる
            _targetPosition = transform.position;
        }

        /// <summary>
        /// 更新
        /// </summary>
        private void Update()
        {
            switch (_currentState)
            {
                case PlayerState.Idle:
                    HandleIdleState();
                    break;
                case PlayerState.Moving:
                    HandleMovingState();
                    break;
                case PlayerState.Dead:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 移動状態の更新
        /// </summary>
        private void HandleMovingState()
        {
            // 目標地点へスムーズに移動
            transform.position = Vector3.MoveTowards(
                transform.position, 
                _targetPosition,
                moveSpeed * Time.deltaTime);
            
            // 目標地点に到達したら待機状態に遷移
            if (transform.position == _targetPosition)
            {
                transform.position = _targetPosition;
                _currentState = PlayerState.Idle;
            }
        }

        /// <summary>
        /// 待機状態の更新
        /// </summary>
        private void HandleIdleState()
        {
            // ユーザー入力に基づいて目標地点を設定
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveY = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(moveX, moveY, 0);
            
            // 入力があれば
            if (input != Vector3.zero)
            {
                // 移動方向をグリッドに合わせる
                SetTargetPosition(input);

                // 移動状態に遷移
                _currentState = PlayerState.Moving;
            }
        }

        /// <summary>
        /// 目標地点を設定
        /// </summary>
        /// <param name="input"></param>
        private void SetTargetPosition(Vector3 input)
        {
            // 移動方向をグリッドに合わせる
            Vector3 direction = input.normalized;
            _targetPosition = transform.position + direction * gridSize;
        }
    }
}