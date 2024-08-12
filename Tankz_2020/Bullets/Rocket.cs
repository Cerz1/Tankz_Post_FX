using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class Rocket : Bullet
    {
        protected bool engineIsOn;
        protected float startEngineAngle;
        protected SoundEmitter engineStart;

        public Rocket() : base("rocketBullet")
        {
            Type = BulletType.Rocket;
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
            RigidBody.Type = RigidBodyType.PlayerBullet;
            RigidBody.IsGravityAffected = true;
            RigidBody.AddCollisionType(RigidBodyType.Player | RigidBodyType.Tile);

            damage = 30;
            startEngineAngle= -0.174533f;//10 deg


            shootSound = new SoundEmitter(this, "whistle");
            components.Add(ComponentType.SoundEmitter, shootSound);

            engineStart = new SoundEmitter(this, "engineStart");
        }

        public override void Reset()
        {
            engineIsOn = false;
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
            {
                if (!engineIsOn && (sprite.Rotation > startEngineAngle || sprite.Rotation < -Math.PI -startEngineAngle))
                {
                    engineIsOn = true;
                    RigidBody.Velocity.X = (18 * Math.Sign(RigidBody.Velocity.X));
                    engineStart.Play();
                }
            }
        }
    }
}
