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

    /// <summary>
    /// Renders this Entity
    /// </summary>
    /// <param name="shader">The Shader whith which this Entity gets Rendered</param>
    void IRenderable.Render(Shader shader)
    {
        _spriteRenderer?.Render(shader);
    }

    internal void Update()
    {
        foreach (var component in _components)
        {
            component.InternalUpdate();
        }
    }

    internal void FixedUpdate()
    {
        foreach (var component in _components)
        {
            component.InternalFixedUpdate();
        }
    }

    //public Entity this[int i] => Children[i];

    /// <summary>
    /// Gets a component of Type T in this Entity
    /// </summary>
    /// <typeparam name="T">Type of Component to Get</typeparam>
    /// <returns>A Component of Type T in this Entity</returns>
    public T? GetComponent<T>() where T : class
    {
        var component = _components.Find(component => component.GetType() == typeof(T));
        return component as T;
    }

    /// <summary>
    /// Gets all Components of Type T of this Entity
    /// </summary>
    /// <typeparam name="T">Type of Components to Get</typeparam>
    /// <returns>A List of all Components of Type T of this Entity</returns>
    public List<T> GetComponents<T>() where T : class
    {
        var components = _components.FindAll(component => component.GetType() == typeof(T));
        return components as List<T> ?? new List<T>();
    }

    /// <summary>
    /// Adds a new component of Type T to this Entity
    /// </summary>
    /// <typeparam name="T">Type of Component to Add</typeparam>
    public void AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        if (typeof(T) == typeof(SpriteRenderer)) _spriteRenderer = component as SpriteRenderer;
        _components.Add(component);
    }

    /// <summary>
    /// Gets a Component of Type T in this Entities Parent
    /// </summary>
    /// <typeparam name="T">The Type of Component to Get</typeparam>
    /// <returns>A Component of Type T in this Entities Parent</returns>
    public T? GetComponentInParent<T>() where T : class
    {
        return Parent?.GetComponent<T>();
    }

    /// <summary>
    /// Gets all Components of Type T in this Entities Parent
    /// </summary>
    /// <typeparam name="T">The Type of Components to Get</typeparam>
    /// <returns>A list of all components of Type T in this Entities Parent</returns>
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
    
    /// <summary>
    /// Instantiates an Empty Entity
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception">Gets thrown when the Maximum number of Entities has been reached</exception>
    public static Entity Instantiate()
    {
        var entity = new Entity();

        //Instantiate the entity
        if (!EntityManager.InstantiateEntity(entity)) throw new Exception("Maximum number of Instantiated Entities Reached");

        return entity;
    }

    /// <summary>
    /// Instantiates a given Entity with the given transform
    /// </summary>
    /// <param name="entity">The Entity to be Instantiated</param>
    /// <param name="transform">The Transform of the Entity</param>
    /// <returns></returns>
    /// <exception cref="Exception">Gets thrown when the Maximum number of Entities has been reached</exception>
    public static Entity Instantiate(Entity entity, Transform transform)
    {
        //Clone entity
        var newEntity = (Entity) entity.Clone();
        newEntity.Transform = transform;
        
        //Instantiate the entity
        if (!EntityManager.InstantiateEntity(entity)) throw new Exception("Maximum number of Instantiated Entities Reached");

        return newEntity;
    }

    /// <summary>
    /// Instantiates an Entity with the given Components
    /// </summary>
    /// <param name="components">The components of the Entity</param>
    /// <returns></returns>
    /// <exception cref="Exception">Gets thrown when the Maximum number of Entities has been reached</exception>
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