using EngineSigma.Core.GFX.Rendering;

namespace EngineSigma.Core.ECS;

internal static class EntityManager
{
    private const int MaxEntities = 100_000;
    
    private static readonly List<Entity> Entities;
    private static readonly Queue<int> IDs;

    static EntityManager()
    {
        Entities = new List<Entity>();
        IDs = new Queue<int>();
        
        //Fill ids
        for (var i = 0; i < MaxEntities; i++)
        {
            IDs.Enqueue(i);
        }
    }

    public static bool InstantiateEntity(Entity entity)
    {
        //If there are no available IDs return
        if (!IDs.TryDequeue(out int availableID)) return false;

        //assign the to be instantiated entity the available ID
        entity.InstanceID = availableID;
        //Add the entity to the entities list
        Entities.Add(entity);
        return true;
    }

    public static bool DestroyEntity(Entity entity)
    {
        if (!Entities.Contains(entity)) return false;
        
        IDs.Enqueue(entity.InstanceID);
        return Entities.Remove(entity);
    }

    public static void OnUpdate()
    {
        
    }

    public static void OnRender(Renderer renderer)
    {
        foreach (var entity in Entities)
        {
            renderer.AddRenderable(entity);
        }
    }
}