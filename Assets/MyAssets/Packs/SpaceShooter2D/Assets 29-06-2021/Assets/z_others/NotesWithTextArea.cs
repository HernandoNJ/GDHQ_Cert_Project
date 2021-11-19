using UnityEngine;

namespace MyAssets.Packs.SpaceShooter2D.Assets_29_06_2021.Assets.z_others
{
public class NotesWithTextArea : MonoBehaviour
{
    [TextArea(5, 7)]
    [Tooltip("Just for writing notes")]
    public string notes = "Write your notes here";

}
}
