using System;

public class TrashCounter : BaseCounter {

    public static event EventHandler OnAnyObjectTrashed;

    public override void Interact(Player player) {
        if (player.HasChickenTool()) {
            player.GetChickenTool().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }

    new public static void ResetStaticData() {
        OnAnyObjectTrashed = null;
    }
}