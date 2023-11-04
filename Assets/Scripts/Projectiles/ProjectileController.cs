using Assets.Scripts.Utils;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Assets.Scripts.Player;

namespace Assets.Scripts.Projectiles
{
    public class ProjectileController : MonoBehaviour
    {
        [field: SerializeField]
        public float AttackCooldown { get; private set; }
        private float lastAttackTime;

        [SerializeField]
        private GameObject lightningProjectile;

        [SerializeField]
        private Player.Player player;

        private void Update()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.Left))
            {
                if (Time.timeSinceLevelLoad - lastAttackTime > AttackCooldown)
                {
                    lastAttackTime = Time.timeSinceLevelLoad;

                    player.Shake();

                    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var targetPos = mousePos + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                    var originPos = player.GetRndAttackPoint();
                    var angle = MathUtils.DirectionToAngle(targetPos - originPos);
                    var distanceToMouse = Vector2.Distance(targetPos, originPos);
                    SpawnProjectile(originPos, angle, distanceToMouse + Random.Range(0.5f, 1.5f));
                }
            }
        }

        private void SpawnProjectile(Vector3 pos, float angleDeg, float liveDistance)
        {
            var projectile = Instantiate(lightningProjectile, pos, transform.rotation, transform).GetComponent<Projectile>();
            projectile.AngleDeg = angleDeg;
            projectile.LiveDistance = liveDistance;
        }
    }
}