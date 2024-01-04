using TrueRogueliike.Components;

namespace TrueRogueliike.Core
{
    public interface IGameSceneEditor
    {
        IReadOnlyList<GameObject> GameObjects { get; }
        int Width { get; }
        int Height { get; }
        void Finished();
        void RemoveGameObject(GameObject gameObject);
        void Unsubscribe();
    }
}
