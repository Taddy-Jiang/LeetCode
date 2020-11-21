using System;
using System.Collections;
using System.Collections.Queue;

/// <summary>
/// 二叉树知识点：
/// 
/// 1.层序遍历
///      1
///     / \
///    3   2
///   /   / \
///  6   4   3
/// 
/// a.前序遍历：访问根节点，访问左子树，访问右子树
/// 1,3,6,2,4,3
/// 
/// 递归写法：
/// C++
/// void preorder(TreeNode * root)
/// {
///     if(root == nullptr)
///     {
///         return;
///      }
///      ans.push_back(root->val);
///      preorder(root->left);
///      preorder(root->right);
/// }
/// 
/// 栈写法：
/// 灰白标记法
/// 使用颜色标记节点的状态，新节点为白色，已访问的节点为灰色。
/// 如果遇到的节点为白色，则将其标记为灰色，然后将其右子节点、左子节点、自身依次入栈。（栈先入后出）
/// 如果遇到的节点为灰色，则将节点的值输出。
/// public class Solution
/// {
///     IList<int> result = new List<int>();
///     public IList<int> InorderTraversal(TreeNode root)
///     {
///         int white = 0;  //白色表示未访问
///         int gray = 1;   //灰色表示已访问
///         Stack<int> Colortemp = new Stack<int>();
///         Stack<TreeNode> Roottemp = new Stack<TreeNode>();
///         Colortemp.Push(white);
///         Roottemp.Push(root);
///         while (Colortemp.Count != 0 && Roottemp.Count != 0)
///         {
///             int color = Colortemp.Pop();
///             TreeNode node = Roottemp.Pop();
///             if (node == null) continue;
///             if (color == white)
///             {
///                 //前序遍历就是右左中的插入
///                 Colortemp.Push(white);
///                 Roottemp.Push(node.right);
///                 Colortemp.Push(white);
///                 Roottemp.Push(node.left);
///                 Colortemp.Push(gray);
///                 Roottemp.Push(node);
///             }
///             else
///             {
///                 result.Add(node.val);
///             }
///         }
///         return result;
///     }
/// }
/// 
/// 栈正常：弹出中，放右，左
class Solution
{
    public:
    vector<int> preorderTraversal(TreeNode* root)
    {
        stack<TreeNode*> st;
        vector<int> result;
        st.push(root);
        while (!st.empty())
        {
            TreeNode* node = st.top();                      // 中
            st.pop();
            if (node != NULL) result.push_back(node->val);
            else continue;
            st.push(node->right);                           // 右
            st.push(node->left);                            // 左
        }
        return result;
    }
};
///  
/// 普遍写法：
/// 
/// 
/// b.中序遍历：访问左子树，访问根节点，访问右子树
/// 6，3，1，4，2，3
/// 递归写法：
/// void inorder(TreeNode* root) {
///     if (root == nullptr)
///     {
///         return;
///     }
///     inorder(root->left);
///     ans.push_back(root->val);
///     inorder(root->right);
/// }
/// 
/// 栈写法：
/// 灰白标记法
/// 使用颜色标记节点的状态，新节点为白色，已访问的节点为灰色。
/// 如果遇到的节点为白色，则将其标记为灰色，然后将其右子节点、自身、左子节点依次入栈。（栈先入后出）
/// 如果遇到的节点为灰色，则将节点的值输出。
/// public class Solution
/// {
///     IList<int> result = new List<int>();
///     public IList<int> InorderTraversal(TreeNode root)
///     {
///         int white = 0;  //白色表示未访问
///         int gray = 1;   //灰色表示已访问
///         Stack<int> Colortemp = new Stack<int>();
///         Stack<TreeNode> Roottemp = new Stack<TreeNode>();
///         Colortemp.Push(white);
///         Roottemp.Push(root);
///         while (Colortemp.Count != 0 && Roottemp.Count != 0)
///         {
///             int color = Colortemp.Pop();
///             TreeNode node = Roottemp.Pop();
///             if (node == null) continue;
///             if (color == white)
///             {
///                 //中序遍历就是右中左的插入
///                 Colortemp.Push(white);
///                 Roottemp.Push(node.right);
///                 Colortemp.Push(gray);
///                 Roottemp.Push(node);
///                 Colortemp.Push(white);
///                 Roottemp.Push(node.left);
///             }
///             else
///             {
///                 result.Add(node.val);
///             }
///         }
///         return result;
///     }
/// }
/// 
/// 栈正常：一直左到底再弹出
class Solution
{
    public:
    vector<int> inorderTraversal(TreeNode* root)
    {
        vector<int> result;
        stack<TreeNode*> st;
        TreeNode* cur = root;
        while (cur != NULL || !st.empty())
        {
            if (cur != NULL)
            { // 指针来访问节点，访问到最底层
                st.push(cur); // 讲访问的节点放进栈
                cur = cur->left;                // 左
            }
            else
            {
                cur = st.top(); // 从栈里弹出的数据，就是要处理的数据（放进result数组里的数据）
                st.pop();
                result.push_back(cur->val);     // 中
                cur = cur->right;               // 右
            }
        }
        return result;
    }
};
/// 
/// c.后序遍历：访问左子树，访问右子树，访问根节点
/// 6,3,4,3,2,1
/// 递归写法：
/// void postorder(TreeNode* root) {
///     if (root == nullptr)
///     {
///         return;
///     }
///     postorder(root->left);
///     postorder(root->right);
///     ans.push_back(root->val);
/// }
/// 
/// 栈写法：
/// 灰白标记法
/// 使用颜色标记节点的状态，新节点为白色，已访问的节点为灰色。
/// 如果遇到的节点为白色，则将其标记为灰色，然后将其自身、右子节点、左子节点依次入栈。（栈先入后出）
/// 如果遇到的节点为灰色，则将节点的值输出。
/// public class Solution
/// {
///     IList<int> result = new List<int>();
///     public IList<int> InorderTraversal(TreeNode root)
///     {
///         int white = 0;  //白色表示未访问
///         int gray = 1;   //灰色表示已访问
///         Stack<int> Colortemp = new Stack<int>();
///         Stack<TreeNode> Roottemp = new Stack<TreeNode>();
///         Colortemp.Push(white);
///         Roottemp.Push(root);
///         while (Colortemp.Count != 0 && Roottemp.Count != 0)
///         {
///             int color = Colortemp.Pop();
///             TreeNode node = Roottemp.Pop();
///             if (node == null) continue;
///             if (color == white)
///             {
///                 //后序遍历就是中右左的插入
///                 Colortemp.Push(gray);
///                 Roottemp.Push(node);
///                 Colortemp.Push(white);
///                 Roottemp.Push(node.right);
///                 Colortemp.Push(white);
///                 Roottemp.Push(node.left);
///             }
///             else
///             {
///                 result.Add(node.val);
///             }
///         }
///         return result;
///     }
/// }
/// 
/// 栈正常：前序翻转：先弹出中，再右左，最后数组翻转
class Solution
{
    public:

