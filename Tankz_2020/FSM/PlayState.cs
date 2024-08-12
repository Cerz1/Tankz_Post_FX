using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class PlayState : State
    {
        public PlayState()
        {
        }

        public override void OnEnter()
        {
            //Reset Timer
            ((PlayScene)Game.CurrentScene).ResetTimer();
        }

        public override void OnExit()
        {
            stateMachine.Owner.RigidBody.Velocity.X = 0;
        }

        public override void Update()
        {
            if (((PlayScene)Game.CurrentScene).PlayerTimer <= 1)
            {
                stateMachine.GoTo(StateEnum.WAIT);
                return;
            }
            stateMachine.Owner.Input();
        }
    }
}
