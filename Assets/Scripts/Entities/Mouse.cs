using Data;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Mouse : Entity
    {
        protected override void Hit(Transform hitter)
        {
            base.Hit(hitter);
            Die();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.name == "MiceCryingZone")
            {
                var angle = MathUtils.DirectionToAngle(transform.position - collision.transform.position);
                AngleDeg = angle + Random.Range(-90, 90);
            }
        }
    }
}