    vector<int> postorderTraversal(TreeNode* root)
    {
        stack<TreeNode*> st;
        vector<int> result;
        st.push(root);
        while (!st.empty())
        {
            TreeNode* node = st.top();
            st.pop();
            if (node != NULL) result.push_back(node->val);
            else continue;
            st.push(node->left); // 相对于前序遍历，这更改一下入栈顺序
            st.push(node->right);
        }
        reverse(result.begin(), result.end()); // 将结果反转之后就是左右中的顺序了
        return result;
    }
};
/// 
/// 2.层序遍历特点：构建二叉树
/// 前序遍历 + 中序遍历：
/// 前序遍历从前往后是根节点，在中序遍历中找到对应的根节点，左边的就是左子树，右边的就是右子树
/// 因为为前序遍历，所以先构建左子树再构建右子树
/// 
/// 后序遍历 + 中序遍历
/// 后序遍历从后往前是根节点，在中序遍历中找到对应的根节点，左边的就是左子树，右边的就是右子树
/// 因为为后序遍历，所以先构建右子树再构建左子树
/// 
/// 注：使用哈希表
/// 
/// 3.二叉树一般与深度优先DFS/广度优先BFS结合使用
/// 
/// 4.二叉搜索树：左边节点小于根节点，右边节点大于根节点
/// 
/// 5.Morris解法
/// 
/// 原理通用：通过二叉树中指向null的指针节省空间
/// Java
public static void preOrderMorris(TreeNode head)
{
    if (head == null)
    {
        return;
    }
    TreeNode cur1 = head;//当前开始遍历的节点
    TreeNode cur2 = null;//记录当前结点的左子树
    while (cur1 != null)
    {
        cur2 = cur1.left;
        if (cur2 != null)
        {
            while (cur2.right != null && cur2.right != cur1)
            {//找到当前左子树的最右侧节点，且这个节点应该在指向根结点之前，否则整个节点又回到了根结点。
                cur2 = cur2.right;
            }
            if (cur2.right == null)
            {//这个时候如果最右侧这个节点的右指针没有指向根结点，创建连接然后往下一个左子树的根结点进行连接操作。
                cur2.right = cur1;
                cur1 = cur1.left;
                continue;
            }
            else
            {//当左子树的最右侧节点有指向根结点，此时说明我们已经回到了根结点并重复了之前的操作，同时在回到根结点的时候我们应该已经处理完 左子树的最右侧节点 了，把路断开。
                cur2.right = null;
            }
        }
        cur1 = cur1.right;//一直往右边走，参考图
    }
}
/// 前序遍历
public static void preOrderMorris(TreeNode head)
{
    if (head == null)
    {
        return;
    }
    TreeNode cur1 = head;
    TreeNode cur2 = null;
    while (cur1 != null)
    {
        cur2 = cur1.left;
        if (cur2 != null)
        {
            while (cur2.right != null && cur2.right != cur1)
            {
                cur2 = cur2.right;
            }
            if (cur2.right == null)
            {
                cur2.right = cur1;
                System.out.print(cur1.value + " ");
                cur1 = cur1.left;
                continue;
            }
            else
            {
                cur2.right = null;
            }
        }
        else
        {
            System.out.print(cur1.value + " ");
        }
        cur1 = cur1.right;
    }
}
/// 中序遍历
public static void inOrderMorris(TreeNode head)
{
    if (head == null)
    {
        return;
    }
    TreeNode cur1 = head;
    TreeNode cur2 = null;
    while (cur1 != null)
    {
        cur2 = cur1.left;
        //构建连接线
        if (cur2 != null)
        {
            while (cur2.right != null && cur2.right != cur1)
            {
                cur2 = cur2.right;
            }
            if (cur2.right == null)
            {
                cur2.right = cur1;
                cur1 = cur1.left;
                continue;
            }
            else
            {
                cur2.right = null;
            }
        }
        System.out.print(cur1.value + " ");
        cur1 = cur1.right;
    }
}
/// 后序遍历
//后序Morris
public static void postOrderMorris(TreeNode head)
{
    if (head == null)
    {
        return;
    }
    TreeNode cur1 = head;//遍历树的指针变量
    TreeNode cur2 = null;//当前子树的最右节点
    while (cur1 != null)
    {
        cur2 = cur1.left;
        if (cur2 != null)
        {
            while (cur2.right != null && cur2.right != cur1)
            {
                cur2 = cur2.right;
            }
            if (cur2.right == null)
            {
                cur2.right = cur1;
                cur1 = cur1.left;
                continue;
            }
            else
            {
                cur2.right = null;
                postMorrisPrint(cur1.left);
            }
        }
        cur1 = cur1.right;
    }
    postMorrisPrint(head);
}
//打印函数
public static void postMorrisPrint(TreeNode head)
{
    TreeNode reverseList = postMorrisReverseList(head);
    TreeNode cur = reverseList;
    while (cur != null)
    {
        System.out.print(cur.value + " ");
        cur = cur.right;
    }
    postMorrisReverseList(reverseList);
}
//翻转单链表
public static TreeNode postMorrisReverseList(TreeNode head)
{
    TreeNode cur = head;
    TreeNode pre = null;
    while (cur != null)
    {
        TreeNode next = cur.right;
        cur.right = pre;
        pre = cur;
        cur = next;
    }
    return pre;
}
/// </summary>


