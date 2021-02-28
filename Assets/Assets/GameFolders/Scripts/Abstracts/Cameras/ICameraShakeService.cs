namespace BumperCarGamePrototype.Abstracts.Cameras
{
    public interface ICameraShakeService
    {
        void ShakeCamera();
        bool IsCameraShake { get; set; }
    }
}