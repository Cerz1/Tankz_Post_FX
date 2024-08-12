using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class WeaponsGUI : GameObject
    {
        protected BulletGUIitem[] weapons;
        protected string[] textureNames = { "bullet_ico", "missile_ico" };

        protected int selectedWeapon;
        protected Sprite selection;
        protected Texture selectionTexture;
        protected float itemWidth;

        public int SelectedWeapon
        {
            get { return selectedWeapon; }
            protected set
            {
                selectedWeapon = value;
                selection.position = weapons[selectedWeapon].Position;
            }
        }

        public WeaponsGUI(Vector2 position, float w = 0, float h = 0) : base("weapons_GUI", DrawLayer.GUI, w, h)
        {
            sprite.pivot = Vector2.Zero;
            sprite.position = position;
            sprite.Camera = CameraMgr.GetCamera("GUI");
            weapons = new BulletGUIitem[textureNames.Length];

            itemWidth = Game.PixelsToUnits(32);

            float itemPosY = position.Y + HalfHeight;
            float firstItemPosX = Game.PixelsToUnits(7);
            float itemsHorizontalDistance = Game.PixelsToUnits(7);

            for (int i = 0; i < weapons.Length; i++)
            {
                weapons[i] = new BulletGUIitem(new Vector2(position.X + itemsHorizontalDistance + itemWidth * 0.5f + i * (itemWidth), itemPosY), textureNames[i], 2, this);
            }

            //default weapon config
            weapons[0].IsSelected = true;
            weapons[0].IsAvailable = true;
            weapons[0].IsInfinite = true;

            selectionTexture = GfxMgr.GetTexture("weapon_selection");
            selection = new Sprite(itemWidth, itemWidth);//selection has icon same size
            selection.pivot = new Vector2(selection.Width * 0.5f);
            SelectedWeapon = 0;
            selection.Camera = CameraMgr.GetCamera("GUI");
        }

        public override void Draw()
        {
            if (IsActive)
            {
                base.Draw();
                selection.DrawTexture(selectionTexture);
            }
        }

        public BulletType NextWeapon(int direction = 1)
        {
            int currentWeapon = selectedWeapon;

            do
            {
                selectedWeapon += direction;
                if (selectedWeapon >= weapons.Length)
                {
                    selectedWeapon = 0;
                }
                else if (selectedWeapon < 0)
                {
                    selectedWeapon = weapons.Length - 1;
                }

            } while (!weapons[selectedWeapon].IsAvailable && selectedWeapon!=currentWeapon);

            SelectedWeapon = selectedWeapon;

            return (BulletType)selectedWeapon;
        }

        public BulletType DecrementsBullets()
        {
            if (!weapons[selectedWeapon].IsInfinite)
            {
                weapons[selectedWeapon].DecremenBullets();

                if (!weapons[selectedWeapon].IsAvailable)
                {
                   NextWeapon();
                }
            }

            return (BulletType)selectedWeapon;
        }

        public void AddBullets(BulletType type, int amount)
        {
            weapons[(int)type].NumBullets += amount;
        }
    }
}
