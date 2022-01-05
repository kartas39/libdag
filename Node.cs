#pragma warning disable

using System.Runtime.CompilerServices;

namespace libdag
{
    public class Node
    {

        public enum NodeStatus
        {
            Created,
            Started,
            Completed,
            Failed,
            ChildFailed
        }
        public bool IsRoot { get; private set; }
        public string NodeId { get; private set; }
        public Dictionary<string, Node> ParentNodes = new Dictionary<string, Node>();
        public Dictionary<string, Node> ChildNodes = new Dictionary<string, Node>();
        public NodeStatus Status
        {
            get;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set;
        } = NodeStatus.Created;

        public void AddParent(Node parent)
        {
            IsRoot = false;
            ParentNodes[parent.NodeId] = parent;
            parent.ChildNodes[NodeId] = this;
        }
    }
}