/// <summary>
/// 题号：106. 从中序与后序遍历序列构造二叉树
/// 题目：
/// 根据一棵树的中序遍历与后序遍历构造二叉树。
/// 注意:
/// 你可以假设树中没有重复的元素。
/// 例如，给出
/// 中序遍历 inorder = [9,3,15,20,7]
/// 后序遍历 postorder = [9, 15, 7, 20, 3]
/// 
/// 返回如下的二叉树：
///   3
///  / \
/// 9  20
///   /  \
///  15   7
/// 
/// 哈希表
/// 二叉树
/// 层序遍历
/// 
/// 后序遍历 + 中序遍历
/// 后序遍历从后往前是根节点，在中序遍历中找到对应的根节点，左边的就是左子树，右边的就是右子树
/// 因为为后序遍历，所以先构建右子树再构建左子树
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    //哈希表
    Hashtable idx_map = new Hashtable();
    int post_idx;

    public TreeNode BuildTree(int[] inorder, int[] postorder)
    {
        int idx = 0;
        for (int i = 0; i < inorder.Length; i++)
        {
            //先执行再自加
            idx_map.Add(inorder[i],idx++);
        }
        post_idx = postorder.Length - 1;
        return Build(inorder, postorder, 0, inorder.Length - 1);
    }

    /// <summary>
    /// 构建二叉树
    /// </summary>
    /// <param name="inorder">前序遍历</param>
    /// <param name="postorder">后序遍历</param>
    /// <param name="post_idx">后续遍历最后一位序号：即对应根节点</param>
    /// <param name="in_left">前序左右子树左起序号</param>
    /// <param name="in_right">前序左右子树右起序号</param>
    /// <returns></returns>
    private TreeNode Build(int[] inorder, int[] postorder, int in_left, int in_right)
    {
        if (in_left > in_right)
        {
            //已经没有子树了
            return null;
        }

        //取后序遍历最后一位根节点
        int root_val = postorder[post_idx];
        //找到中序遍历对应序号
        int in_idx = (int)idx_map[root_val];

        //建立对应节点
        TreeNode root = new TreeNode(root_val);

        post_idx--;

        //构建右子树
        root.right = Build(inorder, postorder, in_idx + 1, in_right);

        //构建左子树
        root.left = Build(inorder, postorder, in_left, in_idx - 1);
        return root;
    }
}

