namespace Leetcode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[] nums = { 2, 7, 11, 15 };
            //int target = 17;

            //Solution solution = new Solution();
            //int[] result = solution.TwoSum(nums, target);
            //Console.WriteLine($"Indices: [{result[0]}, {result[1]}]");
        }
    }

    public class Solution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            Span<int> numbers = nums;

            for (int i = 0; i < numbers.Length; i++)
            {
                var subSpan = numbers.Slice(i + 1);

                for (int j = 0; j < subSpan.Length; j++)
                {
                    if (numbers[i] + subSpan[j] == target)
                    {
                        return new int[] { i, i + 1 + j };
                    }
                }
            }

            return Array.Empty<int>();
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            ListNode dummyHead = new ListNode(0);
            ListNode current = dummyHead;
            int carry = 0;

            while (l1 != null || l2 != null || carry > 0)
            {
                int x = (l1 != null) ? l1.val : 0;
                int y = (l2 != null) ? l2.val : 0;

                int sum = x + y + carry;
                carry = sum / 10;

                current.next = new ListNode(sum % 10);
                current = current.next;

                if (l1 != null) l1 = l1.next;
                if (l2 != null) l2 = l2.next;
            }

            return dummyHead.next;
        }

        public int LengthOfLongestSubstring(string s)
        {
            HashSet<char> seen = new HashSet<char>();
            int left = 0, maxLength = 0;

            for (int right = 0; right < s.Length; right++)
            {
                while (seen.Contains(s[right]))
                {
                    seen.Remove(s[left]); // shrink the window from the left
                    left++;
                }

                seen.Add(s[right]); // expand the window to the right
                maxLength = Math.Max(maxLength, right - left + 1);
            }

            return maxLength;
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
                return FindMedianSortedArrays(nums2, nums1);  // Always binary search on the smaller one

            int x = nums1.Length;
            int y = nums2.Length;
            int low = 0, high = x;

            while (low <= high)
            {
                int partitionX = (low + high) / 2;
                int partitionY = (x + y + 1) / 2 - partitionX;

                int maxLeftX = (partitionX == 0) ? int.MinValue : nums1[partitionX - 1];
                int minRightX = (partitionX == x) ? int.MaxValue : nums1[partitionX];

                int maxLeftY = (partitionY == 0) ? int.MinValue : nums2[partitionY - 1];
                int minRightY = (partitionY == y) ? int.MaxValue : nums2[partitionY];

                if (maxLeftX <= minRightY && maxLeftY <= minRightX)
                {
                    if ((x + y) % 2 == 0)
                    {
                        return (Math.Max(maxLeftX, maxLeftY) + Math.Min(minRightX, minRightY)) / 2.0;
                    }
                    else
                    {
                        return Math.Max(maxLeftX, maxLeftY);
                    }
                }
                else if (maxLeftX > minRightY)
                {
                    high = partitionX - 1;  // too far right, move left
                }
                else
                {
                    low = partitionX + 1;   // too far left, move right
                }
            }

            throw new InvalidOperationException("Input arrays are not sorted or invalid.");
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
}
