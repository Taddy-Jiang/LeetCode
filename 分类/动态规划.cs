/// <summary>
/// 题号：LCP 19. 秋叶收藏集
/// 题目：
/// 小扣出去秋游，途中收集了一些红叶和黄叶，他利用这些叶子初步整理了一份秋叶收藏集 leaves， 字符串 leaves 仅包含小写字符 r 和 y， 其中字符 r 表示一片红叶，字符 y 表示一片黄叶。
/// 出于美观整齐的考虑，小扣想要将收藏集中树叶的排列调整成「红、黄、红」三部分。每部分树叶数量可以不相等，但均需大于等于 1。每次调整操作，小扣可以将一片红叶替换成黄叶或者将一片黄叶替换成红叶。
/// 请问小扣最少需要多少次调整操作才能将秋叶收藏集调整完毕。
/// 示例 1：
/// 输入：leaves = "rrryyyrryyyrr"
/// 输出：2
/// 解释：调整两次，将中间的两片红叶替换成黄叶，得到 "rrryyyyyyyyrr"
/// 示例 2：
/// 输入：leaves = "ryr"
/// 输出：0
/// 解释：已符合要求，不需要额外操作
/// 提示：
/// 3 <= leaves.length <= 10^5
/// leaves 中只包含字符 'r' 和字符 'y'
/// 
/// 动态规划
/// 状态1：红；状态2：黄；状态3：红
/// dp[i][j]:0-i位置的叶子且第i位置叶子为状态j
/// dp[0][0] = isRed(0);
/// dp[i][0] = dp[i-1][0] + isRed(i) 前面为状态1的红时，i位置如果是状态0，前面一定为状态0
/// dp[i][1] = min{dp[i-1][0],dp[i-1][1]} + isYe(i) i位置为状态1 ，前面可以为状态0或1
/// dp[i][2] = min{dp[i-1][1],dp[i-1][2]} + idRed(i) i位置为状态2时，前面可以为状态1或2，不能为0，因为每种状态至少为1
/// </summary>
public class Solution
{
    public int MinimumOperations(string leaves)
    {
        int n = leaves.Length;
        int[,] dp = new int[n, 3];
        dp[0, 0] = isRed(leaves[0]);  //第一个位置一定为红
        dp[0, 1] = dp[0, 2] = dp[1, 2] = Int32.MaxValue;  //dp[1,2]肯定为正无穷，因为某种状态都至少为1
        dp[1, 0] = dp[0, 0] + isRed(leaves[1]);
        dp[1, 1] = Math.Min(dp[0, 0], dp[0, 1]) + isYe(leaves[1]);
        for (int i = 2; i < n; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + isRed(leaves[i]);
            dp[i, 1] = Math.Min(dp[i - 1, 0], dp[i - 1, 1]) + isYe(leaves[i]);
            dp[i, 2] = Math.Min(dp[i - 1, 1], dp[i - 1, 2]) + isRed(leaves[i]);
        }
        return dp[n - 1, 2];
    }

    private int isRed(char c)
    {
        return c == 'r' ? 0 : 1;
    }

    private int isYe(char c)
    {
        return c == 'y' ? 0 : 1;
    }
}

/// <summary>
/// 题号：62. 不同路径
/// 题目：
/// 一个机器人位于一个 m x n 网格的左上角 （起始点在下图中标记为“Start” ）。
/// 机器人每次只能向下或者向右移动一步。机器人试图达到网格的右下角（在下图中标记为“Finish”）。
/// 问总共有多少条不同的路径？
/// 例如，上图是一个7 x 3 的网格。有多少可能的路径？
/// 示例 1:
/// 输入: m = 3, n = 2
/// 输出: 3
/// 解释:
/// 从左上角开始，总共有 3 条路径可以到达右下角。
/// 1.向右->向右->向下
/// 2.向右->向下->向右
/// 3.向下->向右->向右
/// 示例 2:
/// 输入: m = 7, n = 3
/// 输出: 28
/// 
/// 动态规划
/// 求最优解时用动规
/// 求所有解时用DFS
/// </summary>
public class Solution
{
    public int UniquePaths(int m, int n)
    {
        if (m == 0 || n == 0) return 0;
        int[,] dp = new int[m, n];
        //初始化
        dp[0, 0] = 1;
        //每行，每列都只能有一种可能性
        for (int i = 1; i < m; i++)
        {
            dp[i, 0] = dp[i - 1, 0];
        }
        for (int i = 1; i < n; i++)
        {
            dp[0, i] = dp[0, i - 1];
        }
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = dp[i - 1, j] + dp[i, j - 1];
            }
        }
        return dp[m - 1, n - 1];
    }
}

/// <summary>
/// 题号：64. 最小路径和
/// 题目：
/// 给定一个包含非负整数的 m x n 网格，请找出一条从左上角到右下角的路径，使得路径上的数字总和为最小。
/// 说明：每次只能向下或者向右移动一步。
/// 示例:
/// 输入:
/// [
/// [1,3,1],
/// [1,5,1],
/// [4,2,1]
/// ]
/// 输出: 7
/// 解释: 因为路径 1→3→1→1→1 的总和最小。
/// 
/// 动态规划
/// 求最优解时用动规
/// 求所有解时用DFS
/// </summary>
public class Solution
{
    public int MinPathSum(int[][] grid)
    {
        int m = grid.Length;
        if (m == 0) return 0;
        int n = grid[0].Length;

        int[,] dp = new int[m, n];

        //初始条件
        dp[0, 0] = grid[0][0];
        //在第0行与第0列的数只能从左或从上加
        for (int i = 1; i < m; i++)
        {
            dp[i, 0] = dp[i - 1, 0] + grid[i][0];
        }
        for (int i = 1; i < n; i++)
        {
            dp[0, i] = dp[0, i - 1] + grid[0][i];
        }
        for (int i = 1; i < m; i++)
        {
            for (int j = 1; j < n; j++)
            {
                dp[i, j] = Math.Min(dp[i - 1, j], dp[i, j - 1]) + grid[i][j];
            }
        }
        return dp[m - 1, n - 1];
    }
}