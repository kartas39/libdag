#pragma warning disable RCS1037, RCS1036

using System.Runtime.CompilerServices;

namespace libdag
{
    public class Node
    {
        public enum NodeStatus { Pending, Started, Completed, Failed, ChildFailed }
        public bool IsRoot { get; private set; }
        public string NodeId { get; set; }
        public Dictionary<string, Node> ParentNodes = new Dictionary<string, Node>();
        public Dictionary<string, Node> ChildNodes = new Dictionary<string, Node>();
        public NodeStatus Status
        {
            get;
            [MethodImpl(MethodImplOptions.Synchronized)]
            set;
        } = NodeStatus.Pending;

        public Node(string nodeId)
        {
            NodeId = nodeId;
            IsRoot = true;
        }

        public void AddParent(Node parent)
        {
            IsRoot = false;
            ParentNodes[parent.NodeId] = parent;
            parent.ChildNodes[NodeId] = this;
        }


    }
}