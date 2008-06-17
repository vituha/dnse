using System;
using System.Collections.Generic;
using System.Text;
using VS.Library.Generics.Common;

namespace VS.Library.Cache
{
	public interface IAsyncAction : IDisposable
	{
		void Start();
		void Stop();
	}

	public abstract class ActionBase : IAsyncAction
	{
		private enum ActionState
		{
			Disposed,
			Stopped,
			Started
		};

		private ActionState actionState = ActionState.Disposed;

		protected void ReSpawn()
		{
			actionState = ActionState.Stopped;
		}

		#region IDualAction Members

		public virtual void Start()
		{
			if (PrepareSwitchState(ActionState.Started))
			{
				DoStartAction();
				actionState = ActionState.Started;
			}
		}

		public void Stop()
		{
			if (PrepareSwitchState(ActionState.Stopped))
			{
				DoEndAction();
				actionState = ActionState.Stopped;
			}
		}

		#endregion

		protected abstract void DoStartAction();
		protected abstract void DoEndAction();

		#region - Dispose Pattern -

		private bool disposed;
		protected bool Disposed
		{
			get
			{
				return disposed;
			}
		}

		protected void CheckDisposed()
		{
			if (Disposed)
			{
				throw new ObjectDisposedException("this");
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			Stop();
			if (PrepareSwitchState(ActionState.Disposed))
				actionState = ActionState.Disposed;
		}

		public void Dispose()
		{
			Dispose(true);
			disposed = true;
			GC.SuppressFinalize(this);
		}

		~ActionBase()
		{
			Dispose(false);
		}
		#endregion

		private bool PrepareSwitchState(ActionState newState)
		{
			if (newState == actionState)
				return false;

			switch (actionState)
			{
				case ActionState.Disposed:
					ThrowDetachedException();
					break;
				case ActionState.Stopped:
					break;
				case ActionState.Started:
					if (newState == ActionState.Disposed)
						ThrowDetachStartedException();
					break;
				default:
					break;
			}
			return true;
		}

		private static void ThrowDetachedException()
		{
			throw new ApplicationException("Cannot control the Action while detached");
		}

		private static void ThrowDetachStartedException()
		{
			throw new ApplicationException("Cannot detach a started action");
		}
	}
}