/// <summary>
/// 题号：112. 路径总和
/// 题目：
/// 给定一个二叉树和一个目标和，判断该树中是否存在根节点到叶子节点的路径，这条路径上所有节点值相加等于目标和。
/// 说明: 叶子节点是指没有子节点的节点。
/// 示例:给定如下二叉树，以及目标和 sum = 22，
///           5
///          / \
///         4   8
///        /   / \
///       11  13  4
///      /  \    / \
///     7    2  5   1
/// 返回:true:5,4,11,2
/// 
/// 二叉树
/// 深度优先搜索
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public bool HasPathSum(TreeNode root, int sum)
    {
        if (root == null) return false;
        return DFS(root, sum);
    }

    private bool DFS(TreeNode root, int sum)
    {
        if (root.left == null && root.right == null)
        {
            if (root.val == sum)
            {
                return true;
            }
            return false;
        }
        sum -= root.val;
        if (root.right == null)
        {
            return DFS(root.left, sum);
        }
        if (root.left == null)
        {
            return DFS(root.right, sum);
        }
        return DFS(root.left, sum) || DFS(root.right, sum);
    }
}

/// <summary>
/// 题号：113. 路径总和 II
/// 题目：
/// 给定一个二叉树和一个目标和，找到所有从根节点到叶子节点路径总和等于给定目标和的路径。
/// 说明: 叶子节点是指没有子节点的节点。
/// 示例:
/// 给定如下二叉树，以及目标和 sum = 22，
///           5
///          / \
///         4   8
///        /   / \
///       11  13  4
///      /  \    / \
///     7    2  5   1
/// 返回:
/// [   [5,4,11,2],   [5,8,4,5]]
/// 
/// 二叉树
/// 深度优先搜索
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    IList<IList<int>> result = new List<IList<int>>();

    public IList<IList<int>> PathSum(TreeNode root, int sum)
    {
        if (root == null) return result;
        IList<int> temp = new List<int>();
        DFS(root, sum, temp);
        return result;
    }

    private void DFS(TreeNode root, int sum, IList<int> temp)
    {
        //注意一般都用自身为null为终止条件
        if (root == null)
        {
            return;
        }

        temp.Add(root.val);
        sum -= root.val;

        if (root.left == null && root.right == null)
        {
            if (sum == 0)
            {
                result.Add(temp.ToList<int>());
            }
        }

        if (root.left != null)
        {
            DFS(root.left, sum, temp);
        }
        if (root.right != null)
        {
            DFS(root.right, sum, temp);
        }

        temp.RemoveAt(temp.Count - 1);
    }
}

