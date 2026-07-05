using UnityEngine;

public class Character
{
    private string Name;
    public enum Type { none, player, enemySphere, enemyCube, enemyCylinder }
    public enum Health { none, low, medium, high }
    public enum Speed { none, low, medium, high }
    public enum Attack { none, low, medium, high }
    private Health HealthStat;
    private Speed SpeedStat;
    private Attack AttackStat;
    private int ScorePoints;

    public Character(Health health, Speed speed, Attack attack)
    {
        this.HealthStat = health;
        this.SpeedStat = speed;
        this.AttackStat = attack;
    }

    public Character(Health health, Speed speed, Attack attack, int scorePoints)
    {
        this.HealthStat = health;
        this.SpeedStat = speed;
        this.AttackStat = attack;
        this.ScorePoints = scorePoints;
    }

    public Character(Type type)
    {
        switch (type)
        {
            case Type.player:
                this.Name = "Glocksses Guy";
                this.HealthStat = Health.high;
                this.SpeedStat = Speed.high;
                this.AttackStat = Attack.low;
                break;
            case Type.enemySphere:
                this.Name = "Sphere Enemy";
                this.HealthStat = Health.medium;
                this.SpeedStat = Speed.medium;
                this.AttackStat = Attack.medium;
                this.ScorePoints = 10;
                break;
            case Type.enemyCube:
                this.Name = "Cube Enemy";
                this.HealthStat = Health.high;
                this.SpeedStat = Speed.low;
                this.AttackStat = Attack.high;
                this.ScorePoints = 20;
                break;
            case Type.enemyCylinder:
                this.Name = "Cube Enemy";
                this.HealthStat = Health.low;
                this.SpeedStat = Speed.high;
                this.AttackStat = Attack.low;
                this.ScorePoints = 30;
                break;
        }
    }

    public void SetHealth(Health health)
    {
        this.HealthStat = health;
    }

    public void SetSpeed(Speed speed)
    {
        this.SpeedStat = speed;
    }

    public void SetAttack(Attack attack)
    {
        this.AttackStat = attack;
    }

    public (Health, Speed, Attack, int) GetStats()
    {
        return (HealthStat, SpeedStat, AttackStat, ScorePoints);
    }
}