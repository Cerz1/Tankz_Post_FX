using OpenTK;
using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class PlayScene : Scene
    {
        protected Background bg;

        protected List<Player> players;
        protected int currentPlayerIndex;

        protected int turnDuration = 16;
        protected TextObject timerTxt;
        public float PlayerTimer { get; protected set; }


        public Player CurrentPlayer { get; protected set; }
        public float GroundY { get; protected set; }


        public PlayScene()
        {
        }

        public override void Start()
        {
            LoadAssets();

            CameraMgr.Init();
            CameraMgr.CameraLimits = new CameraLimits(Game.Window.OrthoWidth * 1.1f, Game.Window.OrthoWidth * 0.5f, Game.Window.OrthoHeight * 0.06f, -1);

            CameraMgr.AddCamera("GUI", new Camera());
            CameraMgr.AddCamera("Bg_0", cameraSpeed: 0.95f);
            CameraMgr.AddCamera("Bg_1", cameraSpeed: 0.8f);

            GfxMgr.InitFX();

            bg = new Background();

            FontMgr.Init();
            Font stdFont = FontMgr.AddFont("stdFont", "Assets/textSheet.png", 15, 32, 20, 20);
            Font comic = FontMgr.AddFont("comics", "Assets/comics.png", 10, 32, 61, 65);

            timerTxt = new TextObject(new Vector2(Game.Window.OrthoWidth * 0.5f, 3), "", comic, 0);
            timerTxt.IsActive = false;

            //BulletsMgr.Init();
            //SpawnMgr.Init();
            GroundY = 5.2f;

            Tile t = new Tile("crate");
            t.Position = new Vector2(5, 2);
            t.IsActive = true;

            Tile t2 = new Tile("crate");
            t2.Position = new Vector2(5, 1);
            t2.IsActive = true;

            Tile t3 = new Tile("crate");
            t3.Position = new Vector2(5, -1);
            t3.IsActive = true;

            players = new List<Player>();

            Player Player = new Player(Game.GetController(0), 0);
            Player.Position = new Vector2(5, -1.8f);
            Player.IsActive = true;
            players.Add(Player);
            CameraMgr.SetTarget(Player);

            Player player2 = new Player(Game.GetController(1), 1);
            player2.Position = new Vector2(7, 3);
            player2.IsActive = true;
            players.Add(player2);

            CurrentPlayer = players[currentPlayerIndex];
            CurrentPlayer.Play();

            BulletsMgr.Init();

            //Tile t1 = new Tile();
            //t1.Position = new Vector2(10, 7);

            //Tile t2 = new Tile();
            //t2.Position = t1.Position;
            //t2.X += t1.HalfWidth * 2;


            Game.Window.AddPostProcessingEffect(new BlackBandFX());
            //Game.Window.AddPostProcessingEffect(new GrayScaleFX());
            //Game.Window.AddPostProcessingEffect(new SepiaFX());
            //Game.Window.AddPostProcessingEffect(new NegativeFX());
            //Game.Window.AddPostProcessingEffect(new BlurFX());
            //Game.Window.AddPostProcessingEffect(new WobbleFX());
            //Game.Window.AddPostProcessingEffect(new WobbleMouseFX());


            base.Start();
        }

        private static void LoadAssets()
        {
            GfxMgr.AddTexture("tracks", "Assets/tanks_tankTracks1.png");
            GfxMgr.AddTexture("body", "Assets/tanks_tankGreen_body1.png");
            GfxMgr.AddTexture("cannon", "Assets/tanks_turret2.png");

            GfxMgr.AddTexture("stdBullet", "Assets/tank_bullet1.png");
            GfxMgr.AddTexture("rocketBullet", "Assets/tank_bullet3.png");

            GfxMgr.AddTexture("explosion_1", "Assets/explosion.png");

            GfxMgr.AddTexture("barFrame", "Assets/loadingBar_frame.png");
            GfxMgr.AddTexture("blueBar", "Assets/loadingBar_bar.png");

            GfxMgr.AddTexture("crate", "Assets/crate.png");

            GfxMgr.AddTexture("weapon_selection", "Assets/weapon_GUI_selection.png");
            GfxMgr.AddTexture("weapons_GUI", "Assets/weapons_GUI_frame.png");

            GfxMgr.AddTexture("bullet_ico", "Assets/bullet_ico.png");
            GfxMgr.AddTexture("missile_ico", "Assets/missile_ico.png");

            GfxMgr.AddClip("shoot", "Assets/sounds/cannonShoot.wav");
            GfxMgr.AddClip("crack_1", "Assets/sounds/wood_crack_1.ogg");
            GfxMgr.AddClip("crack_2", "Assets/sounds/wood_crack_2.ogg");
            GfxMgr.AddClip("whistle", "Assets/sounds/whistle.ogg");
            GfxMgr.AddClip("engineStart", "Assets/sounds/engineStart.wav");
        }

        public override void Update()
        {
            if (timerTxt.IsActive)
            {
                PlayerTimer -= Game.DeltaTime;
                timerTxt.Text = ((int)PlayerTimer).ToString();
            }

            PhysicsMgr.Update();
            UpdateMgr.Update();
            //SpawnMgr.Update();


            //check collisions
            PhysicsMgr.CheckCollisions();

            CameraMgr.Update();
        }

        public override void Draw()
        {
            DrawMgr.Draw();
        }

        public override void Input()
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].UpdateStateMachine();
            }
        }

        public virtual void NextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            CurrentPlayer = players[currentPlayerIndex];

            CameraMgr.SetTarget(CurrentPlayer,false);
            CameraMgr.MoveTo(CurrentPlayer.Position, 0.8f);


            //send current player on Play State
            CurrentPlayer.Play();
        }

        public virtual void OnPlayerDies(Player deadPlayer)
        {
            players.Remove(deadPlayer);

            if (players.Count == 1)
            {
                IsPlaying = false;
            }
        }

        public virtual void ResetTimer()
        {
            PlayerTimer = turnDuration;
            timerTxt.Text = PlayerTimer.ToString();
            timerTxt.IsActive = true;
        }

        public virtual void StopTimer()
        {
            timerTxt.IsActive = false;
        }

        public override Scene OnExit()
        {
            CurrentPlayer = null;
            currentPlayerIndex = 0;

            for (int i = 0; i < players.Count; i++)
            {
                players[i] = null;
            }
            players = null;

            //BulletsMgr.ClearAll();
            //SpawnMgr.ClearAll();
            UpdateMgr.ClearAll();
            DrawMgr.ClearAll();
            PhysicsMgr.ClearAll();
            GfxMgr.ClearAll();
            FontMgr.ClearAll();
            CameraMgr.ResetCamera();

            return base.OnExit();
        }
    }
}
