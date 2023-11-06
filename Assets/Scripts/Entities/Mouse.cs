using Data;
using UnityEngine;
using Utils;

namespace Entities
{
    public class Mouse : Entity
    {
        [SerializeField]
        private float timeForKill;

        protected override void Hit(Transform hitter)
        {
            base.Hit(hitter);
            Die();
        }

        protected override void OnOutOfField()
        {
            base.OnOutOfField();

            if (!CanBeCaptured)
            {
                GameManager.Singleton.Timer.RemainingTime += timeForKill;
                StaticGameData.KilledMouseInGame++;
            }
        }

        public override void Die()
        {
            if (timeForKill > 0)
            {
                GameManager.Singleton.Timer.RemainingTime += timeForKill;
                GameManager.Singleton.EffectManager.DrawAddIndicator("+" + timeForKill, transform.position);
            }

            base.Die();
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

        protected override void OnUpdate()
        {
            base.OnUpdate();

            TryFlip();
        }

        protected override void OnAngleChanged()
        {
            base.OnAngleChanged();

            Flip();
        }
    }
}