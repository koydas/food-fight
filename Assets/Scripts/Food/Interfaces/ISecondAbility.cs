namespace Assets.Scripts.Food.Interfaces
{
    public interface ISecondAbility
    {
        bool SecondSkillDestroyObject { get; set; }
        bool SecondSkillRepeatable { get; set; }

        void UseSecondAbility();
    }
}
