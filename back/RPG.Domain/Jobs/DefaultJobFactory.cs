namespace RPG.Domain.Jobs;

/// <summary>
/// Factory pour créer les jobs par défaut d'un personnage
/// </summary>
public static class DefaultJobFactory
{
    public const string Squire = "Ecuyer";
    public const string Knight = "Chevalier";
    public const string Alchemist = "Alchimiste";
    public const string WhiteMage = "Mage Blanc";
    public const string BlackMage = "Mage Noir";
    public const string Archer = "Archer";
    public const string Monk = "Moine";

    /// <summary>
    /// Crée la collection de jobs par défaut pour un nouveau personnage
    /// </summary>
    public static JobCollection CreateDefaultJobs()
    {
        // Ecuyer - Aucune condition (débloqué par défaut)
        var squire = Job.New(Squire, isUnlockedByDefault: true);

        // Chevalier - Ecuyer niveau 3
        var knight = Job.New(Knight, isUnlockedByDefault: false, 
            new UnlockConditionCollection(
                new UnlockCondition(Squire, 3)
            ));

        // Alchimiste - Aucune condition (débloqué par défaut)
        var alchemist = Job.New(Alchemist, isUnlockedByDefault: true);

        // Mage Blanc - Alchimiste niveau 3
        var whiteMage = Job.New(WhiteMage, isUnlockedByDefault: false,
            new UnlockConditionCollection(
                new UnlockCondition(Alchemist, 3)
            ));

        // Mage Noir - Alchimiste niveau 3
        var blackMage = Job.New(BlackMage, isUnlockedByDefault: false,
            new UnlockConditionCollection(
                new UnlockCondition(Alchemist, 3)
            ));

        // Archer - Ecuyer niveau 3 ET Alchimiste niveau 2
        var archer = Job.New(Archer, isUnlockedByDefault: false,
            new UnlockConditionCollection(
                new UnlockCondition(Squire, 3),
                new UnlockCondition(Alchemist, 2)
            ));

        // Moine - Chevalier niveau 3
        var monk = Job.New(Monk, isUnlockedByDefault: false,
            new UnlockConditionCollection(
                new UnlockCondition(Knight, 3)
            ));

        return new JobCollection(squire, knight, alchemist, whiteMage, blackMage, archer, monk);
    }
}
