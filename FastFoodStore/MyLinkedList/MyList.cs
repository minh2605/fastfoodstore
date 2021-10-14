using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFoodStore.MyLinkedList
{
    public class MyList<T>
    {
        private Node head;
        private int count;

        public Node Head
        {
            get { return head; }
            set { head = value; }
        }
        public MyList() //luc khoi tao thi chua co node nao` nen head = null
        {
            this.head = null;
            this.count = 0;
        }
        public bool Empty
        {
            get{ return this.count == 0; }
        }
        public int Count
        {
            get { return this.count; }
        }
        public T Add(int index, T o)
        {
            if(index<0)
            {
                throw new ArgumentOutOfRangeException("Index: " + index);
            }
            if(index>count)
            {
                index = count;
            }

            Node current = this.head; // node hien tai se la node đầu

            if(this.Empty || index == 0) // nêu chua có node nào thì node đầu sẽ được khởi tạo là 1 node mới
            {
                this.head = new Node(o, this.head);
            }
            else 
            {
                for(int i =0;i<index -1;i++)
                {
                    current = current.Next;
                }
                current.Next = new Node(o, current.Next);
            }
            count++;
            return o;
        }
        public T Add(T o)
        {
            return this.Add(count, o);
        }
    }
}
