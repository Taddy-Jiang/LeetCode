/// <summary>
/// 链表知识点
/// 
/// 1.小技巧
/// a.在首链表头加一个值，防止溢出
/// 
/// b.链表需要不断往后插入，指针位移
/// 
/// c.最好不要在原链表上改，使用辅助链表
/// 
/// 2.链表一般都用双指针的方法：快慢指针
/// 快指针每次移动两格，慢指针每次一格
/// </summary>

/// <summary>
/// 题号：2. 两数相加
/// 题目：
/// 给出两个 非空 的链表用来表示两个非负的整数。其中，它们各自的位数是按照 逆序 的方式存储的，并且它们的每个节点只能存储 一位 数字。
/// 如果，我们将这两个数相加起来，则会返回一个新的链表来表示它们的和。
/// 您可以假设除了数字 0 之外，这两个数都不会以 0 开头。
/// 输入：(2 -> 4 -> 3) + (5 -> 6 -> 4)
/// 输出：7-> 0-> 8
/// 原因：342 + 465 = 807
/// 
/// 链表
/// 注意点：如果链表长度不一样，需要补全
/// 每一项需要加上前一项的进位
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
    {
        int val = 0;   //进位
        ListNode resultNode = new ListNode(0);   //结果的链表首相赋值为0防止意外
        ListNode tempNode = resultNode;         //临时借用一个链表代替原链表，因为如果直接用源链表的话，最后返回结果应该为源链表后续的字段，不全。
        while (l1 != null || l2 != null || val != 0)
        {
            //当l1，l2不为空链表时，且进位不为空时运行
            val = val + (l1 == null ? 0 : l1.val) + (l2 == null ? 0 : l2.val);
            //当前位的值为进位值+l1当前位值+l2当前位值
            tempNode.next = new ListNode(val % 10);   //当前位值应该为此时计算的值的余数
            tempNode = tempNode.next;   //向后移一位
            val = val / 10;     //确定进位为多少
            l1 = (l1 == null ? null : l1.next);    //l1链表为空时赋值为空，否则移下一位
            l2 = (l2 == null ? null : l2.next);
        }
        return resultNode.next;     //虽然使用tempNode代替，但指向的地址没有变化，所以resultNode开始为第一项0，但后续地址都有
    }
}

/// <summary>
/// 206. 反转链表
/// 题目：
/// 反转一个单链表。
/// 示例:
/// 输入: 1->2->3->4->5->NULL
/// 输出: 5->4->3->2->1->NULL
/// 
/// 链表
/// 双指针
/// </summary>
/**
*Definition for singly - linked list.

* public class ListNode
{
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
*/
public class Solution
{
    public ListNode ReverseList(ListNode head)
    {
        if (head == null) return head;
        ListNode result = new ListNode(0);
        ListNode p = head;

        while (p != null)
        {
            ListNode temp = result.next;
            result.next = p;
            p = p.next;
            result.next.next = temp;
        }

        return result.next;
    }
}

/// <summary>
/// 题号：24. 两两交换链表中的节点
/// 题目：
/// 给定一个链表，两两交换其中相邻的节点，并返回交换后的链表。
/// 你不能只是单纯的改变节点内部的值，而是需要实际的进行节点交换。
/// 示例:
/// 给定 1->2->3->4, 你应该返回 2->1->4->3.
/// 
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution
{
    public ListNode SwapPairs(ListNode head)
    {
        ListNode result = new ListNode(0);
        ListNode temp = result;

        while (head != null)
        {
            if (head.next == null)
            {
                temp.next = head;
                head = head.next;
            }
            else
            {
                ListNode temp2 = head.next.next;
                temp.next = head.next;
                temp = temp.next;
                head.next = null;
                temp.next = head;
                temp = temp.next;
                head = temp2;
            }
        }
        return result.next;
    }
}

public class Solution
{
    public ListNode SwapPairs(ListNode head)
    {
        ListNode result = new ListNode(0);
        ListNode temp = result;

        while (head != null)
        {
            if (head.next == null)
            {
                temp.next = head;
                head = head.next;
            }
            else
            {
                ListNode temp2 = temp.next;
                temp.next = head;
                head = head.next;
                temp.next.next = temp2;
                temp2 = temp.next;
                temp.next = head;
                head = head.next;
                temp.next.next = temp2;
                temp = temp.next.next;
            }
        }
        return result.next;
    }
}

