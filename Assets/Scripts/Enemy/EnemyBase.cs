using UnityEngine;

namespace Enemy
{
    public class EnemyBase
        : MonoBehaviour
    {
        private float _health;
        private float _damage;
        private float _speed;

        public void Initialize(EnemyData enemyData)
        {
            _health = enemyData.health;
            _damage = enemyData.damage;
            _speed = enemyData.speed;
        }
    }
}