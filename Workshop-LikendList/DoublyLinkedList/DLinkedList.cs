namespace DoublyLinkedList;

public class DLinkedList<T> : LinkedList<T> where T : IComparable<T>
{
    // ─── Fields ──────
    private Node<T>? _head;
    private Node<T>? _tail;

    // ─── Builders ── 
    public DLinkedList()
    {
        _head = null;
        _tail = null;
    }
    // ─── Methods ───

    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);

        // Case 1: The list is empty — the new node is both head and tail.
        if (_head == null)
        {
            _head = _tail = newNode;
            return;
        }

        // Case 2: The new data belongs at the beginning (less than or equal to the head).
        if (data.CompareTo(_head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
            return;
        }

        // Case 3: The new data belongs at the end (greater than or equal to the tail).
        if (data.CompareTo(_tail!.Data) >= 0)
        {
            newNode.Previous = _tail;
            _tail.Next = newNode;
            _tail = newNode;
            return;
        }

        // Case 4: The new data belongs somewhere in the middle.
        Node<T> current = _head;
        while (current.Next != null && current.Next.Data!.CompareTo(data) < 0)
        {
            current = current.Next;
        }

        // Adjust the four pointers to insert the node between current and current.Next.
        newNode.Next = current.Next;
        newNode.Previous = current;

        if (current.Next != null)
        {
            current.Next.Previous = newNode;
        }
        current.Next = newNode;
        Console.WriteLine($"'{data}' added successfully.");
    }

    public void ShowForward()
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = _head;
        Console.WriteLine("\n--- Forward Traversal ---");

        while (current != null)
        {
            Console.Write($"[ {current.Data} ]");

            if (current.Next != null)
                Console.Write(" ==> ");

            current = current.Next;
        }

        Console.WriteLine(" -> null\n");
    }

    public void ShowBack()
    {
        if (_tail == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = _tail;
        Console.WriteLine("\n--- Backward Traversal ---");
        Console.Write("null <== ");

        while (current != null)
        {
            Console.Write($"[ {current.Data} ]");

            if (current.Previous != null)
                Console.Write(" <== ");

            current = current.Previous;
        }

        Console.Write("\n");
    }

    public void DescendingOrder()
    {
        // An empty list or a single node needs no reversal.
        if (_head == null || _head.Next == null) return;

        Node<T>? current = _head;
        Node<T>? temp = null;

        // Swap the Previous and Next pointers of every node.
        while (current != null)
        {
            temp = current.Previous;
            current.Previous = current.Next;
            current.Next = temp;

            // After the swap, the old Next is now stored in Previous,
            // so we advance by following Previous.
            current = current.Previous;
        }

        // Swap the head and tail references to complete the reversal.
        // temp.Previous now points to the new head.
        if (temp != null)
        {
            _tail = _head;
            _head = temp.Previous;
        }
    }

    public void ShowMode()
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Dictionary<T, int> frequencies = new Dictionary<T, int>();
        Node<T>? current = _head;
        int maxCount = 0;

        while (current != null)
        {
            if (current.Data != null)
            {
                if (frequencies.ContainsKey(current.Data))
                    frequencies[current.Data]++;
                else
                    frequencies[current.Data] = 1;

                if (frequencies[current.Data] > maxCount)
                    maxCount = frequencies[current.Data];
            }
            current = current.Next;
        }

        Console.Write("Mode(s): ");

        if (maxCount == 1)
        {
            Console.WriteLine("No repetitions — every element appears exactly once.");
            return;
        }

        foreach (var entry in frequencies)
        {
            if (entry.Value == maxCount)
                Console.Write($"{entry.Key}  (frequency: {entry.Value})  ");
        }

        Console.WriteLine();
    }

    public void ShowChart()
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        // Build the frequency table.
        Dictionary<T, int> frequencies = new Dictionary<T, int>();
        Node<T>? current = _head;

        while (current != null)
        {
            if (current.Data != null)
            {
                if (frequencies.ContainsKey(current.Data))
                    frequencies[current.Data]++;
                else
                    frequencies[current.Data] = 1;
            }
            current = current.Next;
        }

        // Find the maximum count and the widest label for alignment.
        int maxCount = 0;
        int labelWidth = 0;

        foreach (var entry in frequencies)
        {
            if (entry.Value > maxCount) maxCount = entry.Value;
            int len = entry.Key!.ToString()!.Length;
            if (len > labelWidth) labelWidth = len;
        }

        // Print the chart.
        Console.WriteLine("\n======= OCCURRENCE CHART =======");
        foreach (var entry in frequencies)
        {
            string label = entry.Key!.ToString()!.PadLeft(labelWidth);
            string bars = new string('*', entry.Value);
            Console.WriteLine($"{label} | {bars} ({entry.Value})");
        }

        Console.WriteLine(new string('-', labelWidth + maxCount + 10));
        Console.WriteLine("Each * = one occurrence.\n");
    }

    public void ItExists(T data)
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = _head;
        bool found = false;
        int position = 1;

        while (current != null)
        {
            if (current.Data != null && current.Data.CompareTo(data) == 0)
            {
                found = true;
                break;
            }
            current = current.Next;
            position++;
        }

        if (found)
            Console.WriteLine($"✓ '{data}' exists in the list at position {position}.");
        else
            Console.WriteLine($"✗ '{data}' does not exist in the list.");
    }

    public void RemoveOccurrence(T data)
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = _head;

        while (current != null)
        {
            if (current.Data != null && current.Data.CompareTo(data) == 0)
            {
                // Case 1: Only node in the list.
                if (current.Previous == null && current.Next == null)
                {
                    _head = null;
                    _tail = null;
                }
                // Case 2: Node is the head.
                else if (current.Previous == null)
                {
                    _head = current.Next;
                    _head!.Previous = null;
                }
                // Case 3: Node is the tail.
                else if (current.Next == null)
                {
                    _tail = current.Previous;
                    _tail!.Next = null;
                }
                // Case 4: Node is in the middle.
                else
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                }

                Console.WriteLine($"✓ First occurrence of '{data}' removed successfully.");
                return;
            }

            current = current.Next;
        }

        Console.WriteLine($"✗ '{data}' does not exist in the list.");
    }

    public void RemoveAllOccurrences(T data)
    {
        if (_head == null)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        Node<T>? current = _head;
        int count = 0;

        while (current != null)
        {
            Node<T>? next = current.Next; // Save the next node before potentially unlinking current.

            if (current.Data != null && current.Data.CompareTo(data) == 0)
            {
                // Case 1: Only node in the list.
                if (current.Previous == null && current.Next == null)
                {
                    _head = null;
                    _tail = null;
                }
                // Case 2: Node is the head.
                else if (current.Previous == null)
                {
                    _head = current.Next;
                    _head!.Previous = null;
                }
                // Case 3: Node is the tail.
                else if (current.Next == null)
                {
                    _tail = current.Previous;
                    _tail!.Next = null;
                }
                // Case 4: Node is in the middle.
                else
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                }

                count++;
            }

            current = next;
        }

        if (count > 0)
            Console.WriteLine($"✓ {count} occurrence(s) of '{data}' removed successfully.");
        else
            Console.WriteLine($"✗ '{data}' does not exist in the list.");
    }
    public override string ToString()
    {
        var current = _head;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }
        result += "null";
        return result;
    }
}