using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Pizza
{
    public class Edge
    {
        public Edge(Tuple<int, int> vertices, int value)
        {
            Vertices = vertices;
            Value = value;
        }

        public Tuple<int, int> Vertices { get; }

        public int Value { get; }
    }

    public class Graph
    {
        public Graph(IEnumerable<Edge> edges)
        {
            _edges = edges;

            _vertices = new HashSet<int>();
            foreach (var edge in edges)
            {
                _vertices.Add(edge.Vertices.Item1);
                _vertices.Add(edge.Vertices.Item2);
            }
        }


        private readonly IEnumerable<Edge> _edges;

        private readonly HashSet<int> _vertices;

        public IEnumerable<int> GetVertexNeighbours(int vertex)
        {
            var neighbours = _edges.Where(edge => edge.Vertices.Item1 == vertex || edge.Vertices.Item2 == vertex)
                .Select(edge => edge.Vertices.Item1 == vertex ? edge.Vertices.Item2 : edge.Vertices.Item1);

            return neighbours;
        }

        private int GetEdgeValue(Tuple<int, int> vertices)
        {
            var edge = _edges.First(e =>
                Math.Min(e.Vertices.Item1, e.Vertices.Item2).Equals(Math.Min(vertices.Item1, vertices.Item2)) &&
                Math.Max(e.Vertices.Item1, e.Vertices.Item2).Equals(Math.Max(vertices.Item1, vertices.Item2)));

            return edge.Value;
        }

        public Dictionary<int, int> GetDistances(int vertex)
        {
            var distancesResult = new Dictionary<int, int>
            {
                [vertex] = 0
            };

            var usedVertices = new HashSet<int>();

            GetDistance(vertex, distancesResult, usedVertices);

            return distancesResult;
        }

        private void GetDistance(int vertex, Dictionary<int, int> distancesResult, HashSet<int> usedVertices)
        {
            if (!usedVertices.Contains(vertex))
            {
                var neighbours = GetVertexNeighbours(vertex);
                foreach (var x in neighbours)
                {
                    var vertices = new Tuple<int, int>(vertex, x);
                    var distance = distancesResult[vertex] + GetEdgeValue(vertices);

                    if (!distancesResult.ContainsKey(x) || distance < distancesResult[x])
                    {
                        distancesResult[x] = distance;
                    }
                }

                usedVertices.Add(vertex);

                var sortedVertices = distancesResult
                    .Where(x => neighbours.Contains(x.Key))
                    .OrderBy(x => x.Value)
                    .Select(x => x.Key);

                foreach (var v in sortedVertices)
                {
                    GetDistance(v, distancesResult, usedVertices);
                }
            }
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class Node
    {
        public int val;
        public Node prev;
        public Node next;
        public Node child;
    }

    class A
    {
        public virtual void Func()
        {
            Console.WriteLine("A");
        }
    }

    class B: A
    {
        public override void Func()
        {
            Console.WriteLine("B");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            A variable = new B();
            variable.Func();
        }

        private static void Function(List<int> list)
        {
            list.Add(3);
            //list = new List<int>(1)
            //{
            //    2
            //};
        }

        class Point
        {
            public int X { get; set; }

            public int Y { get; set; }

            //public override bool Equals(object obj)
            //{
            //    var point = (Point)obj;
            //    var areXEquals = point.X == this.X;
            //    var areYEquals = point.Y == this.Y;

            //    return areXEquals && areYEquals;
            //}
        }

        public static IEnumerable<int> MergeSort(List<int> array)
        {
            var skipIndex = 1;
            var counter = 0;
            while (skipIndex < array.Count)
            {
                var resultList = new List<int>(array.Count);
                for (var i = 0; i < array.Count; i += skipIndex * 2)
                {
                    var j = i;
                    var rightLength = Math.Min(i + 2 * skipIndex, array.Count);
                    var leftLength = Math.Min(i + skipIndex, array.Count);
                    var k = i + skipIndex;

                    while (j < leftLength || k < rightLength)
                    {
                        counter++;
                        if (j >= leftLength)
                        {
                            resultList.Add(array[k++]);
                            continue;
                        }

                        if (k >= rightLength)
                        {
                            resultList.Add(array[j++]);
                            continue;
                        }

                        if (array[j] <= array[k])
                        {
                            resultList.Add(array[j++]);
                        }
                        else
                        {
                            resultList.Add(array[k++]);
                        }
                    }
                }
                skipIndex *= 2;
                array = resultList;
            }

            return array;
        }

        static void Encryption()
        {
            Console.WriteLine("Enter a:");
            string a = Console.ReadLine();
            string[] numbers_a = { "1", "3", "5", "7", "9", "11", "15", "17", "19", "21", "23", "25" };
            foreach (string x in numbers_a)
            {
                if (a.Contains(x))
                {
                    Console.WriteLine(a);
                }
                else
                {
                    Console.WriteLine("Error, enter a from the list:{0}", string.Join(", ", numbers_a));
                    //   Console.WriteLine(numbers_a);
                }
                //   Console.WriteLine("Enter number from the list:", numbers_a);
            }
        }

        private static string MergeString(string str1, string str2)
        {
            var index1 = 0;
            var index2 = 0;
            var condition1 = index1 < str1.Length;
            var condition2 = index2 < str2.Length;
            var strBuilder = new System.Text.StringBuilder();
            while (condition1 || condition2)
            {
                if (condition1)
                {
                    strBuilder.Append(str1[index1++]);
                    condition1 = index1 < str1.Length;
                }

                if (condition2)
                {
                    strBuilder.Append(str2[index2++]);
                    condition2 = index2 < str2.Length;
                }
            }

            return strBuilder.ToString();
        }

        public static Node Flatten(Node head)
        {
            var iterator = head;
            Node temp = null;
            Node temp1 = null;

            while (iterator != null)
            {
                if (iterator.child == null)
                {
                    if (iterator.next != null)
                    {
                        iterator.next.prev = iterator;
                    }
                    iterator = iterator.next;
                    continue;
                }
                else
                {
                    if (iterator.child.child == null)
                    {
                        iterator.child.prev = iterator;
                        temp = iterator.next;
                        iterator.next = iterator.child;
                        temp1 = iterator.child.next;
                        iterator.child.next = temp;
                        iterator.child.child = temp1;
                        iterator.child = null;
                        iterator = iterator.next;
                    }
                    else
                    {
                        var internalIterator = iterator.child;
                        while (internalIterator.next != null)
                        {
                            internalIterator = internalIterator.next;
                        }

                        internalIterator.next = iterator.next;
                        iterator.child.prev = iterator;
                        iterator.next = iterator.child;
                        iterator.child = null;
                        iterator = iterator.next;
                    }
                }

            }

            return head;
        }

        private static Node iterator = null;

        private static void Helper(Node node)
        {
            if (node.next == null)
            {
                return;
            }

            if (node.child == null)
            {
                iterator = iterator.next;
                Helper(iterator);
            }
            else
            {
                var temp = iterator.next;
                iterator.next = iterator.child;
                iterator.child = null;
                iterator.next.prev = iterator;
                iterator = iterator.next;
                Helper(iterator);
                iterator.next = temp;
                iterator.next.prev = iterator;
                iterator = iterator.next;
                Helper(iterator);
            }
        }

        public static ListNode RemoveElements(ListNode head, int val)
        {
            GetHead(head, val);

            return head;
        }

        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            ListNode temp;
            if (l1.val > l2.val)
            {
                temp = l1;
                l1 = l2;
                l2 = temp;
            }
            var start = l1;

            while (l1.next != null)
            {
                if (l2 == null || l1.next.val < l2.val)
                {
                    l1 = l1.next;
                }
                else
                {
                    temp = l2;
                    l2 = l2.next;
                    temp.next = l1.next;
                    l1.next = temp;
                }
            }

            return start;
        }

        public static void GetHead(ListNode head, int val)
        {
            if (head == null) return;
            if (head.val == val)
            {
                head = head.next;
                if (head == null)
                {
                    return;
                }
            }
            var temp = head.next?.val == val ? head.next?.next : head.next;

            head.next = temp;
            GetHead(head.next, val);
        }

        public static ListNode ReverseList(ListNode head)
        {
            ListNode curr = head;
            ListNode prev = null;

            while (curr.next != null)
            {
                ListNode temp = new ListNode(0, curr.next);
                curr.next = prev;
                prev = curr;
                curr = temp.next;
            }

            head = curr;
            return head;
        }

        public static bool HasCycle(ListNode head)
        {

            if (head == null || head.next == null)
            {
                return false;
            }

            while (head != null && head.next != head)
            {
                ListNode temp = head.next;
                head.next = head;
                head = temp;
            }

            return head != null;
        }

        public static void Func(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                while (nums[i] != i + 1 && nums[i] != nums[nums[i] - 1])
                {
                    int swap = nums[nums[i] - 1];
                    nums[nums[i] - 1] = nums[i];
                    nums[i] = swap;
                }
                nums.Clone();

            }
        }

        public static int removeElement(int[] nums, int val)
        {
            int downIndex = 0;
            int upIndex = nums.Length - 1;

            while (downIndex < upIndex)
            {
                if (nums[downIndex] == val && nums[upIndex] != val)
                {
                    int swap = nums[downIndex];
                    nums[downIndex] = nums[upIndex];
                    nums[upIndex] = swap;
                    downIndex++;
                    upIndex--;
                    continue;
                }

                if (nums[downIndex] != val)
                {
                    downIndex++;
                    continue;
                }

                if (nums[upIndex] == val)
                {
                    upIndex--;
                    continue;
                }
            }

            return downIndex;
        }

        public static int[] SortArrayByParity(int[] A)
        {
            int downIndex = 0;
            int upIndex = A.Length - 1;

            while (downIndex != upIndex)
            {
                if (A[downIndex] % 2 == 1 && A[upIndex] % 2 == 0)
                {
                    int swap = A[downIndex];
                    A[downIndex] = A[upIndex];
                    A[upIndex] = swap;
                    downIndex++;
                    upIndex--;
                    continue;
                }

                if (A[downIndex] % 2 == 0)
                {
                    downIndex++;
                    continue;
                }

                if (A[upIndex] % 2 == 1)
                {
                    upIndex--;
                    continue;
                }
            }

            return A;
        }

        public static void MoveZeroes(int[] nums)
        {
            int index = -1;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0 && index == -1)
                {
                    index = i;
                }

                if (nums[i] != 0 && index != -1)
                {
                    int swap = nums[i];
                    nums[i] = nums[index];
                    nums[index] = swap;
                    index = i;
                }
            }
        }

        public static int[] ReplaceElements(int[] arr)
        {
            int maxIndex = GetMaxIndex(arr, 0);
            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (i == maxIndex)
                {
                    maxIndex = GetMaxIndex(arr, maxIndex);
                }

                arr[i] = arr[maxIndex];
            }

            arr[arr.Length - 1] = -1;

            return arr;
        }

        public static int GetMaxIndex(int[] arr, int maxIndex)
        {
            maxIndex += 1;
            for (int i = maxIndex + 1; i < arr.Length; i++)
            {
                if (arr[i] > arr[maxIndex])
                {
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            bool plusDecade = false;
            var result = new ListNode();
            ListNode iterator = result;
            do
            {
                var sum = (l1?.val ?? 0) + (l2?.val ?? 0);

                if (plusDecade)
                {
                    sum += 1;
                }

                if (sum >= 10)
                {
                    sum -= 10;
                    plusDecade = true;
                }
                else
                {
                    plusDecade = false;
                }

                if (result.next == null)
                {
                    result.val = sum;
                    result.next = new ListNode();
                }
                else
                {
                    iterator.next = new ListNode(sum);
                    iterator = iterator.next;
                }
                l1 = l1?.next;
                l2 = l2?.next;
            }
            while (l1 != null || l2 != null);

            return result;
        }
    }
}
