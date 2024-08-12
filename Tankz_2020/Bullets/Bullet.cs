using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Audio;

namespace Tankz_2020
{
    enum BulletType { StdBullet, Rocket, LAST}

    abstract class Bullet : GameObject
    {
        protected float speed = 15;
        protected int damage = 25;
        protected SoundEmitter shootSound;

        public BulletType Type { get; protected set; }

        public Bullet(string textureName, float w = 0, float h = 0) : base(textureName, DrawLayer.Middleground, w, h)
        {
            RigidBody = new RigidBody(this);
        }

        public virtual void Shoot(Vector2 startPosition, Vector2 direction)
        {
            Position = startPosition;
            RigidBody.Velocity = direction * speed;
            shootSound.Play(direction.Length);
        }

        public override void Update()
        {
            if (IsActive)
            {
                Vector2 centerDist = Position - CameraMgr.MainCamera.position;
                if (centerDist.LengthSquared > CameraMgr.HalfDiagonalSquared)
                {
                    BulletsMgr.RestoreBullet(this);
                }
                else
                {
                    if (RigidBody.Velocity != Vector2.Zero)
                    {
                        Forward = RigidBody.Velocity;
                    }
                }
            }
        }

        public override void OnCollide(Collision collisionInfo)
        {
            if (collisionInfo.Collider is Player)
            {
                ((Player)collisionInfo.Collider).AddDamage(damage);
            }
            BulletsMgr.RestoreBullet(this);
            Explosion1 exp = (Explosion1)GfxMgr.GetSpecialFX(SpecialFX.Explosion_1);
            if (exp != null)
            {
                exp.Position = this.Position;
                exp.Play();
            }
        }

        public virtual void Reset()
        {

        }
    }
}
