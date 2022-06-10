using EngineSigma.Core.ECS.Components;
using EngineSigma.Core.GFX.Rendering;
using EngineSigma.Core.GFX.Shaders;

namespace EngineSigma.Core.ECS;

public sealed class Entity : ICloneable, IRenderable
{
    public int InstanceID { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }

    private List<Component> _components;
    public List<Entity> Children;
    public Entity? Parent;

    private SpriteRenderer? _spriteRenderer;
    public Transform Transform;

    private Entity()
    {
        Transform = new Transform();
        Parent = null;
        _spriteRenderer = null;
        _components = new List<Component>();
        Children = new List<Entity>();
        Name = "New Entity";
        InstanceID = -1;
        Active = true;
    }

    void IRenderable.Render(Shader shader)
    {
        _spriteRenderer?.Render(shader);
    }

    //public Entity this[int i] => Children[i];

    public T? GetComponent<T>() where T : class
    {
        var component = _components.Find(component => component.GetType() == typeof(T));
        return component as T;
    }

    public List<T> GetComponents<T>() where T : class
    {
        var components = _components.FindAll(component => component.GetType() == typeof(T));
        return components as List<T> ?? new List<T>();
    }

    public void AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        if (typeof(T) == typeof(SpriteRenderer)) _spriteRenderer = component as SpriteRenderer;
        _components.Add(component);
    }

    public T? GetComponentInParent<T>() where T : class
    {
        return Parent?.GetComponent<T>();
    }

    public List<T>? GetComponentsInParent<T>() where T : class
    {
        return Parent?.GetComponents<T>();
    }

    public object Clone()
    {
        //Create Cloned Entity
        var entity = new Entity
        {
            Name = Name,
            Active = Active,
            _components = _components.Select(component => (Component) component.Clone()).ToList(),
            Children = Children.Select(child => (Entity) child.Clone()).ToList()
        };

        return entity;
    }

    public override string ToString()
    {
        return $"Entity: {Name}" +
               $"InstanceID: {InstanceID}" +
               $"Active: {Active}" +
               $"Components: {_components.Count}";
    }
    
    public static Entity Instantiate()
    {
        var entity = new Entity();
        
        //Instantiate the entity
        if (!EntityManager.InstantiateEntity(entity)) throw new Exception("Maximum number of Instantiated Entities Reached");

        return entity;
    }

    public static Entity Instantiate(Entity entity)
    {
        //Clone entity
        var newEntity = (Entity) entity.Clone();
        
        //Instantiate the entity
        if (!EntityManager.InstantiateEntity(entity)) throw new Exception("Maximum number of Instantiated Entities Reached");

        return newEntity;
    }

    public static Entity Instantiate(params Component[] components)
    {
        //Create new Entity
        var entity = new Entity
        {
            _components = components.ToList()
        };

        //Assign entity of components
        foreach (var component in entity._components)
        {
            component.Entity = entity;
            if (component is SpriteRenderer renderer) entity._spriteRenderer = renderer;
        }
        
        //Instantiate the entity
        if (!EntityManager.InstantiateEntity(entity)) throw new Exception("Maximum number of Instantiated Entities Reached");

        return entity;
    }
}