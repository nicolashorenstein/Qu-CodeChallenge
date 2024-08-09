namespace Qu_CodeChallenge.CORE.Helpers;

public class TrieNode
{
    public Dictionary<char, TrieNode> Children = new();
    public bool IsEndOfWord = false;
}