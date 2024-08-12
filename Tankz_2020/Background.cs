using OpenTK;
using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class Background : IDrawable
    {
        protected Sprite[] playgroundsArray;
        protected Texture playgroundTexture;

        protected Texture[] textures;
        protected Sprite[] sprites;

        int numRepetitions = 4;
        int numBgLayers = 2;

        public DrawLayer Layer { get; protected set; }

        public Background()
        {
            Layer = DrawLayer.Background;

            playgroundTexture = new Texture("Assets/bg_playground.png");

            playgroundsArray = new Sprite[numRepetitions];
            float playgroundPosY = 0.2f;

            for (int i = 0; i < numRepetitions; i++)
            {
                playgroundsArray[i] = new Sprite(Game.PixelsToUnits(playgroundTexture.Width), Game.PixelsToUnits(playgroundTexture.Height));
                playgroundsArray[i].position.Y = playgroundPosY;
                playgroundsArray[i].position.X = i*playgroundsArray[i].Width;
            }

            textures = new Texture[numBgLayers];
            sprites = new Sprite[numBgLayers * numRepetitions];

            float[] positions = new float[] { -0.1f, -5.5f };

            for (int i = 0; i < numBgLayers; i++)
            {//textures iteration
                textures[i] = new Texture($"Assets/bg_{i}.png");

                for (int r = 0; r < numRepetitions; r++)
                {//create sprites
                    int spriteIndex = numRepetitions * i + r;
                    sprites[spriteIndex] = new Sprite(Game.PixelsToUnits(textures[i].Width), Game.PixelsToUnits(textures[i].Height));
                    sprites[spriteIndex].position.Y = positions[i];
                    sprites[spriteIndex].position.X = r * sprites[spriteIndex].Width;

                    sprites[spriteIndex].Camera = CameraMgr.GetCamera($"Bg_{i}");
                }
            }

            DrawMgr.AddItem(this);
        }


        public void Draw()
        {

            for (int i = numBgLayers - 1; i >= 0; i--)
            {//textures iteration
                for (int j = 0; j <numRepetitions; j++)
                {//sprites iteration
                    int spriteIndex = numRepetitions * i + j;
                    sprites[spriteIndex].DrawTexture(textures[i]); 
                }
            }

            for (int i = 0; i < numRepetitions; i++)
            {
                playgroundsArray[i].DrawTexture(playgroundTexture);
            }

        }
    }
}
