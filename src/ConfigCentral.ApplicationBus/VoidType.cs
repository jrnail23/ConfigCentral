namespace ConfigCentral.ApplicationBus
{
    public sealed class VoidType
    {
        public static readonly VoidType Default = new VoidType();

        private VoidType() {}
    }
}