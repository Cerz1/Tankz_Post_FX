using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Tankz_2020
{
    abstract class Tank : Actor
    {
        protected Sprite body;
        protected Sprite cannon;

        protected Texture bodyTexture;
        protected Texture cannonTexture;

        protected Vector2 bodyOffset;
        protected Vector2 cannonOffset;

        protected float maxAngle = (float)-Math.PI;

        protected float moveAccumulator;
        protected Vector2 shakeOffset;

        public float CannonLength { get { return cannon.Width; } }


        public Tank() : base("tracks")
        {
            bodyTexture = GfxMgr.GetTexture("body");
            cannonTexture = GfxMgr.GetTexture("cannon");

            body = new Sprite(Game.PixelsToUnits(bodyTexture.Width), Game.PixelsToUnits(bodyTexture.Height));
            cannon = new Sprite(Game.PixelsToUnits(cannonTexture.Width), Game.PixelsToUnits(cannonTexture.Height));

            body.pivot = new Vector2(body.Width * 0.5f, body.Height * 0.5f);
            cannon.pivot = new Vector2(0, cannon.Height * 0.5f);

            bodyOffset = new Vector2(Game.PixelsToUnits(2), -body.Height * 0.40f);
            cannonOffset = new Vector2(0, -body.Height * 0.4f);

            //RigidBody.AddCollisionType(RigidBodyType.Player | RigidBodyType.PlayerBullet);
            moveAccumulator = 0;
        }

        protected virtual void Shoot(float speedPercentage=1.0f)
        {
            Vector2 direction = new Vector2((float)Math.Cos(cannon.Rotation), (float)Math.Sin(cannon.Rotation));
            Vector2 bulletPosition = cannon.position + direction * CannonLength;

            Shoot(direction * speedPercentage, bulletPosition);

        }

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();

                if (RigidBody.Velocity.X != 0)
                {
                    moveAccumulator += Game.DeltaTime*10;
                    shakeOffset.Y = (float)Math.Sin(moveAccumulator) * 0.01f;
                    shakeOffset.X = (float)Math.Cos(moveAccumulator) * 0.01f;
                }

                body.position = sprite.position + bodyOffset + shakeOffset;//tracks + offset
                cannon.position = body.position + cannonOffset;
            }
        }

        public override void Draw()
        {
            if (IsActive)
            {
                cannon.DrawTexture(cannonTexture);
                sprite.DrawTexture(texture);//tracks
                body.DrawTexture(bodyTexture);
            }
        }
    }
}
