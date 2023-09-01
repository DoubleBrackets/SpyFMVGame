namespace DoubleOhPew.Interactions.Core
{
    public interface IInteractTrigger<TTriggerPose> : IInteractTrigger where TTriggerPose : IHandlesDrawable
    {
        public void UpdatePose(TTriggerPose pose);
    }

    public interface IInteractTrigger
    {
        public bool EvaluateInteraction(InteractionInfo interactionInfo);

        public void Initialize();

        public void Dispose();

        public void Enable();

        public void Disable();
    }
}