/// <summary>
/// 题号：117. 填充每个节点的下一个右侧节点指针 II
/// 题目：
/// 给定一个二叉树
/// struct Node
/// {
/// int val;
/// Node* left;
/// Node* right;
/// Node* next;
/// }
/// 填充它的每个 next 指针，让这个指针指向其下一个右侧节点。如果找不到下一个右侧节点，则将 next 指针设置为 NULL。
/// 初始状态下，所有 next 指针都被设置为 NULL。
/// 进阶：
/// 你只能使用常量级额外空间。
/// 使用递归解题也符合要求，本题中递归程序占用的栈空间不算做额外的空间复杂度。
/// 示例：
/// 输入：root = [1,2,3,4,5,null,7]
/// 输出：[1,#,2,3,#,4,5,7,#]
/// 解释：给定二叉树如图 A 所示，你的函数应该填充它的每个 next 指针，以指向其下一个右侧节点，如图 B 所示。
/// 提示：
/// 树中的节点数小于 6000
/// -100 <= node.val <= 100
/// 
/// 二叉树
///层次遍历
///链表
/// </summary>
/*
// Definition for a Node.
public class Node {
    public int val;
    public Node left;
    public Node right;
    public Node next;

    public Node() {}

    public Node(int _val) {
        val = _val;
    }

    public Node(int _val, Node _left, Node _right, Node _next) {
        val = _val;
        left = _left;
        right = _right;
        next = _next;
    }
}
*/

public class Solution
{
    public Node Connect(Node root)
    {
        if (root == null) return null;

        Queue<Node> temp = new Queue<Node>();
        temp.Enqueue(root);
        while (temp.Count != 0)
        {
            int size = temp.Count;
            Node last = null;   //最后一位

            for (int i = 0; i < size; i++)
            {
                Node m = temp.Dequeue();
                if (m.left != null) temp.Enqueue(m.left);
                if (m.right != null) temp.Enqueue(m.right);
                //如果pre为空就表示node节点是这一行的第一个，
                //没有前一个节点指向他，否则就让前一个节点指向他
                if (last != null)
                {
                    last.next = m;
                }
                //然后再让当前节点成为前一个节点
                last = m;
            }
        }
        return root;
    }
}

/// <summary>
/// 题号：235. 二叉搜索树的最近公共祖先
/// 题目：
/// 给定一个二叉搜索树, 找到该树中两个指定节点的最近公共祖先。
/// 百度百科中最近公共祖先的定义为：“对于有根树 T 的两个结点 p、q，最近公共祖先表示为一个结点 x，满足 x 是 p、q 的祖先且 x 的深度尽可能大（一个节点也可以是它自己的祖先）。”
/// 例如，给定如下二叉搜索树:  root = [6, 2, 8, 0, 4, 7, 9, null, null, 3, 5]
/// 输入: root = [6,2,8,0,4,7,9,null,null,3,5], p = 2, q = 8
/// 输出: 6
/// 解释: 节点 2 和节点 8 的最近公共祖先是 6。
/// 
/// 最近公共祖先：两个节点在左右两侧或一个就是祖先
/// 二叉搜索性质
/// 二叉搜索树：当两个值都比根节点大，则肯定为右子树
/// 小的时候肯定为左子树，否则就为最近公共祖先
/// 
/// 二叉搜索树性质
/// 递归
/// </summary>
//解法一：递归
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root == p || root == q) return root;
        if (root.val > p.val && root.val > q.val) root = LowestCommonAncestor(root.left, p, q);
        if (root.val < p.val && root.val < q.val) root = LowestCommonAncestor(root.right, p, q);
        return root;
    }
}
//解法二：非递归
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        //如果值相减为正，则肯定在两侧
        while ((root.val - p.val) * (root.val - q.val) > 0)
        {
            root = root.val > p.val ? root.left : root.right;
        }
        //为负就在两侧即公共祖先
        return root;
    }
}

