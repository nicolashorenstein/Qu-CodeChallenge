namespace Qu_CodeChallenge.CORE.Helpers;

public class Trie
{
    private readonly TrieNode root;

    public Trie()
    {
        root = new TrieNode();
    }

    public void Insert(string word)
    {
        var node = root;
        foreach (var ch in word)
        {
            if (!node.Children.ContainsKey(ch))
                node.Children[ch] = new TrieNode();

            node = node.Children[ch];
        }

        node.IsEndOfWord = true;
    }

    public TrieNode GetRoot()
    {
        return root;
    }
}