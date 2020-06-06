using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using DisruptorUnity3d;

namespace memoryTest
{
    class Vector2
    {
        private int x, y;
        
        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"Vector2({this.x}, {this.y})";
        }
    }

    class CircularBuffer<T>
    {
        private int _capacity;
        private T[] _elements;
        private int _head, _tail;
        private bool _full;
        private readonly object _mLock = new object();

        public CircularBuffer(int capacity)
        {
            _capacity = capacity;
            _elements = new T[_capacity];
            _head = 0;
            _tail = 0;
            _full = false;
        }

        public int Capacity => _capacity;

        public int Count
        {
            get
            {
                var size = _capacity;
                
                if (!_full)
                    size = _head >= _tail ? _head - _tail : _capacity + _head - _tail;

                return size;
            }
        }

        public bool IsEmpty => !_full && _head == _tail;

        private void MoveHead()
        {
            if (_full)
            {
                _tail = (_tail + 1) % _capacity;
            }

            _head = (_head + 1) % _capacity;
            _full = _head == _tail;
        }

        private void MoveTail()
        {
            _tail = (_tail + 1) % _capacity;
            _full = false;
        }

        public void Enqueue(T item)
        {
            //lock (_mLock)
           // {
                _elements[_head] = item;
                MoveHead();
          //  }
        }

        public T Dequeue()
        {
            while (IsEmpty)
                Thread.SpinWait(1);
            
            T value = _elements[_tail];
            MoveTail();

            return value;
        }

        public bool TryDequeue(out T value)
        {
            if (IsEmpty)
            {
                value = default(T);
                return false;
            }

            value = Dequeue();
            return true;
        }

        public void Print()
        {
            Console.Write("[PRINT] [");
            var t = Count;
            for (int i = 0; i < t; ++i)
                Console.Write(_elements[i] + ", ");


            while (t < _capacity)
            {
                Console.Write("null, ");
                t++;
            }
                
            Console.WriteLine("]");
        }
    }
    
    internal class Program
    {
        private static int items = 3;
        static void Insert(CircularBuffer<Vector2> q)
        {
            var nr = Int32.Parse(Thread.CurrentThread.Name);
            Console.WriteLine($"Thread {nr} started");
            
            for (int i = nr * 60; i < (nr * 60) + 60; i++)
            {
                q.Enqueue(new Vector2(i+1, i+1));
            }
        }
        
        
        public static void Main(string[] args)
        {
            CircularBuffer<Vector2> q = new CircularBuffer<Vector2>(items);
            
            /*
            Console.WriteLine("IsEmpty: " + q.IsEmpty);
            
            Console.WriteLine("Capacity: " + q.Capacity);
            Console.WriteLine("Count: " + q.Count);
            Console.WriteLine("IsEmpty: " + q.IsEmpty);*/
            
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 20; i++)
            {
                Thread t = new Thread(() => Insert(q));
                t.Name = (i + 1).ToString();
                threads.Add(t);
                threads[i].Start();
            }
            
            for (int i = 0; i < 20; i++)
            {
                threads[i].Join();
            }
            
            q.Print();
        }
    }
}