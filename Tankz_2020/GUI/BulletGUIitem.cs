using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class BulletGUIitem : GUIitem
    {
        protected int numBullets;
        protected TextObject numBulletsTxt;
        protected bool isInfinite;

        public bool IsAvailable { get; set; }

        public bool IsInfinite
        {
            get { return isInfinite; }
            set {
                isInfinite = value;
                numBulletsTxt.IsActive = !isInfinite;
            }
        }

        public int NumBullets
        {
            get
            {
                return numBullets;
            }
            set
            {
                int oldValue = numBullets;
                numBullets = value;
                if (numBullets <= 0)
                {
                    IsAvailable = false;
                    SetColor(new Vector4(1.0f, 0, 0, 0.4f));
                    numBulletsTxt.IsActive = false;
                }
                else
                {//bullets available

                    numBulletsTxt.Text = numBullets.ToString();

                    if (oldValue <= 0)
                    {
                        IsAvailable = true;
                        numBulletsTxt.IsActive = true;
                        SetColor(Vector4.One);
                    }
                }
            }
        }

        public BulletGUIitem(Vector2 position, string textureName, int numBullets, GameObject itemOwner, float w = 0, float h = 0) : base(position, textureName, itemOwner, w, h)
        {
            numBulletsTxt = new TextObject(new Vector2(position.X - 0.1f, position.Y), numBullets.ToString());
            NumBullets = numBullets;
            IsActive = true;
        }

        public void DecremenBullets()
        {
            NumBullets--;
        }
    }
}
