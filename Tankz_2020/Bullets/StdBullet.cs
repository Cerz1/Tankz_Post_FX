using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class StdBullet : Bullet
    {
        public StdBullet() : base("stdBullet")
        {
            Type = BulletType.StdBullet;
            RigidBody.Collider = ColliderFactory.CreateCircleFor(this);
            RigidBody.Type = RigidBodyType.PlayerBullet;
            RigidBody.IsGravityAffected = true;
            RigidBody.AddCollisionType(RigidBodyType.Player | RigidBodyType.Tile);

            shootSound = new SoundEmitter(this, "shoot");
            components.Add(ComponentType.SoundEmitter, shootSound);
        }

        
    }
}
