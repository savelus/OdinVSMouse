using Assets.Scripts.Utils;
using System.Collections;
using Data;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Mouse : Entity
    {
        protected override void Hit(Transform hitter)
        {
            Destroy(gameObject);
            StaticGameData.KilledMouseInGame++;
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