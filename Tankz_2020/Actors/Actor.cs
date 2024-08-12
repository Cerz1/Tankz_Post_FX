using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Tankz_2020
{
    abstract class Actor : Groundable
    {
        protected BulletType bulletType;
        protected int energy;

        public int Energy { get { return energy; } protected set { energy = value; energyBar.Scale((float)energy / (float)MaxEnergy); } }
        public int MaxEnergy { get; protected set; }

        public Bullet LastShotBullet { get; protected set; }

        protected float nextShoot;

        protected ProgressBar energyBar;


        protected Actor(string textureName, DrawLayer layer = DrawLayer.Playground, float w = 0, float h = 0)
            : base(textureName, layer, w, h)
        {
            float unitDist = Game.PixelsToUnits(4);
            energyBar = new ProgressBar("barFrame", "blueBar", new Vector2(unitDist));
            energyBar.IsActive = true;
            energy = MaxEnergy = 100;
            //energyBar.Position = new Vector2(Position.X - energyBar.HalfWidth, Position.Y - HalfHeight - energyBar.HalfHeight * 3);

        }

        protected virtual void Shoot(Vector2 direction, Vector2 position)
        {
            Bullet b = BulletsMgr.GetBullet(bulletType);

            if (b != null)
            {
                b.IsActive = true;
                position += direction.Normalized() * b.HalfWidth;
                b.Shoot(position, direction);
            }
            LastShotBullet = b;
        }

        public virtual void AddDamage(int dmg)
        {
            Energy -= dmg;
            if (Energy <= 0)
            {
                Energy = 0;
                OnDie();
            }
        }

        public virtual void AddEnergy(int amount)
        {
            Energy = Math.Min(Energy + amount, MaxEnergy);
        }

        public abstract void OnDie();
    }
}
