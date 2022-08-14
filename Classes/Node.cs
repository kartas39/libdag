#pragma warning disable RCS1037, RCS1036



using System.Runtime.CompilerServices;

namespace libdag;

public class Node
{
    public enum NodeStatus { Pending, Started, Completed, Failed, ChildFailed }
    public bool IsRoot { get; private set; } = true;
    public string NodeId { get; init; }
    public Dictionary<string, Node> ParentNodes = new();
    public Dictionary<string, Node> ChildNodes = new();
    public delegate void OnStatusChanged(Node sender);
    public event OnStatusChanged StatusChanged;
    public NodeStatus Status { get; private set; } = NodeStatus.Pending;



    public Action OnStart { get; set; }

    public Node(string nodeId, Action onStart)
    {
        NodeId = nodeId;
        OnStart = onStart;
        IsRoot = true;
    }

    public void AddParent(Node parent)
    {
        IsRoot = false;
        ParentNodes[parent.NodeId] = parent;
        parent.ChildNodes[NodeId] = this;
        StatusChanged += parent.OnChildStatusChanged(this);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private OnStatusChanged OnChildStatusChanged(Node sender)
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        try
        {
            OnStart();
            this.Status = NodeStatus.Completed;
        }
        catch (Exception e)
        {
            this.Status = NodeStatus.Failed;
        }
        finally
        {
            StatusChanged(this);
        }


    }

}
