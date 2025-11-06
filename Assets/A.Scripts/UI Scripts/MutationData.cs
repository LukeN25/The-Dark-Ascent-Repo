using UnityEngine;

[CreateAssetMenu(menuName = "Mutations/Mutation Data")]
public class MutationData : ScriptableObject
{
    public string mutationName;
    [TextArea] public string description;
    public Sprite icon;
}
