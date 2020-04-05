
namespace Altruist {

    public interface IVisuals {

        void Use();
        void ReviveSurvivor();
        void RoomEnter();
        void Reveal();
        void Hide();
        void TriggerEffect();
        void DieSoft();
        void DieHard();
        void DieExtreme();
        void Wake();
        void Highlight(bool enabled);
        void Pickup();
        void Impact();
    }
}