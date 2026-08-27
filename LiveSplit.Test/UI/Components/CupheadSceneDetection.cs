using System;

using LiveSplit.Cuphead;

namespace LiveSplit.UI.Components
{
    public class CupheadSceneDetection : IDisposable
    {
        private readonly MemoryManager memory;

        private string currentScene;

        public string CurrentScene
        {
            get { return currentScene; }
        }

        public bool IsHooked
        {
            get { return memory.IsHooked; }
        }

        public bool IsLoading
        {
            get
            {
                if (!memory.IsHooked)
                    return true;

                try
                {
                    return memory.Loading();
                }
                catch
                {
                    return true;
                }
            }
        }

        public event Action<string> SceneChanged;

        public CupheadSceneDetection()
        {
            memory = new MemoryManager();
            currentScene = null;
        }

        public void Update()
        {
            /*
             * Always let MemoryManager verify the current
             * Cuphead process.
             *
             * This is important when Cuphead is closed and
             * relaunched.
             */
            if (!memory.HookProcess())
            {
                SetScene(null);
                return;
            }

            /*
             * Make sure the process still exists.
             */
            if (memory.Program == null ||
                memory.Program.HasExited)
            {
                SetScene(null);
                return;
            }

            string scene;

            try
            {
                /*
                 * While Cuphead is loading a new scene,
                 * don't report an active scene.
                 */
                if (memory.Loading())
                {
                    SetScene(null);
                    return;
                }

                scene = memory.SceneName();
            }
            catch
            {
                SetScene(null);
                return;
            }

            if (string.IsNullOrWhiteSpace(scene))
            {
                SetScene(null);
                return;
            }

            SetScene(scene);
        }

        private void SetScene(string scene)
        {
            if (string.Equals(
                currentScene,
                scene,
                StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            currentScene = scene;

            if (SceneChanged != null)
            {
                SceneChanged(currentScene);
            }
        }

        public bool IsScene(string sceneName)
        {
            return string.Equals(
                CurrentScene,
                sceneName,
                StringComparison.OrdinalIgnoreCase);
        }

        public void Dispose()
        {
            memory.Dispose();
        }
    }
}