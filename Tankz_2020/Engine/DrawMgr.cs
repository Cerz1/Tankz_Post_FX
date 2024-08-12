using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;

namespace Tankz_2020
{
    enum DrawLayer { Background, Middleground, Playground, Foreground, GUI, Last }

    static class DrawMgr
    {
        private static List<IDrawable>[] items;
        private static RenderTexture sceneTexture;
        private static Sprite scene;

        static DrawMgr()
        {
            items = new List<IDrawable>[(int)DrawLayer.Last];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new List<IDrawable>();
            }

            sceneTexture = new RenderTexture(Game.Window.Width, Game.Window.Height);
            scene = new Sprite(Game.Window.OrthoWidth, Game.Window.OrthoHeight);
            scene.Camera = CameraMgr.GetCamera("GUI");
        }

        public static void AddItem(IDrawable item)
        {
            items[(int)item.Layer].Add(item);
        }

        public static void RemoveItem(IDrawable item)
        {
            items[(int)item.Layer].Remove(item);
        }

        public static void ClearAll()
        {
            for (int i = 0; i < items.Length; i++)
            {
                items[i].Clear();
            }
        }

        public static void Draw()
        {
            Game.Window.RenderTo(sceneTexture); // start rendering on render texture

            //update all items
            for (int i = 0; i < items.Length; i++)
            {
                if ((DrawLayer)i == DrawLayer.GUI)
                {
                    Game.Window.RenderTo(null); //start rendering on screen
                    scene.DrawTexture(sceneTexture);
                }

                for (int j = 0; j < items[i].Count; j++)
                {
                    items[i][j].Draw();
                }
            }
        }
    }
}
