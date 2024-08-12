using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tankz_2020
{
    abstract class State
    {
        virtual public void OnEnter() { }
        virtual public void OnExit() { }
        abstract public void Update();

        protected StateMachine stateMachine;

        public void SetStateMachine(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
    }
}