/// <summary>
/// 题号：236. 二叉树的最近公共祖先
/// 题目：
/// 给定一个二叉树, 找到该树中两个指定节点的最近公共祖先。
/// 百度百科中最近公共祖先的定义为：“对于有根树 T 的两个结点 p、q，最近公共祖先表示为一个结点 x，满足 x 是 p、q 的祖先且 x 的深度尽可能大（一个节点也可以是它自己的祖先）。”
/// 输入: root = [3,5,1,6,2,0,8,null,null,7,4], p = 5, q = 1
/// 输出: 3
/// 解释: 节点 5 和节点 1 的最近公共祖先是节点 3。
/// 
/// 递归
/// 后续遍历DFS
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root == p || root == q) return root;
        //向左右分别递归
        TreeNode left = LowestCommonAncestor(root.left, p, q);
        TreeNode right = LowestCommonAncestor(root.right, p, q);
        if (left == null) return right;  //当左边为空时，p,q肯定在右侧
        if (right == null) return left;  //当右边为空时，p,q肯定在左侧
        return root;  //if(left != null && right != null) root在中间时即为公共祖先
    }
}

/// <summary>
/// 题号：226. 翻转二叉树
/// 题目：
/// 翻转一棵二叉树。
/// 示例：
/// 输入：
/// 
///      4
///    /   \
///   2     7
///  / \   / \
/// 1   3 6   9
/// 输出：
///      4
///    /   \
///   7     2
///  / \   / \
/// 9   6 3   1
/// 
/// 递归
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    public TreeNode InvertTree(TreeNode root)
    {
        if (root == null) return root;
        TreeNode left = InvertTree(root.left);
        TreeNode right = InvertTree(root.right);
        root.left = right;
        root.right = left;
        return root;
    }
}

/// <summary>
/// 题号：530. 二叉搜索树的最小绝对差
/// 题目：
/// 给你一棵所有节点为非负值的二叉搜索树，请你计算树中任意两节点的差的绝对值的最小值。
/// 示例：
/// 输入：
///    1
///     \
///      3
///     /
///    2
/// 输出：1
/// 解释：
/// 最小绝对差为 1，其中 2 和 1 的差的绝对值为 1（或者 2 和 3）。
/// 
/// 中序遍历
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int x) { val = x; }
 * }
 */
public class Solution
{
    int result = int.MaxValue;
    List<int> temp = new List<int>();
    public int GetMinimumDifference(TreeNode root)
    {
        inorder(root);
        int n = temp.Count;
        for (int i = 0; i < n - 1; i++)
        {
            result = Math.Min(result, Math.Abs(temp[i] - temp[i + 1]));
        }
        return result;
    }

    private void inorder(TreeNode root)
    {
        if (root == null) return;
        inorder(root.left);
        temp.Add(root.val);
        inorder(root.right);
    }
}

/// <summary>
/// 题号：701. 二叉搜索树中的插入操作
/// 题目：
/// 给定二叉搜索树（BST）的根节点和要插入树中的值，将值插入二叉搜索树。 返回插入后二叉搜索树的根节点。 输入数据保证，新值和原始二叉搜索树中的任意节点值都不同。
/// 注意，可能存在多种有效的插入方式，只要树在插入后仍保持为二叉搜索树即可。 你可以返回任意有效的结果。
/// 
/// 递归
/// </summary>
/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution
{
    public TreeNode InsertIntoBST(TreeNode root, int val)
    {
        if (root == null)
        {
            return new TreeNode(val);
        }

        if (root.val < val)
        {
            root.right = InsertIntoBST(root.right, val);
        }
        else
        {
            root.left = InsertIntoBST(root.left, val);
        }
        return root;
    }
}