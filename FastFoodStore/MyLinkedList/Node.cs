using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodStore.MyLinkedList
{
    public class Node
    {
        private object data;
        private Node next;

        public Node(object data, Node next)
        {
            this.Data = data;
            this.Next = next;
        }
        public object Data
        {
            get {return data; }
            set { data = value; }
        }

        public Node Next
        {
            get { return next; }
            set { next = value; }
        }
        
    }
}
