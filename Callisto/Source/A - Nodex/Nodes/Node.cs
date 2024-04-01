using SFML.System;

namespace Nodex;

class Node
{
    // Fields

    public string Name = "";
    public List<Node> Children = [];
    public Node Parent;
    public Window Window;
    public Vector2f Position = new(0, 0);
    public bool InheritsPosition = true;
    public bool IsActive { get; private set; } = true;

    // Properties

    private Vector2f globalPosition = new(0, 0);

    public Vector2f GlobalPosition
    {
        get
        {
            if (Parent != null)
            {
                if (InheritsPosition)
                {
                    return Parent.GlobalPosition + Position;
                }

                return globalPosition;
            }
            else
            {
                return Position;
            }
        }

        set
        {
            globalPosition = value;
        }
    }

    // Constructor

    public Node() { }

    // Public

    public virtual void Update()
    {
        if (!IsActive) return;

        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].Update();
        }
    }

    public virtual void Start() { }

    public virtual void Destroy()
    {
        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].Destroy();
        }
    }

    public virtual void Activate()
    {
        IsActive = true;

        foreach (Node child in Children)
        {
            child.Activate();
        }
    }

    public virtual void Deactivate()
    {
        IsActive = false;

        foreach (Node child in Children)
        {
            child.Deactivate();
        }
    }

    // Get special nodes

    public T GetParent<T>() where T : Node
    {
        if (Parent != null)
        {
            return (T)Parent;
        }

        return (T)this;
    }

    // Get node from the root

    public T? GetNode<T>(string path) where T : Node
    {
        if (path == "")
        {
            return (T)Window.RootNode;
        }

        string[] nodeNames = path.Split('/');

        Node currentNode = Window.RootNode;

        for (int i = 0; i < nodeNames.Length; i ++)
        {
            currentNode = currentNode.GetChild(nodeNames[i]);
        }

        return (T)currentNode;
    }

    public Node GetNode(string path)
    {
        if (path == "")
        {
            return Window.RootNode;
        }

        string[] nodeNames = path.Split('/');

        Node currentNode = Window.RootNode;

        for (int i = 0; i < nodeNames.Length; i ++)
        {
            currentNode = currentNode.GetChild(nodeNames[i]);
        }

        return currentNode;
    }

    // Get child

    public Node GetChild(string name)
    {
        foreach (Node child in Children)
        {
            if (child.Name == name)
            {
                return child;
            }
        }

        return null;
    }

    public T GetChild<T>(string name) where T : Node
    {
        foreach (Node child in Children)
        {
            if (child.Name == name)
            {
                return (T)child;
            }
        }

        throw new Exception($"Could not find child of type '{typeof(T)}' named '{name}'.");
    }

    public T GetChild<T>() where T : Node
    {
        foreach (Node child in Children)
        {
            if (child.GetType() == typeof(T))
            {
                return (T)child;
            }
        }

        throw new Exception($"Could not find child of type '{typeof(T)}'.");
    }

    // Add child

    public void AddChild(Node node, string name)
    {
        node.Name = name;
        node.Window = Window;
        node.Parent = this;
        node.Start();
        Children.Add(node);
    }

    public void AddChild(Node node)
    {
        node.Name = node.GetType().Name;
        node.Window = Window;
        node.Parent = this;
        node.Start();
        Children.Add(node);
    }

    // Change scene

    public void ChangeScene(Node node)
    {
        Window.ResetView();
        Window.RootNode.Destroy();
        Window.RootNode = node;

        node.Name = node.GetType().Name;
        node.Window = Window;
        node.Start();
    }
}