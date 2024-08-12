using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class Player : Tank
    {
        protected Controller controller;
        protected float speed = 4;
        protected float jumpSpeed = -8;

        protected bool isJumpPressed;
        protected bool isFirePressed;
        protected bool isChangeWeaponPressed;

        protected TextObject playerName;

        protected ProgressBar loadingBar;
        protected bool IsLoading;
        protected float currentLoadingValue = 0;
        protected float loadIncrease = 50;
        protected float maxLoadingValue = 100;

        protected WeaponsGUI weaponsGUI;

        protected StateMachine stateMachine;

        public bool IsGrounded { get { return RigidBody.Velocity.Y == 0; } }
        public int PlayerID { get; protected set; }

        public bool IsAlive;


        public Player(Controller ctrl, int playerID) : base()
        {
            IsAlive = true;
            RigidBody.Type = RigidBodyType.Player;
            RigidBody.Collider = ColliderFactory.CreateBoxFor(this);
            RigidBody.AddCollisionType(RigidBodyType.Tile);
            RigidBody.IsGravityAffected = true;

            bulletType = BulletType.StdBullet;

            PlayerID = playerID;

            controller = ctrl;

            energyBar.Position = new Vector2(playerID * 5.5f + 1.0f, 0.6f);

            playerName = new TextObject(new Vector2(energyBar.Position.X, 0.35f), "Player " + (PlayerID + 1));

            float unitDist = Game.PixelsToUnits(4);
            loadingBar = new ProgressBar("barFrame", "blueBar", new Vector2(unitDist));
            loadingBar.Camera = null;
            loadingBar.IsActive = false;

            weaponsGUI = new WeaponsGUI(new Vector2(energyBar.Position.X, 1f));
            weaponsGUI.IsActive = true;

            stateMachine = new StateMachine(this);
            stateMachine.AddState(StateEnum.WAIT, new WaitState());
            stateMachine.AddState(StateEnum.PLAY, new PlayState());
            stateMachine.AddState(StateEnum.SHOOT, new ShootState());
        }

        public virtual void Input()
        {
            float directionX = 0;
            if (!IsLoading)
            {
                directionX = controller.GetHorizontal();
            }
            RigidBody.Velocity.X = directionX * speed;


            float directionY = controller.GetVertical();
            if (directionY != 0)
            {
                //rotate cannon
                cannon.Rotation += Game.DeltaTime * directionY;
                cannon.Rotation = MathHelper.Clamp(cannon.Rotation, maxAngle, 0);
            }

            if (controller.IsFirePressed())
            {
                if (!isFirePressed)
                {
                    isFirePressed = true;
                    StartLoading();
                }
            }
            else
            {
                if (isFirePressed)
                {
                    isFirePressed = false;
                    StopLoading();
                    Shoot(currentLoadingValue / maxLoadingValue);
                    if (LastShotBullet != null)
                    {
                        bulletType = weaponsGUI.DecrementsBullets();
                        CameraMgr.SetTarget(LastShotBullet);
                        stateMachine.GoTo(StateEnum.SHOOT);
                    }
                }
            }

            if (controller.NextWeapon())
            {
                if (!isChangeWeaponPressed)
                {
                    bulletType = weaponsGUI.NextWeapon();
                    isChangeWeaponPressed = true;
                }
            }
            else if (controller.PrevWeapon())
            {
                if (!isChangeWeaponPressed)
                {
                    bulletType = weaponsGUI.NextWeapon(-1);
                    isChangeWeaponPressed = true;
                }
            }
            else
            {
                isChangeWeaponPressed = false;
            }

        }

        //protected virtual void Jump()
        //{
        //    RigidBody.Velocity.Y = jumpSpeed;
        //}

        public override void Update()
        {
            if (IsActive)
            {
                base.Update();

                if (IsLoading)
                {
                    currentLoadingValue += Game.DeltaTime * loadIncrease;
                    if (currentLoadingValue > maxLoadingValue)
                    {
                        currentLoadingValue = maxLoadingValue;
                        loadIncrease = -loadIncrease;
                    }
                    else if (currentLoadingValue < 0)
                    {
                        currentLoadingValue = 0;
                        loadIncrease = -loadIncrease;
                    }

                    loadingBar.Scale(currentLoadingValue / maxLoadingValue);
                }
            }
        }

        protected virtual void StartLoading()
        {
            currentLoadingValue = 0;
            loadIncrease = Math.Abs(loadIncrease);
            loadingBar.Position = new Vector2(Position.X - loadingBar.HalfWidth, Position.Y - 1.5f);
            loadingBar.IsActive = true;
            IsLoading = true;
        }

        public virtual void StopLoading()
        {
            IsLoading = false;
            loadingBar.IsActive = false;
        }

        public virtual void Play()
        {
            stateMachine.GoTo(StateEnum.PLAY);
        }

        public virtual void Wait()
        {
            stateMachine.GoTo(StateEnum.WAIT);
        }

        public virtual void UpdateStateMachine()
        {
            stateMachine.Update();
        }


        public override void OnDie()
        {
            IsAlive = false;
            ((PlayScene)Game.CurrentScene).OnPlayerDies(this);
        }
    }
}
