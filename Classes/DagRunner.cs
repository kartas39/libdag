#pragma warning disable RCS1037, RCS1036
using Nodes = System.Collections.Generic.Dictionary<string, libdag.Node>;

namespace libdag
{

    public class DagRunner
    {
        public Nodes Dag { get; set; }

        public DagRunner(Nodes dag)
        {
            this.Dag = dag;
        }

		public void Run()
		{
			foreach (var node in Dag.Values)
			{
				if (node.IsRoot)
				{
					node.OnStart();
				}
			}
		}

    }
}