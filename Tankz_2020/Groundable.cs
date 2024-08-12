using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    abstract class Groundable : GameObject
    {
        public bool IsGrounded
        {
            get { return !RigidBody.IsGravityAffected; }
            set { RigidBody.IsGravityAffected = !value; }
        }

        public Groundable(string textureName, DrawLayer layer = DrawLayer.Playground, float w = 0, float h = 0) : base(textureName, layer, w, h)
        {
            RigidBody = new RigidBody(this);
        }

        public virtual void OnGrounded()
        {
            IsGrounded = true;
            RigidBody.Velocity.Y = 0;
        }

        public virtual void OnGroundableCollide()
        {

        }

        public override void OnCollide(Collision collisionInfo)
        {
            if(collisionInfo.Collider is Groundable)
            {
                if (collisionInfo.Delta.X < collisionInfo.Delta.Y)
                {
                    //horizontal collision
                    if (X < collisionInfo.Collider.X)
                    {
                        //collision from left
                        collisionInfo.Delta.X = -collisionInfo.Delta.X;
                    }
                    X += collisionInfo.Delta.X;
                    RigidBody.Velocity.X = 0;

                }
                else
                {
                    //vertical collision
                    if (!IsGrounded && ((Groundable)collisionInfo.Collider).IsGrounded)
                    {
                        //RigidBody.Velocity.Y = 0;
                        if (Y < collisionInfo.Collider.Y)
                        {//collision from top
                            collisionInfo.Delta.Y = -collisionInfo.Delta.Y;
                            OnGrounded();
                            Y += collisionInfo.Delta.Y;
                        }
                    }

                }
            }
        }

        public override void Update()
        {
            if (IsActive)
            {
                IsGrounded = false;

                //if (!IsGrounded)
                //{
                    float groundY = ((PlayScene)Game.CurrentScene).GroundY;
                    if (Position.Y + HalfHeight > groundY)
                    {
                        Y = groundY - HalfHeight;
                        OnGrounded();
                    }
                //}
                //else
                //{
                //    //IsGrounded = false;
                //}
            }
        }

    }
}