/// <summary>
/// 25.K 个一组翻转链表
/// 题目：
/// 给你一个链表，每 k 个节点一组进行翻转，请你返回翻转后的链表。
/// k 是一个正整数，它的值小于或等于链表的长度。
/// 如果节点总数不是 k 的整数倍，那么请将最后剩余的节点保持原有顺序。
/// 示例：
/// 给你这个链表：1->2->3->4->5
/// 当 k = 2 时，应当返回: 2->1->4->3->5
/// 当 k = 3 时，应当返回: 3->2->1->4->5
/// 说明：
/// 你的算法只能使用常数的额外空间。
/// 你不能只是单纯的改变节点内部的值，而是需要实际进行节点交换。
/// 
/// 链表
/// 双指针
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null) return head;
        ListNode result = new ListNode(0);
        ListNode temp = result;
        ListNode p = head;   //每k个首位置值
        ListNode q = head;   //交换位置值

        while (p != null)
        {
            for (int i = 0; i < k; i++)
            {
                if (p == null)
                {
                    temp.next = q;
                    return result.next;
                }
                p = p.next;
            }
            //每k项进行交换
            //依次插入
            for (int i = 0; i < k; i++)
            {
                ListNode temp2 = temp.next;
                temp.next = q;
                q = q.next;
                temp.next.next = temp2;
            }
            for (int i = 0; i < k; i++)
            {
                temp = temp.next;
            }
            temp.next = q;
        }

        return result.next;
    }
}

/// <summary>
/// 题号：141. 环形链表
/// 题目：
/// 翻转一棵二叉树。
/// 给定一个链表，判断链表中是否有环。
/// 如果链表中有某个节点，可以通过连续跟踪 next 指针再次到达，则链表中存在环。 为了表示给定链表中的环，我们使用整数 pos 来表示链表尾连接到链表中的位置（索引从 0 开始）。 
/// 如果 pos 是 -1，则在该链表中没有环。注意：pos 不作为参数进行传递，仅仅是为了标识链表的实际情况。
/// 如果链表中存在环，则返回 true 。 否则，返回 false 。
/// 进阶：
/// 你能用 O(1)（即，常量）内存解决此问题吗？
/// 示例 1：
/// 输入：head = [3, 2, 0, -4], pos = 1
/// 输出：true
/// 解释：链表中有一个环，其尾部连接到第二个节点。
/// 示例 2：
/// 输入：head = [1, 2], pos = 0
/// 输出：true
/// 解释：链表中有一个环，其尾部连接到第一个节点。
/// 示例 3：
/// 输入：head = [1], pos = -1
/// 输出：false
/// 解释：链表中没有环。
/// 提示：
/// 链表中节点的数目范围是[0, 104]
/// - 105 <= Node.val <= 105
/// pos 为 -1 或者链表中的一个 有效索引 。
/// 
/// 快慢指针
/// 快指针每次走两步，慢指针每次走一步
/// 如果有环，肯定在某一时刻相遇
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution
{
    public bool HasCycle(ListNode head)
    {
        if (head == null || head.next == null) return false;
        ListNode slow = head;
        ListNode fast = head.next;
        while (slow != fast)
        {
            if (fast.next == null || fast.next.next == null) return false;
            slow = slow.next;
            fast = fast.next.next;
        }
        return true; //相遇了
    }
}

/// <summary>
/// 题号：142. 环形链表 II
/// 题目：
/// 给定一个链表，返回链表开始入环的第一个节点。 如果链表无环，则返回 null。
/// 为了表示给定链表中的环，我们使用整数 pos 来表示链表尾连接到链表中的位置（索引从 0 开始）。 如果 pos 是 -1，则在该链表中没有环。
/// 说明：不允许修改给定的链表。
/// 示例 1：
/// 输入：head = [3,2,0,-4], pos = 1
/// 输出：tail connects to node index 1
/// 解释：链表中有一个环，其尾部连接到第二个节点。
/// 示例 2：
/// 输入：head = [1,2], pos = 0
/// 输出：tail connects to node index 0
/// 解释：链表中有一个环，其尾部连接到第一个节点。
/// 示例 3：
/// 输入：head = [1], pos = -1
/// 输出：no cycle
/// 解释：链表中没有环。
/// 进阶：
/// 你是否可以不用额外空间解决此题？
/// 
/// 哈希表
/// 
/// 快慢指针：原理见leedcode
/// </summary>
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
//哈希表
public class Solution
{
    public ListNode DetectCycle(ListNode head)
    {
        HashSet<ListNode> map = new HashSet<ListNode>();
        while (head != null)
        {
            if (map.Contains(head))
            {
                return head;
            }
            map.Add(head);
            head = head.next;
        }
        return null;
    }
}

//快慢指针
public class Solution
{
    public ListNode DetectCycle(ListNode head)
    {
        if (head == null || head.next == null) return null;
        ListNode slow = head;
        ListNode fast = head.next;
        ListNode temp = head;  //辅助节点
        while (slow != fast)
        {
            if (fast.next == null || fast.next.next == null) return null;
            slow = slow.next;
            fast = fast.next.next;
        }
        //相遇了
        //相遇点的下一位和原首位在n圈后肯定相遇
        //当slow与fast同时从首位出发的时候就是这一位
        slow = slow.next;
        while (temp != slow)
        {
            temp = temp.next;
            slow = slow.next;
        }
        return temp;
    }
}