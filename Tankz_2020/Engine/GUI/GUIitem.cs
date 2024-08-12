using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class GUIitem : GameObject
    {
        public bool IsSelected { get; set; }

        protected GameObject owner;
        protected Vector2 offset;

        public GUIitem(Vector2 position, string textureName, GameObject itemOwner, float w = 0, float h = 0) : base(textureName, DrawLayer.GUI, w, h)
        {
            owner = itemOwner;
            sprite.position = position;
            sprite.Camera = CameraMgr.GetCamera("GUI");
            offset = position - owner.Position;
        }

        public void SetColor(Vector4 color)
        {
            sprite.SetMultiplyTint(color);
        }
    }
}
