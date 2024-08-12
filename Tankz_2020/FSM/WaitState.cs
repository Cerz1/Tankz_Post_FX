using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    class WaitState : State
    {
        public WaitState()
        {
        }

        public override void OnEnter()
        {
            stateMachine.Owner.StopLoading();
            ((PlayScene)Game.CurrentScene).NextPlayer();
        }

        public override void Update()
        {
            
        }
    }
}
